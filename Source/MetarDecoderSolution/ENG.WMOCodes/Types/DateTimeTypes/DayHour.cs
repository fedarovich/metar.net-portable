using System;
using System.Collections.Generic;
using ENG.WMOCodes.Extensions;

namespace ENG.WMOCodes.Types.DateTimeTypes
{
    /// <summary>
    /// Represents day/time value defined by day-hour.
    /// </summary>
    public class DayHour : DateTimeType
    {
        private int _day;
        ///<summary>
        /// Sets/gets Day value. Default value is current day.
        ///</summary>
        public int Day
        {
            get
            {
                return _day;
            }
            set
            {
                if (!value.IsBetween(1, 31))
                    throw new ArgumentOutOfRangeException(nameof(value), "Value have to be between 1-31.");
                _day = value;
            }
        }

        private int _hour;
        ///<summary>
        /// Sets/gets Hour value. Default value is current hour.
        ///</summary>
        public int Hour
        {
            get
            {
                return _hour;
            }
            set
            {
                if (!value.IsBetween(0, 24))
                    throw new ArgumentOutOfRangeException(nameof(value), "Value have to be between 0-24.");
                _hour = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DayHour"/> class.
        /// </summary>
        /// <param name="day">The day.</param>
        /// <param name="hour">The hour.</param>
        public DayHour(int day, int hour)
        {
            Day = day;
            Hour = hour;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DayHour"/> class.
        /// </summary>
        public DayHour()
        {
            var now = DateTime.Now;
            Day = now.Day;
            Hour = now.Hour;
        }

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public override string ToCode()
        {
            return Day.ToString("00") + Hour.ToString("00");
        }

        /// <summary>
        /// Proceed sanity check of inserted values.
        /// </summary>
        /// <param name="errors">Found errors.</param>
        /// <param name="warnings">Found warnings.</param>
        public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
        {
            // nothing to do
        }

    }
}
