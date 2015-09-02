using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  internal class NILDecoder : TypeDecoder<bool>
  {
    public override string Description
    {
      get { return "NIL - nil report"; }
    }

    public override string RegEx
    {
      get { return "^NIL"; }
    }

    protected override bool _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      return true;
    }
  }
}
