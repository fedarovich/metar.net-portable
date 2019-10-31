using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class SeaStateDecoder : TypeDecoder<SeaState?>
    {
        public override string Description => "Sea state";

        public override string RegEx => @"^/(\d{2})";

        protected override SeaState? DecodeCore(GroupCollection groups)
        {
            int pom = groups[1].GetIntValue();

            SeaState ret = (SeaState)pom;

            return ret;
        }
    }
}
