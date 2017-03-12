using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class WindShearDecoder : TypeDecoder<WindShear>
    {
        public override string Description => "Wind shear for runway";

        public override string RegEx => @"^RWY(\d{2}(R|L|C)?)";

        protected override WindShear DecodeCore(GroupCollection groups)
        {
            WindShear ret = new WindShear { Runway = groups[1].Value };
            return ret;
        }
    }
}
