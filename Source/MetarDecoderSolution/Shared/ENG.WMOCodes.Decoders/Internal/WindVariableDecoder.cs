using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  class WindVariableDecoder : TypeDecoder<WindVariable>
  {
    public override string Description
    {
      get { return "Wind variability"; }
    }

    public override string RegEx
    {
      get { return @"^((\d{3})V(\d{3}))"; }
    }

    protected override WindVariable _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      WindVariable ret = new WindVariable();

      ret.FromDirection = groups[2].GetIntValue();
      ret.ToDirection = groups[3].GetIntValue();

      return ret;
    }
  }
}
