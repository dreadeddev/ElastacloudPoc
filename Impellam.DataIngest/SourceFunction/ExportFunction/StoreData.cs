using System;
using System.Globalization;
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
    private static TraceWriter Log;
    public static async Task Run(string jsData, IBinder binder, TraceWriter log)
    {
      Log = log;
      var sourceArray = JsonConvert.DeserializeObject<SourceData[]>(jsData);
      log.Info($"StoreData ServiceBus queue trigger function processed message - records: {jsData}");
      var x = DateTime.Now;
      ContainerName = $"{x.Year}_{x.Month:00}{x.Day:00}";
      BlobName = $"{x.ToString(CultureInfo.CurrentCulture).Replace("/", "-")}.csv";
      var csv = "";
      csv = sourceArray.Select(ExportFormat.FromSourceFormat)
        .Select(CreateCsvRow)
        .Aggregate(csv, (current, s) => current + s);

      using (var writer =
        await binder.BindAsync<TextWriter>(new BlobAttribute($"impellamexport/{ContainerName}/{BlobName}")))
      {
        writer.Write(csv);
      }
    }

    private static string CreateCsvRow(ExportFormat datarow)
    {
      var type = datarow.GetType();
      var props = type.GetProperties();
      var csvData = new object[props.Length];
      var csvRow = "";
      foreach (var p in props)
      {
        var x = (CsvOrderAttribute[])p.GetCustomAttributes(typeof(CsvOrderAttribute), false);
        if (x.Length > 0)
        {
          var order = x[0].Order;

          //Log.Info($"CSV Order for Property {p.Name} = {order:000} ");
          if (order > csvData.Length - 1)
          {
            throw new IndexOutOfRangeException($"Order specified by attribute is higher than array length: Attribute value={order}");
          }
          csvData[order] = ExportFormat.GetPropertyValue(datarow, p.Name, Log);
        }
        else
        {
          //This is logged as a warning so that the processor is aware that these properties are not going to be on the output
          //Not necessarily an actual error as it is a way of excluding input properties that we don't want on the output
          Log.Warning($"CsvOrderAttribute is missing from property: {p.Name}");
        }
      }

      csvRow = csvData.Aggregate(csvRow, (current, propValue) => current + $"{propValue},");
      
      return csvRow.Substring(0,csvRow.Length-1) + Environment.NewLine;
    }
  }
}