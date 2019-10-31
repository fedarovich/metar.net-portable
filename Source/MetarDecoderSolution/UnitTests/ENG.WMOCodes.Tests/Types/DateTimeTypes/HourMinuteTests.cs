using System;
using ENG.WMOCodes.Types.DateTimeTypes;
using Xunit;

namespace ENG.WMOCodes.Tests.Net45.Types.DateTimeTypes
{
    public class HourMinuteTests
    {
        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(1, 30, 1, 30)]
        public void Equal(int h1, int m1, int h2, int m2)
        {
            var first = new HourMinute(h1, m1);
            var second = new HourMinute(h2, m2);
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
        [InlineData(1, 20, 20, 1)]
        [InlineData(3, 30, 1, 30)]
        [InlineData(1, 30, 3, 30)]
        [InlineData(5, 20, 5, 30)]
        public void NotEqual(int h1, int m1, int h2, int m2)
        {
            var first = new HourMinute(h1, m1);
            var second = new HourMinute(h2, m2);
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
        [InlineData(1, 10, 1, 30)]
        [InlineData(1, 10, 2, 10)]
        [InlineData(1, 30, 2, 10)]
        public void Less(int h1, int m1, int h2, int m2)
        {
            var first = new HourMinute(h1, m1);
            var second = new HourMinute(h2, m2);
            Assert.True(first.CompareTo(second) < 0);
            Assert.True(first < second);
            Assert.True(first <= second);
            Assert.True(((IComparable)first).CompareTo(second) < 0);
        }

        [Theory]
        [InlineData(2, 10, 1, 30)]
        [InlineData(2, 10, 1, 10)]
        [InlineData(1, 30, 1, 10)]
        public void Greater(int h1, int m1, int h2, int m2)
        {
            var first = new HourMinute(h1, m1);
            var second = new HourMinute(h2, m2);
            Assert.True(first.CompareTo(second) > 0);
            Assert.True(first > second);
            Assert.True(first >= second);
            Assert.True(((IComparable)first).CompareTo(second) > 0);
        }

        [Fact]
        public void Deconstruct()
        {
            var hourMinute = new HourMinute(12, 25);
            var (hour, minute) = hourMinute;
            Assert.Equal(hourMinute.Hour, hour);
            Assert.Equal(hourMinute.Minute, minute);
        }
    }
}
