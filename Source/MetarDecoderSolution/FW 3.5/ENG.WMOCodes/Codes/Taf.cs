using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem.Extensions;
using ENG.WMOCodes.Types.DateTimeTypes;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Codes
{
  /// <summary>
  /// Represents TAF report representing the weather forecast.
  /// </summary>
  public class Taf : TrendReport
  {
    #region Nested

    /// <summary>
    /// Enum used to describe special flag of TAF report.
    /// </summary>
    [Flags()]
    public enum eTafFlag
    {
      /// <summary>
      /// No flag
      /// </summary>
      None = 0,
      /// <summary>
      /// Ammended, keyword AMD in TAF report.
      /// </summary>
      Ammended,
      /// <summary>
      /// Cancelled, keyword CNL in TAF report.
      /// </summary>
      Cancelled,
      /// <summary>
      /// Corrected, keyword COR in TAF report.
      /// </summary>
      Corrected,
      /// <summary>
      /// Missing, keyword NIL in TAF report
      /// </summary>
      Missing
    }

    #endregion Nested

    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eTafFlag _TafType = eTafFlag.None;

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private List<TrendInfoForTaf> _Trends = new List<TrendInfoForTaf>();
    ///<summary>
    /// Sets/gets SubReports value.
    ///</summary>
    public List<TrendInfoForTaf> Trends
    {
      get
      {
        return (_Trends);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("Property " + Trends +
            " cannot be null. Invoce Clear() method or set new empty instance.");

        _Trends = value;
      }
    }

    /// <summary>
    /// Gets/sets if TAF is ammended. <seealso cref="eTafFlag"/>
    /// </summary>
    public bool IsAmmended
    {
      get
      {
        return GetFlag(eTafFlag.Ammended);
      }
      set
      {
        UpdateFlag(eTafFlag.Ammended, value);
      }
    }
    /// <summary>
    /// Gets/sets if TAF is cancelled. <seealso cref="eTafFlag"/>
    /// </summary>
    public bool IsCancelled
    {
      get
      {
        return GetFlag(eTafFlag.Cancelled);
      }
      set
      {
        UpdateFlag(eTafFlag.Cancelled, value);
      }
    }
    /// <summary>
    /// Gets/sets if TAF is corrected. <seealso cref="eTafFlag"/>
    /// </summary>
    public bool IsCorrected
    {
      get
      {
        return GetFlag(eTafFlag.Corrected);
      }
      set
      {
        UpdateFlag(eTafFlag.Corrected, value);
      }
    }
    /// <summary>
    /// Gets/sets if TAF is missing. <seealso cref="eTafFlag"/>
    /// </summary>
    public bool IsMissing
    {
      get
      {
        return GetFlag(eTafFlag.Missing);
      }
      set
      {
        UpdateFlag(eTafFlag.Missing, value);
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private string _ICAO;
    ///<summary>
    /// Sets/gets ICAO value.
    ///</summary>
    public string ICAO
    {
      get
      {
        return (_ICAO);
      }
      set
      {
        _ICAO = value;
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private DayHourMinute _DayTime;
    ///<summary>
    /// Sets/gets DayTime value.
    ///</summary>
    public DayHourMinute DayTime
    {
      get
      {
        return (_DayTime);
      }
      set
      {
        _DayTime = value;
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private DayHourDayHour _Period;
    ///<summary>
    /// Sets/gets Period value.
    ///</summary>
    public DayHourDayHour Period
    {
      get
      {
        return (_Period);
      }
      set
      {
        _Period = value;
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private string _Remark = "";
    ///<summary>
    /// Sets/gets Remark value. Default value is "".
    ///</summary>
    public string Remark
    {
      get
      {
        return (_Remark);
      }
      set
      {
        _Remark = value;
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private TemperatureExtremeTX _MaxTemperature = null;
    ///<summary>
    /// Sets/gets MaxTemperature value. Default value is null.
    ///</summary>
    public TemperatureExtremeTX MaxTemperature
    {
      get
      {
        return (_MaxTemperature);
      }
      set
      {
        _MaxTemperature = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private TemperatureExtremeTN _MinTemperature = null;
    ///<summary>
    /// Sets/gets MinTemperature value. Default value is null.
    ///</summary>
    public TemperatureExtremeTN MinTemperature
    {
      get
      {
        return (_MinTemperature);
      }
      set
      {
        _MinTemperature = value;
      }
    }

    #endregion Properties

    /// <summary>
    /// Obsolete. Use decoders to decode TAF report from TAF code.
    /// </summary>
    /// <param name="taf"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException">This method is obsolete and is not supported.</exception>
    [Obsolete("Not supported.")]
    public static Taf Create(string taf)
    {
      throw new NotSupportedException();
    }

    private bool GetFlag(eTafFlag eTafType)
    {
      return (_TafType & eTafType) == eTafType;
    }

    private void UpdateFlag(eTafFlag eTafType, bool value)
    {
      if (value)
        _TafType = _TafType | eTafType;
      else
        _TafType = _TafType & ~eTafType;
    }

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public override string ToCode()
    {
      StringBuilder ret = new StringBuilder();

      ret.Append("TAF");
      if (IsCorrected) ret.AppendPreSpaced("COR");
      if (IsAmmended) ret.AppendPreSpaced("AMD");
      ret.AppendPreSpaced(this.ICAO);
      ret.AppendPreSpaced(this.DayTime.ToCode() + "Z");
      if (IsMissing) ret.AppendPreSpaced("NIL");

      if (IsMissing == false)
      {
        ret.AppendPreSpaced(Period.ToCode());
        if (IsCancelled) ret.AppendPreSpaced("CNL");
        ret.AppendPreSpaced(base.ToCode());
        if (MaxTemperature != null) ret.AppendPreSpaced(MaxTemperature.ToCode());
        if (MinTemperature != null) ret.AppendPreSpaced(MinTemperature.ToCode());

        foreach (var fItem in this.Trends)
        {
          ret.AppendPreSpaced(fItem.ToCode());
        } // foreach (var fItem in this.SubReports)
      }

      return ret.ToString();
    }

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      base.SanityCheck(ref errors, ref warnings);
    }
  }
}