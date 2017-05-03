using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Impellam.DataIngest.Models;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace Impellam.DataIngest.Functions.SourceFunction
{
  public class IngestSource
  {
    public static HttpResponseMessage Run(HttpRequestMessage req, out string outputSbQueue, TraceWriter log)
    {
      // Get request body - This is how the data SHOULD be passed in
      dynamic data = req.Content.ReadAsAsync<object>().Result;
      log.Info($"IngestSource HTTP trigger function processed a request:{data}");

      HttpResponseMessage z = ValidateRequest(req, data, out outputSbQueue, log);
      return z;
    }

    private static HttpResponseMessage ValidateRequest(HttpRequestMessage req, dynamic reqBody,out string outputSbQueue, TraceWriter log)
    {
      SourceData[] dataArray = null;
      try
      {
        dataArray = JsonConvert.DeserializeObject<SourceData[]>(reqBody.ToString());
        if (dataArray == null || dataArray.Length == 0 )
        {
          log.Error($"Data cannot be parsed into correct format: data received {reqBody}");
          return req.CreateErrorResponse(HttpStatusCode.BadRequest,
            $"Data cannot be parsed into correct format: data received {reqBody}");
        }
        foreach (var data in dataArray)
        {
          if (data.DateWorked > DateTime.Now)
          {
            log.Error($"DateWorked cannot be in the future: data received {data}");
            return req.CreateErrorResponse(HttpStatusCode.BadRequest, "DateWorked cannot be in the future.");
          }
          if (data.PayDate < DateTime.Today)
          {
            log.Error($"PayDate cannot be in the past: data received {data}");
            return req.CreateErrorResponse(HttpStatusCode.BadRequest, "PayDate cannot be in the past.");
          }
          if (string.IsNullOrEmpty(data.SourceName))
          {
            log.Error($"SourceName cannot empty: data received {data}");
            return req.CreateErrorResponse(HttpStatusCode.BadRequest, "SourceName cannot be empty");
          }
          return req.CreateResponse(HttpStatusCode.OK,
            $"Data for {data.SourceName} and Date {data.DateWorked} passed initial validation");
        }
      }
      catch (Exception ex)
      {
        log.Error("ValidateRequest caught error: ", ex);
      }
      finally
      {
        outputSbQueue = JsonConvert.SerializeObject(dataArray);
        log.Info($"Output to Servicebus: {outputSbQueue}");
      }
      return null;
    }
  }
}