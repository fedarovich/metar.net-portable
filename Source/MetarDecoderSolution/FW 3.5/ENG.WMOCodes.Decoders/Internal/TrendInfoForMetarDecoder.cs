using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  class TrendInfoForMetarDecoder : CustomDecoder<TrendInfoForMetar>
  {
    private const string RT_TYPEDATES = @"((NOSIG)|((TEMPO|BECMG)(( " + RT_TYPEDATE + ")*)))";
    private const string RT_TYPEDATE = @"(FM|TL|AT)(\d{2})(\d{2})";

    protected override TrendInfoForMetar _Decode(ref string source)
    {
      TrendInfoForMetar ret = null;

      Match m = Regex.Match(source, RT_TYPEDATES);
      if (m.Success)
      {
        source = source.Substring(m.Groups[0].Length).TrimStart();
        ret = DecodeTrend(m.Groups, ref source);
      }
      else
        if (Required)
          throw new DecodeException(Description, new ArgumentException("source"));
        else
          ret = null;

      return ret;
    }

    private TrendInfoForMetar DecodeTrend(GroupCollection groups, ref string source)
    {
      TrendInfoForMetar ret = new TrendInfoForMetar();

      if (groups[2].Success)
        ret.Type = TrendInfoForMetar.eType.NOSIG;
      else
      {
        ret.Type = (TrendInfoForMetar.eType)Enum.Parse(typeof(TrendInfoForMetar.eType), groups[4].Value, false);

        ret.Times = DecodeTrendDates(groups[5].Value);

        DecodeTrendValues(ref source, ret);
      }

      return ret;
    }

    private void DecodeTrendValues(ref string source, TrendInfoForMetar ret)
    {
      TrendReport pom = new TrendReportDecoder().Decode(ref source);

      pom.CopyPropertiesTo(ret);
    }

    private TrendTimeInfo DecodeTrendDates(string source)
    {
      TrendTimeInfo ret = new TrendTimeInfo();

      Match m = Regex.Match(source, RT_TYPEDATE);
      while (m.Success)
      {
        ret.Add(DecodeTrendDate(m.Groups));

        m = m.NextMatch();
      }
      return ret;
    }

    private static TrendTime DecodeTrendDate(GroupCollection groups)
    {
      TrendTime ret = new TrendTime();

      ret.Type = (TrendTime.eType)Enum.Parse(typeof(TrendTime.eType), groups[1].Value, false);
      ret.Hour = int.Parse(groups[2].Value);
      ret.Minute = int.Parse(groups[3].Value);

      return ret;
    }

    public override string Description
    {
      get { return "METAR TREND"; }
    }
  }
}
