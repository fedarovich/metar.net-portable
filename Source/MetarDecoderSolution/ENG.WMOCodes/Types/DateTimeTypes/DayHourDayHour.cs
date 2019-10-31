using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ENG.WMOCodes.Types.DateTimeTypes
{
    /// <summary>
    /// Represents date/time interval defined from day-hour to day-hour
    /// </summary>
    public class DayHourDayHour : DateTimeType, IEquatable<DayHourDayHour>
    {
        public DayHourDayHour()
        {
        }

        public DayHourDayHour(DayHour from, DayHour to)
        {
            From = from;
            To = to;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out DayHour from, out DayHour to)
        {
            from = From;
            to = To;
        }

        private DayHour _from = new DayHour();
        ///<summary>
        /// Sets/gets From value. Default value is new DayHour().
        ///</summary>
        public DayHour From
        {
            get => _from;
            set => _from = value ?? throw new ArgumentNullException(nameof(value));
        }

        private DayHour _to = new DayHour();
        ///<summary>
        /// Sets/gets To value. Default value is new DayHour().
        ///</summary>
        public DayHour To
        {
            get => _to;
            set => _to = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public override string ToCode()
        {
            StringBuilder ret = new StringBuilder();

            ret.Append(From.ToCode());
            ret.Append("/");
            ret.Append(To.ToCode());

            return ret.ToString();
        }

        /// <summary>
        /// Proceed sanity check of inserted values.
        /// </summary>
        /// <param name="errors">Found errors.</param>
        /// <param name="warnings">Found warnings.</param>
        public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
        {
            From.SanityCheck(ref errors, ref warnings);
            To.SanityCheck(ref errors, ref warnings);
        }

        #region Equality

        public bool Equals(DayHourDayHour other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _from.Equals(other._from) && _to.Equals(other._to);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DayHourDayHour) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_from.GetHashCode() * 397) ^ _to.GetHashCode();
            }
        }

        public static bool operator ==(DayHourDayHour left, DayHourDayHour right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DayHourDayHour left, DayHourDayHour right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}
