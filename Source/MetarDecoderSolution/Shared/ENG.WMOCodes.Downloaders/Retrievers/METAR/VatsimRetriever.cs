using System;
using System.IO;
using System.Threading.Tasks;

namespace ENG.WMOCodes.Downloaders.Retrievers.Metar
{
    /// <summary>
    /// This class is able to download metar from usa vatsim network source.
    /// Downloaded metar is associated to VATSIM online network, and can differ
    /// significantly from real weather.
    /// </summary>  
    public class VatsimRetriever : IRetriever
    {
        /// <summary>
        /// URL
        /// </summary>
        private const string Source = "http://metar.vatsim.net/metar.php?id=";

        #region IRetriever Members

        /// <summary>
        /// Returns URL where METAR information is stored.
        /// </summary>
        /// <param name="icao">ICAO code of airport/station.</param>
        /// <returns></returns>
        public string GetUrlForICAO(string icao)
        {
            return Source + icao.ToUpper();
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
            StreamReader str = new StreamReader(sourceStream);

            string metar = await str.ReadToEndAsync().ConfigureAwait(false);

            if (IsNotValidMetar(metar))
                throw new Exception("Invalid icao code.");

            return "METAR " + metar;
        }

        /// <summary>
        /// Checks if metar is valid.
        /// </summary>
        /// <param name="metar"></param>
        /// <returns></returns>
        private static bool IsNotValidMetar(string metar)
        {
            return metar.StartsWith("No METAR available");
        }

        #endregion
    }
}