using System.Collections.Generic;
using System.Text;
using ENG.WMOCodes.Extensions;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Represents information about trend. To mark trend as not used. set null value into property type.
    /// </summary>
    /// <seealso cref="T:ENG.Metar.Decoder.MetarItem"/>
    public class TrendInfoForMetar : TrendReport
    {
        #region Properties

        ///<summary>
        /// Sets/gets Type value.
        ///</summary>
        public MetarTrendType Type { get; set; } = MetarTrendType.NOSIG;

        ///<summary>
        /// Sets/gets Dates value.
        ///</summary>
        public TrendTimeInfo Times { get; set; } = new TrendTimeInfo();

        #endregion Properties

        #region Inherited

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public override string ToCode()
        {
            StringBuilder ret = new StringBuilder();

            ret.AppendPreSpaced(Type.ToString());
            Times.ForEach(
              i => ret.AppendPreSpaced(i.ToCode()));
            if (Wind != null)
                ret.AppendPreSpaced(Wind.ToCode());
            if (Visibility != null)
                ret.AppendPreSpaced(Visibility.ToCode());
            if (Phenomena != null)
                ret.AppendPreSpaced(Phenomena.ToCode());
            if (Clouds != null)
                ret.AppendPreSpaced(Clouds.ToCode());

            return ret.ToString().TrimEnd();
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
        public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
        {
            Wind?.SanityCheck(ref errors, ref warnings);
            Visibility?.SanityCheck(ref errors, ref warnings);
            Phenomena?.SanityCheck(ref errors, ref warnings);
            Clouds?.SanityCheck(ref errors, ref warnings);
        }

        #endregion

        #endregion Inherited
    }
}
