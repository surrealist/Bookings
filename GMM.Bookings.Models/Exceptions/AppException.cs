using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMM.Bookings.Models.Exceptions
{
  public abstract class AppException : ApplicationException
  {
    public int HttpStatusCode { get; }

    public Guid? CurrentUserId { get; set; }
    public string? CurrentUserName { get; set; }

    public AppException(int httpStatusCode, string? message) : base(message)
    {
      HttpStatusCode = httpStatusCode;
    }
     
  }

  public class NotFoundException : AppException
  {
    public NotFoundException(string? message) 
      : base(404, message)
    {
    }
  }
}
