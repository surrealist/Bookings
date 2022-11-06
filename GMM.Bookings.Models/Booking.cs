using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMM.Bookings.Models
{
  public class Booking
  {
    public int Id { get; set; }
    public Student Student { get; set; } = null!;
    public Teacher Teacher { get; set; } = null!;
    public Course Course { get; set; } = null!;

    public DateTimeOffset DateStart { get; set; }
    public DateTimeOffset DateEnd { get; set; }
  }
}
