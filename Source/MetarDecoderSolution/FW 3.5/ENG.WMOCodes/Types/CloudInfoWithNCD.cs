using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Represents cloud info (<seealso cref="CloudInfo"/>) with NCD (no clouds detected) flag.
  /// </summary>
  public class CloudInfoWithNCD : CloudInfo
  {
    #region Properties
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsNCD = false;
    ///<summary>
    /// Gets "no clouds detected" value. That is NCD in metar.
    ///</summary>
    public bool IsNCD
    {
      get
      {
        return ((this.Count == 0) && _IsNCD);
      }
    }
    #endregion Properties

    #region Methods

    /// <summary>
    /// Sets all flags (VV, NSC, etc.) off.
    /// </summary>
    protected override void SetAllFlagsOff()
    {
      _IsNCD = false;
      base.SetAllFlagsOff();
    }

    /// <summary>
    /// Sets the NCD (no clouds detected) flag.
    /// </summary>
    public virtual void SetNCD()
    {
      SetAllFlagsOff();
      _IsNCD = true;
    }

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public override string ToCode()
    {
      if (IsNCD)
        return "NCD";
      else
        return base.ToCode();
    }

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      base.SanityCheck(ref errors, ref warnings);
      if (IsNCD && (IsSKC || IsNSC || IsCLR || IsVerticalVisibility || Count > 0))
        errors.Add("Unable to have IsNCD flag on, when IsSKC, IsNSC, IsCLR, or IsVerticalVisibility is true or count of clouds > 0.");
    }

    #endregion Methods
  }
}
