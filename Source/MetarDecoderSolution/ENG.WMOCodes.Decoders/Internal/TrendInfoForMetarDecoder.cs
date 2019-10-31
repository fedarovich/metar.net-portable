using System;
using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class TrendInfoForMetarDecoder : CustomDecoder<TrendInfoForMetar>
    {
        private const string TypeDateRegexPattern = @"(FM|TL|AT)(\d{2})(\d{2})";
        private const string TypeDatesRegexPattern = @"((NOSIG)|((TEMPO|BECMG)(( " + TypeDateRegexPattern + ")*)))";

        protected override TrendInfoForMetar DecodeCore(ref string source)
        {
            TrendInfoForMetar ret;

            Match m = Regex.Match(source, TypeDatesRegexPattern);
            if (m.Success)
            {
                source = source.Substring(m.Groups[0].Length).TrimStart();
                ret = DecodeTrend(m.Groups, ref source);
            }
            else
            {
                if (Required)
                    throw new DecodeException(Description, new ArgumentException("source"));
                ret = null;
            }

            return ret;
        }

        private TrendInfoForMetar DecodeTrend(GroupCollection groups, ref string source)
        {
            TrendInfoForMetar ret = new TrendInfoForMetar();

            if (groups[2].Success)
                ret.Type = MetarTrendType.NOSIG;
            else
            {
                ret.Type = (MetarTrendType)Enum.Parse(typeof(MetarTrendType), groups[4].Value, false);

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

            Match m = Regex.Match(source, TypeDateRegexPattern);
            while (m.Success)
            {
                ret.Add(DecodeTrendDate(m.Groups));

                m = m.NextMatch();
            }
            return ret;
        }

        private static TrendTime DecodeTrendDate(GroupCollection groups)
        {
            TrendTime ret = new TrendTime
            {
                Type = (TrendTimeType) Enum.Parse(typeof(TrendTimeType), groups[1].Value, false),
                Hour = int.Parse(groups[2].Value),
                Minute = int.Parse(groups[3].Value)
            };

            return ret;
        }

        public override string Description => "METAR TREND";
    }
}
