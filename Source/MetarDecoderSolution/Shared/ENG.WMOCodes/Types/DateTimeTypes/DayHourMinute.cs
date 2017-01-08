using System;
using System.Collections.Generic;
using ENG.WMOCodes.Extensions;

namespace ENG.WMOCodes.Types.DateTimeTypes
{
    /// <summary>
    /// Represents day-time information when metar was presented.
    /// </summary>
    public class DayHourMinute : DateTimeType
    {
        #region Properties


        private int _day;
        ///<summary>
        /// Sets/gets Day value.
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
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 1 and 31.");
                _day = value;
            }
        }

        private int _hour;
        ///<summary>
        /// Sets/gets Hour value.
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
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and 24.");
                _hour = value;
            }
        }

        private int _minute;
        ///<summary>
        /// Sets/gets Minute value.
        ///</summary>
        public int Minute
        {
            get
            {
                return _minute;
            }
            set
            {
                if (!value.IsBetween(0, 59))
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and 59.");
                _minute = value;
            }
        }


        #endregion Properties

        #region .ctor

        /// <summary>
        /// Initializes a new Instance of ENG.Metar.Decoder.DayTime
        /// </summary>
        public DayHourMinute()
        {
            var now = DateTime.Now;
            Day = now.Day;
            Hour = now.Hour;
            Minute = now.Minute;
        }

        /// <summary>
        /// Initializes a new Instance of ENG.Metar.Decoder.DayTime
        /// </summary>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        public DayHourMinute(int day, int hour, int minute)
        {
            Day = day;
            Hour = hour;
            Minute = minute;
        }

        #endregion .ctor

        #region Inherited

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public override string ToCode()
        {
            return Day.ToString("00") + Hour.ToString("00") + Minute.ToString("00");
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current instance.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current instance.</returns>
        public override string ToString()
        {
            return this.ToInlineInfoString();
        }

        /// <summary>
        /// Proceed sanity check of inserted values.
        /// </summary>
        /// <param name="errors">Found errors.</param>
        /// <param name="warnings">Found warnings.</param>
        public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
        {
            // nothing
        }

        #endregion Inherited

    }
}
