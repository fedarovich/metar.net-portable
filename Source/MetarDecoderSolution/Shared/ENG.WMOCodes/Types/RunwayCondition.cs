using System.Collections.Generic;
using System.Text;
using ENG.WMOCodes.Extensions;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Represents info about runway conditions (contamination, depth and braking action).
    /// </summary>
    /// <seealso cref="T:ENG.Metar.Decoder.MetarItem"/>
    public class RunwayCondition : ICodeItem
    {
        #region Properties

        ///<summary>
        /// Sets/gets if runway is cleared right now.
        ///</summary>
        public bool IsCleared { get; set; }

        ///<summary>
        /// Sets/gets Runway name/sign.
        ///</summary>
        public string Runway { get; set; }

        /// <summary>
        /// True if definition is for all runways. Is true, if runway condition runway id is equal to 88.
        /// </summary>
        /// <value></value>
        public bool IsForAllRunways => Runway == "88";

        /// <summary>
        /// True if information is obsolete from last report. Is true, if runway condition runway id is equal to 99.
        /// </summary>
        public bool IsObsolete => Runway == "99";

        ///<summary>
        /// Sets/gets deposit of runway. Null if unknown/not reported (that is / in metar).
        ///</summary>
        public RunwayDeposit? Deposit { get; set; }

        ///<summary>
        /// Sets/gets contamination level of runway. Null if unknown/not reported (that is / in metar).
        ///</summary>
        public RunwayContamination? Contamination { get; set; }

        ///<summary>
        /// Sets/gets contamination depth on runway. Null if unknown/not reported (that is // in metar).
        ///</summary>
        public RunwayContaminationDepth? Depth { get; set; }

        ///<summary>
        /// Sets/gets friction/braking effect of runway. Null if unknown/not reported (that is // in metar).
        ///</summary>
        public RunwayFriction? Friction { get; set; }

        #endregion Properties

        #region Inherited

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public string ToCode()
        {
            StringBuilder ret = new StringBuilder();

            ret.Append(
              "R" + Runway + "/");
            if (IsCleared)
                ret.Append("CLDR//");
            else
            {
                ret.Append(
                  (Deposit.HasValue ? ((int)Deposit.Value).ToString() : "/"));
                ret.Append(
                      (Contamination.HasValue ? ((int)Contamination.Value).ToString() : "/"));
                ret.Append(
                (Depth.HasValue ? ((int)Depth.Value).ToString("00") : "//"));
                ret.Append(
                (Friction.HasValue ? ((int)Friction.Value).ToString("00") : "//"));
            }

            return ret.ToString();
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
            if (string.IsNullOrEmpty(Runway))
                errors.Add("Runway number/sign is not set.");

            if (Contamination.HasValue)
            {
                switch (Contamination.Value)
                {
                    case RunwayContamination.Reserved0:
                    case RunwayContamination.Reserved3:
                    case RunwayContamination.Reserved4:
                    case RunwayContamination.Reserved6:
                    case RunwayContamination.Reserved7:
                    case RunwayContamination.Reserved8:
                        warnings.Add("This runway contamination value is reserved and should not be used.");
                        break;
                }
            }
        }

        #endregion

        #endregion Inherited

    }
}

