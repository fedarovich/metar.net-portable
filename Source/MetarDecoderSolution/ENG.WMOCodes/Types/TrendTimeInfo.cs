﻿using System.Collections.Generic;
using System.Text;
using ENG.WMOCodes.Extensions;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Represents set of time infos in metar trend.
  /// </summary>
  public class TrendTimeInfo : List<TrendTime>, ICodeItem
  {
    #region Inherited

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public string ToCode()
    {
      StringBuilder ret = new StringBuilder();

      this.ForEach(i => ret.AppendPreSpaced(i.ToCode()));

      return ret.ToString().TrimEnd();
    }

    /// <summary>
    /// Returns a <see cref="T:System.String"/> that represents the current instance.
    /// </summary>
    /// <returns>A <see cref="T:System.String"/> that represents the current instance.</returns>
    public override string ToString() => this.ToInlineInfoString();

      #region MetarItem Members


    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      if (Count > 2)
        warnings.Add("Expected one value (at) or two values (from-to) in collection.");

      if (Count == 2)
      {
        if (this[0].Type != TrendTimeType.FM || this[1].Type != TrendTimeType.TL)
          warnings.Add("For two types expected pair from-to.");
      }
    }

    #endregion

    #endregion Inherited
  }
}
