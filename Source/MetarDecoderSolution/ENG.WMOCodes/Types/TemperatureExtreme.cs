using System.Collections.Generic;
using ENG.WMOCodes.Types.Basic;
using ENG.WMOCodes.Types.DateTimeTypes;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Abstract. Represents trend temperature for trend TAF report.
    /// </summary>
    public abstract class TemperatureExtreme : ICodeItem
    {
        ///<summary>
        /// Sets/gets Temperature value. Default value is 0.
        ///</summary>
        public int Temperature { get; set; }

        ///<summary>
        /// Sets/gets Time value. Default value is new DayHourFlag().
        ///</summary>
        public DayHour Time { get; set; } = new DayHour();

        #region ICodeItem Members

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public abstract string ToCode();

        /// <summary>
        /// Proceed sanity check of inserted values.
        /// </summary>
        /// <param name="errors">Found errors.</param>
        /// <param name="warnings">Found warnings.</param>
        public abstract void SanityCheck(ref List<string> errors, ref List<string> warnings);

        #endregion
    }
}
