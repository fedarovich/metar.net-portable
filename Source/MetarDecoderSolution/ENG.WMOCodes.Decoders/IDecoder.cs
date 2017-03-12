using ENG.WMOCodes.Decoders.Internal;

namespace ENG.WMOCodes.Decoders
{
    /// <summary>
    /// Represents decoder used to decode some type from the source string.
    /// </summary>
    /// <typeparam name="T">Resulting type</typeparam>
    public interface IDecoder<out T>
    {
        /// <summary>
        /// Implements decoding from the source.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <returns>Resulting object</returns>
        /// <exception cref="DecodeException">Raised if some error occurs.</exception>
        T Decode(string source);
    }
}
