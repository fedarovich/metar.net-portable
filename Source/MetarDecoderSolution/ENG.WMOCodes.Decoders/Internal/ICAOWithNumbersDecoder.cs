using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class ICAOWithNumbersDecoder : TypeDecoder<string>
    {
        public override string Description => "ICAO (with numbers)";

        public override string RegEx => "[A-Z0-9]{4}";

        protected override string DecodeCore(GroupCollection groups)
        {
            string ret = groups[0].Value;
            return ret;
        }
    }
}
