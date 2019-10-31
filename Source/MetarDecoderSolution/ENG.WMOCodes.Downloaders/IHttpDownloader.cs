using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ENG.WMOCodes.Downloaders
{
    /// <summary>
    /// <para>The classes implementing this interface can be used to download raw METAR reports by the ICAO code.</para>
    /// <para>This interface extends <see cref="IDownloader"/> and allows to use a custom <see cref="HttpMessageHandler"/>.</para>
    /// </summary>
    public interface IHttpDownloader : IDownloader
    {
        /// <summary>
        /// Download metar asynchronously.
        /// </summary>
        /// <param name="icao">Icao code of airport/station.</param>
        /// <param name="handler">The HTTP handler stack to use for sending requests.</param>
        /// <param name="disposeHandler">The value indicating whether the <paramref name="handler"/> must be disposed after the operation completes.</param>
        /// <exception cref="ArgumentNullException"><paramref name="icao"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="handler"/> is <see langword="null"/>.</exception>
        Task<string> DownloadAsync(
            string icao, 
            HttpMessageHandler handler, 
            bool disposeHandler);
    }
}
