﻿using System;
using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class VisibilityForMetarDecoder : TypeDecoder<VisibilityForMetar>
    {
        public override string Description => "Visibility";

        public override string RegEx => @"^((CAVOK)|(SKC)|((\d{4})(NE|SW|NW|SE|N|E|S|W)?( (\d{4})(N|NE|E|SE|S|SW|W|NW))?)|(((M)?|(\d+) )(\d+)(/(\d\d?))?SM))";

        protected override VisibilityForMetar DecodeCore(GroupCollection groups)
        {
            VisibilityForMetar ret = new VisibilityForMetar();

            if (groups[2].Success)
            {
                ret.SetCAVOK();
            }
            else if (groups[3].Success)
            {
                ret.SetSKC();
            }
            else if (groups[4].Success)
            {
                int distance = groups[5].GetIntValue();
                Direction? dir = null;
                int? otherDist = null;
                Direction? otherDir = null;

                if (groups[6].Success)
                {
                    dir = (Direction)Enum.Parse(
                      typeof(Direction), groups[6].Value, false);
                }

                if (groups[7].Success)
                {
                    otherDist = groups[8].GetIntValue();
                    otherDir = (Direction)Enum.Parse(
                      typeof(Direction), groups[9].Value, false);
                }

                ret.SetMeters(distance, dir, otherDist, otherDir);
            }
            else
            {
                Rational r = new Rational(
                  groups[13].Success ? groups[13].GetIntValue() : 0,
                  groups[14].GetIntValue(),
                  groups[16].Success ? groups[16].GetIntValue() : 1
                  );

                bool isMinimal = groups[12].Success;

                ret.SetMiles(r, isMinimal);
            }


            return ret;
        }
    }
}
