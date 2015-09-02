using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Decoders.Internal
{
  class VisibilityForMetarDecoder : TypeDecoder<VisibilityForMetar>
  {
    public override string Description
    {
      get { return "Visibility"; }
    }

    public override string RegEx
    {
      get
      {
        return
          @"^((CAVOK)|(SKC)|((\d{4})(NE|SW|NW|SE|N|E|S|W)?( (\d{4})(N|NE|E|SE|S|SW|W|NW))?)|(((M)?|(\d+) )(\d+)(/(\d\d?))?SM))";
      }
    }

    protected override VisibilityForMetar _Decode(System.Text.RegularExpressions.GroupCollection groups)
    {
      VisibilityForMetar ret = new VisibilityForMetar();

      if (groups[2].Success)
        ret.SetCAVOK();
      else if (groups[3].Success)
        ret.SetSKC();
      else if (groups[4].Success)
      {
        int distance = groups[5].GetIntValue();
        Common.eDirection? dir = null;
        int? otherDist = null;
        Common.eDirection? otherDir = null;

        if (groups[6].Success)
          dir = (Common.eDirection)Enum.Parse(
            typeof(Common.eDirection), groups[6].Value, false);

        if (groups[7].Success)
        {
          otherDist = groups[8].GetIntValue();
          otherDir = (Common.eDirection)Enum.Parse(
            typeof(Common.eDirection), groups[9].Value, false);
        }

        ret.SetMeters(distance, dir, otherDist, otherDir);
      }
      else
      {
        Racional r = new Racional(
          (groups[13].Success) ? groups[13].GetIntValue() : 0,
          groups[14].GetIntValue(),
          (groups[16].Success) ? groups[16].GetIntValue() : 1
          );

        bool isMinimal = groups[12].Success;

        ret.SetMiles(r, isMinimal);
      }
      

      return ret;
    }
  }
}
