using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;
using ENG.WMOCodes.Codes;

namespace ENG.WMOCodes.Decoders.Internal
{
  class MetarPrefixDecoder : TypeDecoder<MetarType>
  {
    public override string Description => "METAR/SPECI prefix";

      public override string RegEx => "(^METAR)|(^SPECI)";

      protected override MetarType _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      MetarType ret;
      if (groups[1].Success)
        ret = MetarType.METAR;
      else if (groups[2].Success)
        ret = MetarType.SPECI;
      else
        throw new NotSupportedException();
      return ret;
    }
  }
}
