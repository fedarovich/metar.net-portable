using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;
using ENG.WMOCodes.Types.DateTimeTypes;

namespace ENG.WMOCodes.Decoders.Internal
{
  class DayHourMinuteDecoder : TypeDecoder<DayHourMinute>
  {
    public override string Description
    {
      get { return "Day-time"; }
    }

    public override string RegEx
    {
      get { return @"(\d{2})(\d{2})(\d{2})Z"; }
    }

    protected override DayHourMinute _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      DayHourMinute ret = new DayHourMinute();

      ret.Day = groups[1].GetIntValue();
      ret.Hour = groups[2].GetIntValue();
      ret.Minute = groups[3].GetIntValue();

      return ret;
    }
  }
}
