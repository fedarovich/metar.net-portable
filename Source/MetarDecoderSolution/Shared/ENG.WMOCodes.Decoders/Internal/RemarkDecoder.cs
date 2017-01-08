using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class RemarkDecoder : TypeDecoder<string>
    {
        public override string Description => "Remark";

        public override string RegEx => "^RMK (.*)";

        protected override string DecodeCore(GroupCollection groups)
        {
            return groups[1].Value;
        }
    }
}
