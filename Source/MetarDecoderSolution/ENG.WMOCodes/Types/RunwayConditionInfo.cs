using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using ENG.WMOCodes.Extensions;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Represets sets of runway conditions.
    /// </summary>
    public class RunwayConditionInfo : List<RunwayCondition>, ICodeItem
    {
        #region Properties

        ///<summary>
        /// Sets/gets if airport is closed due to snow (that is SNOCLO in metar).
        ///</summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public bool IsSNOCLO { get; set; }

        #endregion Properties

        #region Inherited

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public string ToCode()
        {
            if (IsSNOCLO)
                return "SNOCLO";
            StringBuilder ret = new StringBuilder();

            foreach (var fItem in this)
            {
                ret.AppendPreSpaced(fItem.ToCode());
            } // foreach (var fItem in RunwayConditions)

            return ret.ToString().TrimEnd();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current instance.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current instance.</returns>
        public override string ToString()
        {
            return this.ToInlineInfoString();
        }

        #region MetarItem Members

        /// <summary>
        /// Proceed sanity check of inserted values.
        /// </summary>
        /// <param name="errors">Found errors.</param>
        /// <param name="warnings">Found warnings.</param>
        public void SanityCheck(ref List<string> errors, ref List<string> warnings)
        {
            foreach (var fItem in this)
            {
                fItem.SanityCheck(ref errors, ref warnings);
            } // foreach (var fItem in this)
        }

        #endregion

        #endregion Inherited


        internal bool IsEmpty()
        {
            return !IsSNOCLO && Count == 0;
        }
    }
}
