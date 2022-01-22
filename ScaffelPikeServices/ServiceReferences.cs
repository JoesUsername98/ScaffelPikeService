﻿using System;
using ScaffelPikeContracts;
using ScaffelPikeDataAccess.Data;
using ScaffelPikeLogger;

namespace ScaffelPikeServices
{

  //Consider Container Injection in the future
  //https://stackoverflow.com/questions/2042609/injecting-data-to-a-wcf-service/2042858#2042858
  /// <summary>
  /// Used to hold DI objects in the future
  /// </summary>
  public static class ServiceReferences
  {
    public static ILogger Logger { get; private set; }
    public static IUserData UserDA { get; private set; }
    public static string Env { get; private set; }
    public static Guid ServerGuid { get; private set; }
    public static DateTime ServerStarTime { get; private set; }

    public static ApiKeys ApiKeys { get; private set; } 

    internal static void Configure(ILogger logger, IUserData userDA, string env, ApiKeys apiKeys)
    {
      Logger = logger;
      UserDA = userDA;
      Env = env;
      ServerGuid = Guid.NewGuid();
      ServerStarTime = DateTime.Now;
      ApiKeys = apiKeys;
    }
  }
}
