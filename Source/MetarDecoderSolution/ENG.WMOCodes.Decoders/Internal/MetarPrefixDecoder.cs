using System;
using System.Text.RegularExpressions;
using ENG.WMOCodes.Codes;
using ENG.WMOCodes.Decoders.Internal.Basic;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class MetarPrefixDecoder : TypeDecoder<MetarType>
    {
        public override string Description => "METAR/SPECI prefix";

        public override string RegEx => "(^METAR)|(^SPECI)";

        protected override MetarType DecodeCore(GroupCollection groups)
        {
            MetarType ret;
            if (groups[1].Success)
                ret = MetarType.METAR;
            else if (groups[2].Success)
                ret = MetarType.SPECI;
            else
                throw new NotSupportedException();
            return ret;
        }
    }
}
