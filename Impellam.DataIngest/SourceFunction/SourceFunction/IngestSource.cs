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
          
          if (!string.IsNullOrEmpty(data.PayRateInput))
          {
            var parseVal = data.PayRateInput.Substring(1);
            double payrate;
            if (double.TryParse(parseVal, out payrate))
            {
              if (payrate <= 0)
              {
                log.Error($"PayRate must be greater than 0: {data}");
                return req.CreateErrorResponse(HttpStatusCode.BadRequest, "PercentagePay must be between 0 and 100");
              }
            }
          }
          else
          {
            log.Error($"PayRate must be a valid rate (e.g £2.20 or $100.30) actual value:  {data.PayRateInput}");
            return req.CreateErrorResponse(HttpStatusCode.BadRequest, "PercentagePay must be between 0 and 100");
          }

          if (data.ShiftDate >= DateTime.Today)
          {
            log.Error($"ShiftDate must be in the past: data received {data}");
            return req.CreateErrorResponse(HttpStatusCode.BadRequest, $"ShiftDate must be in the past :{data.ShiftDate.ToLongDateString()}");
          }
          if (string.IsNullOrEmpty(data.ShiftPlacementAssignmentRef))
          {
            log.Error($"SourceName cannot empty: data received {data}");
            return req.CreateErrorResponse(HttpStatusCode.BadRequest, "SourceName cannot be empty");
          }
          return req.CreateResponse(HttpStatusCode.OK,
            $"Data for {data.ShiftPlacementAssignmentRef} {data.ShiftDate} and Ref {data.WorkerName} passed initial validation");
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