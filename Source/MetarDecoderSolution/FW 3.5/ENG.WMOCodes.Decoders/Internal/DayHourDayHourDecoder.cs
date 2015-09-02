using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;
using ENG.WMOCodes.Types.DateTimeTypes;

namespace ENG.WMOCodes.Decoders.Internal
{
  class DayHourDayHourDecoder : TypeDecoder<DayHourDayHour>
  {
    public override string Description
    {
      get { return "TAF period"; }
    }

    public override string RegEx
    {
      get { return @"((\d{2})(\d{2})/(\d{2})(\d{2}))"; }
    }

    protected override DayHourDayHour _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      DayHourDayHour ret = null;
      
        ret = DecodeRegularTaf(groups);

      return ret;
    }

    private DayHourDayHour DecodeRegularTaf(System.Text.RegularExpressions.GroupCollection groups)
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
