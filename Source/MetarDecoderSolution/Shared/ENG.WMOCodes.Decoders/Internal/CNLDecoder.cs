using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  internal class CNLDecoder : TypeDecoder<bool>
  {
    public override string Description
    {
      get { return "CNL - amended"; }
    }

    public override string RegEx
    {
      get { return "^CNL"; }
    }

    protected override bool _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      return true;
    }
  }
}
