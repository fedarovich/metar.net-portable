using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;
using ENG.WMOCodes.Codes;

namespace ENG.WMOCodes.Decoders.Internal
{
  class MetarPrefixDecoder : TypeDecoder<Metar.eType>
  {
    public override string Description
    {
      get { return "METAR/SPECI prefix"; }
    }

    public override string RegEx
    {
      get { return "(^METAR)|(^SPECI)"; }
    }

    protected override Metar.eType _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      Metar.eType ret;
      if (groups[1].Success)
        ret = Metar.eType.METAR;
      else if (groups[2].Success)
        ret = Metar.eType.SPECI;
      else
        throw new NotSupportedException();
      return ret;
    }
  }
}
