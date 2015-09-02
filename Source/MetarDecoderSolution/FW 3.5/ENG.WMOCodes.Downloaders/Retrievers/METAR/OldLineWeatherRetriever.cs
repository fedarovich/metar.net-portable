using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ENG.WMOCodes.Downloaders;

namespace ENG.WMOCodes.Downloaders.Retrievers.Metar
{
  /// <summary>
  /// This class is able to download metar from web OldLineWeather.
  /// </summary>
  /// <seealso cref="T:ENG.Metar.Downloader.IMetarRetrieve"/>
  public class OldLineWeatherRetriever : IRetriever
  {
    #region IMetarRetrieve Members

    /// <summary>
    /// Returns URL where METAR information is stored.
    /// </summary>
    /// <param name="icao">ICAO code of airport/station.</param>
    /// <returns></returns>
    public string GetUrlForICAO(string icao)
    {
      return "http://www.oldlineweather.com/wxmetar.php?station=" + icao.ToUpper();
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
      string ret = null;

      System.IO.StreamReader rdr = new System.IO.StreamReader(sourceStream);
      string pom = rdr.ReadToEnd();
      rdr = null;
      
      string rgx = @"METAR = (([A-Z]|[0-9]|/|[+\-]| )+)";

      Match m = Regex.Match(pom, rgx);
      if (m.Success)
        ret = m.Groups[1].Value;
      else
        throw new DownloadException("Unable to decode information from page. Incorrect ICAO?");

      ret = "METAR " + ret;

      return ret;
    }

    #endregion
  }
}
