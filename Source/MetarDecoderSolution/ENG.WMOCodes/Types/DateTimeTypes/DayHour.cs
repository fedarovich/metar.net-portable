using System;
using System.Collections.Generic;
using System.ComponentModel;
using ENG.WMOCodes.Extensions;

namespace ENG.WMOCodes.Types.DateTimeTypes
{
    /// <summary>
    /// Represents day/time value defined by day-hour.
    /// </summary>
    public class DayHour : DateTimeType, IEquatable<DayHour>, IComparable<DayHour>, IComparable
    {
        private int _day;
        ///<summary>
        /// Sets/gets Day value. Default value is current day.
        ///</summary>
        public int Day
        {
            get => _day;
            set => _day = value.IsBetween(1, 31)
                ? value
                : throw new ArgumentOutOfRangeException(nameof(value), "Value have to be between 1-31.");
        }

        private int _hour;
        ///<summary>
        /// Sets/gets Hour value. Default value is current hour.
        ///</summary>
        public int Hour
        {
            get => _hour;
            set => _hour = value.IsBetween(0, 24)
                ? value
                : throw new ArgumentOutOfRangeException(nameof(value), "Value have to be between 0-24.");
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

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out int day, out int hour)
        {
            day = Day;
            hour = Hour;
        }

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public override string ToCode()
        {
            return $"{Day:00}{Hour:00}";
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

        #region Equality

        public bool Equals(DayHour other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _day == other._day && _hour == other._hour;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DayHour) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_day * 397) ^ _hour;
            }
        }

        public static bool operator ==(DayHour left, DayHour right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DayHour left, DayHour right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Comparison

        public int CompareTo(DayHour other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var dayComparison = _day.CompareTo(other._day);
            if (dayComparison != 0) return dayComparison;
            return _hour.CompareTo(other._hour);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is DayHour)) throw new ArgumentException($"Object must be of type {nameof(DayHour)}");
            return CompareTo((DayHour) obj);
        }

        public static bool operator <(DayHour left, DayHour right)
        {
            return Comparer<DayHour>.Default.Compare(left, right) < 0;
        }

        public static bool operator >(DayHour left, DayHour right)
        {
            return Comparer<DayHour>.Default.Compare(left, right) > 0;
        }

        public static bool operator <=(DayHour left, DayHour right)
        {
            return Comparer<DayHour>.Default.Compare(left, right) <= 0;
        }

        public static bool operator >=(DayHour left, DayHour right)
        {
            return Comparer<DayHour>.Default.Compare(left, right) >= 0;
        }

        #endregion
    }
}
