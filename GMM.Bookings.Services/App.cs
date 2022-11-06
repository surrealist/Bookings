using GMM.Bookings.Models;
using GMM.Bookings.Models.Exceptions;
using GMM.Bookings.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMM.Bookings.Services
{
  public sealed class App
  {
    internal readonly AppDb db;

    private Lazy<TeacherService> _teacherService;

    public App(AppDb db)
    {
      this.db = db;

      Users = new UserService(this);

      Courses = new CourseService(this);
      Students = new StudentService(this);
      _teacherService = new Lazy<TeacherService>(() => new TeacherService(this));
    }

    public UserService Users { get; }
    public User? CurrentUser { get; private set; } = null;
    public bool IsAuthenticated => CurrentUser != null;

    public CourseService Courses { get; }
    public StudentService Students { get;  }
    public TeacherService Teachers => _teacherService.Value;

    public int SaveChanges() => db.SaveChanges();
    public Task<int> SaveChangesAsync() => db.SaveChangesAsync();

    public Func<DateTimeOffset> Now { get; private set; } = () => DateTimeOffset.Now;
    public void SetNow(DateTimeOffset now) => Now = () => now;
    public void ResetNow() => Now = () => DateTimeOffset.Now;
    public DateTimeOffset Today() => Now().Date;

    public void SetCurrentUser(Guid id, string username)
    {
      var user = Users.Find(id);
      if (user == null)
      {
        user = new User
        {
          Id = id,
          Name = username,
          CreatedDate = Now(),
          Note = null
        };
        Users.Add(user);
        SaveChanges();
      }

      CurrentUser = user;
    }
  }
}
