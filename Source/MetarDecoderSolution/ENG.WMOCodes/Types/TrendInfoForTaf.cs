﻿using System.Collections.Generic;
using System.Text;
using ENG.WMOCodes.Extensions;
using ENG.WMOCodes.Types.DateTimeTypes;

namespace ENG.WMOCodes.Types
{

    /// <summary>
    /// Represents trend report contained in TAF report. <seealso cref="TrendReport"/>
    /// </summary>
    public class TrendInfoForTaf : TrendReport
    {
        #region Properties

        ///<summary>
        /// Sets/gets Interval value. Default value is new Intervals.FromToInterval(){ Type= Intervals.FromToInterval.eType.BECMG}.
        ///</summary>
        public TafInterval Interval { get; set; } = new TafIntervalNonFM(TafIntervalType.BECMG, new DayHourDayHour());

        #endregion Properties

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public override string ToCode()
        {
            StringBuilder ret = new StringBuilder();

            ret.AppendPreSpaced(Interval.ToCode());
            ret.AppendPreSpaced(base.ToCode());

            return ret.ToString();
        }

        /// <summary>
        /// Proceed sanity check of inserted values.
        /// </summary>
        /// <param name="errors">Found errors.</param>
        /// <param name="warnings">Found warnings.</param>
        public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
        {
            base.SanityCheck(ref errors, ref warnings);
            Interval.SanityCheck(ref errors, ref warnings);
        }
    }
}
