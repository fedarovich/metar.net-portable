using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Represents standard wind (<see cref=" Wind"/>) extended with wind variability.
  /// </summary>
  public class WindWithVariability : Wind
  {
    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private WindVariable _Variability = null;
    ///<summary>
    /// Sets/gets variable value. Null if not used. <see cref="IsVarying"/><see cref="WindVariable"/>
    ///</summary>
    public WindVariable Variability
    {
      get
      {
        return (_Variability);
      }
      set
      {
        _Variability = value;
      }
    }

    /// <summary>
    /// Returns true if wind is varying between two headings.. <see cref="Variability"/> <see cref="WindVariable"/>
    /// </summary>
    /// <value></value>
    public bool IsVarying
    {
      get
      {
        return (Variability != null);
      }
    }

    ///// <summary>
    ///// Returns item in text string.
    ///// </summary>
    ///// <param name="formatter">Formatter used to format string.</param>
    ///// <returns></returns>
    //public virtual string ToInfo(InfoFormatter formatter)
    //{

    //  string ret;

    //  string f = null;
    //  try
    //  {
    //    f = formatter.WindFormat;
    //  }
    //  catch { }
    //  if (f == null)
    //    return null;
    //  else if (f.Length == 0)
    //    return "";

    //  ret = formatter.Format(
    //    formatter.WindFormat,
    //    IsVariable,
    //    IsCalm,
    //    Direction,
    //    Direction.HasValue ? formatter.eDirectionToString(ENG.Metar.Decoder.Common.HeadingToeDirection(Direction.Value), false) : null,
    //    Direction.HasValue ? formatter.eDirectionToString(ENG.Metar.Decoder.Common.HeadingToeDirection(Direction.Value), true) : null,
    //    Speed,
    //    Unit.ToString().ToLower(),
    //    GustSpeed,
    //    GustSpeed.HasValue ? GustSpeed.Value : Speed,
    //    IsVarying ? Variability.FromDirection.ToString() : null,
    //    IsVarying ? Variability.ToDirection.ToString() : null
    //    );

    //  return ret.ToString();
    //}

    /// <summary>
    /// Returns item in code string.
    /// </summary>
    /// <returns></returns>
    public override string ToCode()
    {
      StringBuilder ret = new StringBuilder(base.ToCode());

      if (IsVarying)
      {
        ret.Append(" " + Variability.ToCode());
      }

      return ret.ToString();
    }

    /// <summary>
    /// Proceed sanity check of inserted values.
    /// </summary>
    /// <param name="errors">Found errors.</param>
    /// <param name="warnings">Found warnings.</param>
    public override void SanityCheck(ref List<string> errors, ref List<string> warnings)
    {
      base.SanityCheck(ref errors, ref warnings);
      if (IsVarying)
        this.Variability.SanityCheck(ref errors, ref warnings);
    }
  }
}
