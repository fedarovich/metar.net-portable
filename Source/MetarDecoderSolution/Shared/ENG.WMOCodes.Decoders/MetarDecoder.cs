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
  /// Represents decoder for metar code.
  /// </summary>
  public class MetarDecoder : PublicDecoder<Metar>
  {
    /// <summary>
    /// Gets the description.
    /// </summary>
    /// <value>The description.</value>
    public override string Description
    {
      get { return "Whole METAR"; }
    }

    /// <summary>
    /// The descendant's implementation of the decoding function.
    /// </summary>
    /// <param name="source">Source string to decode.</param>
    /// <returns></returns>
    protected override Metar _Decode(string source)
    {
      Metar ret = new Metar();
      string p = source;

      ret.Type = new MetarPrefixDecoder().Decode(ref p);
      ret.IsCorrected = new CORDecoder() { Required = false }.Decode(ref p);
      ret.ICAO = new ICAODecoder().Decode(ref p);
      ret.Date = new DayHourMinuteDecoder().Decode(ref p);
      ret.IsMissing = new NILDecoder() { Required = false }.Decode(ref p);
      ret.IsAUTO = new AUTODecoder() { Required = false }.Decode(ref p);
      ret.Wind = new WindWithVariabilityDecoder().Decode(ref p);
      ret.Visibility = new VisibilityForMetarDecoder().Decode(ref p);
      ret.Visibility.Runways =
        new RunwayVisibilityListDecoder().Decode(ref p);
      var phenomens = new PhenomInfoDecoder() { Required = false }.Decode(ref p);
      if (phenomens == null) phenomens = new Types.PhenomInfo();
      ret.Phenomens = phenomens;
      ret.Clouds = new CloudInfoWithNCDDecoder() { Required = false }.Decode(ref p);
      ret.Temperature = new TemperatureDecoder().Decode(ref p);
      ret.DewPoint = new DewPointDecoder().Decode(ref p);
      ret.Pressure = new PressureInfoDecoder().Decode(ref p);
      ret.RePhenomens = new RePhenomInfoDecoder().Decode(ref p);
      ret.WindShears = new WindShearInfoDecoder().Decode(ref p);
      ret.SeaSurfaceTemperature = new SeaSurfaceTemperatureDecoder() { Required = false }.Decode(ref p);
      ret.SeaState = new SeaStateDecoder() { Required = false }.Decode(ref p);
      ret.RunwayConditions = new RunwayConditionInfoDecoder().Decode(ref p);
      ret.Trend = new TrendInfoForMetarDecoder() { Required = false }.Decode(ref p);
      ret.Remark = new RemarkDecoder() { Required = false }.Decode(ref p);

      return ret;
    }
  }
}
