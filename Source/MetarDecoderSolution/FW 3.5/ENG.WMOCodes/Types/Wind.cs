using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem.Extensions;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Represents information about wind.
  /// </summary>
  /// <seealso cref="T:ENG.Metar.Decoder.MetarItem"/>
  /// <remarks>
  /// The mean true direction in degrees rounded off to the nearest 10 degrees from which the
  /// wind is blowing and the mean speed of the wind over the 10-minute period immediately
  /// preceding the observation shall be reported for dddff followed, without a space, by one of
  /// the abbreviations KMH, KT  or MPS, to specify the unit used for reporting wind speed.
  /// Values of wind direction less than 100° shall be preceded by 0 and a wind from true north
  /// shall be reported as 360. Values of wind speed less than 10 units shall be preceded by 0.
  /// However, when the 10-minute period includes a marked discontinuity in the wind charac-
  /// teristics, only data after the discontinuity shall be used for obtaining mean wind speed and
  /// maximum gust values, and mean wind direction and variations of the wind direction, hence
  /// the time interval in these circumstances shall be correspondingly reduced.
  /// </remarks>
  public class Wind : ICodeItem
  {
    #region Nested

    /// <summary>
    /// This enumeration is obsolete. Use <see cref="Common.eDistanceUnit">eDistanceUnit</see>
    /// or <see cref="Common.eSpeedUnit">eSpeedUnit</see> enumerations.
    /// </summary>
    [Obsolete("This type is obsolete and is no more used. Use eDistanceUnit or eSpeedUnit enumerations.", true)]
    public enum eUnit
    {
      /// <summary>
      /// Obsolete
      /// </summary>
      ObsoleteUseDistanceUnitEnumeration
    }
    #endregion Nested

    #region Properties

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private Common.eSpeedUnit _Unit = Common.eSpeedUnit.kt;
    ///<summary>
    /// Sets/gets unit of wind speed.
    ///</summary>
    public Common.eSpeedUnit Unit
    {
      get
      {
        return (_Unit);
      }
      set
      {
        _Unit = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int? _Direction = 0;
    ///<summary>
    /// Sets/gets wind direction value.
    ///</summary>
    public int? Direction
    {
      get
      {
        return (_Direction);
      }
      set
      {
        if ((value.HasValue) && (!value.Value.IsBetween(0, 360)))
          throw new ArgumentOutOfRangeException("Value must be between 0 and 360.");
        _Direction = value;
      }
    }

    ///<summary>
    /// Sets/gets true if wind si variable (VRB). Do not confuse with wind variability!
    ///</summary>
    ///<remarks>
    ///In the case of variable wind direction, ddd shall be encoded as VRB when the mean wind
    ///speed is less than 3 knots (2 m s–1 or 6 km h–1). A variable wind at higher speeds shall be
    ///reported only when the variation of wind direction is 180° or more or when it is impossible
    ///to determine a single wind direction, for example when a thunderstorm passes over the
    ///aerodrome.
    ///</remarks>
    public bool IsVariable
    {
      get
      {
        return (Direction == null);
      }
      set
      {
        if (value)
          Direction = null;
        else
          throw new ArgumentException("To unset variable wind insert value (heading) into Direction property.");
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private NonNegInt _Speed = 0;
    ///<summary>
    /// Sets/gets wind speed.
    ///</summary>
    public NonNegInt Speed
    {
      get
      {
        return (_Speed);
      }
      set
      {
        if (value > 200)
          throw new Exception("Unable to capture greater speed than 200kts.");
        _Speed = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private NonNegInt? _GustSpeed = null;
    ///<summary>
    /// Sets/gets GustSpeed value. Null if no gusts defined.
    ///</summary>
    ///<remarks>
    ///If, during the 10-minute period preceding the observation, the maximum wind gust speed
    ///exceeds the mean speed by 10 knots (5 m s–1 or 20 km h–1) or more, this maximum speed
    ///shall be reported as Gfmfm immediately after dddff, followed immediately, without a space,
    ///by one of the abbreviations KMH, KT or MPS to specify the units used for reporting wind
    ///speed. Otherwise the element Gfmfm shall not be included.
    ///</remarks>
    public NonNegInt? GustSpeed
    {
      get
      {
        return (_GustSpeed);
      }
      set
      {
        if (value > 200)
          throw new Exception("Unable to capture greater speed than 200kts.");
        _GustSpeed = value;
      }
    }

    /// <summary>
    /// Return true if wind is calm.
    /// </summary>
    public bool IsCalm
    {
      get
      {
        return (!GustSpeed.HasValue && Speed == 0);
      }
    }

    /// <summary>
    /// Returns direction of the wind as N/S/E/W/... <seealso cref="Common.eDirection"/>.
    /// Cannot return value for calm or variable wind.
    /// </summary>
    /// <exception cref="Exception">If wind is calm or wind is variable and therefore has no direction value.</exception>
    public Common.eDirection WorldDirection
    {
      get
      {
        if (this.Direction.HasValue == false || this.Speed == 0)
          throw new Exception("Cannot return direction. Wind direction is not specified, probably calm or variable.");
        Common.eDirection ret = Common.HeadingToeDirection(this.Direction.Value);
        return ret;
      }
    }
    #endregion Properties

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public virtual string ToCode()
    {
      StringBuilder ret = new StringBuilder();

      if (IsVariable)
        ret.Append("VRB");
      else
        ret.Append(Direction.Value.ToString("000"));
      ret.Append(Speed.ToString("00"));
      if (GustSpeed.HasValue)
        ret.Append("G" + GustSpeed.Value.ToString("00"));
      ret.Append(Unit.ToString().ToUpper());      

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
      if (GustSpeed.HasValue)
      {
        if (GustSpeed.Value < (Speed + 10))
          errors.Add("Wind gust speed should be reported only if is at least 10KT faster than mean wind speed.");
      }
    }


    /// <summary>
    /// Gets new instance of calm wind.
    /// </summary>
    /// <value>The calm.</value>
    public static Wind Calm
    {
      get
      {
        return new Wind() { Direction = null, Speed = 0, GustSpeed = null };
      }
    }

  }
}
