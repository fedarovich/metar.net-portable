using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class WindVariableDecoder : TypeDecoder<WindVariable>
    {
        public override string Description => "Wind variability";

        public override string RegEx => @"^((\d{3})V(\d{3}))";

        protected override WindVariable DecodeCore(GroupCollection groups)
        {
            WindVariable ret = new WindVariable
            {
                FromDirection = groups[2].GetIntValue(),
                ToDirection = groups[3].GetIntValue()
            };
            return ret;
        }
    }
}
