using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Impellam.DataIngest.Models
{
  public class SourceData
  {
    public virtual string ShiftPlacementAssignmentRef { get; set; }
    public virtual string TimesheeetTempestNo { get; set; }
    public virtual DateTime WeekEndingDate { get; set; }
    public virtual DateTime ShiftDate { get; set; }
    public virtual string ShiftSiteClientInvoiceName { get; set; }
    public virtual string TimesheetSiteClientId { get; set; }
    public virtual string WorkerName { get; set; }
    public virtual string PayrollPresentedRateCode { get; set; }
    public virtual double Hours { get; set; }
    public virtual string PayRateInput { get; set; }
    public virtual string AdhocRate { get; set; }
    // ReSharper disable once InconsistentNaming Left as is as PO is an acronym
    public virtual string PONumber { get; set; }
    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }
  }
}
