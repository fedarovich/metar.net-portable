using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
  class CloudInfoDecoder : TypeDecoder<CloudInfo>
  {
    public override string Description
    {
      get { return "Cloud information"; }
    }

    private const string R_CLOUD_ITEM = @"( ?(FEW|SCT|BKN|OVC)(\d{3})(CB|TCU)?/*)";
    public override string RegEx
    {
      get
      {
        return
          @"^((NSC)|(SKC)|(CLR)|VV((\d{3})|/{3})|" + R_CLOUD_ITEM + "+)";
      }
    }

    protected override CloudInfo _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      CloudInfo ret = null;

      if (groups[0].Success)
      {
        ret = new CloudInfo();

        if (groups[2].Success)
          ret.SetNSC();
        else if (groups[3].Success)
          ret.SetSKC();
        else if (groups[4].Success)
            ret.SetCLR();
        else if (groups[5].Success)
        {
          if (groups[5].Value == "///")
            ret.SetVerticalVisibility(null);
          else
            ret.SetVerticalVisibility(
              groups[5].GetIntValue());
        }
        else
        {
          string str = groups[1].Value;
          Match m = Regex.Match(str, R_CLOUD_ITEM);

          while (m.Success)
          {
            ret.Add(xDecodeCloud(m));

            m = m.NextMatch();
          }
        }
      }

      return ret;
    }

    private static Cloud xDecodeCloud(Match m)
    {
      Cloud ret = null;

      ret = new Cloud(
        m.Groups[2].Value, m.Groups[3].GetIntValue(), m.Groups[4].Value == "CB", m.Groups[4].Value == "TCU");

      return ret;
    }
  }
}
