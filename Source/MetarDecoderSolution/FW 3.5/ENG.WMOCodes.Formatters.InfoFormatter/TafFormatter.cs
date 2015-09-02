using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.WMOCodes.Formatters.InfoFormatter
{
  /// <summary>
  /// Converts taf report into human readable string.
  /// </summary>
  public class TafFormatter: ENG.WMOCodes.Formatters.ITafFormatter
  {
    #region ITafFormatter Members

    /// <summary>
    /// Returns taf formatted as text output.
    /// </summary>
    /// <param name="taf"></param>
    /// <returns></returns>
    public string ToString(Codes.Taf taf)
    {
      string ret = Formatter.ToString(taf);

      return ret;
    }

    #endregion
  }
}
