using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Represents information about basic visibility or CAVOK.
  /// </summary>
  public class Visibility : ICodeItem
  {
    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _UseEUStyle;
    ///<summary>
    /// Sets/gets if to use E-U style in ToMetar method. If E-U style, visibility is in meters, if false, visibility is in NM.
    ///</summary>
    public bool UseEUStyle
    {
      get
      {
        return (_UseEUStyle);
      }
      protected set
      {
        _UseEUStyle = value;
      }
    }

    /// <summary>
    /// Returns distance unit. If EUStyle, returns meters, if non-eu style, returns miles.
    /// </summary>
    public Common.eDistanceUnit DistanceUnit
    {
      get
      {
        if (UseEUStyle)
          return Common.eDistanceUnit.m;
        else
          return Common.eDistanceUnit.mi;
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsDevicesMinimumValue;
    ///<summary>
    /// Sets/gets if measured distance is equipments minimal measurable value.
    ///</summary>
    public bool IsDevicesMinimumValue
    {
      get
      {
        return (_IsDevicesMinimumValue);
      }
      protected set
      {
        _IsDevicesMinimumValue = value;
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsClear;
    ///<summary>
    /// Sets/gets clear visibility value. If true, most of other properties are omitted.
    ///</summary>
    public bool IsClear
    {
      get
      {
        return (_IsClear);
      }
      protected set
      {
        _IsClear = value;
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private Racional? _Distance;
    ///<summary>
    /// Sets/gets distance value. If style is E-U, value is in meters. If style is non-EU, value is in NM.
    ///</summary>
    public Racional? Distance
    {
      get
      {
        return (_Distance);
      }
      protected set
      {
        _Distance = value;
        if (value.HasValue)
          IsClear = false;
      }
    }




    #endregion Properties

    #region .ctor

    public Visibility()
    {
      this.SetClear(true);
    }

    #endregion .ctor

    #region Methods
    /// <summary>
    /// Sets "cloud and visibility ok" (CAVOK) weather (that is clear sky in EU style).
    /// </summary>
    public void SetCAVOK()
    {
      SetClear(false);
    }
    /// <summary>
    /// Sets "sky clear" (SKC) weather (that is clear sky in US style).
    /// </summary>
    public void SetSKC()
    {
      SetClear(true);
    }
    /// <summary>
    /// Sets clear sky. Parameter define if to use EU style (US otherwise).
    /// </summary>
    /// <param name="useEUStyle">True if EU style to use, false otherwise (that is US style).</param>
    public void SetClear(bool useEUStyle)
    {
      IsClear = true;
      UseEUStyle = useEUStyle;
    }
    /// <summary>
    /// Set distance in meters. Sets EU style.
    /// </summary>
    /// <param name="distance">Visibility distance.</param>
    public virtual void SetMeters(int distance)
    {
      UseEUStyle = true;
      Distance = distance;
      IsDevicesMinimumValue = false;
    }
    /// <summary>
    /// Sets visibility distance in miles. Sets US style (non EU style).
    /// </summary>
    /// <param name="distance">Distance</param>
    /// <param name="isDevicesMinimumValue">True if value is minimum of measuring equipment.</param>
    public void SetMiles(Racional distance, bool isDevicesMinimumValue)
    {
      IsDevicesMinimumValue = isDevicesMinimumValue;
      UseEUStyle = false;
      Distance = distance;
    }
    /// <summary>
    /// Sets visibility distance in miles. Sets US style (non EU style).
    /// </summary>
    /// <param name="distance">Distance</param>
    /// <param name="isDevicesMinimumValue">True if value is minimum of measuring equipment.</param>
    public void SetMiles(int distance, bool isDevicesMinimumValue)
    {
      SetMiles((Racional)distance, isDevicesMinimumValue);
    }

    /// <summary>
    /// Returns distance converted to selected unit. If distance has no value, null is returned.
    /// </summary>
    /// <param name="targetUnit"></param>
    /// <returns></returns>
    public double? GetDistanceIn(Common.eDistanceUnit targetUnit)
    {
      if (Distance.HasValue == false)
        return null;

      double ret = Common.Convert(Distance.Value.Value, DistanceUnit, targetUnit);

      return ret;
    }

    #endregion Methods

    #region Inherited

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public virtual string ToCode()
    {
      StringBuilder ret = new StringBuilder();

      if (IsClear)
      {
        if (UseEUStyle)
          ret.Append("SKC");
        else
          ret.Append("CAVOK");
      }
      else if (UseEUStyle)
      {
        ret.Append(((int)Distance.Value).ToString("0000"));
      }
      else
      {
        if (IsDevicesMinimumValue)
          ret.Append("M");
        ret.Append(Distance.Value.ToString(Racional.eToStringFormats.UsePreceedingWhole) + "SM");
      }

      return ret.ToString();
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
    public virtual void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      if (UseEUStyle && Distance.HasValue && (Distance.Value > 10000))
        errors.Add("Maximum value for EU distance is 9999 meters. If more, use CAVOK instead.");
      else if (!UseEUStyle && Distance.HasValue && (Distance.Value > 10))
        errors.Add("Maximum value for non-EU (USA) distance is 10 miles. If more, use SKC instead.");

      if (UseEUStyle && IsDevicesMinimumValue)
        warnings.Add("IsDeviceMinimumValue flag is not used in EU style and will be ignored.");
    }

    #endregion Inherited

  }
}
