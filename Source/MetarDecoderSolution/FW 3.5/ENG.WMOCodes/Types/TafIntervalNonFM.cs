using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem.Extensions;
using ENG.WMOCodes.Types.DateTimeTypes;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Represents TAF TREND interval information. Contains type (BECMG, TEMPO, etc.) and interval validity (e.g. 0220/0302).
  /// </summary>
  public class TafIntervalNonFM : TafInterval
  {
    /// <summary>
    /// Represents type of the information, e.g. BECMG, PROB30 TEMPO, etc. 
    /// </summary>
    public enum eType
    {
      /// <summary>
      /// Becoming
      /// </summary>
      BECMG,
      /// <summary>
      /// Temporarily
      /// </summary>
      TEMPO,
      /// <summary>
      /// Temporarily with 30% prob.
      /// </summary>
      TEMPO_PROB30,
      /// <summary>
      /// Temporarily with 40% prob.
      /// </summary>
      TEMPO_PROB40,
      /// <summary>
      /// Unknown, but used in US tafs.
      /// </summary>
      INTER,
      /// <summary>
      /// With 30% prob.
      /// </summary>
      PROB30,
      /// <summary>
      /// With 40% prob.
      /// </summary>
      PROB40
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eType _Type = eType.BECMG;
    ///<summary>
    /// Sets/gets Type value. Default value is eType.BECMG.
    ///</summary>
    public eType Type
    {
      get
      {
        return (_Type);
      }
      set
      {
        _Type = value;
      }
    }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private DayHourDayHour _Interval = new DayHourDayHour();
    ///<summary>
    /// Sets/gets Interval value. Default value is new FromDayTimeToDayTimeFlag().
    ///</summary>
    public DayHourDayHour Interval
    {
      get
      {
        return (_Interval);
      }
      set
      {
        _Interval = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TafIntervalNonFM"/> class.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="intervalFlag">The interval flag.</param>
    public TafIntervalNonFM(eType type, DayHourDayHour intervalFlag)
    {
      Type = type;
      Interval = intervalFlag;
    }

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public override string ToCode()
    {
      StringBuilder ret = new StringBuilder();

      ret.AppendPreSpaced(GetTypeCode());
      ret.AppendPreSpaced(Interval.ToCode());

      return ret.ToString();
    }

    /// <summary>
    /// Converts enum to string.
    /// </summary>
    /// <returns></returns>
    private string GetTypeCode()
    {
      string ret = null;
      switch (Type)
      {
        case eType.TEMPO_PROB30:
          ret = "PROB30 TEMPO";
          break;
        case eType.TEMPO_PROB40:
          ret = "PROB40 TEMPO";
          break;
        default:
          ret = Type.ToString();
          break;
      }

      return ret;
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
