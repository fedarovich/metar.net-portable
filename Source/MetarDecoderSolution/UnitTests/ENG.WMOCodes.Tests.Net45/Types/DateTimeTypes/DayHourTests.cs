using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENG.WMOCodes.Types.DateTimeTypes;
using Xunit;

namespace ENG.WMOCodes.Tests.Net45.Types.DateTimeTypes
{
    public class DayHourTests
    {
        [Theory]
        [InlineData(1, 0, 1, 0)]
        [InlineData(1, 20, 1, 20)]
        public void Equal(int d1, int h1, int d2, int h2)
        {
            var first = new DayHour(d1, h1);
            var second = new DayHour(d2, h2);
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
        [InlineData(3, 20, 1, 20)]
        [InlineData(1, 20, 3, 20)]
        [InlineData(5, 10, 5, 20)]
        public void NotEqual(int d1, int h1, int d2, int h2)
        {
            var first = new DayHour(d1, h1);
            var second = new DayHour(d2, h2);
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
        [InlineData(1, 10, 1, 15)]
        [InlineData(1, 10, 2, 10)]
        [InlineData(1, 15, 2, 10)]
        public void Less(int d1, int h1, int d2, int h2)
        {
            var first = new DayHour(d1, h1);
            var second = new DayHour(d2, h2);
            Assert.True(first.CompareTo(second) < 0);
            Assert.True(first < second);
            Assert.True(first <= second);
            Assert.True(((IComparable)first).CompareTo(second) < 0);
        }

        [Theory]
        [InlineData(2, 10, 1, 15)]
        [InlineData(2, 10, 1, 10)]
        [InlineData(1, 15, 1, 10)]
        public void Greater(int d1, int h1, int d2, int h2)
        {
            var first = new DayHour(d1, h1);
            var second = new DayHour(d2, h2);
            Assert.True(first.CompareTo(second) > 0);
            Assert.True(first > second);
            Assert.True(first >= second);
            Assert.True(((IComparable)first).CompareTo(second) > 0);
        }

        [Fact]
        public void Deconstruct()
        {
            var dayHour = new DayHour(5, 12);
            var (day, hour) = dayHour;
            Assert.Equal(dayHour.Day, day);
            Assert.Equal(dayHour.Hour, hour);
        }
    }
}
