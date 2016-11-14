using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  class RePhenomInfoDecoder : CustomDecoder<RePhenomInfo>
  {
    private const string prefixPattern = "^RE([^ ]+)";

    protected override RePhenomInfo _Decode(ref string source)
    {
      RePhenomInfo ret = new RePhenomInfo();

      Match m = Regex.Match(source, prefixPattern);
      while (m.Success)
      {        

        source = source.Substring(m.Groups[0].Length).TrimStart();
        string p = m.Groups[0].Value.Substring(2);
        PhenomInfo pi = new PhenomInfoDecoder().Decode(ref p);
        ret.AddRange(pi);

        m = Regex.Match(source, prefixPattern);
      }

      return ret;
    }

    public override string Description
    {
      get { return "Recent phenomens"; }
    }
  }
}
