using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;
using ENG.WMOCodes.Types.DateTimeTypes;

namespace ENG.WMOCodes.Decoders.Internal
{
    // ReSharper disable once InconsistentNaming
    internal class TNDecoder : TypeDecoder<TemperatureExtremeTN>
    {
        public override string Description => "TAF TN";

        public override string RegEx => @"^TN(M)?(\d{2})/(\d{2})(\d{2})Z";

        protected override TemperatureExtremeTN DecodeCore(GroupCollection groups)
        {
            TemperatureExtremeTN ret = new TemperatureExtremeTN();

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
