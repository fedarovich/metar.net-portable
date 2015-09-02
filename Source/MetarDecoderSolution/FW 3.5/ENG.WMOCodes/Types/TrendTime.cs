using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESystem.Extensions;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{

  /// <summary>
  /// Represents trend time information.
  /// </summary>
  /// <seealso cref="T:ENG.Metar.Decoder.MetarItem"/>
  public class TrendTime : ICodeItem
  {
    #region Nested

    /// <summary>
    /// Type of trend time.
    /// </summary>
    public enum eType
    {
      /// <summary>
      /// From date/time
      /// </summary>
      FM,
      /// <summary>
      /// Until date/time
      /// </summary>
      TL,
      /// <summary>
      /// At date/time
      /// </summary>
      AT
    }

    #endregion Nested

    #region Properties


    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private eType _Type;
    ///<summary>
    /// Sets/gets time type.
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
    private int _Hour;
    ///<summary>
    /// Sets/gets Hour value.
    ///</summary>
    public int Hour
    {
      get
      {
        return (_Hour);
      }
      set
      {
        if (!value.IsBetween(0, 24))
          throw new ArgumentOutOfRangeException("Value must be between 0 and 24.");
        _Hour = value;
      }
    }
    /// <summary>
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _Minute;
    ///<summary>
    /// Sets/gets Minute value.
    ///</summary>
    public int Minute
    {
      get
      {
        return (_Minute);
      }
      set
      {
        if (!value.IsBetween(0, 59))
          throw new ArgumentOutOfRangeException("Value must be between 0 and 59.");
        _Minute = value;
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
      return Type.ToString() + Hour.ToString("00") + Minute.ToString("00");
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
      if ((Hour == 24) && (Type != eType.TL))
        warnings.Add("Hour value 24 should be used only with Type TL (until).");
      if ((Hour == 24) && (Minute != 0))
        errors.Add("Hour value 24 should be used only when minute value is 00.");
    }

    #endregion

    #endregion Inherited

    #region Private

    private string eTypeToString(eType eType)
    {
      switch (eType)
      {
        case eType.AT:
          return "at";
        case eType.FM:
          return "from";
        case eType.TL:
          return "until";
        default:
          throw new NotImplementedException();
      }
    }
    #endregion Private

  }
}
