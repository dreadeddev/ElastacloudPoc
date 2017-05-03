using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impellam.DataIngest.Models
{
  public class ExportFormat : SourceData
  {
    public ExportFormat()
    {
      ExportDate = DateTime.Now;
    }

    public DateTime ExportDate { get; }

    public bool Paid { get; set; } = false;
    public DateTime? DatePaid { get; set; }
    public static ExportFormat FromSourceFormat(SourceData input)
    {
      return new ExportFormat()
      {
        Id = input.Id,
        DateWorked = input.DateWorked,
        HoursWorked = input.HoursWorked,
        PayDate = input.PayDate,
        SourceName = input.SourceName
      };

    }



  }
}
