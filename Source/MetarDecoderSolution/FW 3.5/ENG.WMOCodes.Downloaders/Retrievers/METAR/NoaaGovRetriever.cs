using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
#if PCL
using System.Threading.Tasks;
#endif

namespace ENG.WMOCodes.Downloaders.Retrievers.Metar
{
    /// <summary>
    /// Downloads metar from web noaa.gov.
    /// </summary>
    /// <seealso cref="T:ENG.Metar.Downloader.IMetarRetrieve"/>
    public class NoaaGovRetriever : IRetriever
    {
        #region IMetarRetrieve Members

        /// <summary>
        /// Returns URL where METAR information is stored.
        /// </summary>
        /// <param name="icao">ICAO code of airport/station.</param>
        /// <returns></returns>
        public string GetUrlForICAO(string icao)
        {
            return "http://tgftp.nws.noaa.gov/data/observations/metar/stations/" + icao.ToUpper() + ".TXT";
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
            var rdr = new StreamReader(sourceStream);
            await rdr.ReadLineAsync().ConfigureAwait(false);
            string r = await rdr.ReadLineAsync().ConfigureAwait(false);

            return "METAR " + r;
        }

#else

        /// <summary>
        /// Decodes metar from stream. Stream should be downloaded from URL address obtained 
        /// from GetUrlForICAO() method. <seealso cref="GetUrlForICAO"/>.
        /// </summary>
        /// <param name="sourceStream">Source stream, from which the metar will be obtained.</param>
        /// <returns>Metar string.</returns>
        /// <exception cref="DownloadException">Returns if anything fails. Inner exception should contain more accurate info.</exception>
        public string DecodeWMOCode(System.IO.Stream sourceStream)
        {
            System.IO.StreamReader rdr = new System.IO.StreamReader(sourceStream);
            rdr.ReadLine();
            string r = rdr.ReadLine();

            return "METAR " + r;
        }

#endif

        #endregion
    }
}
