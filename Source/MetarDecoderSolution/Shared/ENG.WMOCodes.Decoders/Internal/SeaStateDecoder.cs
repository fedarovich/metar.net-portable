using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;
using ENG.WMOCodes.Codes;

namespace ENG.WMOCodes.Decoders.Internal
{
  class SeaStateDecoder : TypeDecoder<SeaState?>
  {
    public override string Description => "Sea state";

      public override string RegEx => @"^/(\d{2})";

      protected override SeaState? _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      int pom = groups[1].GetIntValue();

      SeaState ret = (SeaState)pom;

      return ret;
    }
  }
}
