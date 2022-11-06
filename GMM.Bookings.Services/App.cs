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

      //Users = new UserService(this);

      Courses = new CourseService(this);
      Students = new StudentService(this);
      //Teachers = new TeacherService(this);
      //RoomTypes = new RoomTypeService(this);
      _teacherService = new Lazy<TeacherService>(() => new TeacherService(this));
    }

    //public UserService Users { get; }
    //public User CurrentUser { get; private set; } = null;
    //public bool IsAuthenticated => CurrentUser != null;

    public CourseService Courses { get; }
    public StudentService Students { get;  }
    public TeacherService Teachers => _teacherService.Value;

    //public RoomTypeService RoomTypes { get; }
    //public ReservationService Reservations => _reservationService.Value;

    public int SaveChanges() => db.SaveChanges();
    public Task<int> SaveChangesAsync() => db.SaveChangesAsync();

    public Func<DateTime> Now { get; private set; } = () => DateTime.Now;
    public void SetNow(DateTime now) => Now = () => now;
    public void ResetNow() => Now = () => DateTime.Now;
    public DateTime Today() => Now().Date;

    //public void SetCurrentUser(Guid id, string username)
    //{
    //  var user = Users.Find(id);
    //  if (user == null)
    //  {
    //    user = new User
    //    {
    //      Id = id,
    //      UserName = username,
    //      CreatedDate = Now(),
    //      Note = null
    //    };
    //    Users.Add(user);
    //    SaveChanges();
    //  }

    //  CurrentUser = user;
    //}

    //public void Throws(AppException ex)
    //{
    //  ex.UserName = CurrentUser?.UserName;

    //  throw ex;
    //}
  }
}
