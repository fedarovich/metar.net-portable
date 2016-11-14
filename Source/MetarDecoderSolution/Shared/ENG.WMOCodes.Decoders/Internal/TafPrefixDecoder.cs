using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  class TafPrefixDecoder : TypeDecoder<bool>
  {
    public override string Description
    {
      get { return "TAF prefix"; }
    }

    public override string RegEx
    {
      get { return "TAF"; }
    }

    protected override bool _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      if (groups[0].Success)
        return true;
      else
        return false;
    }
  }
}
