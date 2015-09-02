using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Codes;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Decoders.Internal;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders
{
  /// <summary>
  /// Represents decoder able decode non-full SPECI/METAR reports. WARNING! In this case
  /// some fields (which were not set) may contain invalid or unconsistent data!
  /// </summary>
  public class MetarDecoderWithAllOptional : PublicDecoder<Metar>
  {
    /// <summary>
    /// Gets the description.
    /// </summary>
    /// <value>The description.</value>
    public override string Description
    {
      get { return "Whole METAR/SPECI with optional temperature"; }
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

      ret.ICAO = new ICAOWithNumbersDecoder().Decode(ref p);

      ret.Date = new DayHourMinuteDecoder().Decode(ref p);

      ret.IsMissing = new NILDecoder() { Required = false }.Decode(ref p);

      ret.IsAUTO = new AUTODecoder() { Required = false }.Decode(ref p);

      var wind = new WindWithVariabilityDecoder() { Required = false }.Decode(ref p);
      if (wind == null)
        wind = new WindWithVariability();
      ret.Wind = wind;

      var visib = new VisibilityForMetarDecoder() { Required = false }.Decode(ref p);
      if (visib == null)
        visib = new VisibilityForMetar();
      ret.Visibility = visib;
      ret.Visibility.Runways =
        new RunwayVisibilityListDecoder() { Required = false }.Decode(ref p);

      var phenomens = new PhenomInfoDecoder() { Required = false }.Decode(ref p);
      if (phenomens == null) phenomens = new Types.PhenomInfo();
      ret.Phenomens = phenomens;

      ret.Clouds = new CloudInfoWithNCDDecoder() { Required = false }.Decode(ref p);

      ret.Temperature = new TemperatureDecoder() { Required = false }.Decode(ref p);

      ret.DewPoint = new DewPointDecoder() { Required = false }.Decode(ref p);

      PressureInfo pi = new PressureInfoDecoder() { Required = false }.Decode(ref p);
      if (pi == null)
        pi = new PressureInfo() { QNH = 1013 };
      ret.Pressure = pi;

      ret.RePhenomens = new RePhenomInfoDecoder() { Required = false }.Decode(ref p);

      ret.WindShears = new WindShearInfoDecoder() { Required = false }.Decode(ref p);

      ret.SeaSurfaceTemperature = new SeaSurfaceTemperatureDecoder() { Required = false }.Decode(ref p);

      ret.SeaState = new SeaStateDecoder() { Required = false }.Decode(ref p);

      ret.RunwayConditions = new RunwayConditionInfoDecoder() { Required = false }.Decode(ref p);

      ret.Trend = new TrendInfoForMetarDecoder() { Required = false }.Decode(ref p);

      ret.Remark = new RemarkDecoder() { Required = false }.Decode(ref p);

      return ret;
    }
  }
}
