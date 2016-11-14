using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
#if PCL
using System.Threading.Tasks;
#endif

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

#else

        /// <summary>
        /// Decodes code as string from stream. Stream should be downloaded from URL address obtained
        /// from GetUrlForICAO() method. <seealso cref="GetUrlForICAO"/>.
        /// </summary>
        /// <param name="sourceStream">Source stream, from which the metar will be obtained.</param>
        /// <returns>Code string.</returns>
        /// <exception cref="DownloadException">
        /// Returns if anything fails. Inner exception should contain more accurate info.
        /// </exception>
        public string DecodeWMOCode(System.IO.Stream sourceStream)
        {
            StringBuilder ret = new StringBuilder();

            System.IO.StreamReader rdr = new System.IO.StreamReader(sourceStream);
            rdr.ReadLine();
            string line = rdr.ReadLine();
            while (line != null)
            {
                ret.Append(line + " ");
                line = rdr.ReadLine();
            }

            return ret.ToString();
        }

#endif

        #endregion
    }
}
