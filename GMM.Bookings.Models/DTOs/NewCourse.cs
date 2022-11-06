namespace GMM.Bookings.Models.DTOs
{
  public class NewCourse
  {
    public string CoursePrefix { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public double Hours { get; set; }

    public Course ToModel()
    {
      return new Course
      {
        Id = $"{CoursePrefix}-000",
        Name = Name,
        Price = Price,
        Hours = Hours,
        IsActive = true,
      };
    }
  }
}
