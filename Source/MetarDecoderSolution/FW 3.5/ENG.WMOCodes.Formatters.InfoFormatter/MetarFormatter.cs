using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.WMOCodes.Formatters.InfoFormatter
{
  /// <summary>
  /// Converts metar report into information string.
  /// </summary>
  public partial class MetarFormatter : ENG.WMOCodes.Formatters.IMetarFormatter
  {
    /// <summary>
    /// Returns converted information about metar report.
    /// </summary>
    /// <param name="metar">The metar.</param>
    /// <returns>
    /// A <see cref="System.String"/> that represents this instance.
    /// </returns>
    public string ToString(Codes.Metar metar)
    {
      StringBuilder ret =
        Formatter.ToString(metar);

      return ret.ToString();
    }

  }
}
