using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace Impellam.DataIngest.Models
{
  public class ExportFormat : SourceData
  {

    [CsvOrder(0)]
    public override string ShiftPlacementAssignmentRef { get; set; }
    [CsvOrder(1)]
    public string WorkerRef { get; set; }
    [CsvOrder(2)]
    public string PayCode { get; set; }

    [CsvOrder(3)]
    public int PayRunId { get; set; }

    [CsvOrder(4)]
    public string Blank01 { get; set; }

    [CsvOrder(5)]
    public string Ignore02 { get; set; } = @"<IGNORE>";
    [CsvOrder(6)]
    public string Ignore03 { get; set; } = @"<IGNORE>";
    [CsvOrder(7)]
    public string Ignore04 { get; set; } = @"<IGNORE>";
    [CsvOrder(8)]
    public DateTime? PayDate { get; set; } 
    [CsvOrder(9)]
    public string OrganisationId { get; set; }
    [CsvOrder(10)]
    public string SupplierPayRef { get; set; }
    [CsvOrder(11)]
    public string PayTypeCode { get; set; }
    [CsvOrder(12)]
    public string Ignore05 { get; set; } = @"<IGNORE>";
    [CsvOrder(13)]
    public override double Hours { get; set; }
    [CsvOrder(14)]
    public double PayRate { get; set; }
    [CsvOrder(15)]
    public double ChargeRate { get; set; }
    [CsvOrder(16)]
    public string Ignore06 { get; set; } = @"<IGNORE>";
    [CsvOrder(17)]
    public string Ignore07 { get; set; } = @"<IGNORE>";
    [CsvOrder(18)]
    public string Ignore08 { get; set; } = @"<IGNORE>";
    [CsvOrder(19)]
    public string Ignore09 { get; set; } = @"<IGNORE>";
    [CsvOrder(20)]
    public string Ignore10 { get; set; } = @"<IGNORE>";
    [CsvOrder(21)]
    public string OrganisationName { get; set; }
    [CsvOrder(22)]
    public string JobName { get; set; }
    [CsvOrder(23)]
    public string PayeeName { get; set; }
    [CsvOrder(24)]
    public string Ignore11 { get; set; } = @"<IGNORE>";
    [CsvOrder(25)]
    public string PayeeCode { get; set; }
    [CsvOrder(26)]
    public string Ignore12 { get; set; } = @"<IGNORE>";
    [CsvOrder(27)]
    public string Ignore13 { get; set; } = @"<IGNORE>";
    [CsvOrder(28)]
    public string SupplierInvoiceRef { get; set; }
    [CsvOrder(29)]
    public string Ignore14 { get; set; } = @"<IGNORE>";
    [CsvOrder(30)]
    public string Ignore15 { get; set; } = @"<IGNORE>";
    [CsvOrder(31)]
    public string Ignore16 { get; set; } = @"<IGNORE>";
    [CsvOrder(32)]
    public string Ignore17 { get; set; } = @"<IGNORE>";
    [CsvOrder(33)]
    public string Ignore18 { get; set; } = @"<IGNORE>";
    [CsvOrder(34)]
    public string ToBePaid { get; set; } = @"<IGNORE>";
    [CsvOrder(35)]
    public int? AgentId { get; set; }
    [CsvOrder(36)]
    public double PercentagePay { get; set; }
    [CsvOrder(37)]
    public string Ignore19 { get; set; } = @"<IGNORE>";
    [CsvOrder(38)]
    public string Ignore20 { get; set; }
    [CsvOrder(39)]
    public string Ignore21 { get; set; } = @"<IGNORE>";
    [CsvOrder(40)]
    public string Ignore22 { get; set; } = @"<IGNORE>";
    [CsvOrder(41)]
    public string Blank02 { get; set; }
    [CsvOrder(42)]
    public string Ignore24 { get; set; } = @"<IGNORE>";
    [CsvOrder(43)]
    public string Ignore25 { get; set; } = @"<IGNORE>";
    [CsvOrder(44)]
    public string PayRunCode { get; set; }
    [CsvOrder(45)]
    public string Blank03 { get; set; }
    [CsvOrder(46)]
    public string PayRunId02 { get; set; }
    [CsvOrder(47)]
    public string Branch { get; set; }
    [CsvOrder(48)]
    public string Blank04 { get; set; }
    [CsvOrder(49)]
    public string Ignore26 { get; set; } = @"<IGNORE>";
    [CsvOrder(50)]
    public string Blank05 { get; set; }
    [CsvOrder(51)]
    public string Blank06 { get; set; }
    [CsvOrder(52)]
    public string BatchCode { get; set; }
    [CsvOrder(53)]
    public string AlphaCode { get; set; }

    [CsvOrder(54)]
    public int CodeField01 { get; set; } = 0;

    [CsvOrder(55)]
    public int CodeField02 { get; set; } = 0;

    [CsvOrder(56)]
    public int CodeField03 { get; set; } = 0;

    [CsvOrder(57)]
    public int CodeField04 { get; set; } = 0;

    [CsvOrder(58)]
    public int CodeField05 { get; set; } = 0;

    [CsvOrder(59)]
    public int CodeField06 { get; set; } = 0;
    [CsvOrder(60)]
    public string ActivityCode { get; set; }
    [CsvOrder(61)]
    public string Ignore27 { get; set; }
    [CsvOrder(62)]
    public string Ignore28 { get; set; }
    [CsvOrder(64)]
    public override DateTime WeekEndingDate { get; set; }
    [CsvOrder(63)]
    public override DateTime ShiftDate { get; set; }
    [CsvOrder(65)]
    public int Priority { get; set; }
    [CsvOrder(66)]
    public bool BitField01 { get; set; }
    [CsvOrder(67)]
    public bool BitField02 { get; set; }
    [CsvOrder(68)]
    public bool BitField03 { get; set; }
    [CsvOrder(69)]
    public bool BitField04 { get; set; }
    [CsvOrder(70)]
    public bool BitField05 { get; set; }
    [CsvOrder(71)]
    public bool BitField06 { get; set; }

    [CsvOrder(72)]
    public bool BitField07 { get; set; } 
    [CsvOrder(73)]
    public string DayOfWeekPaid { get; set; }
    [CsvOrder(74)]
    public string PayRefCode { get; set; }

    [CsvOrder(75)]
    public DateTime AzureTimestamp { get; } = DateTime.UtcNow;

    [CsvOrder(76)]
    public override string TimesheeetTempestNo { get; set; }

    public static object GetPropertyValue(object src, string propName, TraceWriter log)
    {
      var val = src.GetType().GetProperty(propName)?.GetValue(src, null);
      log.Info($"{propName}={val} - {src}");
      return val;
    }

    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }
    public static ExportFormat FromSourceFormat(SourceData input)
    {
      var export = new ExportFormat
      {
        AdhocRate = input.AdhocRate,
        PONumber = input.PONumber,
        PayRateInput = input.PayRateInput,
        PayrollPresentedRateCode = input.PayrollPresentedRateCode,
        ShiftDate = input.ShiftDate,
        ShiftSiteClientInvoiceName = input.ShiftSiteClientInvoiceName,
        ShiftPlacementAssignmentRef = input.ShiftPlacementAssignmentRef,
        Hours = input.Hours,
        WeekEndingDate = input.WeekEndingDate,
        TimesheeetTempestNo = input.TimesheeetTempestNo,
        TimesheetSiteClientId = input.TimesheetSiteClientId,
        WorkerName = input.WorkerName
      };
      ConvertPayRate(input, export);
      //TODO: Do any processing here that you want Azure to populate the output file with - Things like AcvitiyCode are not received on the input
      return export;
    }


    private static void ConvertPayRate(SourceData input, ExportFormat export)
    {
      var parseVal = input.PayRateInput.Substring(1);
      double payrate;
      if (double.TryParse(parseVal, out payrate))
      {
        export.PayRate = payrate;
      }
    }
  }
}
