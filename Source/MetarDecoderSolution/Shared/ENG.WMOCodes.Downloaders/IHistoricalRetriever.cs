using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ENG.WMOCodes.Downloaders
{
    public interface IHistoricalRetriever : IRetriever
    {
        /// <summary>
        /// Returns URL where Code information is stored.
        /// </summary>
        /// <param name="icao">ICAO code of airport/station.</param>
        /// <param name="period">The period of time.</param>
        /// <returns></returns>
        string GetUrlForICAO(string icao, TimeSpan period);

        /// <summary>
        /// Returns URL where Code information is stored.
        /// </summary>
        /// <param name="icao">ICAO code of airport/station.</param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        string GetUrlForICAO(string icao, DateTimeOffset startTime, DateTimeOffset endTime);

        /// <summary>
        /// Decodes codes as strings from stream asynchronously. 
        /// Stream should be downloaded from URL address obtained 
        /// from <see cref="GetUrlForICAO(string,TimeSpan)"/> or
        /// <see cref="GetUrlForICAO(string,DateTimeOffset,DateTimeOffset)"/> method.
        /// </summary>
        /// <param name="sourceStream">Source stream, from which the metar will be obtained.</param>
        /// <returns>Code strings.</returns>
        /// <exception cref="DownloadException">
        /// Returns if anything fails. Inner exception should contain more accurate info.
        /// </exception>
        Task<IEnumerable<string>> DecodeWMOCodesAsync(System.IO.Stream sourceStream);
    }
}
