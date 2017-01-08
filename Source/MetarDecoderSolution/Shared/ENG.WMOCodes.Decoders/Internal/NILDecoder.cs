using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;

namespace ENG.WMOCodes.Decoders.Internal
{
    // ReSharper disable once InconsistentNaming
    internal class NILDecoder : TypeDecoder<bool>
    {
        public override string Description => "NIL - nil report";

        public override string RegEx => "^NIL";

        protected override bool DecodeCore(GroupCollection groups)
        {
            return true;
        }
    }
}
