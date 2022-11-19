using GMM.Bookings.Models;
using Microsoft.EntityFrameworkCore;

namespace GMM.Bookings.Services.Data
{
  public class AppDb : DbContext
  {
    public AppDb(DbContextOptions<AppDb> options) : base(options)
    {
      //
    }

    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Teacher> Teachers { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;

    public DbSet<Booking> Bookings { get; set; } = null!;


    public DbSet<User> Users { get; set; } = null!;
  }
}
