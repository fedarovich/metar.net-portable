using System;

namespace ENG.WMOCodes.Decoders.Internal.Basic
{
    internal abstract class CustomDecoder<T> : InternalDecoder<T>
    {
        protected abstract override T DecodeCore(ref string source);

        protected void TryFailIfRequired(string source)
        {
            if (Required)
                throw new DecodeException(Description, new ArgumentException(source));
        }
    }
}
