using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Types.Basic;
using ESystem.Extensions;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Represents information about windshears in metar.
  /// </summary>
  public class WindShearInfo : List<WindShear>, ICodeItem
  {
    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _IsAllRunways;
    ///<summary>
    /// Sets/gets if winshear warning is true for all runways (WS ALL RWY). If so, collection data are ignored.
    ///</summary>
    public bool IsAllRunways
    {
      get
      {
        return (_IsAllRunways);
      }
      set
      {
        _IsAllRunways = value;
      }
    }

    #endregion Properties

    #region Inherited

    ///// <summary>
    ///// Returns item in text string.
    ///// </summary>
    ///// <param name="formatter">Formatter used to format string.</param>
    ///// <returns></returns>
    //public string ToInfo(InfoFormatter formatter)
    //{
    //  string ret = null;

    //  /* WIND SHEARS INFO
    //   * 0 - true if is non-empty
    //   * 1 - True if is for all runways
    //   * 2 - true if inner content is non-empty
    //   * 3 - WIND-SHEAR-INFO
    //   * */

    //  string f = null;
    //  try
    //  {
    //    f = formatter.WindShearsFormat;
    //  }
    //  catch { }
    //  if (f == null)
    //    return null;
    //  else if (f.Length == 0)
    //    return "";

    //  ret = formatter.Format(
    //        formatter.WindShearsFormat,
    //        (this.IsAllRunways || this.Count != 0),
    //        IsAllRunways,
    //        this.Count != 0,
    //        GetWindshearInfo(formatter));

    //  return ret;
    //}

    //private string GetWindshearInfo(InfoFormatter formatter)
    //{
    //  StringBuilder ret = new StringBuilder();

    //  this.ForEach(ws => ret.Append(ws.ToInfo(formatter)));

    //  return ret.ToString();
    //}

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public string ToCode()
    {
      if (IsAllRunways)
        return "WS ALL RWY";
      else
      {
        StringBuilder b = new StringBuilder();

        foreach (var fItem in this)
        {
          b.AppendPreSpaced(fItem.ToCode());
        } // foreach (var fItem in WindShears)

        if (b.Length > 0)
          return "WS " + b.ToString().TrimEnd();
        else
          return "";
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
      if (IsAllRunways && (this.Count > 0))
        warnings.Add("If IsAllRunways flag is set to true, ws definitions for concrete runways will be ignored (now are non-empty).");
    }

    #endregion

    #endregion Inherited


    internal bool IsEmpty()
    {
      return (!IsAllRunways && this.Count == 0);
    }
  }
}
