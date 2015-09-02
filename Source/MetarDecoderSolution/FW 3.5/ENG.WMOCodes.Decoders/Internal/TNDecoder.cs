using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;
using ENG.WMOCodes.Types.DateTimeTypes;

namespace ENG.WMOCodes.Decoders.Internal
{
  class TNDecoder : TypeDecoder<TemperatureExtremeTN>
  {
    public override string Description
    {
      get { return "TAF TN"; }
    }

    public override string RegEx
    {
      get { return @"^TN(M)?(\d{2})/(\d{2})(\d{2})Z"; }
    }

    protected override TemperatureExtremeTN _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      TemperatureExtremeTN ret = new TemperatureExtremeTN();

      bool negate = groups[1].Success;

      ret.Temperature = groups[2].GetIntValue();
      if (negate)
        ret.Temperature = -ret.Temperature;

      ret.Time = new DayHour();
      ret.Time.Day = groups[3].GetIntValue();
      ret.Time.Hour = groups[4].GetIntValue();

      return ret;
    }
  }
}
