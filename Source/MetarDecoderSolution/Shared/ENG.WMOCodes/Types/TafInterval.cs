using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Represents definition of trend TAF interval - that means from-to time when trend is active.
  /// </summary>
  public abstract class TafInterval : ICodeItem
  {
    #region ICodeItem Members

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public abstract string ToCode();

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public abstract void SanityCheck(ref List<string> errors, ref List<string> warnings);

    #endregion
  }
}
