using m = GMM.Bookings.Models;

namespace GMM.Bookings.Models.DTOs
{
  public class CourseResponse
  {  
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public double Hours { get; set; }

    public static CourseResponse FromModel(m.Course item)
    {
      return new CourseResponse()
      {
        Id = item.Id,
        Name = item.Name,
        Price = item.Price,
        Hours = item.Hours,
      };
    }
  }
}
