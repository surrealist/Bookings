namespace GMM.Bookings.Models.DTOs
{
  public class UpdatedCourse
  {
    //public string Id { get; set; }

    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
    public decimal Price { get; set; }
    public double Hours { get; set; }
  }
}
