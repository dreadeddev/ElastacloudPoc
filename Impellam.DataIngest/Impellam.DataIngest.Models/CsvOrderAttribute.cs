using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Impellam.DataIngest.Models
{
  [AttributeUsage(AttributeTargets.Property)]
  public class CsvOrderAttribute : Attribute
  {
    public CsvOrderAttribute(int order)
    {
      Order = order;
    }
    protected CsvOrderAttribute()
    {
      
    }
    public int Order { get; set; }
  }
}
