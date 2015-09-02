using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem.Extensions;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Standard phenom info (<seealso cref="PhenomInfo"/>) extended by NSW (no significant weather) flag.
  /// </summary>
public  class PhenomInfoWithNSW : PhenomInfo
{
  #region Properties

  [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
  private bool _IsNSW;
  ///<summary>
  /// Sets/gets if no-significant-weather flag is used (NSW).
  ///</summary>
  public bool IsNSW
  {
    get
    {
      return ((this.Count == 0) && _IsNSW);
    }
    set
    {
      _IsNSW = value;
      if (value)
        this.Clear();
    }
  }

  #endregion Properties

  #region Inherited

  /// <summary>
  /// Returns item in code string.
  /// </summary>
  /// <returns></returns>
  public override string ToCode()
  {
    if (IsNSW)
      return "NSW";
    else
    {
      StringBuilder ret = new StringBuilder();

      this.ForEach(
        i => ret.AppendPostSpaced(i.ToCode()));

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
  public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
  {
    if (IsNSW && (this.Count > 0))
      warnings.Add("When IsNSW flag is set to true, phenom definitions will be ignored (now list is nonempty).");
  }

  #endregion

  #endregion Inherited

  #region Methods

  internal override bool IsEmpty()
  {
    return ((this.Count == 0) && !IsNSW);
  }

  #endregion Methods
}
}
