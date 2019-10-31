using System;
using System.Collections.Generic;
using System.ComponentModel;
using ENG.WMOCodes.Extensions;

namespace ENG.WMOCodes.Types.DateTimeTypes
{
    /// <summary>
    /// Represents day-time information when metar was presented.
    /// </summary>
    public class DayHourMinute : DateTimeType, IEquatable<DayHourMinute>, IComparable<DayHourMinute>, IComparable
    {
        #region Properties

        private int _day;
        ///<summary>
        /// Sets/gets Day value.
        ///</summary>
        public int Day
        {
            get => _day;
            set => _day = value.IsBetween(1, 31)
                ? value
                : throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 1 and 31.");
        }

        private int _hour;
        ///<summary>
        /// Sets/gets Hour value.
        ///</summary>
        public int Hour
        {
            get => _hour;
            set => _hour = value.IsBetween(0, 24)
                ? value
                : throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and 24.");
        }

        private int _minute;
        ///<summary>
        /// Sets/gets Minute value.
        ///</summary>
        public int Minute
        {
            get => _minute;
            set => _minute = value.IsBetween(0, 59)
                ? value
                : throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and 59.");
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

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out int day, out int hour, out int minute)
        {
            day = Day;
            hour = Hour;
            minute = Minute;
        }

        #region Inherited

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public override string ToCode()
        {
            return $"{Day:00}{Hour:00}{Minute:00}";
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

        #region Equality

        public bool Equals(DayHourMinute other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _day == other._day && _hour == other._hour && _minute == other._minute;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DayHourMinute) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = _day;
                hashCode = (hashCode * 397) ^ _hour;
                hashCode = (hashCode * 397) ^ _minute;
                return hashCode;
            }
        }

        public static bool operator ==(DayHourMinute left, DayHourMinute right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DayHourMinute left, DayHourMinute right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region Comparison

        public int CompareTo(DayHourMinute other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var dayComparison = _day.CompareTo(other._day);
            if (dayComparison != 0) return dayComparison;
            var hourComparison = _hour.CompareTo(other._hour);
            if (hourComparison != 0) return hourComparison;
            return _minute.CompareTo(other._minute);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;
            if (!(obj is DayHourMinute)) throw new ArgumentException($"Object must be of type {nameof(DayHourMinute)}");
            return CompareTo((DayHourMinute) obj);
        }

        public static bool operator <(DayHourMinute left, DayHourMinute right)
        {
            return Comparer<DayHourMinute>.Default.Compare(left, right) < 0;
        }

        public static bool operator >(DayHourMinute left, DayHourMinute right)
        {
            return Comparer<DayHourMinute>.Default.Compare(left, right) > 0;
        }

        public static bool operator <=(DayHourMinute left, DayHourMinute right)
        {
            return Comparer<DayHourMinute>.Default.Compare(left, right) <= 0;
        }

        public static bool operator >=(DayHourMinute left, DayHourMinute right)
        {
            return Comparer<DayHourMinute>.Default.Compare(left, right) >= 0;
        }

        #endregion
    }
}
