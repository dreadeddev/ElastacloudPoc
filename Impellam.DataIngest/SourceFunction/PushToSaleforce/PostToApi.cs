using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Azure.WebJobs.Host;

namespace Impellam.DataIngest.Functions.PushToSaleforce
{
  public class PostToApi
  {
    public static void Run(string inputstream, TraceWriter log)
    {
      //TODO: Create a Http post to whichever URL
      log.Info($"Post to Api Function recieved blob data : {inputstream}");
    }
  }
}