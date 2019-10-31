using System;
using ENG.WMOCodes.Types.DateTimeTypes;
using Xunit;

namespace ENG.WMOCodes.Tests.Net45.Types.DateTimeTypes
{
    public class DayHourMinuteTests
    {
        [Theory]
        [InlineData(1, 0, 0, 1, 0, 0)]
        [InlineData(1, 15, 30, 1, 15, 30)]
        public void Equal(int d1, int h1, int m1, int d2, int h2, int m2)
        {
            var first = new DayHourMinute(d1, h1, m1);
            var second = new DayHourMinute(d2, h2, m1);
            Assert.Equal(first, second);
            Assert.True(first == second);
            Assert.True(second == first);
            Assert.False(first != second);
            Assert.False(second != first);
            Assert.True(first <= second);
            Assert.True(first >= second);
            Assert.True(second <= first);
            Assert.True(second >= first);
            Assert.False(first < second);
            Assert.False(first > second);
            Assert.False(second < first);
            Assert.False(second > first);
            Assert.True(first.Equals(second));
            Assert.True(second.Equals(first));
            Assert.True(Object.Equals(first, second));
            Assert.True(Object.Equals(second, first));
            Assert.True(((object)first).Equals(second));
            Assert.True(((object)second).Equals(first));
            Assert.Equal(first.GetHashCode(), second.GetHashCode());
            Assert.Equal(0, first.CompareTo(second));
            Assert.Equal(0, second.CompareTo(first));
        }

        [Theory]
        [InlineData(1, 0, 0, 1, 0, 50)]
        [InlineData(1, 0, 0, 1, 10, 0)]
        [InlineData(1, 0, 0, 2, 0, 0)]
        [InlineData(1, 2, 3, 10, 20, 30)]
        [InlineData(1, 2, 3, 2, 3, 1)]
        public void NotEqual(int d1, int h1, int m1, int d2, int h2, int m2)
        {
            var first = new DayHourMinute(d1, h1, m1);
            var second = new DayHourMinute(d2, h2, m2);
            Assert.NotEqual(first, second);
            Assert.False(first == second);
            Assert.False(second == first);
            Assert.True(first != second);
            Assert.True(second != first);
            Assert.False(first.Equals(second));
            Assert.False(second.Equals(first));
            Assert.False(Object.Equals(first, second));
            Assert.False(Object.Equals(second, first));
            Assert.False(((object)first).Equals(second));
            Assert.False(((object)second).Equals(first));
        }

        [Theory]
        [InlineData(1, 0, 0, 2, 0, 0)]
        [InlineData(1, 0, 0, 1, 1, 0)]
        [InlineData(1, 0, 0, 1, 0, 1)]
        [InlineData(1, 23, 59, 2, 0, 0)]
        [InlineData(1, 0, 59, 1, 1, 0)]
        public void Less(int d1, int h1, int m1, int d2, int h2, int m2)
        {
            var first = new DayHourMinute(d1, h1, m1);
            var second = new DayHourMinute(d2, h2, m2);
            Assert.True(first.CompareTo(second) < 0);
            Assert.True(first < second);
            Assert.True(first <= second);
            Assert.True(((IComparable)first).CompareTo(second) < 0);
        }

        [Theory]
        [InlineData(2, 0, 0, 1, 0, 0)]
        [InlineData(1, 1, 0, 1, 0, 0)]
        [InlineData(1, 0, 1, 1, 0, 0)]
        [InlineData(2, 0, 0, 1, 23, 59)]
        [InlineData(1, 1, 0, 1, 0, 59)]
        public void Greater(int d1, int h1, int m1, int d2, int h2, int m2)
        {
            var first = new DayHourMinute(d1, h1, m1);
            var second = new DayHourMinute(d2, h2, m2);
            Assert.True(first.CompareTo(second) > 0);
            Assert.True(first > second);
            Assert.True(first >= second);
            Assert.True(((IComparable)first).CompareTo(second) > 0);
        }

        [Fact]
        public void Deconstruct()
        {
            var dayHourMinute = new DayHourMinute(5, 12, 25);
            var (day, hour, minute) = dayHourMinute;
            Assert.Equal(dayHourMinute.Day, day);
            Assert.Equal(dayHourMinute.Hour, hour);
            Assert.Equal(dayHourMinute.Minute, minute);
        }
    }
}
