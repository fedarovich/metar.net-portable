using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Represents one cloud (e.g. OVC040TCU).
  /// </summary>
  public class Cloud : ICodeItem
  {
    #region Nested

    /// <summary>
    /// List of types of clouds. Comparable by "int" value.
    /// </summary>
    public enum eType
    {
      /// <summary>
      /// Few clouds.
      /// </summary>
      FEW = 1,
      /// <summary>
      /// Scattered clouds.
      /// </summary>
      SCT = 2,
      /// <summary>
      /// Broken clouds.
      /// </summary>
      BKN = 3,
      /// <summary>
      /// Overcast clouds.
      /// </summary>
      OVC = 4
    }

    #endregion Nested

    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsCB;
    ///<summary>
    /// Sets/gets if cloud is cumulonimbus. That is, if there is prefix with CB, e.g. OVC040CB.
    ///</summary>
    ///<remarks>
    ///This property cannot be true if IsTCU is true.
    ///</remarks>
    public bool IsCB
    {
      get
      {
        return (_IsCB);
      }
      set
      {
        _IsCB = value;
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsTCU;
    ///<summary>
    /// Sets/gets if cloud is towering cumulus. That is, if there is prefix with TCU, e.g. OVC040TCU.
    ///</summary>
    ///<remarks>
    ///This property cannot be true if IsCB is true.
    ///</remarks>
    public bool IsTCU
    {
      get
      {
        return (_IsTCU);
      }
      set
      {
        _IsTCU = value;
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private NonNegInt _Altitude;
    ///<summary>
    /// Sets/gets Altitude value, in hundreds of feet, e.g. OVC040 for 4000 ft above airport or station.
    /// To get altitude in other unit, see method <seealso cref="GetAltitudeIn"/>
    ///</summary>
    public NonNegInt Altitude
    {
      get
      {
        return (_Altitude);
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eType _Type;
    ///<summary>
    /// Sets/gets type of clouds. <see cref="eType"/>
    ///</summary>
    public eType Type
    {
      get
      {
        return (_Type);
      }
    }

    #endregion Properties

    #region .ctor

    /// <summary>
    /// Initializes a new Instance of ENG.Metar.Decoder.Cloud
    /// </summary>
    /// <param name="type"></param>
    /// <param name="altitude"></param>
    public Cloud(eType type, NonNegInt altitude) : this (type, altitude, false, false)
    { }

    /// <summary>
    /// Initializes a new Instance of ENG.Metar.Decoder.Cloud
    /// </summary>
    /// <param name="type"></param>
    /// <param name="altitude"></param>
    /// <param name="isCB"></param>
    /// <param name="isTCU"></param>
    public Cloud (eType type, NonNegInt altitude, bool isCB, bool isTCU)
    {
      this.SetClouds(type, altitude, isCB, isTCU);
    }

    /// <summary>
    /// Initializes a new Instance of ENG.Metar.Decoder.Cloud
    /// </summary>
    /// <param name="type"></param>
    /// <param name="altitude"></param>
    /// <param name="isCB"></param>
    /// <param name="isTCU"></param>
    public Cloud(string type, NonNegInt altitude, bool isCB, bool isTCU)
    {
      this.SetClouds(type, altitude, isCB, isTCU);
    }

    #endregion .ctor

    #region Methods

    /// <summary>
    /// Sets cloud type.
    /// </summary>
    /// <param name="type">Type of cloud.</param>
    /// <param name="altitude">Altitude in hunderds of ft.</param>
    /// <param name="isCB">True if cloud is cumulonimbus (CB).</param>
    /// <param name="isTCU">True if cloud is towering cumulus (TCU).</param>
    public void SetClouds(eType type, int altitude, bool isCB, bool isTCU)
    {
      if (isCB && isTCU)
        throw new ArgumentException("Unable to set both CB and TCU flags to true.", "isTCU");

      _IsTCU = isTCU;
      _IsCB = isCB;

      _Type = type;

      if (altitude > 999)
        throw new ArgumentException("Invalid altitude value. Maximum is 999.", "altitude");
      _Altitude = altitude;
    }

    /// <summary>
    /// Sets cloud type.
    /// </summary>
    /// <param name="type">Type of cloud.</param>
    /// <param name="altitude">Altitude in hunderds of ft.</param>
    public void SetClouds(eType type, int altitude)
    {
      SetClouds(type, altitude, false, false);
    }

    /// <summary>
    /// Sets cloud type.
    /// </summary>
    /// <param name="type">Type of cloud (as string).</param>
    /// <param name="altitude">Altitude in hunderds of ft.</param>
    /// <param name="isCB">True if cloud is cumulonimbus (CB).</param>
    /// <param name="isTCU">True if cloud is towering cumulus (TCU).</param>
    public void SetClouds(string type, int altitude, bool isCB, bool isTCU)
    {
      eType t = (eType)Enum.Parse(typeof(eType), type, false);
      SetClouds(t, altitude, isCB, isTCU);
    }

    /// <summary>
    /// Sets cloud type.
    /// </summary>
    /// <param name="type">Type of cloud (as string).</param>
    /// <param name="altitude">Altitude in hunderds of ft.</param>
    public void SetClouds(string type, int altitude)
    {
      eType t = (eType)Enum.Parse(typeof(eType), type, false);
      SetClouds(t, altitude);
    }

    /// <summary>
    /// Returns cloud altitude in selected distance unit.
    /// </summary>
    /// <param name="distanceUnit"></param>
    /// <returns></returns>
    public double GetAltitudeIn(Common.eDistanceUnit distanceUnit)
    {
      double ret = 0;
      int inFt = this.Altitude * 100;
      if (distanceUnit == Common.eDistanceUnit.ft)
        ret = inFt;
      else
        ret = Common.Convert(inFt, Common.eDistanceUnit.ft, distanceUnit);

        return ret;
    }

    #endregion Methods

    #region Inherited

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public string ToCode()
    {
      StringBuilder ret = new StringBuilder();

      ret.Append(Type.ToString());
      ret.Append(Altitude.ToString("000"));
      if (IsTCU)
        ret.Append("TCU");
      else if (IsCB)
        ret.Append("CB");

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
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      if (Altitude > 400)
        warnings.Add("Altitude value over 400 is probably incorrect.");
      if (IsTCU && IsCB)
        errors.Add("IsTCU and IsCB flags cannot be set both at same time. Only one of them can be used.");
    }

    #endregion Inherited

  }
}
