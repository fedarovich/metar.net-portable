using System;
using System.Collections.Generic;
using System.Text;
using ENG.WMOCodes.Extensions;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Represents runway visibility information.
    /// </summary>
    /// <seealso cref="T:ENG.Metar.Decoder.MetarItem"/>
    public class RunwayVisibility : ICodeItem
    {

        #region Nested

        #endregion Nested

        #region Properties

        ///<summary>
        /// Sets/gets device measurement restriction. Null if not used.
        ///</summary>
        public DeviceMeasurementRestriction? DeviceMeasurementRestriction { get; set; } = null;


        ///<summary>
        /// Sets/gets visibility tendency value. Null if not used.
        ///</summary>
        public RunwayVisibilityTendency? Tendency { get; set; }


        ///<summary>
        /// Sets/gets Visibility value.
        ///</summary>
        public NonNegInt Distance { get; set; }

        ///<summary>
        /// Sets/gets VariableVisibility value. Null if visibility does not vary.
        ///</summary>
        public NonNegInt? VariableDistance { get; set; }

        ///<summary>
        /// Sets/gets Runway designator.
        ///</summary>
        public string Runway { get; set; }


        private DistanceUnit _unit = DistanceUnit.m;
        ///<summary>
        /// Sets/gets Unit value. Default value is Common.eDistanceUnit.m.
        ///</summary>
        public DistanceUnit Unit
        {
            get
            {
                return _unit;
            }
            set
            {
                if (value != DistanceUnit.m && value != DistanceUnit.ft)
                    throw new ArgumentException("Value for property Unit of RunwayVisibility have to be feet or meters.");
                _unit = value;
            }
        }

        #endregion Properties

        #region Inherited

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public string ToCode()
        {
            StringBuilder ret = new StringBuilder();

            ret.Append("R" + Runway + "/");
            if (DeviceMeasurementRestriction != null)
                ret.Append(DeviceMeasurementRestriction.Value);
            ret.Append(Distance.ToString("0000"));
            if (VariableDistance.HasValue)
                ret.Append("V" + VariableDistance.Value.ToString("0000"));
            if (Unit == DistanceUnit.ft)
                ret.Append("FT");
            else if (Tendency.HasValue)
                ret.Append(Tendency);

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

        #region MetarItem Members


        /// <summary>
        /// Proceed sanity check of inserted values.
        /// </summary>
        /// <param name="errors">Found errors.</param>
        /// <param name="warnings">Found warnings.</param>
        public void SanityCheck(ref List<string> errors, ref List<string> warnings)
        {
            if (string.IsNullOrEmpty(Runway))
                errors.Add("Runway number/sign is not set.");
        }

        #endregion

        #endregion Inherited

    }
}
