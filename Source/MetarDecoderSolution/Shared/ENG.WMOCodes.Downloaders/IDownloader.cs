using System;
using System.Threading.Tasks;

namespace ENG.WMOCodes.Downloaders
{
    /// <summary>
    /// The classes implementing this interface can be used to download raw METAR reports by the ICAO code.
    /// </summary>
    public interface IDownloader
    {
        /// <summary>
        /// Download metar asynchronously.
        /// </summary>
        /// <param name="icao">Icao code of airport/station.</param>
        /// <exception cref="ArgumentNullException"><paramref name="icao"/> is <see langword="null"/>.</exception>
        Task<string> DownloadAsync(string icao);
    }
}
