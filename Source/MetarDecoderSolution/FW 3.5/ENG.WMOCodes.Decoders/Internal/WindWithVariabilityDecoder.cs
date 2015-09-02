using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  class WindWithVariabilityDecoder : CustomDecoder<WindWithVariability>
  {

    protected override WindWithVariability _Decode(ref string source)
    {
      Wind w = new WindDecoder() { Required = this.Required }.Decode(ref source);
      WindVariable wv = new WindVariableDecoder() { Required = false }.Decode(ref source);

      WindWithVariability ret;
      if (w == null)
      {
        if (wv != null)
          throw new Exception("No wind definition found, but wind variability definition found.  Possible invalid data?");
        else
          ret = null;
      }
      else
      {
        ret = new WindWithVariability();
        w.CopyPropertiesTo(ret, "IsVariable");
        ret.Variability = wv;
      }

      return ret;
    }

    public override string Description
    {
      get { return "Wind with variability"; }
    }
  }
}
