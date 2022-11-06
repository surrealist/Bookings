using FluentValidation;
using GMM.Bookings.Models;
using GMM.Bookings.Models.DTOs;
using GMM.Bookings.Models.Exceptions;
using GMM.Bookings.Services.Data;
using System.Linq.Expressions;

namespace GMM.Bookings.Services
{
  public class CourseService : ServiceBase<Course>
  {
    public CourseService(App app) : base(app) { }

    public Course CreateCourse(NewCourse item)
    {
      var prefix = item.CoursePrefix.Substring(0, 2);
      var newId = "";
      var lastCourse = (from x in app.db.Courses
                        where x.Id.StartsWith(prefix)
                        orderby x.Id descending
                        select x).FirstOrDefault();
      if (lastCourse != null)
      {
        var running = int.Parse(lastCourse.Id.Substring(3, 3)); // XX-005
        newId = $"{prefix}-{running + 1:000}";
      }
      else
      {
        newId = $"{prefix}-001";
      }

      var c = item.ToModel();
      c.Id = newId;
      app.db.Add(c);
      app.db.SaveChanges();
      return c;
    }

    public Course UpdateCourse(string id, UpdatedCourse item)
    { 
      var course = Find(id);
      if (course == null) 
        throw new NotFoundException($"Course {id} Not found");

      course.Name = item.Name;
      course.IsActive = item.IsActive;
      course.Price = item.Price;
      course.Hours = item.Hours;
      app.SaveChanges();
      return course;
    }

    public override Course Remove(Course item)
    {
      //return base.Remove(item);
      item.Name += "_DELETED";
      return item;
    }

    public override IQueryable<Course> Query(Expression<Func<Course, bool>> predicate)
    {
      return base.Query(predicate).Where(x => !x.Name.EndsWith("_DELETED"));
    }
    public override Course? Find(params object[] keys)
    {
      var item = base.Find(keys);
      if (item != null && item.Name.EndsWith("_DELETED")) return null;
      return item;
    }
  }
}
