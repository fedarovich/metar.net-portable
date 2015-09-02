using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.WMOCodes.Types.Basic
{
  /// <summary>
  /// Zero-value or positive integer.
  /// </summary>
  public struct NonNegInt
  {
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private int _Value;
    ///<summary>
    /// Sets/gets Value value.
    ///</summary>
    public int Value
    {
      get
      {
        return (_Value);
      }
      set
      {
        if (value < 0)
          throw new Exception("Unable to set negative value into this type.");
        _Value = value;
      }
    }

    /// <summary>
    /// Implicit conversion into integer.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static implicit operator int(NonNegInt value)
    {
      return value.Value;
    }

    /// <summary>
    /// Explicit conversion from integer.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static implicit operator NonNegInt(int value)
    {
      return new NonNegInt() { Value = value };
    }

    /// <summary>
    /// Returns value of the instance as string.
    /// </summary>
    public override string ToString()
    {
      return this.Value.ToString();
    }

    /// <summary>
    /// Returns string represention of this value.
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    public string ToString(string format)
    {
      return Value.ToString(format);
    }
  }
}
