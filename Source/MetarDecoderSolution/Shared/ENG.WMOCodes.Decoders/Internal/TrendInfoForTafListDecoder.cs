using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  class TrendInfoForTafListDecoder : CustomDecoder<List<TrendInfoForTaf>>
  {
    public override string Description
    {
      get { return "Trend/Tempo/Becoming sets"; }
    }

    public string regexPattern
    {
      get { return "(^TEMPO)|(^BECMG)|(^FM)|(^PROB40)|(^PROB30)"; }
    }

    protected override List<TrendInfoForTaf> _Decode(ref string source)
    {
      List<TrendInfoForTaf> ret = new List<TrendInfoForTaf>();
      TrendInfoForTaf report = null;

      string p = source;
      bool found = true;
      while (found)
      {
        var m = Regex.Match(p, regexPattern);
        if (m.Success)
        {
          try
          {
            report = new TrendInfoForTafDecoder().Decode(ref p);
          } // try
          catch (Exception ex)
          {
            throw new DecodeException(this.Description, ex);
          } // catch (Exception ex)
          ret.Add(report);
        }
        else
          found = false;
      }

      source = p;
      return ret;
    }
  }
}
