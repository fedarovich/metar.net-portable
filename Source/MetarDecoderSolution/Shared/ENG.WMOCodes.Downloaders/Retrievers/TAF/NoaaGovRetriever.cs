using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENG.WMOCodes.Downloaders.Retrievers.Taf
{
    /// <summary>
    /// Downloads taf data from weather.noaa.gov web site.
    /// </summary>
    public class NoaaGovRetriever : IRetriever
    {
        #region IRetriever Members

        private const string url = @"http://tgftp.nws.noaa.gov/data/forecasts/taf/stations/";
        /// <summary>
        /// Returns URL where Code information is stored.
        /// </summary>
        /// <param name="icao">ICAO code of airport/station.</param>
        /// <returns></returns>
        public string GetUrlForICAO(string icao)
        {
            return url + icao.ToUpper() + ".TXT";
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
            StringBuilder ret = new StringBuilder();

            StreamReader rdr = new StreamReader(sourceStream);
            await rdr.ReadLineAsync().ConfigureAwait(false);
            string line = await rdr.ReadLineAsync().ConfigureAwait(false);
            while (line != null)
            {
                ret.Append(line + " ");
                line = rdr.ReadLine();
            }

            return ret.ToString();
        }

        #endregion
    }
}
