namespace GMM.Bookings.Services
{
  public class TimeBox
  {
    public TimeBox(DateTimeOffset start, DateTimeOffset end)
    {
      Start = start;
      End = end;
    }

    public DateTimeOffset Start { get; }
    public DateTimeOffset End { get; }

    public static TimeBox FromSameDay(int startHour, int startMinute, int endHour, int endMinute)
    {
      var today = DateTime.Now.Date;
      var d1 = today + new TimeSpan(startHour, startMinute, 0);
      var d2 = today + new TimeSpan(endHour, endMinute, 0);

      return new TimeBox(
          new DateTimeOffset(d1, TimeSpan.FromHours(7)),
          new DateTimeOffset(d2, TimeSpan.FromHours(7)));
    }
  }
}
