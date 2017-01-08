using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
    class PressureInfoDecoder : TypeDecoder<PressureInfo>
    {
        public override string Description => "Air pressure";

        public override string RegEx => @"(^(Q|A)(\d{4}))";

        protected override PressureInfo _Decode(System.Text.RegularExpressions.GroupCollection groups)
        {
            PressureInfo ret = new PressureInfo();

            if (groups[2].Value == "Q")
                ret.Set(groups[3].GetIntValue(), PressureUnit.hPa);
            else
                ret.Set(groups[3].GetIntValue() / 100.0, PressureUnit.inHg);

            return ret;
        }
    }
}
