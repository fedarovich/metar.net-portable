using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;

namespace ENG.WMOCodes.Decoders.Internal
{
    // ReSharper disable once InconsistentNaming
    internal class CNLDecoder : TypeDecoder<bool>
    {
        public override string Description => "CNL - amended";

        public override string RegEx => "^CNL";

        protected override bool DecodeCore(GroupCollection groups)
        {
            return true;
        }
    }
}
