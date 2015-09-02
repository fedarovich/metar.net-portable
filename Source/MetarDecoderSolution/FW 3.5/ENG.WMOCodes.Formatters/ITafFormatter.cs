using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.WMOCodes.Formatters
{
  /// <summary>
  /// This interface defines method to implement class converting taf report to some different string.
  /// </summary>
  public interface ITafFormatter
  {
    /// <summary>
    /// Converts taf report into information string.
    /// </summary>
    /// <param name="taf">The taf.</param>
    /// <returns>
    /// A <see cref="System.String"/> that represents this instance.
    /// </returns>
    string ToString(ENG.WMOCodes.Codes.Taf taf);
  }
}
