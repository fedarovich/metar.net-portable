using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace ENG.WMOCodes.Downloaders
{
  /// <summary>
  /// Class responsible for downloading metar information from source.
  /// </summary>
  public class Downloader
  {
    /// <summary>
    /// Local decoder used to retrieve metar.
    /// </summary>
    /// 
    IRetriever retr;
    /// <summary>
    /// Delegate used to announce when asynchronous download is completed.
    /// Is used for both, successful and unsuccessful downloads.
    /// </summary>
    /// <param name="result">Result containing data</param>
    public delegate void DownloadCompletedDelegate(RetrieveResult result);
    /// <summary>
    /// private async synchro variable
    /// </summary>
    private string aIcao;
    /// <summary>
    /// private async synchro variable
    /// </summary>
    private DownloadCompletedDelegate aDel;

    /// <summary>
    /// Initializes a new Instance of Downloader
    /// </summary>
    /// <param name="retriever">Retrievere used to achieve code string from source stream</param>
    public Downloader(IRetriever retriever)
    {
      retr = retriever;
    }

#if SILVERLIGHT == false

    /// <summary>
    /// Download metar synchronously.
    /// </summary>
    /// <param name="icao">Icao code of airport/station.</param>
    /// <param name="retriever">Metar retrievere used to decode metar from source stream</param>
    /// <returns>Metar as string.</returns>
    /// <exception cref="DownloadException">
    /// Raised when any error occurs.
    /// </exception>
    public static string Download(string icao, IRetriever retriever)
    {
      Downloader d = new Downloader(retriever);

      string ret = d.Download(icao);

      return ret;
    }

#endif


#if SILVERLIGHT == false

    /// <summary>
    /// Download metar synchronously.
    /// </summary>
    /// <param name="ICAO">Icao code of airport/station.</param>
    /// <returns>Metar as string.</returns>
    /// <exception cref="DownloadException">
    /// Raised when any error occurs.
    /// </exception>
    public string Download(string ICAO)
    {
      string ret = "";

      WebRequest req = HttpWebRequest.Create(
        retr.GetUrlForICAO(ICAO));

      try
      {
        WebResponse resp = req.GetResponse();

        System.IO.Stream respStream = resp.GetResponseStream();

        ret = retr.DecodeWMOCode(respStream);

        respStream.Close();
        resp.Close();
      }
      catch (Exception ex)
      {
        throw new DownloadException ("Failed to download metar from web.", ex) ;
      }

      return ret;
    }

#endif

    /// <summary>
    /// Download metar asynchronously.
    /// </summary>
    /// <param name="icao">Icao code of airport/station.</param>
    /// <param name="retriever">Metar retrievere used to decode metar from source stream</param>
    /// <param name="downloadCompletedDelegate">Delegate function raised when download is completed or error occured.</param>
    /// <exception cref="DownloadException">
    /// Raised when any error occurs.
    /// </exception>
    public static void DownloadAsync(string icao, IRetriever retriever,
      DownloadCompletedDelegate downloadCompletedDelegate)
    {
      Downloader d = new Downloader(retriever);

      d.DownloadAsync(icao, downloadCompletedDelegate);
    }

    /// <summary>
    /// Download metar asynchronously.
    /// </summary>
    /// <param name="icao">Icao code of airport/station.</param>
    /// <param name="downloadCompletedDelegate">Delegate function raised when download is completed or error occured.</param>
    /// <exception cref="DownloadException">
    /// Raised when any error occurs.
    /// </exception>
    public void DownloadAsync(
      string icao, DownloadCompletedDelegate downloadCompletedDelegate)
    {
      System.Threading.Thread t = new System.Threading.Thread(
        new System.Threading.ThreadStart(DownloadAsynchronously));

      aIcao = icao;
      aDel = downloadCompletedDelegate;

      t.Start();
    }

    private class MyRequest
    {
      public WebRequest Request = null;
      public WebResponse Response = null;
      public Stream Stream = null;
      public DownloadCompletedDelegate Finisher = null;
    }

    /// <summary>
    /// Used to download metar asynchronously.
    /// </summary>
    private void DownloadAsynchronously()
    {
      string icao = aIcao;

      RetrieveResult ret = null;

      WebRequest req = HttpWebRequest.Create(
        retr.GetUrlForICAO(icao));

      MyRequest mr = new MyRequest()
      {
        Request = req,
        Finisher = aDel
      };

      try
      {

        req.BeginGetResponse(new AsyncCallback(_BeginGetResponseCallback), mr);

      }
      catch (Exception ex)
      {
        ret = new RetrieveResult(
          new DownloadException("Failed to download metar from web.", ex));
      }

      if (ret != null)
        mr.Finisher(ret);
    }

    private void _BeginGetResponseCallback(IAsyncResult asynchronousResult)
    {
      RetrieveResult ret = null;
      MyRequest myRequestState = (MyRequest)asynchronousResult.AsyncState;

      try
      {
        // State of request is asynchronous.
        WebRequest myHttpWebRequest = myRequestState.Request;
        myRequestState.Response = (HttpWebResponse)myHttpWebRequest.EndGetResponse(asynchronousResult);

        // Read the response into a Stream object.
        Stream responseStream = myRequestState.Response.GetResponseStream();
        myRequestState.Stream = responseStream;

        string metar = retr.DecodeWMOCode(responseStream);

        myRequestState.Stream.Close();
        myRequestState.Response.Close();

        ret = new RetrieveResult(metar);
      }
      catch (WebException ex)
      {
        ret = new RetrieveResult(
          new Exception("Failed to download data with metar.", ex));
      }

      myRequestState.Finisher(ret);
    }

  }
}
