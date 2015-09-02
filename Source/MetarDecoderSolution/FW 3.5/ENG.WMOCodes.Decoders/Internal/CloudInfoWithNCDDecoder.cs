using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  class CloudInfoWithNCDDecoder : CustomDecoder<CloudInfoWithNCD>
  {
    private const string NCD = "NCD";
    protected override CloudInfoWithNCD _Decode(ref string source)
    {
      CloudInfoWithNCD ret = new CloudInfoWithNCD();

      if (source.StartsWith(NCD))
      {
        ret.SetNCD();
        source = source.Substring(NCD.Length).TrimStart();
      }
      else
      {
        CloudInfo ci = new CloudInfoDecoder() { Required = this.Required }.Decode(ref source);
        ci.CopyPropertiesTo(ret);
        if (ci.IsCLR)
          ret.SetCLR();
        else if (ci.IsNSC)
          ret.SetNCD();
        else if (ci.IsSKC)
          ret.SetSKC();
        else if (ci.IsVerticalVisibility)
          ret.SetVerticalVisibility(ci.VVDistance);
      }

      return ret;
    }

    public override string Description
    {
      get { return "Cloud info with NCD"; }
    }
  }
}
