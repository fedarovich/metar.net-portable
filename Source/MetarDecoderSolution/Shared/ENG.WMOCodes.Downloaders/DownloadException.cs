using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.WMOCodes.Downloaders
{
    /// <summary>
    /// Raised when some error occurs during metar downloading or decoding.
    /// Inner exception should contain more accurate information.
    /// </summary>
#if !PCL && (!NET_STANDARD || NET_STANDARD_1_3_PLUS)
  [global::System.Serializable]
#endif
    public class DownloadException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DownloadException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public DownloadException(string message, Exception inner) : base(message, inner) { }

#if !PCL && (!NET_STANDARD || NET_STANDARD_1_3_PLUS)

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is null.
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
        /// </exception>
        protected DownloadException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

#endif
    }
}
