using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.WMOCodes.Formatters
{
  /// <summary>
  /// This interface defines method to implement class converting metar report to some different string.
  /// </summary>
  public interface IMetarFormatter
  {
    /// <summary>
    /// Converts metar object to information string.
    /// </summary>
    /// <param name="metar"></param>
    /// <returns></returns>
    string ToString(ENG.WMOCodes.Codes.Metar metar);
  }
}
