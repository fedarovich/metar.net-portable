using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ENG.WMOCodes.Downloaders.Retrievers.Taf
{
    /// <summary>
    /// Downloads TAF data from aviationweather.gov web service.
    /// </summary>
    public class AviationWeatherGovRetriever : IRetriever
    {
        /// <summary>
        /// Returns URL where Code information is stored.
        /// </summary>
        /// <param name="icao">ICAO code of airport/station.</param>
        /// <returns></returns>
        public string GetUrlForICAO(string icao)
        {
            return "https://aviationweather.gov/adds/dataserver_current/httpparam?dataSource=tafs&requestType=retrieve&format=xml&hoursBeforeNow=24&timeType=valid&mostRecent=true&fields=raw_text&stationString=" + icao;
        }

        /// <summary>
        /// Decodes code as string from stream asynchronously. 
        /// Stream should be downloaded from URL address obtained 
        /// from <see cref="IRetriever.GetUrlForICAO"/> method.
        /// </summary>
        /// <param name="sourceStream">Source stream, from which the metar will be obtained.</param>
        /// <returns>Code string.</returns>
        /// <exception cref="DownloadException">
        /// Thrown if anything fails. Inner exception should contain more accurate info.
        /// </exception>
        public async Task<string> DecodeWMOCodeAsync(Stream sourceStream)
        {
            StreamReader reader = new StreamReader(sourceStream);
            XDocument document = XDocument.Parse(await reader.ReadToEndAsync().ConfigureAwait(false));
            if (document.Root == null)
                throw new DownloadException("Invalid XML document.");
            string report = document.Root
                .Elements("data")
                .Elements("TAF")
                .Elements("raw_text")
                .First()
                .Value;
            return report;
        }
    }
}
