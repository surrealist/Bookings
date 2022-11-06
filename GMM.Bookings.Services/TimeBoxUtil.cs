using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMM.Bookings.Services
{
  public class TimeBoxUtil
  {

    public bool IsOverlapped(TimeBox a, List<TimeBox> existings)
    {
      if (a.Start >= a.End)
      {
        var ex = new Exception("Start cannot greater than the End.");
        throw ex;
      }

      //foreach(var b in existings)
      //{
      //  if (a.Start < b.End && a.End > b.Start) return true;
      //}
      if (existings.Any(b => a.Start < b.End && a.End > b.Start))
        return true;

      return false;
    }
  }
}
