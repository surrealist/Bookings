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
  }
}
