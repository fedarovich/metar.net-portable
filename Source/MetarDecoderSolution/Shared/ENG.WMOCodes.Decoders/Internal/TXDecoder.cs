using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;
using ENG.WMOCodes.Types.DateTimeTypes;

namespace ENG.WMOCodes.Decoders.Internal
{
    // ReSharper disable once InconsistentNaming
    internal class TXDecoder : TypeDecoder<TemperatureExtremeTX>
    {
        public override string Description => "TAF TX";

        public override string RegEx => @"^TX(M)?(\d{2})/(\d{2})(\d{2})Z";

        protected override TemperatureExtremeTX DecodeCore(GroupCollection groups)
        {
            TemperatureExtremeTX ret = new TemperatureExtremeTX();

            bool negate = groups[1].Success;

            ret.Temperature = groups[2].GetIntValue();
            if (negate)
                ret.Temperature = -ret.Temperature;

            ret.Time = new DayHour(
                groups[3].GetIntValue(),
                groups[4].GetIntValue());

            return ret;
        }
    }
}
