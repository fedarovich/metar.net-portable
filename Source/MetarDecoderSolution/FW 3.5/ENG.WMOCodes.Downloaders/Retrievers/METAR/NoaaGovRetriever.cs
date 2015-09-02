using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
      return "http://weather.noaa.gov/pub/data/observations/metar/stations/" + icao.ToUpper() + ".TXT";
    }

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

    #endregion
  }
}
