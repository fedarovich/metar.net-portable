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
#if NETSTANDARD2_0
    [Serializable]
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
    }
}
