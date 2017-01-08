using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using ENG.WMOCodes.Extensions;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Represents information about basic visibility or CAVOK.
    /// </summary>
    public class Visibility : ICodeItem
    {
        #region Properties

        ///<summary>
        /// Sets/gets if to use E-U style in ToMetar method. If E-U style, visibility is in meters, if false, visibility is in NM.
        ///</summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public bool UseEUStyle { get; protected set; }

        /// <summary>
        /// Returns distance unit. If EUStyle, returns meters, if non-eu style, returns miles.
        /// </summary>
        public DistanceUnit DistanceUnit => UseEUStyle ? DistanceUnit.m : DistanceUnit.mi;


        ///<summary>
        /// Sets/gets if measured distance is equipments minimal measurable value.
        ///</summary>
        public bool IsDevicesMinimumValue { get; protected set; }


        ///<summary>
        /// Sets/gets clear visibility value. If true, most of other properties are omitted.
        ///</summary>
        public bool IsClear { get; protected set; }


        private Rational? _distance;
        ///<summary>
        /// Sets/gets distance value. If style is E-U, value is in meters. If style is non-EU, value is in NM.
        ///</summary>
        public Rational? Distance
        {
            get
            {
                return _distance;
            }
            protected set
            {
                _distance = value;
                if (value.HasValue)
                    IsClear = false;
            }
        }

        #endregion Properties

        #region .ctor

        public Visibility()
        {
            SetClear(true);
        }

        #endregion .ctor

        #region Methods
        
        /// <summary>
        /// Sets "cloud and visibility ok" (CAVOK) weather (that is clear sky in EU style).
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public void SetCAVOK()
        {
            SetClear(false);
        }

        /// <summary>
        /// Sets "sky clear" (SKC) weather (that is clear sky in US style).
        /// </summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public void SetSKC()
        {
            SetClear(true);
        }

        /// <summary>
        /// Sets clear sky. Parameter define if to use EU style (US otherwise).
        /// </summary>
        /// <param name="useEUStyle">True if EU style to use, false otherwise (that is US style).</param>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public void SetClear(bool useEUStyle)
        {
            IsClear = true;
            UseEUStyle = useEUStyle;
        }
        /// <summary>
        /// Set distance in meters. Sets EU style.
        /// </summary>
        /// <param name="distance">Visibility distance.</param>
        public virtual void SetMeters(int distance)
        {
            UseEUStyle = true;
            Distance = distance;
            IsDevicesMinimumValue = false;
        }

        /// <summary>
        /// Sets visibility distance in miles. Sets US style (non EU style).
        /// </summary>
        /// <param name="distance">Distance</param>
        /// <param name="isDevicesMinimumValue">True if value is minimum of measuring equipment.</param>
        public void SetMiles(Rational distance, bool isDevicesMinimumValue)
        {
            IsDevicesMinimumValue = isDevicesMinimumValue;
            UseEUStyle = false;
            Distance = distance;
        }

        /// <summary>
        /// Sets visibility distance in miles. Sets US style (non EU style).
        /// </summary>
        /// <param name="distance">Distance</param>
        /// <param name="isDevicesMinimumValue">True if value is minimum of measuring equipment.</param>
        public void SetMiles(int distance, bool isDevicesMinimumValue)
        {
            SetMiles((Rational)distance, isDevicesMinimumValue);
        }

        /// <summary>
        /// Returns distance converted to selected unit. If distance has no value, null is returned.
        /// </summary>
        /// <param name="targetUnit"></param>
        /// <returns></returns>
        public double? GetDistanceIn(DistanceUnit targetUnit)
        {
            if (Distance.HasValue == false)
                return null;

            double ret = Convertions.Convert(Distance.Value.Value, DistanceUnit, targetUnit);

            return ret;
        }

        #endregion Methods

        #region Inherited

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public virtual string ToCode()
        {
            StringBuilder ret = new StringBuilder();

            if (IsClear)
            {
                ret.Append(UseEUStyle ? "SKC" : "CAVOK");
            }
            else if (UseEUStyle)
            {
                ret.Append(((int)Distance.Value).ToString("0000"));
            }
            else
            {
                if (IsDevicesMinimumValue)
                    ret.Append("M");
                ret.Append(Distance.Value.ToString(RationalFormat.UsePreceedingWhole) + "SM");
            }

            return ret.ToString();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current instance.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current instance.</returns>
        public override string ToString()
        {
            return this.ToInlineInfoString();
        }

        /// <summary>
        /// Proceed sanity check of inserted values.
        /// </summary>
        /// <param name="errors">Found errors.</param>
        /// <param name="warnings">Found warnings.</param>
        public virtual void SanityCheck(ref List<string> errors, ref List<string> warnings)
        {
            if (UseEUStyle && Distance.HasValue && Distance.Value > 10000)
                errors.Add("Maximum value for EU distance is 9999 meters. If more, use CAVOK instead.");
            else if (!UseEUStyle && Distance.HasValue && Distance.Value > 10)
                errors.Add("Maximum value for non-EU (USA) distance is 10 miles. If more, use SKC instead.");

            if (UseEUStyle && IsDevicesMinimumValue)
                warnings.Add("IsDeviceMinimumValue flag is not used in EU style and will be ignored.");
        }

        #endregion Inherited

    }
}
