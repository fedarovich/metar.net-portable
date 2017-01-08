using System.Collections.Generic;
using System.Text;
using ENG.WMOCodes.Extensions;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Represents trend report for metar/taf codes, containing only(!) wind, visibility, phenomens and clouds. 
    /// This class is usually not used directly, but inherited and descendants are used.
    /// </summary>
    public class TrendReport : ICodeItem
    {
        #region Properties

        ///<summary>
        /// Sets/gets Wind value. Default value is Wind.Calm.
        ///</summary>
        public Wind Wind { get; set; } = null;


        ///<summary>
        /// Sets/gets Visibility value. Default value is null.
        ///</summary>
        public Visibility Visibility { get; set; } = null;


        ///<summary>
        /// Sets/gets Phenomena value. Default value is null.
        ///</summary>
        public PhenomInfoWithNSW Phenomena { get; set; } = null;


        ///<summary>
        /// Sets/gets Clouds value. Default value is null.
        ///</summary>
        public CloudInfo Clouds { get; set; } = null;

        #endregion Properties

        #region ICodeItem Members

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public virtual string ToCode()
        {
            StringBuilder ret = new StringBuilder();

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
        /// Proceed sanity check of inserted values.
        /// </summary>
        /// <param name="errors">Found errors.</param>
        /// <param name="warnings">Found warnings.</param>
        public virtual void SanityCheck(ref List<string> errors, ref List<string> warnings)
        {
            Wind?.SanityCheck(ref errors, ref warnings);
            Visibility?.SanityCheck(ref errors, ref warnings);
            Phenomena?.SanityCheck(ref errors, ref warnings);
            Clouds?.SanityCheck(ref errors, ref warnings);
        }

        #endregion
    }
}
