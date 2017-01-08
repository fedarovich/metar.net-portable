using System;
using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;
using ENG.WMOCodes.Types.DateTimeTypes;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class TrendInfoForTafDecoder : CustomDecoder<TrendInfoForTaf>
    {
        private class Info
        {
            public TrendType Type;
            public DayHourMinute DayTimeFlag;
            public DayHourDayHour FromToFlag;
        }

        private enum TrendType
        {
            None,
            Tempo,
            Tempo30,
            Tempo40,
            Becmg,
            Fm,
            Prob30,
            Prob40
        }

        public override string Description => "Trend/Tempo/Becoming data";

        private const int NumberOfGroups = 8;

        private string regexPattern => "(^TEMPO)|(^PROB30 TEMPO)|(^PROB40 TEMPO)|(^BECMG)|(^FM)|(^PROB40)|(^PROB30)";

        private TrendType[] regexTypes => new[] { TrendType.None, TrendType.Tempo, TrendType.Tempo30, TrendType.Tempo40, TrendType.Becmg, TrendType.Fm, TrendType.Prob40, TrendType.Prob30 };

        protected override TrendInfoForTaf DecodeCore(ref string source)
        {
            Info info = new Info();

            bool found = DecodePrefix(ref source, info);

            TrendReport tr =
              new TrendReportDecoder().Decode(ref source);

            TrendInfoForTaf ret = CreateTafSubReport(info, tr);

            return ret;
        }

        private TrendInfoForTaf CreateTafSubReport(Info info, TrendReport tr)
        {
            TrendInfoForTaf ret = new TrendInfoForTaf();
            tr.CopyPropertiesTo(ret);

            switch (info.Type)
            {
                case TrendType.Becmg:
                    ret.Interval =
                      new TafIntervalNonFM(
                        TafIntervalType.BECMG, info.FromToFlag);
                    break;
                case TrendType.Fm:
                    ret.Interval =
                      new TafIntervalFM(info.DayTimeFlag);
                    break;
                case TrendType.None:
                    throw new NotSupportedException("At this place the type must be something different from \"None\"");
                case TrendType.Prob30:
                    ret.Interval =
                      new TafIntervalNonFM(
                        TafIntervalType.PROB30, info.FromToFlag);
                    break;
                case TrendType.Prob40:
                    ret.Interval =
                      new TafIntervalNonFM(
                        TafIntervalType.PROB40, info.FromToFlag);
                    break;
                case TrendType.Tempo:
                    ret.Interval =
                      new TafIntervalNonFM(
                        TafIntervalType.TEMPO, info.FromToFlag);
                    break;
                case TrendType.Tempo30:
                    ret.Interval =
                      new TafIntervalNonFM(
                        TafIntervalType.TEMPO_PROB30, info.FromToFlag);
                    break;
                case TrendType.Tempo40:
                    ret.Interval =
                      new TafIntervalNonFM(
                        TafIntervalType.TEMPO_PROB40, info.FromToFlag);
                    break;
                default:
                    throw new NotImplementedException();
            }

            return ret;
        }

        private bool DecodePrefix(ref string source, Info info)
        {
            bool ret;
            var match = Regex.Match(source, regexPattern);
            if (match.Success)
            {
                info.Type = GetTypeOfReport(match.Groups);
                source = CutMatch(source, match.Groups[(int)info.Type].Value);
                if (info.Type == TrendType.Fm)
                    info.DayTimeFlag = GetDayTimeFlag(ref source);
                else
                    info.FromToFlag = GetFromToFlag(ref source);

                ret = true;
            }
            else
                ret = false;

            return ret;
        }

        private DayHourDayHour GetFromToFlag(ref string source)
        {
            DayHourDayHour ret =
              new DayHourDayHourDecoder().Decode(ref source);

            return ret;
        }

        private DayHourMinute GetDayTimeFlag(ref string source)
        {
            DayHourMinute ret = new DayHourMinute();
            string ds = source.Substring(0, 2);
            string hs = source.Substring(2, 2);
            string ms = source.Substring(4, 2);
            ret.Day = int.Parse(ds);
            ret.Hour = int.Parse(hs);
            ret.Minute = int.Parse(ms);
            source = source.Substring(6).TrimStart();
            return ret;
        }

        private TrendType GetTypeOfReport(GroupCollection groups)
        {
            TrendType ret = TrendType.None;
            for (int i = 1; i < NumberOfGroups; i++)
            {
                if (groups[i].Success)
                {
                    ret = regexTypes[i];
                    break;
                }
            }
            return ret;
        }

        private string CutMatch(string source, string match)
        {
            return source.Substring(match.Length).TrimStart();
        }

    }
}
