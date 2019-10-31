using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENG.WMOCodes.Types.DateTimeTypes;
using Xunit;

namespace ENG.WMOCodes.Tests.Net45.Types.DateTimeTypes
{
    public class DayHourDayHourTests
    {
        [Theory]
        [InlineData(1, 10, 2, 20, 1, 10, 2, 20)]
        public void Equal(int df1, int hf1, int dt1, int ht1, int df2, int hf2, int dt2, int ht2)
        {
            var first = new DayHourDayHour
            {
                From = new DayHour(df1, hf1),
                To = new DayHour(dt1, ht1)
            };
            var second = new DayHourDayHour
            {
                From = new DayHour(df2, hf2),
                To = new DayHour(dt2, ht2)
            };
            Assert.Equal(first, second);
            Assert.True(first == second);
            Assert.True(second == first);
            Assert.False(first != second);
            Assert.False(second != first);
            Assert.True(first.Equals(second));
            Assert.True(second.Equals(first));
            Assert.True(Object.Equals(first, second));
            Assert.True(Object.Equals(second, first));
            Assert.True(((object)first).Equals(second));
            Assert.True(((object)second).Equals(first));
            Assert.Equal(first.GetHashCode(), second.GetHashCode());
        }

        [Theory]
        [InlineData(1, 10, 3, 20, 2, 10, 3, 20)]
        [InlineData(1, 10, 3, 20, 1, 15, 3, 20)]
        [InlineData(1, 10, 3, 20, 1, 10, 2, 20)]
        [InlineData(1, 10, 3, 20, 1, 10, 3, 15)]
        public void NotEqual(int df1, int hf1, int dt1, int ht1, int df2, int hf2, int dt2, int ht2)
        {
            var first = new DayHourDayHour
            {
                From = new DayHour(df1, hf1),
                To = new DayHour(dt1, ht1)
            };
            var second = new DayHourDayHour
            {
                From = new DayHour(df2, hf2),
                To = new DayHour(dt2, ht2)
            };
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

        [Fact]
        public void Deconstruct()
        {
            var dayHourDayHour = new DayHourDayHour
            {
                From = new DayHour(5, 7),
                To = new DayHour(18, 23)
            };
            var (from, to) = dayHourDayHour;
            Assert.Equal(dayHourDayHour.From, from);
            Assert.Equal(dayHourDayHour.To, to);
        }
    }
}
