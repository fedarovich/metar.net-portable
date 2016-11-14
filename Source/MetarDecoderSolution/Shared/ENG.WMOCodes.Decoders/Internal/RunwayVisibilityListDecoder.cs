using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  class RunwayVisibilityListDecoder : CustomDecoder<List<RunwayVisibility>>
  {    
    private const string regexPattern = @"^R\d{2}";    

    protected override List<RunwayVisibility> _Decode(ref string source)
    {
      List<RunwayVisibility> ret = new List<RunwayVisibility>();
      RunwayVisibility rv = null;

      Match m = Regex.Match(source, regexPattern);
      while (m.Success)
      {
        rv = new RunwayVisibilityDecoder().Decode(ref source);
        ret.Add(rv);
        m = Regex.Match(source, regexPattern);
      }

      return ret;
    }

    public override string Description
    {
      get { return "Runways visibility"; }
    }
  }
}
