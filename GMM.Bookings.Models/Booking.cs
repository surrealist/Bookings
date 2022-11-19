using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMM.Bookings.Models
{
  public class Booking
  {
    public Guid Id { get; set; }

    public Student Student { get; set; } = null!;
    public Guid StudentId { get; set; }

    public Teacher Teacher { get; set; } = null!;
    public Guid TeacherId { get; set; }

    public Course Course { get; set; } = null!;
    public string CourseId { get; set; } = null!;

    public DateTimeOffset DateStart { get; set; }
    public DateTimeOffset DateEnd { get; set; }

    public DateTimeOffset BookingDate { get; set; }
    public User BookingBy { get; set; } = null!;

  }
}
