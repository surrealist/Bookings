using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMM.Bookings.Models.DTOs
{
    public class CreateBooking
    {
      public Guid StudentId { get; set; }
      public Guid TeacherId { get; set; }
      public string CourseId { get; set; } = null!;
      public DateTimeOffset DateStart { get; set; }
      public DateTimeOffset DateEnd { get; set; }
  }
}
