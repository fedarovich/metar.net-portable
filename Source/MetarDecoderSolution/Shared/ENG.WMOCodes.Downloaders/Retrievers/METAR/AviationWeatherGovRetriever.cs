using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ENG.WMOCodes.Downloaders.Retrievers.Metar
{
    /// <summary>
    /// Downloads metar from web aviationweather.gov.
    /// </summary>
    /// <seealso cref="T:ENG.Metar.Downloader.IMetarRetrieve"/>
    public class AviationWeatherGovRetriever : IRetriever, IHistoricalRetriever
    {
        /// <summary>
        /// Returns URL where METAR information is stored.
        /// </summary>
        /// <param name="icao">ICAO code of airport/station.</param>
        /// <returns></returns>
        public string GetUrlForICAO(string icao)
        {
            return "https://aviationweather.gov/adds/dataserver_current/httpparam?dataSource=metars&requestType=retrieve&format=xml&hoursBeforeNow=24&fields=raw_text,metar_type&mostRecent=true&stationString=" + icao;
        }

        public string GetUrlForICAO(string icao, TimeSpan period)
        {
            if (period <= TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(period));

            var hoursBeforeNow = period.TotalHours.ToString("##.####", CultureInfo.InvariantCulture);
            return $"https://aviationweather.gov/adds/dataserver_current/httpparam?dataSource=metars&requestType=retrieve&format=xml&hoursBeforeNow={hoursBeforeNow}&fields=raw_text,metar_type&stationString={icao}";
        }

        public string GetUrlForICAO(string icao, DateTimeOffset startTime, DateTimeOffset endTime)
        {
            if (startTime > endTime)
                throw new ArgumentException("endTime must be greater than startTime");
            
            return $"https://aviationweather.gov/adds/dataserver_current/httpparam?dataSource=metars&requestType=retrieve&format=xml&startTime={startTime:O}&endTime={endTime:O}&fields=raw_text,metar_type&stationString={icao}"
                .Replace("+", "%2B");
        }

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
            XDocument document = XDocument.Parse(await reader.ReadToEndAsync().ConfigureAwait(false));
            if (document.Root == null)
                throw new DownloadException("Invalid XML document.");
            var metars = document.Root
                .Elements("data")
                .Elements("METAR");
            string report = metars.Elements("raw_text").First().Value;
            string type = metars.Elements("metar_type").FirstOrDefault()?.Value ?? "METAR";
            return type + " " + report;
        }

        public async Task<IEnumerable<string>> DecodeWMOCodesAsync(Stream sourceStream)
        {
            StreamReader reader = new StreamReader(sourceStream);
            XDocument document = XDocument.Parse(await reader.ReadToEndAsync().ConfigureAwait(false));
            if (document.Root == null)
                throw new DownloadException("Invalid XML document.");
            var metars = document.Root
                .Elements("data")
                .Elements("METAR");
            var results =
                from metar in metars
                let report = metar.Elements("raw_text").FirstOrDefault()?.Value
                where report != null
                let type = metar.Elements("metar_type").FirstOrDefault()?.Value ?? "METAR"
                select type + " " + report;
            return results.ToArray();
        }
    }
}
