using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMM.Bookings.Models
{
  public class Course
  {
    [StringLength(10)]
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public decimal Price { get; set; }
    public double Hours { get; set; }


  }
}
