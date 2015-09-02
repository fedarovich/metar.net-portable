using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Types.Basic;
using ESystem.Extensions;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Represets sets of runway conditions.
  /// </summary>
  public class RunwayConditionInfo : List<RunwayCondition>, ICodeItem
  {
    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsSNOCLO;
    ///<summary>
    /// Sets/gets if airport is closed due to snow (that is SNOCLO in metar).
    ///</summary>
    public bool IsSNOCLO
    {
      get
      {
        return (_IsSNOCLO);
      }
      set
      {
        _IsSNOCLO = value;
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
      if (IsSNOCLO)
        return "SNOCLO";
      else
      {
        StringBuilder ret = new StringBuilder();

        foreach (var fItem in this)
        {
          ret.AppendPreSpaced(fItem.ToCode());
        } // foreach (var fItem in RunwayConditions)

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
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      foreach (var fItem in this)
      {
        fItem.SanityCheck(ref errors, ref warnings);
      } // foreach (var fItem in this)
    }

    #endregion

    #endregion Inherited


    internal bool IsEmpty()
    {
      return (!IsSNOCLO && this.Count == 0);
    }
  }
}
