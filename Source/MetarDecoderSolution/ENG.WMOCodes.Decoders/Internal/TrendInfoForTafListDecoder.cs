using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class TrendInfoForTafListDecoder : CustomDecoder<List<TrendInfoForTaf>>
    {
        public override string Description => "Trend/Tempo/Becoming sets";

        public string RegexPattern => "(^TEMPO)|(^BECMG)|(^FM)|(^PROB40)|(^PROB30)";

        protected override List<TrendInfoForTaf> DecodeCore(ref string source)
        {
            List<TrendInfoForTaf> ret = new List<TrendInfoForTaf>();

            string p = source;
            bool found = true;
            while (found)
            {
                var m = Regex.Match(p, RegexPattern);
                if (m.Success)
                {
                    TrendInfoForTaf report;
                    try
                    {
                        report = new TrendInfoForTafDecoder().Decode(ref p);
                    } // try
                    catch (Exception ex)
                    {
                        throw new DecodeException(Description, ex);
                    } // catch (Exception ex)
                    ret.Add(report);
                }
                else
                {
                    found = false;
                }
            }

            source = p;
            return ret;
        }
    }
}
