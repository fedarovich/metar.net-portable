using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem.Extensions;

namespace ENG.WMOCodes.Types.DateTimeTypes
{
  /// <summary>
  /// Represents day-time information when metar was presented.
  /// </summary>
  public class DayHourMinute : DateTimeType
  {
    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _Day = DateTime.Now.Day;
    ///<summary>
    /// Sets/gets Day value.
    ///</summary>
    public int Day
    {
      get
      {
        return (_Day);
      }
      set
      {
        if (!value.IsBetween(1, 31))
          throw new ArgumentOutOfRangeException("Value must be between 1 and 31.");
        _Day = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _Hour = DateTime.Now.Hour;
    ///<summary>
    /// Sets/gets Hour value.
    ///</summary>
    public int Hour
    {
      get
      {
        return (_Hour);
      }
      set
      {
        if (!value.IsBetween(0,24))
          throw new ArgumentOutOfRangeException("Value must be between 0 and 24.");
        _Hour = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _Minute = DateTime.Now.Minute;
    ///<summary>
    /// Sets/gets Minute value.
    ///</summary>
    public int Minute
    {
      get
      {
        return (_Minute);
      }
      set
      {
        if (!value.IsBetween(0, 59))
          throw new ArgumentOutOfRangeException("Value must be between 0 and 59.");
        _Minute = value;
      }
    }


    #endregion Properties

    #region .ctor
    /// <summary>
    /// Initializes a new Instance of ENG.Metar.Decoder.DayTime
    /// </summary>
    public DayHourMinute()
    { }
    /// <summary>
    /// Initializes a new Instance of ENG.Metar.Decoder.DayTime
    /// </summary>
    /// <param name="day"></param>
    /// <param name="hour"></param>
    /// <param name="minute"></param>
    public DayHourMinute(int day, int hour, int minute)
    {
      this.Day = day;
      this.Hour = hour;
      this.Minute = minute;
    }
    #endregion .ctor

    #region Inherited

#if INFO
   /// <summary>
    /// Returns item in text string.
    /// </summary>
    /// <param name="verbose">If false, only basic information is returned. If true, all (complex) information is provided.</param>
    /// <returns></returns>
public string ToInfo(bool verbose)
    {
      StringBuilder ret = new StringBuilder();

      ret.AppendSpaced("day " + Day.ToString() + ", " + Hour.ToString("0") + ":" + Minute.ToString("00"));

      return ret.ToString();
    }
#endif //INFO

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public override string ToCode()
    {
      return this.Day.ToString("00") + this.Hour.ToString("00") + this.Minute.ToString("00");
    }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current instance.
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> that represents the current instance.</returns>
    public override string ToString()
    {
      return ESystem.Extensions.ObjectExt.ToInlineInfoString(this);
    }

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      // nothing
    }

    #endregion Inherited

  }
}
