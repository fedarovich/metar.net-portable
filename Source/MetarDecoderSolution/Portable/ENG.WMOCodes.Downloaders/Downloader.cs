using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ENG.WMOCodes.Downloaders
{
    /// <summary>
    /// Class responsible for downloading metar information from source.
    /// </summary>
    public class Downloader
    {
        private readonly IRetriever retriever;

        /// <summary>
        /// Delegate used to announce when asynchronous download is completed.
        /// Is used for both, successful and unsuccessful downloads.
        /// </summary>
        /// <param name="result">Result containing data</param>
        public delegate void DownloadCompletedDelegate(RetrieveResult result);

        public Downloader(IRetriever retriever)
        {
            if (retriever == null)
                throw new ArgumentNullException("retriever");

            this.retriever = retriever;
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
                throw new ArgumentNullException("icao");
            if (handler == null)
                throw new ArgumentNullException("handler");

            var url = retriever.GetUrlForICAO(icao);
            var httpClient = new HttpClient(handler, disposeHandler);
            try
            {
                using (var response = await httpClient.GetStreamAsync(url).ConfigureAwait(false))
                {
                    return await retriever.DecodeWMOCodeAsync(response).ConfigureAwait(false);
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

        /// <summary>
        /// Download metar asynchronously.
        /// </summary>
        /// <param name="icao">Icao code of airport/station.</param>
        /// <param name="downloadCompletedDelegate">Delegate function raised when download is completed or error occured.</param>
        /// <exception cref="DownloadException">
        /// Raised when any error occurs.
        /// </exception>
        [Obsolete("Use the overload returning Task<string> instead.")]
        public async void DownloadAsync(string icao, DownloadCompletedDelegate downloadCompletedDelegate)
        {
            try
            {
                var metar = await DownloadAsync(icao);
                downloadCompletedDelegate(new RetrieveResult(metar));
            }
            catch (Exception ex)
            {
                downloadCompletedDelegate(new RetrieveResult(ex));
            }
        }

        /// <summary>
        /// Download metar asynchronously.
        /// </summary>
        /// <param name="icao">Icao code of airport/station.</param>
        /// <param name="retriever">Metar retrievere used to decode metar from source stream</param>
        /// <param name="downloadCompletedDelegate">Delegate function raised when download is completed or error occured.</param>
        /// <exception cref="DownloadException">
        /// Raised when any error occurs.
        /// </exception>
        [Obsolete("Use the overload returning Task<string> instead.")]
        public static void DownloadAsync(string icao, IRetriever retriever,
            DownloadCompletedDelegate downloadCompletedDelegate)
        {
#pragma warning disable 618
            new Downloader(retriever).DownloadAsync(icao, downloadCompletedDelegate);
#pragma warning restore 618
        }
    }
}
