﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using ScaffelPikeContracts;

namespace ScaffelPikeServices
{
  public static class HeartbeatManagerServerSide
  {
    private static readonly TimeSpan GraceInterval;
    private static Task CleanUpTask;
    public static Dictionary<Guid, HeartbeatDto> Connections { get; private set; }

    static HeartbeatManagerServerSide()
    {
      Connections = new Dictionary<Guid, HeartbeatDto>();
      GraceInterval = new TimeSpan(0, 0, int.Parse(ConfigurationManager.AppSettings["HeartbeatGraceInterval"]));
      InitializeTimer();
    }

    private static void InitializeTimer()
    {
      CleanUpTask = new Task(() => CleanUpConnections());
    }

    private static void CleanUpConnections()
    {
      while (!CleanUpTask.IsCanceled)
      {
        CleanUpTask.Wait(GraceInterval);
        foreach (var connection in Connections.Values.ToList())
          if (DateTime.Now - connection.SentAt > GraceInterval)
          {
            ServiceReferences.Logger.Warning("HeartbeatManagerClientSide",
              $"Server has not replied since {connection.SentAt}");
            Connections.Remove(connection.Guid);
          }
      }
    }
    public static HeartbeatDto Echo(HeartbeatDto incomingHeartbeat)
    {
      switch (incomingHeartbeat.HeartbeatType)
      {
        case HeartbeatType.Start:
          if (!Connections.ContainsKey(incomingHeartbeat.Guid))
          {
            ServiceReferences.Logger.Information("HeartbeatManagerServerSide", $"Initial Connection With {incomingHeartbeat.Guid}");
            Connections.Add(incomingHeartbeat.Guid, incomingHeartbeat);
          }
          else
          {
            ServiceReferences.Logger.Warning("HeartbeatManagerServerSide", $"Existing Connection Sent Start Heartbeat - Updating {incomingHeartbeat.Guid}");
            Connections[incomingHeartbeat.Guid] = incomingHeartbeat;
          }
          break;

        case HeartbeatType.Continue:
          if (!Connections.ContainsKey(incomingHeartbeat.Guid))
          {
            ServiceReferences.Logger.Warning("HeartbeatManagerServerSide", $"Unidentified Connection Sent Continue Heartbeat - Adding {incomingHeartbeat.Guid}");
            Connections.Add(incomingHeartbeat.Guid, incomingHeartbeat);
          }
          else
          {
            var responseInterval = DateTime.Now - Connections[incomingHeartbeat.Guid].SentAt;
            ServiceReferences.Logger.Information("HeartbeatManagerServerSide", $"Connection With {incomingHeartbeat.Guid} took {responseInterval.TotalMilliseconds}s");
            Connections[incomingHeartbeat.Guid] = incomingHeartbeat;
          }
          break;

        case HeartbeatType.Stop:
          if (!Connections.ContainsKey(incomingHeartbeat.Guid))
          {
            ServiceReferences.Logger.Warning("HeartbeatManagerServerSide", $"Unidentified Connection Sent Stop Heartbeat {incomingHeartbeat.Guid}");
          }
          else
          {
            ServiceReferences.Logger.Information("HeartbeatManagerServerSide", $"Final Connection With {incomingHeartbeat.Guid}");
            Connections.Remove(incomingHeartbeat.Guid);
          }
          break;

        case HeartbeatType.Echo:
          ServiceReferences.Logger.Error("HeartbeatManagerServerSide", $"Recieved Erroneous Echo From {incomingHeartbeat.Guid}");
          break;
      }

      return new HeartbeatDto() {
        Guid = ServiceReferences.ServerGuid,
        SentAt = DateTime.Now,
        Interval = incomingHeartbeat.Interval,
        HeartbeatType = HeartbeatType.Echo
      };
    }

    public static void PollHeartbeat()
    {
      foreach(var client in Connections)
      {
        var responseExpectedAt = client.Value.SentAt + client.Value.Interval + GraceInterval;
        if (responseExpectedAt > DateTime.Now)
        {
          ServiceReferences.Logger.Warning("HeartbeatManagerServerSide", $"Expected Echo From {client.Key} at {responseExpectedAt}");
        }
      }
    }
  }
}