using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ENG.WMOCodes.Downloaders
{
    /// <summary>
    /// Class responsible for downloading metar information from source.
    /// </summary>
    public class Downloader : IHttpDownloader
    {
        private readonly IRetriever _retriever;

        public Downloader(IRetriever retriever)
        {
            if (retriever == null)
                throw new ArgumentNullException(nameof(retriever));

            _retriever = retriever;
        }

        /// <summary>
        /// Download metar asynchronously.
        /// </summary>
        /// <param name="icao">Icao code of airport/station.</param>
        /// <exception cref="ArgumentNullException"><paramref name="icao"/> is <see langword="null"/>.</exception>
        public Task<string> DownloadAsync(string icao)
        {
            return DownloadAsync(icao, new HttpClientHandler(), true);
        }

        /// <summary>
        /// Download metar asynchronously.
        /// </summary>
        /// <param name="icao">Icao code of airport/station.</param>
        /// <param name="handler">The HTTP handler stack to use for sending requests.</param>
        /// <param name="disposeHandler">The value indicating whether the <paramref name="handler"/> must be disposed after the operation completes.</param>
        /// <exception cref="ArgumentNullException"><paramref name="icao"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="handler"/> is <see langword="null"/>.</exception>
        public async Task<string> DownloadAsync(
            string icao, 
            HttpMessageHandler handler, 
            bool disposeHandler)
        {
            if (icao == null)
                throw new ArgumentNullException(nameof(icao));
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            var url = _retriever.GetUrlForICAO(icao);
            var httpClient = new HttpClient(handler, disposeHandler);
            try
            {
                using (var response = await httpClient.GetStreamAsync(url).ConfigureAwait(false))
                {
                    return await _retriever.DecodeWMOCodeAsync(response).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                throw new DownloadException("Failed to download metar from web.", ex);
            }
            finally
            {
                httpClient.Dispose();
            }
        }
    }
}
