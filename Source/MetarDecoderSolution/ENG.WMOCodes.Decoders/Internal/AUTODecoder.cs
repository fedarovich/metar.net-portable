using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;

namespace ENG.WMOCodes.Decoders.Internal
{
    // ReSharper disable once InconsistentNaming
    internal class AUTODecoder : TypeDecoder<bool>
    {
        public override string Description => "AUTO - automated report";

        public override string RegEx => "^AUTO";

        protected override bool DecodeCore(GroupCollection groups)
        {
            return true;
        }
    }
}
