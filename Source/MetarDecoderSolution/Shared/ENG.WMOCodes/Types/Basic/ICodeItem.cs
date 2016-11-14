using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.WMOCodes.Types.Basic
{
  /// <summary>
  /// Common interface to describe metar element.
  /// </summary>
  public interface ICodeItem
  {
    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    string ToCode();
    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    void SanityCheck(ref List<string> errors, ref List<string> warnings); 
  }
}

