using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.WMOCodes.Decoders.Internal.Basic
{
  internal abstract class InternalDecoder<T>
  {
    public abstract string Description { get; }
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
    private bool _Required = true;
    ///<summary>
    /// Sets/gets Required value. Default value is true.
    ///</summary>
    public bool Required
    {
      get
      {
        return (_Required);
      }
      set
      {
        _Required = value;
      }
    }

    public T Decode(ref string source)
    {
      T ret = default(T);

      try
      {
        ret = _Decode(ref source);
      } // try
      catch (Exception ex)
      {
        throw new DecodeException(Description, ex);
      } // catch (Exception ex)

      return ret;
    }

    protected abstract T _Decode(ref string source);
  }
}
