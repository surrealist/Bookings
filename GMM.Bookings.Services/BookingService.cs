using GMM.Bookings.Models;
using GMM.Bookings.Models.DTOs;
using GMM.Bookings.Models.Exceptions;

namespace GMM.Bookings.Services
{
  public class BookingService : ServiceBase<Booking>
  {
    public BookingService(App app) : base(app)
    {
    }

    public Booking Create(CreateBooking request)
    {
      if (!app.IsAuthenticated)
      {
        throw new AppException(401, "Anonymous user cannot create a booking.");
      }

      var booking =  new Booking();
      booking.StudentId = request.StudentId;
      booking.TeacherId = request.TeacherId;
      booking.CourseId = request.CourseId;
      booking.DateStart = request.DateStart;
      booking.DateEnd = request.DateEnd;
      booking.BookingDate = app.Now();
      booking.BookingBy = app.CurrentUser!;

      app.Bookings.Add(booking);
      app.SaveChanges();

      return booking;
    }


  }
}
