using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  class RemarkDecoder : TypeDecoder<string>
  {
    public override string Description
    {
      get { return "Remark"; }
    }

    public override string RegEx
    {
      get { return "^RMK (.*)"; }
    }

    protected override string _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      return groups[1].Value;
    }
  }
}
