using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  class WindShearInfoDecoder : CustomDecoder<WindShearInfo>
  {    
    private const string prefixPattern = "WS";
    private const string prefixAllRwy = "WS ALL RWY";

    protected override WindShearInfo _Decode(ref string source)
    {
      WindShearInfo ret = new WindShearInfo();
      WindShear ws = null;

      if (source.StartsWith(prefixAllRwy))
      {
        ret = new WindShearInfo() { IsAllRunways = true };
        source = source.Substring(prefixAllRwy.Length).TrimStart();
      }
      else if (source.StartsWith(prefixPattern))
      {
        source = source.Substring(prefixPattern.Length).TrimStart();
        bool found = true;

        while (found)
        {
          ws = new WindShearDecoder() { Required=false }.Decode(ref source);
          if (ws == null)
            found = false;
          else
            ret.Add(ws);
        }
      }

      return ret;
    }

    public override string Description
    {
      get { return "Wind shears"; }
    }
  }
}
