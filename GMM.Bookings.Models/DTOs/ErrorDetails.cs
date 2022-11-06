using System.Text.Json;

namespace GMM.Bookings.Models.DTOs
{
  public class ErrorDetails
  {
    public int StatusCode { get; set; }
    public string Message { get; set; } = null!;
    
    public Guid? CurrentUserId { get; set; }
    public string? CurrentUserName { get; set; }

    public override string ToString()
    {
      return JsonSerializer.Serialize(this);
    }
  }
}
