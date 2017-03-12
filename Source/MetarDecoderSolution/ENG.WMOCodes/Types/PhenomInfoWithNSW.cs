using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using ENG.WMOCodes.Extensions;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Standard phenom info (<see cref="PhenomInfo"/>) extended by NSW (no significant weather) flag.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class PhenomInfoWithNSW : PhenomInfo
    {
        #region Properties

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private bool _IsNSW;
        ///<summary>
        /// Sets/gets if no-significant-weather flag is used (NSW).
        ///</summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public bool IsNSW
        {
            get
            {
                return Count == 0 && _IsNSW;
            }
            set
            {
                _IsNSW = value;
                if (value)
                    Clear();
            }
        }

        #endregion Properties

        #region Inherited

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public override string ToCode()
        {
            if (IsNSW)
                return "NSW";
            StringBuilder ret = new StringBuilder();

            this.ForEach(
                i => ret.AppendPostSpaced(i.ToCode()));

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
        public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
        {
            if (IsNSW && Count > 0)
                warnings.Add("When IsNSW flag is set to true, phenom definitions will be ignored (now list is nonempty).");
        }

        #endregion

        #endregion Inherited

        #region Methods

        internal override bool IsEmpty()
        {
            return Count == 0 && !IsNSW;
        }

        #endregion Methods
    }
}
