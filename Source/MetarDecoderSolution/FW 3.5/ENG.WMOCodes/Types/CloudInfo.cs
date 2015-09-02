using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Types.Basic;
using ESystem.Extensions;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Represents information about clouds.
  /// </summary>
  public class CloudInfo : List<Cloud>, ICodeItem
  {
    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsCLR = false;
    ///<summary>
    /// Gets "is sky clear" value. That is CLR in metar.
    ///</summary>
    ///<remarks>
    /// CLR value for clear sky: The abbreviation CLR shall be used at automated stations 
    /// when no layers at or below 12,000/10,000 (U.S./Canada) feet are reported. <seealso cref="IsSKC"/> <seealso cref="IsNSC"/>
    ///</remarks>
    public bool IsCLR
    {
        get
        {
            return ((this.Count == 0) && _IsCLR);
        }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsSKC = false;
    ///<summary>
    /// Gets "is sky clear" value. That is SKC in metar.
    ///</summary>
    ///<remarks>
    ///SKC value for clear sky: The abbreviation SKC shall be used at manual stations 
    ///when no layers are reported. <seealso cref="IsCLR"/> <seealso cref="IsNSC"/>
    ///</remarks>
    public bool IsSKC
    {
      get
      {
        return ((this.Count == 0) && _IsSKC);
      }
    }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsNSC = false;
    ///<summary>
    /// Gets "no significant cloud" value. That is NSC in metar.
    ///</summary>
    ///<remarks>
    ///If there are no  clouds  below  1  500  m  (5  000  ft)  or  below  the  highest  
    /// minimum  sector  altitude, whichever is greater, no cumulonimbus and no towering 
    /// cumulus and no restriction on vertical  visibility,  and  the  abbreviations  CAVOK  
    /// is  not  appropriate,  then  the abbreviation NSC shall be used. When an automatic 
    /// observing system is used and no clouds are detected by that system, 
    /// the abbreviation NCD shall be used.
    /// WARNING: NCD is not implemented!
    /// <seealso cref="IsSKC"/> <seealso cref="IsCLR"/>
    ///</remarks>
    public bool IsNSC
    {
      get
      {
        return ((this.Count == 0) && _IsNSC);
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsVerticalVisibility = false;
    ///<summary>
    /// Gets if cloud info is represented by vertical visibility. E.g. VV040 in metar.
    ///</summary>
    public bool IsVerticalVisibility
    {
      get
      {
        return ((this.Count == 0) && _IsVerticalVisibility);
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int? _VVDistance = null;
    ///<summary>
    /// Gets vertical visibility distance (in houndreds of feet). E.g. VV040. 
    /// If not known, value is null. That is VV/// in metar.
    ///</summary>
    public int? VVDistance
    {
      get
      {
        return (_VVDistance);
      }
    }

    #endregion Properties

    #region Methods
    /// <summary>
    /// Sets "clear sky"  <see cref="IsCLR"/>
    /// </summary>
    public void SetCLR()
    {
        SetAllFlagsOff();
        _IsCLR = true;
    }


    /// <summary>
    /// Sets "sky clear". <see cref="IsSKC"/>
    /// </summary>
    public void SetSKC()
    {
      SetAllFlagsOff();
      _IsSKC = true;
    }

    /// <summary>
    /// Sets "no significand cloud". <see cref="IsNSC"/>
    /// </summary>
    public void SetNSC()
    {
      SetAllFlagsOff();
      _IsNSC = true;
    }

    /// <summary>
    /// Sets vertical visibility. Argument is null if not known (that is VV///).
    /// </summary>
    /// <param name="distance">Distance in hounded feet. Null if not known.</param>
    public void SetVerticalVisibility(int? distance)
    {
      SetAllFlagsOff();
      _IsVerticalVisibility = true;
      _VVDistance = distance;
    }

    /// <summary>
    /// Sets all flags (VV, NSC, etc.) off.
    /// </summary>
    protected virtual void SetAllFlagsOff()
    {
      _IsCLR = false;
      _IsSKC = false;
      _IsNSC = false;
      _IsVerticalVisibility = false;
      _VVDistance = null;
      this.Clear();
    }

    #endregion Methods

    #region Implemented

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public virtual string ToCode()
    {
        if (IsCLR)
            return "CLR";
        else if (IsSKC)
            return "SKC";
        else if (IsNSC)
            return "NSC";
        else if (IsVerticalVisibility)
            return "VV" + (VVDistance.HasValue ? VVDistance.Value.ToString("000") : "///");
        else
        {
            StringBuilder ret = new StringBuilder();

            this.ForEach(
              i => ret.AppendPreSpaced(i.ToCode()));

            return ret.ToString().TrimEnd();
        }
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
    public virtual void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      if (GetTrueCount(IsNSC, IsSKC, IsCLR) > 1)
        errors.Add("IsSKC, IsNSC, and IsCLR are mutually exclusive.  More than one flag is currently set.");
      if ((IsSKC || IsNSC || IsCLR) && IsVerticalVisibility)
        errors.Add("Vertical visibility cannot be set true with IsSKC, IsNSC, or IsCLR flags.");
      if ((IsSKC || IsNSC || IsCLR || IsVerticalVisibility) && (Count > 0))
        warnings.Add("When one of flags IsSKC, IsNSC, IsCLR, or IsVerticalVisibility are set to true, cloud defining content (which is now not empty) will be ignored.");
    }

    /// <summary>
    /// Returns count of items of bool parameters, which are true.
    /// </summary>
    /// <param name="booleans"></param>
    /// <returns></returns>
    private int GetTrueCount(params bool[] booleans)
    {
        return booleans.Count(b => b);
    }

    #endregion

    #endregion Implemented


    internal bool IsEmpty()
    {
      return (!IsNSC && !IsSKC && !IsCLR && !IsVerticalVisibility && this.Count == 0);
    }
  }
}
