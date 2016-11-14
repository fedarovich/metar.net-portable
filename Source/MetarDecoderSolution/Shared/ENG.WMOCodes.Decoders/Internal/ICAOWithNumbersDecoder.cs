using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  class ICAOWithNumbersDecoder : TypeDecoder<string>
  {
    public override string Description
    {
      get { return "ICAO (with numbers)"; }
    }

    public override string RegEx
    {
      get { return "[A-Z0-9]{4}"; }
    }

    protected override string _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      string ret = groups[0].Value;

      return ret;
    }
  }
}
