using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Results;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace Impellam.DataIngest.Functions.ImportFunction
{
  public class IngestToSalesforce
  {
    public static HttpResponseMessage Run(Stream blob, string blobName, out string outputSbQueue, TraceWriter log)
    {
      // Get request body - This is how the data SHOULD be passed in
      //if (req.Content != null)
      //{
      //  var data = req.Content.ReadAsAsync<string>().Result;
      //  log.Info($"IngestToSalesforce HTTP trigger function processed a request:{data}");

      //  return ValidateRequest(req, data, out outputSbQueue, log);
      //}
      if (blob != null)
      {
        string data;
        using (var read = new StreamReader(blob))
        {
          data = read.ReadToEnd();
        }
        return ValidateRequest(data, out outputSbQueue, log);
      }
      outputSbQueue = null;
      return new HttpResponseMessage(HttpStatusCode.BadRequest);
    }

    private static HttpResponseMessage ValidateRequest(string data, out string outputSbQueue, TraceWriter log)
    {
      if (data == null)
      {
        outputSbQueue = null;
        return new HttpResponseMessage(HttpStatusCode.BadRequest);
      }
      outputSbQueue = JsonConvert.SerializeObject(data);
      log.Info(outputSbQueue);
      return new HttpResponseMessage(HttpStatusCode.NoContent);

    }
  }
}