using System;
using System.Collections.Generic;
using System.Text;
using ENG.WMOCodes.Extensions;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Represents set of items defining the phenomena. E.g. +RAHZ.
    /// </summary>
    public class PhenomenonCollection : List<Phenomenon>, ICodeItem
    {
        #region Methods

        /// <summary>
        /// Decodes and adds new phenomenom from string (e.g. from +RAHZ).
        /// </summary>
        /// <param name="value">String value to decode adds.</param>
        public void Add(string value)
        {
            Phenomenon val = DecodePhenom(value);
            Add(val);
        }

        private Phenomenon DecodePhenom(string value)
        {
            if (value == "-")
                return Phenomenon.Light;
            if (value == "+")
                return Phenomenon.Heavy;
            Phenomenon ret = (Phenomenon)Enum.Parse(typeof(Phenomenon), value, false);
            return ret;
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

            foreach (var fItem in this)
            {
                if (fItem == Phenomenon.Heavy)
                    ret.Append("+");
                else if (fItem == Phenomenon.Light)
                    ret.Append("-");
                else
                    ret.Append(fItem);
            } // foreach (var fItem in this)

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
        public void SanityCheck(ref List<string> errors, ref List<string> warnings)
        {
            if (IsCorrectPhenomOrder())
            {
                warnings.Add(
                  "There is invalid order of phenoms. The are expected 5 disjoint groups (in brackets) containing " +
                  "(-,+,VC) (MI,BC,PR,DR,BL,SH,TS,FZ) (DZ,RA,SN,SG,IC,PL,GR,GS) (BR,FG,FU,VA,DU,SA,HZ) (PO,SQ,FC,SS,DS).");
            }
        }

        #endregion Inherited


        #region Private

        private bool IsCorrectPhenomOrder()
        {
            int lasVal = 0;
            foreach (Phenomenon fItem in this)
            {
                var currVal = (int)fItem;
                currVal = currVal / 100;
                if (currVal < lasVal)
                    return false;
                lasVal = currVal;
            } // foreach (ePhenom fItem in this)

            return true;
        }

        #endregion Private

    }
}
