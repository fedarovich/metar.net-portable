using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Types.DateTimeTypes;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Represents interval for trend TAF report starting with prefix (e.g. FM011200 )
  /// </summary>
  public class TafIntervalFM : TafInterval
  {
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private DayHourMinute _From = new DayHourMinute();
    ///<summary>
    /// Sets/gets From value. Default value is new DayHourMinute().
    ///</summary>
    public DayHourMinute From
    {
      get
      {
        return (_From);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException();
        _From = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TafIntervalFM"/> class.
    /// </summary>
    /// <param name="intervalFlag">The interval flag.</param>
    public TafIntervalFM(DayHourMinute intervalFlag)
    {
      this.From = intervalFlag;
    }

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public override string ToCode()
    {
      return "FM" + From.ToCode();
    }

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      // nothing to do here
    }
  }
}
