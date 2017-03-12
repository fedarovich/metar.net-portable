using System;
using ENG.WMOCodes.Extensions;

namespace ENG.WMOCodes.Types.DateTimeTypes
{
    /// <summary>
    /// Represents date/time value defined by hour and minute.
    /// </summary>
    public class HourMinute
    {

        private int _hour;
        ///<summary>
        /// Sets/gets Hour value. Default value is DateTime.Now.Hour.
        ///</summary>
        public int Hour
        {
            get
            {
                return _hour;
            }
            set
            {
                if (!value.IsBetween(0, 23))
                    throw new ArgumentOutOfRangeException(nameof(value), "Value have to be between 0-23.");
                _hour = value;
            }
        }

        private int _minute;
        ///<summary>
        /// Sets/gets Minute value. Default value is DateTime.Now.Minute.
        ///</summary>
        public int Minute
        {
            get
            {
                return _minute;
            }
            set
            {
                if (!value.IsBetween(0, 60))
                    throw new ArgumentOutOfRangeException(nameof(value), "Value have to be between 0-60.");
                _minute = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HourMinute"/> class.
        /// </summary>
        public HourMinute()
        {
            var now = DateTime.Now;
            Hour = now.Hour;
            Minute = now.Minute;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HourMinute"/> class.
        /// </summary>
        /// <param name="hour">The hour.</param>
        /// <param name="minute">The minute.</param>
        public HourMinute(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;
        }
    }
}
