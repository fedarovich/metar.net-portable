using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem.Extensions;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Represents wind variability between values.
  /// </summary>
  /// <seealso cref="T:ENG.Metar.Decoder.MetarItem"/>
  /// <remarks>
  /// If, during the 10-minute period preceding the observation, the total variation in wind direc-
  /// tion is 60° or more but less than 180° and the mean wind speed is 3 knots (2 m s–1 or
  /// 6 km h–1) or more, the observed two extreme directions between which the wind has var-
  /// ied shall be given for dndndnVdxdxdx in clockwise order. Otherwise this group shall not be
  /// included.
  /// </remarks>
  public class WindVariable : ICodeItem
  {
    #region Properties

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _FromDirection;
    ///<summary>
    /// Sets/gets FromDirection value.
    ///</summary>
    public int FromDirection
    {
      get
      {
        return (_FromDirection);
      }
      set
      {
        if (!value.IsBetween(0, 360))
          throw new ArgumentOutOfRangeException("Value must be between 0 and 360.");
        _FromDirection = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _ToDirection;
    ///<summary>
    /// Sets/gets ToDirection value.
    ///</summary>
    public int ToDirection
    {
      get
      {
        return (_ToDirection);
      }
      set
      {
        if (!value.IsBetween(0, 360))
          throw new ArgumentOutOfRangeException("Value must be between 0 and 360.");
        _ToDirection = value;
      }
    }

    #endregion Properties

    #region Methods

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public string ToCode()
    {
      return FromDirection.ToString("000") + "V" + ToDirection.ToString("000");
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
      int diff = (FromDirection > ToDirection) ? (ToDirection + 360 - FromDirection) : ToDirection - FromDirection;
      if (diff < 60)
        warnings.Add(
          "Significant variable wind range should be more or equal 60 degrees. Lower values should not be taken as significant.");
      if (diff > 180)
        warnings.Add(
          "Significant variable wind range should be less or equal 180 degrees. Greater values should be taken as variable (VRB) wind.");
    }

    #endregion Methods

  }
}
