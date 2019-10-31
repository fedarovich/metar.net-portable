using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENG.WMOCodes.Downloaders
{
    /// <summary>
    /// Interface to represent retriever to download data from the source.
    /// </summary>
    public interface IRetriever
    {
        /// <summary>
        /// Returns URL where Code information is stored.
        /// </summary>
        /// <param name="icao">ICAO code of airport/station.</param>
        /// <returns></returns>
        string GetUrlForICAO(string icao);


        /// <summary>
        /// Decodes code as string from stream asynchronously. 
        /// Stream should be downloaded from URL address obtained 
        /// from <see cref="GetUrlForICAO"/> method.
        /// </summary>
        /// <param name="sourceStream">Source stream, from which the metar will be obtained.</param>
        /// <returns>Code string.</returns>
        /// <exception cref="DownloadException">
        /// Returns if anything fails. Inner exception should contain more accurate info.
        /// </exception>
        Task<string> DecodeWMOCodeAsync(System.IO.Stream sourceStream);
    }
}
