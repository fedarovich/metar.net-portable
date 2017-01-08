using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Represents cloud info (<see cref="CloudInfo"/>) with NCD (no clouds detected) flag.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class CloudInfoWithNCD : CloudInfo
    {
        #region Properties
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private bool _isNCD;
        ///<summary>
        /// Gets "no clouds detected" value. That is NCD in metar.
        ///</summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public bool IsNCD => Count == 0 && _isNCD;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Sets all flags (VV, NSC, etc.) off.
        /// </summary>
        protected override void SetAllFlagsOff()
        {
            _isNCD = false;
            base.SetAllFlagsOff();
        }

        /// <summary>
        /// Sets the NCD (no clouds detected) flag.
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public virtual void SetNCD()
        {
            SetAllFlagsOff();
            _isNCD = true;
        }

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public override string ToCode()
        {
            if (IsNCD)
                return "NCD";
            return base.ToCode();
        }

        /// <summary>
        /// Proceed sanity check of inserted values.
        /// </summary>
        /// <param name="errors">Found errors.</param>
        /// <param name="warnings">Found warnings.</param>
        public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
        {
            base.SanityCheck(ref errors, ref warnings);
            if (IsNCD && (IsSKC || IsNSC || IsCLR || IsVerticalVisibility || Count > 0))
                errors.Add("Unable to have IsNCD flag on, when IsSKC, IsNSC, IsCLR, or IsVerticalVisibility is true or count of clouds > 0.");
        }

        #endregion Methods
    }
}
