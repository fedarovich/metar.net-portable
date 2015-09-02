using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Codes;
using ENG.WMOCodes.Decoders.Internal;

namespace ENG.WMOCodes.Decoders
{
  /// <summary>
  /// Represents decoder for the TAF report.
  /// </summary>
  public class TafDecoder : PublicDecoder<Taf>
  {
    /// <summary>
    /// Gets the description.
    /// </summary>
    /// <value>The description.</value>
    public override string Description
    {
      get { return "Whole TAF"; }
    }

    /// <summary>
    /// The descendant's implementation of the decoding function.
    /// </summary>
    /// <param name="source">Source string to decode.</param>
    protected override Taf _Decode(string source)
    {
      string pom = source;
      Taf ret = new Taf();

      try
      {
        new TafPrefixDecoder().Decode(ref pom);
        ret.IsCorrected = new CORDecoder() { Required = false }.Decode(ref pom);
        ret.IsAmmended = new AMDDecoder() { Required = false }.Decode(ref pom);
        ret.ICAO = new ICAODecoder().Decode(ref pom);
        ret.DayTime = new DayHourMinuteDecoder().Decode(ref pom);
        ret.IsMissing = new NILDecoder() { Required = false }.Decode(ref pom);
        if (ret.IsMissing == false)
        {
          ret.Period = new DayHourDayHourDecoder().Decode(ref pom);
          ret.IsCancelled = new CNLDecoder() { Required = false }.Decode(ref pom);
          if (ret.IsCancelled == false)
          {
            ret.Wind = new WindDecoder() { Required=false }.Decode(ref pom);
            ret.Visibility = new VisibilityDecoder() { Required = false }.Decode(ref pom);
            ret.Phenomens = new PhenomInfoWithNSWDecoder() { Required = false }.Decode(ref pom);
            ret.Clouds = new CloudInfoDecoder() { Required = false }.Decode(ref pom);
            ret.MaxTemperature = new TXDecoder() { Required = false }.Decode(ref pom);
            ret.MinTemperature = new TNDecoder() { Required = false }.Decode(ref pom);
            ret.Trends = new TrendInfoForTafListDecoder().Decode(ref pom);
          }
        }
        ret.Remark = new RemarkDecoder() { Required = false }.Decode(ref pom);
      }
      catch (Exception ex)
      {
        throw new DecodeException(Description, ex);
      }

      return ret;
    }

  }
}
