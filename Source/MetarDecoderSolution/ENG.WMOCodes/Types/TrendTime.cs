using System;
using System.Collections.Generic;
using ENG.WMOCodes.Extensions;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Represents trend time information.
    /// </summary>
    /// <seealso cref="T:ENG.Metar.Decoder.MetarItem"/>
    public class TrendTime : ICodeItem
    {
        #region Properties

        ///<summary>
        /// Sets/gets time type.
        ///</summary>
        public TrendTimeType Type { get; set; }

        private int _hour;
        ///<summary>
        /// Sets/gets Hour value.
        ///</summary>
        public int Hour
        {
            get
            {
                return _hour;
            }
            set
            {
                if (!value.IsBetween(0, 24))
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and 24.");
                _hour = value;
            }
        }
        /// <summary>
        /// </summary>

        private int _minute;
        ///<summary>
        /// Sets/gets Minute value.
        ///</summary>
        public int Minute
        {
            get
            {
                return _minute;
            }
            set
            {
                if (!value.IsBetween(0, 59))
                    throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and 59.");
                _minute = value;
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
            return Type + Hour.ToString("00") + Minute.ToString("00");
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
            if (Hour == 24 && Type != TrendTimeType.TL)
                warnings.Add("Hour value 24 should be used only with Type TL (until).");
            if (Hour == 24 && Minute != 0)
                errors.Add("Hour value 24 should be used only when minute value is 00.");
        }

        #endregion

        #endregion Inherited

    }
}
