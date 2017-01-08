using System;
using System.Collections.Generic;
using ENG.WMOCodes.Types.DateTimeTypes;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Represents interval for trend TAF report starting with prefix (e.g. FM011200 )
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class TafIntervalFM : TafInterval
    {

        private DayHourMinute _from = new DayHourMinute();
        ///<summary>
        /// Sets/gets From value. Default value is new DayHourMinute().
        ///</summary>
        public DayHourMinute From
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

        /// <summary>
        /// Initializes a new instance of the <see cref="TafIntervalFM"/> class.
        /// </summary>
        /// <param name="intervalFlag">The interval flag.</param>
        public TafIntervalFM(DayHourMinute intervalFlag)
        {
            From = intervalFlag;
        }

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public override string ToCode()
        {
            return "FM" + From.ToCode();
        }

        /// <summary>
        /// Proceed sanity check of inserted values.
        /// </summary>
        /// <param name="errors">Found errors.</param>
        /// <param name="warnings">Found warnings.</param>
        public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
        {
            // nothing to do here
        }
    }
}
