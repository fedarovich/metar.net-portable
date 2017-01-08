using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Extensions;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Represents information about clouds.
    /// </summary>
    public class CloudInfo : List<Cloud>, ICodeItem
    {
        #region Properties


        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private bool _isCLR;
        ///<summary>
        /// Gets "is sky clear" value. That is CLR in metar.
        ///</summary>
        ///<remarks>
        /// CLR value for clear sky: The abbreviation CLR shall be used at automated stations 
        /// when no layers at or below 12,000/10,000 (U.S./Canada) feet are reported. <seealso cref="IsSKC"/> <seealso cref="IsNSC"/>
        ///</remarks>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public bool IsCLR => Count == 0 && _isCLR;


        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private bool _IsSKC;
        ///<summary>
        /// Gets "is sky clear" value. That is SKC in metar.
        ///</summary>
        ///<remarks>
        ///SKC value for clear sky: The abbreviation SKC shall be used at manual stations 
        ///when no layers are reported. <seealso cref="IsCLR"/> <seealso cref="IsNSC"/>
        ///</remarks>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public bool IsSKC => Count == 0 && _IsSKC;

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private bool _IsNSC;
        ///<summary>
        /// Gets "no significant cloud" value. That is NSC in metar.
        ///</summary>
        ///<remarks>
        ///If there are no  clouds  below  1  500  m  (5  000  ft)  or  below  the  highest  
        /// minimum  sector  altitude, whichever is greater, no cumulonimbus and no towering 
        /// cumulus and no restriction on vertical  visibility,  and  the  abbreviations  CAVOK  
        /// is  not  appropriate,  then  the abbreviation NSC shall be used. When an automatic 
        /// observing system is used and no clouds are detected by that system, 
        /// the abbreviation NCD shall be used.
        /// WARNING: NCD is not implemented!
        /// <seealso cref="IsSKC"/> <seealso cref="IsCLR"/>
        ///</remarks>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public bool IsNSC => Count == 0 && _IsNSC;


        private bool _isVerticalVisibility;
        ///<summary>
        /// Gets if cloud info is represented by vertical visibility. E.g. VV040 in metar.
        ///</summary>
        public bool IsVerticalVisibility => Count == 0 && _isVerticalVisibility;


        private int? _vvDistance;
        ///<summary>
        /// Gets vertical visibility distance (in houndreds of feet). E.g. VV040. 
        /// If not known, value is null. That is VV/// in metar.
        ///</summary>
        public int? VVDistance => _vvDistance;

        #endregion Properties

        #region Methods
        /// <summary>
        /// Sets "clear sky"  <see cref="IsCLR"/>
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public void SetCLR()
        {
            SetAllFlagsOff();
            _isCLR = true;
        }


        /// <summary>
        /// Sets "sky clear". <see cref="IsSKC"/>
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public void SetSKC()
        {
            SetAllFlagsOff();
            _IsSKC = true;
        }

        /// <summary>
        /// Sets "no significand cloud". <see cref="IsNSC"/>
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public void SetNSC()
        {
            SetAllFlagsOff();
            _IsNSC = true;
        }

        /// <summary>
        /// Sets vertical visibility. Argument is null if not known (that is VV///).
        /// </summary>
        /// <param name="distance">Distance in hounded feet. Null if not known.</param>
        public void SetVerticalVisibility(int? distance)
        {
            SetAllFlagsOff();
            _isVerticalVisibility = true;
            _vvDistance = distance;
        }

        /// <summary>
        /// Sets all flags (VV, NSC, etc.) off.
        /// </summary>
        protected virtual void SetAllFlagsOff()
        {
            _isCLR = false;
            _IsSKC = false;
            _IsNSC = false;
            _isVerticalVisibility = false;
            _vvDistance = null;
            Clear();
        }

        #endregion Methods

        #region Implemented

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public virtual string ToCode()
        {
            if (IsCLR)
                return "CLR";
            if (IsSKC)
                return "SKC";
            if (IsNSC)
                return "NSC";
            if (IsVerticalVisibility)
                return "VV" + (VVDistance.HasValue ? VVDistance.Value.ToString("000") : "///");
            StringBuilder ret = new StringBuilder();

            this.ForEach(
                i => ret.AppendPreSpaced(i.ToCode()));

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
        public virtual void SanityCheck(ref List<string> errors, ref List<string> warnings)
        {
            if (GetTrueCount(IsNSC, IsSKC, IsCLR) > 1)
                errors.Add("IsSKC, IsNSC, and IsCLR are mutually exclusive.  More than one flag is currently set.");
            if ((IsSKC || IsNSC || IsCLR) && IsVerticalVisibility)
                errors.Add("Vertical visibility cannot be set true with IsSKC, IsNSC, or IsCLR flags.");
            if ((IsSKC || IsNSC || IsCLR || IsVerticalVisibility) && Count > 0)
                warnings.Add("When one of flags IsSKC, IsNSC, IsCLR, or IsVerticalVisibility are set to true, cloud defining content (which is now not empty) will be ignored.");
        }

        /// <summary>
        /// Returns count of items of bool parameters, which are true.
        /// </summary>
        /// <param name="booleans"></param>
        /// <returns></returns>
        private int GetTrueCount(params bool[] booleans)
        {
            return booleans.Count(b => b);
        }

        #endregion

        #endregion Implemented

        internal bool IsEmpty()
        {
            return !IsNSC && !IsSKC && !IsCLR && !IsVerticalVisibility && Count == 0;
        }
    }
}
