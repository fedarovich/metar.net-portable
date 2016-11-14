using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Types.DateTimeTypes;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Abstract. Represents trend temperature for trend TAF report.
  /// </summary>
  public abstract class TemperatureExtreme : ICodeItem
  {
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _Temperature = 0;
    ///<summary>
    /// Sets/gets Temperature value. Default value is 0.
    ///</summary>
    public int Temperature
    {
      get
      {
        return (_Temperature);
      }
      set
      {
        _Temperature = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private DayHour _Time = new DayHour();
    ///<summary>
    /// Sets/gets Time value. Default value is new DayHourFlag().
    ///</summary>
    public DayHour Time
    {
      get
      {
        return (_Time);
      }
      set
      {
        _Time = value;
      }
    }

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
