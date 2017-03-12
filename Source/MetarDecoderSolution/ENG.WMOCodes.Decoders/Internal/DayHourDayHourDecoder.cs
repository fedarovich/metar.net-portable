using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types.DateTimeTypes;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class DayHourDayHourDecoder : TypeDecoder<DayHourDayHour>
    {
        public override string Description => "TAF period";

        public override string RegEx => @"((\d{2})(\d{2})/(\d{2})(\d{2}))";

        protected override DayHourDayHour DecodeCore(GroupCollection groups)
        {
            var ret = DecodeRegularTaf(groups);

            return ret;
        }

        private DayHourDayHour DecodeRegularTaf(GroupCollection groups)
        {
            DayHourDayHour ret = new DayHourDayHour();

            int fd = groups[2].GetIntValue();
            int fh = groups[3].GetIntValue();
            int td = groups[4].GetIntValue();
            int th = groups[5].GetIntValue();

            ret.From = new DayHour(fd, fh);
            ret.To = new DayHour(td, th);

            return ret;
        }
    }
}
