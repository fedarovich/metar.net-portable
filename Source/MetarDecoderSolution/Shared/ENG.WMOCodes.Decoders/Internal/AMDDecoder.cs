using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;

namespace ENG.WMOCodes.Decoders.Internal
{
    // ReSharper disable once InconsistentNaming
    internal class AMDDecoder : TypeDecoder<bool>
    {
        public override string Description => "AMD - amended";

        public override string RegEx => "^AMD";

        protected override bool DecodeCore(GroupCollection groups)
        {
            return true;
        }
    }
}
