using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Types.Basic;
using ESystem.Extensions;

namespace ENG.WMOCodes.Types
{
  /// <summary>
  /// Represents information about trend. To mark trend as not used. set null value into property type.
  /// </summary>
  /// <seealso cref="T:ENG.Metar.Decoder.MetarItem"/>
  public class TrendInfoForMetar : TrendReport, ICodeItem
  {
    #region Nested

    /// <summary>
    /// Type of trend.
    /// </summary>
    public enum eType
    {
      /// <summary>
      /// No significant change trend.
      /// </summary>
      NOSIG,
      /// <summary>
      /// Becoming trend.
      /// </summary>
      BECMG,
      /// <summary>
      /// Temporaly trend.
      /// </summary>
      TEMPO
    }

    #endregion Nested

    #region Properties

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eType _Type = eType.NOSIG;
    ///<summary>
    /// Sets/gets Type value.
    ///</summary>
    public eType Type
    {
      get
      {
        return (_Type);
      }
      set
      {
        _Type = value;
      }
    }

    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private TrendTimeInfo _Times = new TrendTimeInfo();
    ///<summary>
    /// Sets/gets Dates value.
    ///</summary>
    public TrendTimeInfo Times
    {
      get
      {
        return (_Times);
      }
      set
      {
        _Times = value;
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
      StringBuilder ret = new StringBuilder();

      ret.AppendPreSpaced(this.Type.ToString());
      this.Times.ForEach(
        i => ret.AppendPreSpaced(i.ToCode()));
      if (Wind != null)
        ret.AppendPreSpaced(Wind.ToCode());
      if (Visibility != null)
        ret.AppendPreSpaced(this.Visibility.ToCode());
      if (Phenomens != null)
        ret.AppendPreSpaced(this.Phenomens.ToCode());
      if (Clouds != null)
        ret.AppendPreSpaced(this.Clouds.ToCode());

      return ret.ToString().TrimEnd();
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
      if (Wind != null)
        Wind.SanityCheck(ref errors, ref warnings);
      if (Visibility != null)
        this.Visibility.SanityCheck(ref errors, ref warnings);
      if (Phenomens != null)
        this.Phenomens.SanityCheck(ref errors, ref warnings);
      if (Clouds != null)
        this.Clouds.SanityCheck(ref errors, ref warnings);
    }

    #endregion

    #endregion Inherited
  }
}
