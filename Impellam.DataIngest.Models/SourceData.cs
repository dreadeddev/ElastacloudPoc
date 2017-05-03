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
    public string SourceName { get; set; }
    public DateTime PayDate { get; set; }
    public double HoursWorked { get; set; }
    public DateTime DateWorked { get; set; }
    public string Id { get; set; }
    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }
  }
}
