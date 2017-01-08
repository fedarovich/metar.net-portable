using System.Collections.Generic;
using System.Text;
using ENG.WMOCodes.Extensions;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Represents information about windshears in metar.
    /// </summary>
    public class WindShearInfo : List<WindShear>, ICodeItem
    {
        #region Properties

        ///<summary>
        /// Sets/gets if winshear warning is true for all runways (WS ALL RWY). If so, collection data are ignored.
        ///</summary>
        public bool IsAllRunways { get; set; }

        #endregion Properties

        #region Inherited

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public string ToCode()
        {
            if (IsAllRunways)
                return "WS ALL RWY";
            StringBuilder b = new StringBuilder();

            foreach (var fItem in this)
            {
                b.AppendPreSpaced(fItem.ToCode());
            } // foreach (var fItem in WindShears)

            if (b.Length > 0)
                return "WS " + b.ToString().TrimEnd();
            return "";
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current instance.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current instance.</returns>
        public override string ToString() => this.ToInlineInfoString();

        #region MetarItem Members


        /// <summary>
        /// Proceed sanity check of inserted values.
        /// </summary>
        /// <param name="errors">Found errors.</param>
        /// <param name="warnings">Found warnings.</param>
        public void SanityCheck(ref List<string> errors, ref List<string> warnings)
        {
            if (IsAllRunways && Count > 0)
                warnings.Add("If IsAllRunways flag is set to true, ws definitions for concrete runways will be ignored (now are non-empty).");
        }

        #endregion

        #endregion Inherited

        internal bool IsEmpty() => !IsAllRunways && Count == 0;
    }
}
