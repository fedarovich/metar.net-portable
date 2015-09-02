using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Represents runway visibility information.
  /// </summary>
  /// <seealso cref="T:ENG.Metar.Decoder.MetarItem"/>
  public class RunwayVisibility : ICodeItem
  {

    #region Nested

    /// <summary>
    /// Represents tendency of visibility.
    /// </summary>
    public enum eTendency
    {
      /// <summary>
      /// Increasing tendency
      /// </summary>
      U,
      /// <summary>
      /// Decreasing tendency
      /// </summary>
      D,
      /// <summary>
      /// No change expected-tendency.
      /// </summary>
      N
    }

    /// <summary>
    /// Represents measuring device restriction.
    /// </summary>
    public enum eDeviceMeasurementRestriction
    {
      /// <summary>
      /// If used, visibility is at best at this value.
      /// Device cannot measure less value.
      /// </summary>
      M,
      /// <summary>
      /// If used, visibility is at worse at this value.
      /// Device cannot measure bigger value.
      /// </summary>
      P
    }
    #endregion Nested

    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eDeviceMeasurementRestriction? _DeviceMeasurementRestriction = null;
    ///<summary>
    /// Sets/gets device measurement restriction. Null if not used.
    ///</summary>
    public eDeviceMeasurementRestriction? DeviceMeasurementRestriction
    {
      get
      {
        return (_DeviceMeasurementRestriction);
      }
      set
      {
        _DeviceMeasurementRestriction = value;
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eTendency? _Tendency;
    ///<summary>
    /// Sets/gets visibility tendency value. Null if not used.
    ///</summary>
    public eTendency? Tendency
    {
      get
      {
        return (_Tendency);
      }
      set
      {
        _Tendency = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private NonNegInt _Distance;
    ///<summary>
    /// Sets/gets Visibility value.
    ///</summary>
    public NonNegInt Distance
    {
      get
      {
        return (_Distance);
      }
      set
      {
        _Distance = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private NonNegInt? _VariableDistance;
    ///<summary>
    /// Sets/gets VariableVisibility value. Null if visibility does not vary.
    ///</summary>
    public NonNegInt? VariableDistance
    {
      get
      {
        return (_VariableDistance);
      }
      set
      {
        _VariableDistance = value;
      }
    }
    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private string _Runway;
    ///<summary>
    /// Sets/gets Runway designator.
    ///</summary>
    public string Runway
    {
      get
      {
        return (_Runway);
      }
      set
      {
        _Runway = value;
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private Common.eDistanceUnit _Unit = Common.eDistanceUnit.m;
    ///<summary>
    /// Sets/gets Unit value. Default value is Common.eDistanceUnit.m.
    ///</summary>
    public Common.eDistanceUnit Unit
    {
      get
      {
        return (_Unit);
      }
      set
      {
        if (value != Common.eDistanceUnit.m && value != Common.eDistanceUnit.ft)
          throw new ArgumentException("Value for property Unit of RunwayVisibility have to be feet or meters.");
        _Unit = value;
      }
    }

    #endregion Properties

    #region Inherited

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public string ToCode()
    {
      StringBuilder ret = new StringBuilder();

      ret.Append("R" + Runway + "/");
      if (DeviceMeasurementRestriction != null)
        ret.Append(DeviceMeasurementRestriction.Value.ToString());
      ret.Append(Distance.ToString("0000"));
      if (VariableDistance.HasValue)
        ret.Append("V" + VariableDistance.Value.ToString("0000"));
      if (Unit == Common.eDistanceUnit.ft)
        ret.Append("FT");
      else if (Tendency.HasValue)
        ret.Append(Tendency.ToString());

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

    #region MetarItem Members


    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      if (string.IsNullOrEmpty(Runway))
        errors.Add("Runway number/sign is not set.");
    }

    #endregion

    #endregion Inherited

  }
}
