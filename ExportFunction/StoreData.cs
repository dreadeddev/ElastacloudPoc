using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Impellam.DataIngest.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace Impellam.DataIngest.Functions.ExportFunction
{
  public class StoreData
  {
    public static string ContainerName;
    public static string BlobName;

    public static async Task Run(string jsData, IBinder binder, TraceWriter log)
    {

      var sourceArray = JsonConvert.DeserializeObject<SourceData[]>(jsData);
      log.Info($"StoreData ServiceBus queue trigger function processed message - No: records: {jsData.Length}");
      var x = DateTime.Now;
      ContainerName = $"{x.Year}/{x.Month}/{x.Day}/{x.Hour}";
      BlobName = $"{x}.csv";
      var csv = $"Id, DatePaid,ExportDate,Paid,HoursWorked,DateWorked,PayDate,SourceName {Environment.NewLine}";
      csv = sourceArray.Select(ExportFormat.FromSourceFormat)
        .Select(
          datarow =>
            $"{datarow.Id},{datarow.DatePaid},{datarow.ExportDate},{datarow.Paid},{datarow.HoursWorked},{datarow.DateWorked},{datarow.PayDate},{datarow.SourceName} {Environment.NewLine}")
        .Aggregate(csv, (current, row) => current + row);

      using (var writer =
        await binder.BindAsync<TextWriter>(new BlobAttribute($"ImpellamExport/{ContainerName}/{BlobName}")))
      {
        writer.Write(csv);
      }
    }
  }
}