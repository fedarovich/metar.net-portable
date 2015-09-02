using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem.Extensions;

namespace ENG.WMOCodes.Types.DateTimeTypes
{
  /// <summary>
  /// Represents day/time value defined by day-hour.
  /// </summary>
  public class DayHour : DateTimeTypes.DateTimeType
  {
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _Day = DateTime.Now.Day;
    ///<summary>
    /// Sets/gets Day value. Default value is current day.
    ///</summary>
    public int Day
    {
      get
      {
        return (_Day);
      }
      set
      {
        if (value.IsBetween(1, 31) == false) throw new ArgumentException("Value have to be between 1-31.");
        _Day = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _Hour = DateTime.Now.Hour;
    ///<summary>
    /// Sets/gets Hour value. Default value is current hour.
    ///</summary>
    public int Hour
    {
      get
      {
        return (_Hour);
      }
      set
      {
        if (value.IsBetween(0, 24) == false) throw new ArgumentException("Value have to be between 0-24.");
        _Hour = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DayHour"/> class.
    /// </summary>
    /// <param name="day">The day.</param>
    /// <param name="hour">The hour.</param>
    public DayHour(int day, int hour)
    {
      this.Day = day;
      this.Hour = hour;
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="DayHour"/> class.
    /// </summary>
    public DayHour() { }

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public override string ToCode()
    {
      return Day.ToString("00") + Hour.ToString("00");
    }

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      // nothing to do
    }

  }
}
