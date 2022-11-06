using GMM.Bookings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMM.Bookings.Services
{
  public class TeacherService : ServiceBase<Teacher>
  {
    public TeacherService(App app) : base(app)
    {
    }
  }
}
