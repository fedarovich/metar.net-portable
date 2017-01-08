using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;
using System.Text.RegularExpressions;

namespace ENG.WMOCodes.Decoders.Internal
{
  class PhenomInfoDecoder : TypeDecoder<PhenomInfo>
  {
    public override string Description => "Phenomens";

      private const string R_PHENOM_ITEM = @"(\-|\+| |VC|MI|BC|PR(?!OB)|DR|BL|SH|TS|FZ|DZ|RA|SN|SG|IC|PL|GR|GS|BR|FG|FU|VA|DU|SA|HZ|PO|SQ|FC|SS|DS)";

    public override string RegEx => @"(^" + R_PHENOM_ITEM + "+)";

      protected override PhenomInfo _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      PhenomInfo ret = null;

      if (groups[0].Success)
      {
        ret = new PhenomInfo();

        string str = groups[1].Value;
        Match m = Regex.Match(str, RegEx);
        List<PhenomenonCollection> colls = null;

        colls = DecodePhenomSets(m);

        ret.AddRange(colls);
      }

      return ret;
    }

    private static List<PhenomenonCollection> DecodePhenomSets(Match setMatch)
    {
      List<PhenomenonCollection> ret = new List<PhenomenonCollection>();
      PhenomenonCollection curr = new PhenomenonCollection();

      Match m = Regex.Match(setMatch.Value, R_PHENOM_ITEM);

      while (m.Success)
      {
        string val = m.Value.Trim();
        if (val == "-")
          curr.Add(Phenomenon.Light);
        else if (val == "+")
          curr.Add(Phenomenon.Heavy);
        else if (val == "")
        {
          if (curr.Count > 0)
          {
            ret.Add(curr);
            curr = new PhenomenonCollection();
          }
        }
        else
        {
          var ph = (Phenomenon)Enum.Parse(typeof(Phenomenon), m.Value, false);
          curr.Add(ph);
        }
        m = m.NextMatch();
      }

      if (curr.Count > 0)
        ret.Add(curr);

      return ret;
    }
  }
}
