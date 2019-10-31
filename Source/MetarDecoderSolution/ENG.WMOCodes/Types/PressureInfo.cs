using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using ENG.WMOCodes.Extensions;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Represents pressure info.
    /// </summary>
    public class PressureInfo : ICodeItem
    {
        #region Properties

        ///<summary>
        /// Sets/gets Unit value.
        ///</summary>
        public PressureUnit Unit { get; set; } = PressureUnit.hPa;

        ///<summary>
        /// Sets/gets pressure value.
        ///</summary>
        public double Value { get; private set; } = 1013;

        ///<summary>
        /// Sets/gets QNH value.
        ///</summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public int QNH
        {
            get { return (int)Value; }
            set { Value = value; }
        }

        ///<summary>
        /// Sets/gets inHg value.
        ///</summary>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public double inHg
        {
            get { return (Value / 33.86); }
            set { Value = value * 33.86; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Sets pressure value.
        /// </summary>
        /// <param name="value">Pressure value.</param>
        /// <param name="unit">Pressure unit.</param>
        public void Set(double value, PressureUnit unit)
        {
            Unit = unit;
            if (value <= 0)
                throw new ArgumentException("Pressure value cannot be less or equal 0.", "value");
            if (unit == PressureUnit.hPa)
                QNH = (int)value;
            else
                inHg = value;
        }

        #endregion Methods

        #region Inherited

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public string ToCode()
        {
            StringBuilder ret = new StringBuilder();

            if (Unit == PressureUnit.hPa)
                ret.Append("Q" + QNH.ToString("0000"));
            else
                ret.Append(
                  "A" + (inHg * 100).ToString("0000"));

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
            if (!Value.IsBetween(800, 1100))
            {
                if (Unit == PressureUnit.hPa)
                    warnings.Add("Current pressure value in hPA is not probably correct (" + Value + ")");
                else if (Unit == PressureUnit.inHg)
                    warnings.Add("Current pressure value in inHg is not probably correct (" + inHg + ")");
            }
        }

        #endregion

        #endregion Inherited

    }
}
