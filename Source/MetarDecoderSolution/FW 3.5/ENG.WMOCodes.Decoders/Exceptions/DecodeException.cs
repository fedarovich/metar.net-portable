using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.WMOCodes.Decoders.Internal
{
  /// <summary>
  /// Exception thrown when decoding failed.
  /// </summary>
#if SILVERLIGHT == false
  [Serializable]
#endif
  public class DecodeException : Exception
  {
    private string _Description;
    /// <summary>
    /// Gets the description. Description should describe location where error occured.
    /// </summary>
    /// <value>The description.</value>
    public string Description
    {
      get
      {
        return _Description;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DecodeException"/> class.
    /// </summary>
    /// <param name="decoderDescription">The decoder description. Description should describe location where error occured.</param>
    /// <param name="inner">The inner.</param>
    public DecodeException(string decoderDescription, Exception inner)
      : base("", inner)
    {
      this._Description = decoderDescription;
    }

#if SILVERLIGHT == false

    /// <summary>
    /// Initializes a new instance of the <see cref="DecodeException"/> class.
    /// </summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// The <paramref name="info"/> parameter is null.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
    /// </exception>
    protected DecodeException(
    System.Runtime.Serialization.SerializationInfo info,
    System.Runtime.Serialization.StreamingContext context)
      : base(info, context) { }

#endif

    /// <summary>
    /// Gets a message that describes the current exception.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The error message that explains the reason for the exception, or an empty string("").
    /// </returns>
    public override string Message
    {
      get
      {
        return GenerateMessage();
      }
    }

    /// <summary>
    /// Generates the message.
    /// </summary>
    /// <returns></returns>
    private string GenerateMessage()
    {
      StringBuilder tree = new StringBuilder();
      DecodeException curr = this;

      tree.Append("Decoding failed at ");

      while (true)
      {
        tree.Append("->" + curr.Description);
        if (curr.InnerException != null && curr.InnerException is DecodeException)
          curr = curr.InnerException as DecodeException;
        else
          break;
      }

      tree.Append(". Reason:");

      Exception ex = curr.InnerException;
      while (ex != null)
      {
        tree.Append(" >> " + curr.InnerException.Message);
        ex = ex.InnerException;
      }

      return tree.ToString();
    }
  }
}
