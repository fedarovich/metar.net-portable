using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  class DewPointDecoder : TypeDecoder<int>
  {
    public override string Description => "Dew point";

      public override string RegEx => @"/(M)?(\d{2})";

      protected override int _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      int ret = groups[2].GetIntValue();

      if (groups[1].Success)
        ret = -ret;

      return ret;
    }
  }
}
