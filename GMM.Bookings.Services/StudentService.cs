using GMM.Bookings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMM.Bookings.Services
{
  public class StudentService : ServiceBase<Student>
  {
    public StudentService(App app) : base(app)
    {
    }
  }
}
