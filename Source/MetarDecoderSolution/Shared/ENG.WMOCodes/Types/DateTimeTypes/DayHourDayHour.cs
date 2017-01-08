using System;
using System.Collections.Generic;
using System.Text;

namespace ENG.WMOCodes.Types.DateTimeTypes
{
    /// <summary>
    /// Represents date/time interval defined from day-hour to day-hour
    /// </summary>
    public class DayHourDayHour : DateTimeType
    {
        private DayHour _from = new DayHour();
        ///<summary>
        /// Sets/gets From value. Default value is new DayHour().
        ///</summary>
        public DayHour From
        {
            get
            {
                return _from;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                _from = value;
            }
        }

        private DayHour _to = new DayHour();
        ///<summary>
        /// Sets/gets To value. Default value is new DayHour().
        ///</summary>
        public DayHour To
        {
            get
            {
                return _to;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                _to = value;
            }
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
    }
}
