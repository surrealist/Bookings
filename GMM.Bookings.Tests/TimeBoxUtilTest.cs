using GMM.Bookings.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace GMM.Bookings.Tests
{
  public class TimeBoxUtilTest
  {
    public class IsOverlapped
    {
      [Theory]
      [InlineData(11, 13)]
      [InlineData(9, 11)]
      [InlineData(10, 11)]
      [InlineData(8, 14)]
      public void Overlapping(int hour1, int hour2)
      {
        // arrange
        var timeBoxUtil = new TimeBoxUtil();
        var existings = new List<TimeBox>
        {
          TimeBox.FromSameDay(10, 0, 12, 0)
        };

        var tb = TimeBox.FromSameDay(hour1, 0, hour2, 0);


        // act
        bool overlapped = timeBoxUtil.IsOverlapped(tb, existings);

        // assert
        //Assert.Equal(true, overlapped);
        Assert.True(overlapped);
      }

      //[Fact(Skip="Demo")]
      [Fact]
      public void HasMorningBookingBookAfternoon_NotOverlapped()
      {
        // arrange
        var timeBoxUtil = new TimeBoxUtil();
        var existings = new List<TimeBox>
        {
          TimeBox.FromSameDay(10, 0, 12, 0)
        };

        var tb = TimeBox.FromSameDay(12, 0, 14, 0);

        // act
        bool overlapped = timeBoxUtil.IsOverlapped(tb, existings);

        // assert
        Assert.False(overlapped);
      }

      [Fact]
      public void NoExistingBookings_ReturnsFalse()
      {
        // arrange
        var timeBoxUtil = new TimeBoxUtil();
        var existings = new List<TimeBox>();

        TimeBox tb = TimeBox.FromSameDay(startHour: 10, startMinute: 0,
                                         endHour: 12, endMinute: 0);

        // act
        bool overlapped = timeBoxUtil.IsOverlapped(tb, existings);

        // assert
        Assert.False(overlapped);
      }


      [Fact]
      public void InvalidBooking_ThrowsEx()
      {
        // arrange
        var timeBoxUtil = new TimeBoxUtil();
        var existings = new List<TimeBox>();

        TimeBox tb = TimeBox.FromSameDay(startHour: 16, startMinute: 0,
                                         endHour: 12, endMinute: 0);

        // act
        Assert.ThrowsAny<Exception>(() =>
        {
          bool overlapped = timeBoxUtil.IsOverlapped(tb, existings);
        });
      }
    }
  }
}