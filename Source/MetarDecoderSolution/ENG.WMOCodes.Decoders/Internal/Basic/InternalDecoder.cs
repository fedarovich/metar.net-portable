using System;

namespace ENG.WMOCodes.Decoders.Internal.Basic
{
    internal abstract class InternalDecoder<T>
    {
        public abstract string Description { get; }

        ///<summary>
        /// Sets/gets Required value. Default value is true.
        ///</summary>
        public bool Required { get; set; } = true;

        public T Decode(ref string source)
        {
            T ret = default(T);

            try
            {
                ret = DecodeCore(ref source);
            } // try
            catch (Exception ex)
            {
                throw new DecodeException(Description, ex);
            } // catch (Exception ex)

            return ret;
        }

        protected abstract T DecodeCore(ref string source);
    }
}
