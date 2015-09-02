using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.WMOCodes.Decoders.Internal.Basic
{
  /// <summary>
  /// Represents public decoder used to decode some type from the source string.
  /// </summary>
  /// <typeparam name="T">Resulting type</typeparam>
  public abstract class PublicDecoder<T> 
  {
    /// <summary>
    /// Description of the block. Used in exception management in case that error occurs.
    /// </summary>
    public abstract string Description { get; }

    /// <summary>
    /// Realises decoding from the source.
    /// </summary>
    /// <param name="source">Source string.</param>
    /// <returns>Resulting object</returns>
    /// <exception cref="DecodeException">Raised if some error occurs.</exception>
    public T Decode(string source)
    {
      T ret = default(T);

      try
      {
        ret = _Decode(source);
      } // try
      catch (Exception ex)
      {
        throw new DecodeException(Description, ex);
      } // catch (Exception ex)

      return ret;
    }

    /// <summary>
    /// Realises decoding process from the source string.
    /// </summary>
    /// <param name="source">Source string.</param>
    /// <returns>Decoded object.</returns>
    /// <remarks>
    /// Successor have to implement this method to do the real decoding process.
    /// </remarks>
    protected abstract T _Decode(string source);
  }
}
