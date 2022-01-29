﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Quandl.NET.Model.Response;
using YahooFinanceApi;

namespace ScaffelPikeContracts
{
  [ServiceContract(Namespace = "http://ScaeffelPike.Service")]
  public interface IScaffelPikeService
  {
    [OperationContract]
    Task<LogInResponse> LogIn(LogInRequest request);

    [OperationContract]
    Task<HeartbeatDto> Heartbeat(HeartbeatDto heartbeat);

    [OperationContract]
    Task<List<QuandlDatabaseResponse>> GetQuandlDbs();

    [OperationContract]
    Task<List<QuandlDatasetResponse>> GetQuandlDataSets(string dbCode);

    [OperationContract]
    Task<QuandlTimeseriesDataResponse> GetQuandlTimeseries(string dbCode , string dsCode);
    [OperationContract]
    Task<YahooSecurityResponse> GetYahoo(params string[] tickers);
  }
}
