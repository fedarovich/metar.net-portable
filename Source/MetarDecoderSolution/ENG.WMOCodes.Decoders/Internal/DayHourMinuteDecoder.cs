using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types.DateTimeTypes;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class DayHourMinuteDecoder : TypeDecoder<DayHourMinute>
    {
        public override string Description => "Day-time";

        public override string RegEx => @"(\d{2})(\d{2})(\d{2})Z";

        protected override DayHourMinute DecodeCore(GroupCollection groups)
        {
            DayHourMinute ret = new DayHourMinute(
                groups[1].GetIntValue(),
                groups[2].GetIntValue(),
                groups[3].GetIntValue());

            return ret;
        }
    }
}
