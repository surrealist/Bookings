using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMM.Bookings.Models.DTOs
{
  public class UserSignIn
  {
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
  }
}
