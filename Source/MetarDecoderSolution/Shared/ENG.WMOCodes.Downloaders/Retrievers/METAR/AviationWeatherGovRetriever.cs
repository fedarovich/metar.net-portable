using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
#if PCL
using System.Threading.Tasks;
#endif

namespace ENG.WMOCodes.Downloaders.Retrievers.Metar
{
    public class AviationWeatherGovRetriever : IRetriever
    {
        /// <summary>
        /// Returns URL where METAR information is stored.
        /// </summary>
        /// <param name="icao">ICAO code of airport/station.</param>
        /// <returns></returns>
        public string GetUrlForICAO(string icao)
        {
            return "https://aviationweather.gov/adds/dataserver_current/httpparam?dataSource=metars&requestType=retrieve&format=xml&hoursBeforeNow=24&fields=raw_text&mostRecent=true&stationString="+icao;
        }

#if PCL

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
        public async Task<string> DecodeWMOCodeAsync(Stream sourceStream)
        {
            StreamReader reader = new StreamReader(sourceStream);
            XDocument document = XDocument.Load(reader);
            string report = document.Root
                .Elements("data")
                .Elements("METAR")
                .Elements("raw_text")
                .First()
                .Value;
            return "METAR " + report;
        }

#else

        /// <summary>
        /// Decodes metar from stream. Stream should be downloaded from URL address obtained 
        /// from GetUrlForICAO() method. <seealso cref="GetUrlForICAO"/>.
        /// </summary>
        /// <param name="sourceStream">Source stream, from which the metar will be obtained.</param>
        /// <returns>Metar string.</returns>
        /// <exception cref="DownloadException">Returns if anything fails. Inner exception should contain more accurate info.</exception>
        public string DecodeWMOCode(Stream sourceStream)
        {
            StreamReader reader = new StreamReader(sourceStream);
            XDocument document = XDocument.Load(reader);
            string report = document.Root
                .Elements("data")
                .Elements("METAR")
                .Elements("raw_text")
                .First()
                .Value;
            return "METAR " + report;
        }

#endif
    }
}
