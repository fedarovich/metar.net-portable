using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class TafPrefixDecoder : TypeDecoder<bool>
    {
        public override string Description => "TAF prefix";

        public override string RegEx => "TAF";

        protected override bool DecodeCore(GroupCollection groups)
        {
            return groups[0].Success;
        }
    }
}
