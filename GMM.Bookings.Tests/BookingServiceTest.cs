using GMM.Bookings.Models;
using GMM.Bookings.Models.DTOs;
using GMM.Bookings.Models.Exceptions;
using GMM.Bookings.Services;
using GMM.Bookings.Services.Data;
using Microsoft.EntityFrameworkCore;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GMM.Bookings.Tests
{
  public class BookingServiceTest
  {
    public class Create
    {
      [Fact]
      public void NotLogin_ThrowsEx()
      {
        var app = new AppBuilder().Build();

        Assert.ThrowsAny<AppException>(() =>
        {
          var request = new CreateBooking();
          Booking result = app.Bookings.Create(request);
        });
      }


      [Fact]
      public void BasicUsage()
      {
        var now = new DateTimeOffset(2022, 11, 10, 9, 0, 0, TimeSpan.FromHours(7));
        var app = new AppBuilder()
                  .WithBasicScenario()
                  .SetNow(now)
                  .LoginByAlice()
                  .Build();         

        var request = new CreateBooking
        {
          StudentId = AppBuilder.Student_Alice.Id,
          TeacherId = AppBuilder.Teacher_Bob.Id, 
          CourseId = AppBuilder.Course_Trumpet.Id,
          DateStart = new DateTimeOffset(2022, 11, 19, 9, 0, 0, TimeSpan.FromHours(7)),
          DateEnd = new DateTimeOffset(2022, 11, 19, 11, 0, 0, TimeSpan.FromHours(7)),
        };

        // act
        var result = app.Bookings.Create(request);
        var bookings = app.Bookings.All().ToList();

        // assert
        Assert.NotNull(result);
        Assert.Equal(request.StudentId, result.StudentId);
        Assert.Equal(request.TeacherId, result.TeacherId);
        Assert.Equal(request.CourseId, result.CourseId);
        Assert.Equal(new DateTimeOffset(2022, 11, 19, 9, 0, 0, TimeSpan.FromHours(7)), result.DateStart);
        Assert.Equal(new DateTimeOffset(2022, 11, 19, 11, 0, 0, TimeSpan.FromHours(7)), result.DateEnd);

        Assert.Same(app.CurrentUser, result.BookingBy);
        Assert.Equal(now, result.BookingDate);
        Assert.Single(bookings);
      }

      [Fact]
      public void StudentCanSeeOnlyHisOrHerBookings()
      {

      }
      [Fact]
      public void OverlappingBookingsIsNotAllowed()
      {

      }
      [Fact]
      public void TeacherCanSeeOnlyHisOrHerBookings()
      {

      }
      [Fact]
      public void AdminCanSeeAllBookings()
      {

      }

    }

    

  }
}
