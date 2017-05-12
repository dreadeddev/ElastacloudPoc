using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Impellam.DataIngest.Models;

namespace TestAppIgnore
{
  class Program
  {
    static void Main(string[] args)
    {


      var endOfLastWeek = DateTime.Today.AddDays(-(int) DateTime.Today.DayOfWeek).AddDays(-1);
      var names = new[]
      {
        "Adam Advark", "Bob Buley", "Catherine Cathay", "Donald Dump", "Egor Elsteim=n", "Freddie Forester",
        "George Guest", "Harry Hilbottom", "Ian Istav", "John Jolly"
      };
      var js = "[";
      for (var i = 0; i < 9; i++)
      {
        var x = new SourceData
        {
          ShiftPlacementAssignmentRef = "JobScience",
          WeekEndingDate = endOfLastWeek.AddMonths(-6).AddDays(-20),
          ShiftDate = endOfLastWeek.AddMonths(-6).AddDays(-20),
          Hours = 38D,
          PayRateInput = $"£7.6{i}",
          WorkerName = $"John Doe"
        };
        js += $"{x},";
      }
      js += "]";
      Console.WriteLine(js);
      using (var file =
        new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)))
      {
        file.WriteLine(js);
      }

  
      Console.ReadLine();
    }
  }
}
