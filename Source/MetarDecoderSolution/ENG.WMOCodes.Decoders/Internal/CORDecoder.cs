using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;

namespace ENG.WMOCodes.Decoders.Internal
{
    // ReSharper disable once InconsistentNaming
    internal class CORDecoder : TypeDecoder<bool>
    {
        public override string Description => "COR - correction";

        public override string RegEx => "^COR";

        protected override bool DecodeCore(GroupCollection groups)
        {
            return true;
        }
    }
}
