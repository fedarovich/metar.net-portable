using System.IO;
using System.Threading.Tasks;

namespace ENG.WMOCodes.Downloaders.Retrievers.Metar
{
    /// <summary>
    /// Downloads metar from web noaa.gov.
    /// </summary>
    /// <seealso cref="T:ENG.Metar.Downloader.IMetarRetrieve"/>
    public class NoaaGovRetriever : IRetriever
    {
        #region IRetriever Members

        /// <summary>
        /// Returns URL where METAR information is stored.
        /// </summary>
        /// <param name="icao">ICAO code of airport/station.</param>
        /// <returns></returns>
        public string GetUrlForICAO(string icao)
        {
            return "http://tgftp.nws.noaa.gov/data/observations/metar/stations/" + icao.ToUpper() + ".TXT";
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
            var rdr = new StreamReader(sourceStream);
            await rdr.ReadLineAsync().ConfigureAwait(false);
            string r = await rdr.ReadLineAsync().ConfigureAwait(false);

            return "METAR " + r;
        }

        #endregion
    }
}
