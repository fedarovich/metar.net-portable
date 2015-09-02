using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem.Extensions;

namespace ENG.WMOCodes.Types.DateTimeTypes
{
  /// <summary>
  /// Represents date/time value defined by hour and minute.
  /// </summary>
  public class HourMinute
  {
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _Hour = DateTime.Now.Hour;
    ///<summary>
    /// Sets/gets Hour value. Default value is DateTime.Now.Hour.
    ///</summary>
    public int Hour
    {
      get
      {
        return (_Hour);
      }
      set
      {
        if (value.IsBetween(0, 23) == false) throw new ArgumentException("Value have to be between 0-23.");
        _Hour = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _Minute = DateTime.Now.Minute;
    ///<summary>
    /// Sets/gets Minute value. Default value is DateTime.Now.Minute.
    ///</summary>
    public int Minute
    {
      get
      {
        return (_Minute);
      }
      set
      {
        if (value.IsBetween(0, 60) == false) throw new ArgumentException("Value have to be between 0-60.");
        _Minute = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="HourMinute"/> class.
    /// </summary>
    public HourMinute() { }
    /// <summary>
    /// Initializes a new instance of the <see cref="HourMinute"/> class.
    /// </summary>
    /// <param name="hour">The hour.</param>
    /// <param name="minute">The minute.</param>
    public HourMinute(int hour, int minute) { Hour = hour; Minute = minute; }
  }
}
