using System.Collections.Generic;
using System.Text;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Represents standard wind (<see cref=" Wind"/>) extended with wind variability.
    /// </summary>
    public class WindWithVariability : Wind
    {
        ///<summary>
        /// Sets/gets variable value. Null if not used. <see cref="IsVarying"/><see cref="WindVariable"/>
        ///</summary>
        public WindVariable Variability { get; set; } = null;

        /// <summary>
        /// Returns true if wind is varying between two headings.. <see cref="Variability"/> <see cref="WindVariable"/>
        /// </summary>
        /// <value></value>
        public bool IsVarying => Variability != null;

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public override string ToCode()
        {
            StringBuilder ret = new StringBuilder(base.ToCode());

            if (IsVarying)
            {
                ret.Append(" " + Variability.ToCode());
            }

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
            if (IsVarying)
                Variability.SanityCheck(ref errors, ref warnings);
        }
    }
}
