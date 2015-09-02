using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Describes standard visibility (<seealso cref="Visibility"/>) extended by direction-visibility and runway visibility.
  /// </summary>
  /// <seealso cref="T:ENG.Metar.Decoder.TrendVisibility"/>
  public class VisibilityForMetar : Visibility
  {

    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private Common.eDirection? _DirectionSpecification;
    ///<summary>
    /// Sets/gets directory specification value. (e.g. 3000NE). Null if not used.
    ///</summary>
    public Common.eDirection? DirectionSpecification
    {
      get
      {
        return (_DirectionSpecification);
      }
      set
      {
        _DirectionSpecification = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private Racional? _OtherDistance;
    ///<summary>
    /// Sets/gets other direction measured distance (e.g. numeric second part in 3000NE 1200S). Null if not used.
    ///</summary>
    public Racional? OtherDistance
    {
      get
      {
        return (_OtherDistance);
      }
      set
      {
        _OtherDistance = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private Common.eDirection? _OtherDirectionSpecification;
    ///<summary>
    /// Sets/gets other measured distance's direction (e.g. postfix of second part in 3000NE 1200S). Null if not used.
    /// Must be used when OtherDistance is used.
    ///</summary>
    public Common.eDirection? OtherDirectionSpecification
    {
      get
      {
        return (_OtherDirectionSpecification);
      }
      set
      {
        _OtherDirectionSpecification = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private List<RunwayVisibility> _Runways = new List<RunwayVisibility>();
    ///<summary>
    /// Sets/gets runway designator. Cannot be null.
    ///</summary>
    public List<RunwayVisibility> Runways
    {
      get
      {
        return (_Runways);
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("Property Runways cannot be null. Use empty object/collection instead.");
        _Runways = value;
      }
    }
    #endregion Properties

    #region Methods
    /// <summary>
    /// Set distance in meters. Sets EU style.
    /// </summary>
    /// <param name="distance">Visibility distance.</param>
    public override void SetMeters(int distance)
    {
      SetMeters (distance, null, null, null);
    }

    /// <summary>
    /// Sets distance in meters with direction specification.
    /// </summary>
    /// <param name="distance">Distance</param>
    /// <param name="way">Direction specification.</param>
    public void SetMeters(int distance, Common.eDirection way)
    {
      SetMeters(distance, way, null, null);
    }

    /// <summary>
    /// Sets distance in meters with direction specification.
    /// </summary>
    /// <param name="distance">Distance</param>
    /// <param name="way">Direction specification.</param>
    /// <param name="secondDistance">Other visibility</param>
    /// <param name="secondWay">Other visibility direction</param>
    public void SetMeters(int distance, Common.eDirection way, int secondDistance, Common.eDirection secondWay)
    {
      SetMeters(distance, way, secondDistance, secondWay);
    }

    /// <summary>
    /// Sets distance in meters with direction specifications. All parameters except the first one can be null.
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="way"></param>
    /// <param name="secondDistance"></param>
    /// <param name="secondWay"></param>
    public void SetMeters(int distance, Common.eDirection? way, int? secondDistance, Common.eDirection? secondWay)
    {
      UseEUStyle = true;
      IsDevicesMinimumValue = false;

      this.Distance = distance;
      this.DirectionSpecification = way;
      this.OtherDistance = secondDistance;
      this.OtherDirectionSpecification = secondWay;
    }

    #endregion Methods

    #region Inherited

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public override string ToCode()
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
        ret.Append(Distance.Value.Value.ToString("0000"));
        if (DirectionSpecification.HasValue)
          ret.Append(DirectionSpecification.ToString());

        if (OtherDistance.HasValue)
        {
          ret.Append(" " + OtherDistance.Value.Value.ToString("0000"));
          ret.Append(OtherDirectionSpecification.ToString());
        }

      }
      else
      {
        if (IsDevicesMinimumValue)
          ret.Append("M");
        ret.Append(Distance.Value.ToString(Racional.eToStringFormats.UsePreceedingWhole) + "SM");
      }

      foreach (var fItem in Runways)
      {
        ret.Append(" " + fItem.ToCode());
      } // foreach (var fItem in Runways)

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
    public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      base.SanityCheck(ref errors, ref warnings);

      if (UseEUStyle && Distance.HasValue && (Distance.Value > 10000))
        errors.Add("Maximum value for EU distance is 9999 meters. If more, use CAVOK instead.");
      else if (!UseEUStyle && Distance.HasValue && (Distance.Value > 10))
        errors.Add("Maximum value for non-EU (USA) distance is 10 miles. If more, use SKC instead.");

      if (UseEUStyle && IsDevicesMinimumValue)
        warnings.Add("IsDeviceMinimumValue flag is not used in EU style and will be ignored.");

      if ((OtherDistance.HasValue && !OtherDirectionSpecification.HasValue)
        ||
        (!OtherDistance.HasValue && OtherDirectionSpecification.HasValue))
        errors.Add("Both Other-distance and Other-way-restriction must have value or set be to null.");

      if (IsClear && OtherDistance.HasValue)
        warnings.Add("Is-clear is true, and also other-distance value is set. This combination is probably not correct.");
    }

    #endregion Inherited

  }
}
