using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
#if PCL
using System.Threading.Tasks;
#endif

namespace ENG.WMOCodes.Downloaders.Retrievers.Metar
{
    /// <summary>
    /// This class is able to download metar from usa vatsim network source.
    /// Downloaded metar is associated to VATSIM online network, and can differ
    /// significantly from real weather.
    /// </summary>  
    [Obsolete("Use ENG.WMOCodes.Downloaders.Retrievers.METAR.VatsimRetriever instead.")]
    public class USAS1VatsimRetriever : IRetriever
    {
        /// <summary>
        /// URL
        /// </summary>
        private const string SOURCE = "http://usa-s1.vatsim.net/data/metar.php?id=";

        #region IMetarRetrieve Members

        /// <summary>
        /// Returns URL where METAR information is stored.
        /// </summary>
        /// <param name="icao">ICAO code of airport/station.</param>
        /// <returns></returns>
        public string GetUrlForICAO(string icao)
        {
            return SOURCE + icao.ToUpper();
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
            StreamReader str = new StreamReader(sourceStream);

            string metar = await str.ReadToEndAsync().ConfigureAwait(false);

            if (IsNotValidMetar(metar))
                throw new Exception("Invalid icao code.");

            return "METAR " + metar;
        }

#else

        /// <summary>
        /// Decodes metar from stream. Stream should be downloaded from URL address obtained 
        /// from GetUrlForICAO() method. <seealso cref="GetUrlForICAO"/>.
        /// </summary>
        /// <param name="sourceStream">Source stream, from which the metar will be obtained.</param>
        /// <returns>Metar string.</returns>
        /// <exception cref="DownloadException">
        /// Returns if anything fails. Inner exception should contain more accurate info.
        /// </exception>
        ///     
        public string DecodeWMOCode(System.IO.Stream sourceStream)
        {
            StreamReader str = new StreamReader(sourceStream);

            string metar = str.ReadToEnd();

            if (IsNotValidMetar(metar))
                throw new Exception("Invalid icao code.");

            return "METAR " + metar;
        }

#endif

        /// <summary>
        /// Checks if metar is valid.
        /// </summary>
        /// <param name="metar"></param>
        /// <returns></returns>
        private static bool IsNotValidMetar(string metar)
        {
            if (metar.StartsWith("No METAR available"))
                return true;
            else
                return false;
        }

#endregion
    }
}
