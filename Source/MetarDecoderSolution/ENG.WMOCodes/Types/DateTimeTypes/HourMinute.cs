using System;
using System.Collections.Generic;
using ENG.WMOCodes.Extensions;

namespace ENG.WMOCodes.Types.DateTimeTypes
{
    /// <summary>
    /// Represents date/time value defined by hour and minute.
    /// </summary>
    public class HourMinute : IEquatable<HourMinute>, IComparable<HourMinute>, IComparable
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

        #region Equality

        public bool Equals(HourMinute other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _hour == other._hour && _minute == other._minute;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((HourMinute) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_hour * 397) ^ _minute;
            }
        }

        public static bool operator ==(HourMinute left, HourMinute right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(HourMinute left, HourMinute right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Comparison

        public int CompareTo(HourMinute other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var hourComparison = _hour.CompareTo(other._hour);
            if (hourComparison != 0) return hourComparison;
            return _minute.CompareTo(other._minute);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is HourMinute)) throw new ArgumentException($"Object must be of type {nameof(HourMinute)}");
            return CompareTo((HourMinute) obj);
        }

        public static bool operator <(HourMinute left, HourMinute right)
        {
            return Comparer<HourMinute>.Default.Compare(left, right) < 0;
        }

        public static bool operator >(HourMinute left, HourMinute right)
        {
            return Comparer<HourMinute>.Default.Compare(left, right) > 0;
        }

        public static bool operator <=(HourMinute left, HourMinute right)
        {
            return Comparer<HourMinute>.Default.Compare(left, right) <= 0;
        }

        public static bool operator >=(HourMinute left, HourMinute right)
        {
            return Comparer<HourMinute>.Default.Compare(left, right) >= 0;
        }

        #endregion
    }
}
