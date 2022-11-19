namespace GMM.Bookings.Models
{
  public class Teacher
  {
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; }
      = new HashSet<Booking>();
  }
}