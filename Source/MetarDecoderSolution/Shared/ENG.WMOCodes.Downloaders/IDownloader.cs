using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ENG.WMOCodes.Downloaders
{
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
