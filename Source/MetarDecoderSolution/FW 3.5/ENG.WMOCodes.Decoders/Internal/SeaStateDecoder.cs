using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;
using ENG.WMOCodes.Codes;

namespace ENG.WMOCodes.Decoders.Internal
{
  class SeaStateDecoder : TypeDecoder<Common.eSeaState?>
  {
    public override string Description
    {
      get { return "Sea state"; }
    }

    public override string RegEx
    {
      get { return @"^/(\d{2})"; }
    }

    protected override Common.eSeaState? _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      int pom = groups[1].GetIntValue();

      Common.eSeaState ret = (Common.eSeaState)pom;

      return ret;
    }
  }
}
