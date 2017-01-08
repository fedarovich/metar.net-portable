using System;
using System.Collections.Generic;
using System.Text;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Represents maximum temperature in TAF trend record.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class TemperatureExtremeTX : TemperatureExtreme
    {
        /// <summary>
        /// Toes the code.
        /// </summary>
        /// <returns></returns>
        public override string ToCode()
        {
            StringBuilder ret = new StringBuilder();

            ret.Append("TX");
            if (Temperature <= 0)
                ret.Append("M");
            ret.Append(Math.Abs(Temperature).ToString("00"));
            ret.Append("/");
            ret.Append(Time.ToCode());
            ret.Append("Z");

            return ret.ToString();
        }

        /// <summary>
        /// Sanities the check.
        /// </summary>
        /// <param name="errors">The errors.</param>
        /// <param name="warnings">The warnings.</param>
        public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
        {
            // nothing to do
        }
    }
}
