using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;

namespace ENG.WMOCodes.Decoders.Internal
{
  internal class AMDDecoder : TypeDecoder<bool>
  {
    public override string Description
    {
      get { return "AMD - amended"; }
    }

    public override string RegEx
    {
      get { return "^AMD"; }
    }

    protected override bool _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      return true;
    }
  }
}
