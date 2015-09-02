using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ESystem.Extensions;
using ENG.WMOCodes.Types.Basic;
using ENG.WMOCodes.Types.DateTimeTypes;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Codes
{
  /// <summary>
  /// Represents metar.
  /// </summary>
  public class Metar : ICodeItem
  {
    #region Nested

    /// <summary>
    /// Types of metar. Now METAR only supported, SPECI not supported.
    /// </summary>
    ///<remarks>
    /// METAR is the name of the code for an aerodrome routine meteorological report. SPECI is the name of the code
    /// for an aerodrome special meteorological report. A METAR report and a SPECI report may have a trend forecast
    /// appended.
    ///</remarks>
    public enum eType
    {
      /// <summary>
      /// Metar type - Aerodrome routine meteorological report 
      /// </summary>
      METAR,
      /// <summary>
      /// Speci type - Aerodrome special meteorological report
      /// </summary>
      SPECI
    }

    #endregion Nested

    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eType _Type = eType.METAR;
    ///<summary>
    /// Sets/gets Type value.
    ///</summary>
    ///<remarks>
    ///The code name METAR or SPECI shall be included at the beginning of each individual
    ///report.
    ///When a deterioration of one weather element is accompanied by an improvement in
    ///another element (for example, lowering of clouds and an improvement in visibility), a single
    ///SPECI report shall be issued.
    ///</remarks>
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
    private string _ICAO = "----";
    ///<summary>
    /// Sets/gets ICAO value.
    ///</summary>
    ///<remarks>
    ///The identiﬁcation of the reporting station in each individual report shall be indicated by
    ///means of the ICAO location indicator.
    ///</remarks>
    public string ICAO
    {
      get
      {
        return (_ICAO);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("This value cannot be null.");
        _ICAO = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private DayHourMinute _Date = new DayHourMinute()
    {
      Day = DateTime.Today.Day,
      Hour = DateTime.Now.Hour,
      Minute = 0
    };
    ///<summary>
    /// Sets/gets Date value.
    ///</summary>
    ///<remarks>
    ///The day of the month and the time of observation in hours and minutes UTC followed, with-
    ///out a space, by the letter indicator Z shall be included in each individual METAR report.
    ///This group shall be included in each individual SPECI report. In SPECI reports, this group
    ///shall indicate the time of occurrence of the change(s) which justiﬁed the issue of the report.
    ///</remarks>
    public DayHourMinute Date
    {
      get
      {
        return (_Date);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("This value cannot be null.");
        _Date = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsAUTO = false;
    ///<summary>
    /// Sets/gets IsAUTO value. The optional code word AUTO shall be inserted before the wind group 
    /// when a report contains fully automated observations without human intervention. 
    ///</summary>
    ///<remarks>
    ///The optional code word AUTO shall be inserted before the wind group when a report con-
    ///tains fully automated observations without human intervention. The ICAO requirement is
    ///that all of the speciﬁed elements shall be reported. However, if any element cannot be
    ///observed, the group in which it would have been encoded shall be replaced by the appro-
    ///  priate number of solidi. The number of solidi depends on the number of symbolic
    ///  letters for the speciﬁc group which is not able to be reported; i.e. four for the visibility
    ///  group, two for the present weather group and three or six for the cloud group, as appro-
    ///  priate.
    ///  </remarks>
    public bool IsAUTO
    {
      get
      {
        return (_IsAUTO);
      }
      set
      {
        _IsAUTO = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private WindWithVariability _Wind = new WindWithVariability();
    ///<summary>
    /// Sets/gets Wind value, including VRB wind, wind varying, and gusts.
    ///</summary>
    public WindWithVariability Wind
    {
      get
      {
        return (_Wind);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("This value cannot be null.");
        _Wind = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private VisibilityForMetar _Visibility = new VisibilityForMetar();
    ///<summary>
    /// Sets/gets Visibility value, including directions.
    ///</summary>
    public VisibilityForMetar Visibility
    {
      get
      {
        return (_Visibility);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("This value cannot be null.");
        _Visibility = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private PhenomInfo _Phenomens = null;
    ///<summary>
    /// Sets/gets Phenomens value.
    ///</summary>
    public PhenomInfo Phenomens
    {
      get
      {
        return (_Phenomens);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("Property Phenomens cannot be null. Use empty object/collection instead.");
        _Phenomens = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private CloudInfoWithNCD _Clouds = null;
    ///<summary>
    /// Sets/gets Clouds value. Cannot be null.
    ///</summary>
    public CloudInfoWithNCD Clouds
    {
      get
      {
        return (_Clouds);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("Property Clouds cannot be null. Use empty object/collection instead.");
        _Clouds = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _Temperature = 20;
    ///<summary>
    /// Sets/gets Temperature value.
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
    private int _DewPoint = 10;
    ///<summary>
    /// Sets/gets DewPoint value.
    ///</summary>
    public int DewPoint
    {
      get
      {
        return (_DewPoint);
      }
      set
      {
        _DewPoint = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private PressureInfo _Pressure = new PressureInfo();
    ///<summary>
    /// Sets/gets Pressure value.
    ///</summary>
    public PressureInfo Pressure
    {
      get
      {
        return (_Pressure);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("This value cannot be null.");
        _Pressure = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private RePhenomInfo _RePhenomens = null;
    ///<summary>
    /// Sets/gets RePhenoms value. Cannot be null
    ///</summary>
    public RePhenomInfo RePhenomens
    {
      get
      {
        return (_RePhenomens);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("Property RePhenomens cannot be null. Use empty object/collection instead.");
        _RePhenomens = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private WindShearInfo _WindShears = null;
    ///<summary>
    /// Sets/gets WindShears value. Cannot be null.
    ///</summary>
    public WindShearInfo WindShears
    {
      get
      {
        return (_WindShears);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("Property WindShears cannot be null. Use empty object/collection instead.");
        _WindShears = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private RunwayConditionInfo _RunwayConditions = null;
    ///<summary>
    /// Sets/gets RunwayConditions value. Cannot be null.
    ///</summary>
    public RunwayConditionInfo RunwayConditions
    {
      get
      {
        return (_RunwayConditions);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("Property Runways cannot be null. Use empty object/collection instead.");
        _RunwayConditions = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private TrendInfoForMetar _Trend = new TrendInfoForMetar();
    ///<summary>
    /// Sets/gets Trend value. Allways value is in here, when no info found, trend type is null.
    /// Trend in metar is required, (NOSIG text is minimum).
    ///</summary>
    public TrendInfoForMetar Trend
    {
      get
      {
        return (_Trend);
      }
      set
      {
        _Trend = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private string _Remark = null;
    ///<summary>
    /// Sets/gets Remark value (without RMK prefix), or null if not presented in metar.
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
    private bool _IsCorrected = false;
    ///<summary>
    /// Sets/gets IsCorrected value. Default value is false.
    ///</summary>
    public bool IsCorrected
    {
      get
      {
        return (_IsCorrected);
      }
      set
      {
        _IsCorrected = value;
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsMissing = false;
    ///<summary>
    /// Sets/gets IsMissing value. Default value is false.
    ///</summary>
    public bool IsMissing
    {
      get
      {
        return (_IsMissing);
      }
      set
      {
        _IsMissing = value;
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private Common.eSeaState? _SeaState = null;
    ///<summary>
    /// Sets/gets SeaState value. Default value is null.
    ///</summary>
    public Common.eSeaState? SeaState
    {
      get
      {
        return (_SeaState);
      }
      set
      {
        _SeaState = value;
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int? _SeaSurfaceTemperature = null;
    ///<summary>
    /// Sets/gets SeaSurfaceTemperature value. Default value is null.
    ///</summary>
    public int? SeaSurfaceTemperature
    {
      get
      {
        return (_SeaSurfaceTemperature);
      }
      set
      {
        _SeaSurfaceTemperature = value;
      }
    }

    /// <summary>
    /// Calculates humidity. Very rough aproximation.
    /// </summary>
    public double Humidity
    {
      get
      {
        return 100 - 5d * (Temperature - DewPoint);
      }
    }

    #endregion Properties

    #region .ctor
    #endregion .ctor

    #region Static methods

    /// <summary>
    /// Creates metar instance from string.
    /// </summary>
    /// <param name="metarString"></param>
    /// <returns></returns>
    [Obsolete("Not supported.")]
    public static Metar Create(string metarString)
    {
      throw new NotImplementedException();
      //Metar ret = null;

      //ret = new MetarDecoder().Decode(metarString);

      //return ret;
    }

    /// <summary>
    /// Checks if metar string is recognizable by this class.
    /// Returns true if success, otherwise false, if failed, and in error variable is error description.
    /// </summary>
    /// <param name="metarString"></param>
    /// <param name="error"></param>
    /// <returns></returns>
    [Obsolete("No more supported. Use decoder classes instead.")]
    public static bool CheckMetarString(string metarString, out string error)
    {
      throw new NotSupportedException();
      //error = null;
      //bool ret = false;
      //try
      //{
      //  Metar m = Create(metarString);
      //  ret = true;
      //} // try
      //catch (Exception ex)
      //{
      //  error = ex.Message;
      //  ret = false;
      //} // catch (Exception ex)
      //return ret;
    }

    #endregion Static methods

    #region Consts

    #endregion Consts

    #region Internal methods

    #endregion Internal methods    

    #region Inherited

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public string ToCode()
    {
      StringBuilder ret = new StringBuilder();

      //    ENG.Metar.Decoder.Metar x =
      //ENG.Metar.Decoder.Metar.Create(
      //"METAR LKPR 312300Z VRB03KT 0800 R08/0300 R09/0400D -SN +RABR FEW020CB OVC040TCU M01/M10 Q0997 " +
      //"RERA RESN WS RWY24C RWY04 R04C/012345 R22/////// " +
      //"TEMPO FM1010 TL2020 AT1330 VRB03G20KT 010V040 M1/8SM RA SNBR FEW040 OVC050TCU RMK HOTOVO");

      ret.AppendPreSpaced(this.Type.ToString());
      ret.AppendPreSpaced(this.ICAO);
      ret.AppendPreSpaced(this.Date.ToCode() + "Z");
      if (this.IsAUTO) ret.AppendPreSpaced("AUTO");
      ret.AppendPreSpaced(this.Wind.ToCode());
      ret.AppendPreSpaced(this.Visibility.ToCode());
      if (this.Phenomens != null)
        ret.AppendPreSpaced(this.Phenomens.ToCode());
      if (this.Clouds != null)
        ret.AppendPreSpaced(this.Clouds.ToCode());
      ret.AppendPreSpaced(IntToMetarString(this.Temperature) + "/" + IntToMetarString(this.DewPoint));
      ret.AppendPreSpaced(this.Pressure.ToCode());
      if (this.RePhenomens != null)
        ret.AppendPreSpaced(this.RePhenomens.ToCode());
      if (this.WindShears != null)
        ret.AppendPreSpaced(this.WindShears.ToCode());
      if (this.RunwayConditions != null)
        ret.AppendPreSpaced(this.RunwayConditions.ToCode());
      if (this.Trend != null)
        ret.AppendPreSpaced(this.Trend.ToCode());
      if (!string.IsNullOrEmpty(this.Remark))
        ret.AppendPreSpaced("RMK " + this.Remark);

      return ret.ToString().TrimEnd();
    }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current instance.
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> that represents the current instance.</returns>
    public override string ToString()
    {
      return ESystem.Extensions.ObjectExt.ToInlineInfoString(this);
    }

    #region MetarItem Members

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      Date.SanityCheck(ref errors, ref warnings);
      Wind.SanityCheck(ref errors, ref warnings);
      Visibility.SanityCheck(ref errors, ref warnings);
      if (Phenomens != null)
      {
        if (Phenomens.IsEmpty()) warnings.Add("Phenomens are used but are empty.");
        Phenomens.SanityCheck(ref errors, ref warnings);
      }
      if (Clouds != null)
      {
        if (Clouds.IsEmpty()) warnings.Add("Clouds are used but are empty.");
        Clouds.SanityCheck(ref errors, ref warnings);
      }
      Pressure.SanityCheck(ref errors, ref warnings);
      if (RePhenomens != null)
      {
        if (RePhenomens.IsEmpty()) warnings.Add("Re-phenomens are used but are empty.");
        RePhenomens.SanityCheck(ref errors, ref warnings);
      }
      if (WindShears != null)
      {
        if (WindShears.IsEmpty()) warnings.Add("Windshears are used but are empty.");
        WindShears.SanityCheck(ref errors, ref warnings);
      }
      if (RunwayConditions != null)
      {
        if (RunwayConditions.IsEmpty()) warnings.Add("Runway conditions are used but are empty.");
        RunwayConditions.SanityCheck(ref errors, ref warnings);
      }
      if (Trend != null)
        Trend.SanityCheck(ref errors, ref warnings);
    }

    #endregion MetarItem Members

    #endregion Inherited

    #region Private methods
    private static string IntToMetarString(int p)
    {
      if (p <= 0)
        return "M" + (-p).ToString("00");
      else
        return p.ToString("00");
    }

    #endregion Private methods
  }
}
