using System;
using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class WindDecoder : TypeDecoder<Wind>
    {
        public override string Description => "Wind";
        public override string RegEx => @"^((\d{3}|VRB)(\d{2})(G(\d{2,3}))?(KT|MPS|KMH))";

        protected override Wind DecodeCore(GroupCollection grp)
        {
            Wind ret = new Wind();

            if (grp[2].Value == "VRB")
                ret.IsVariable = true;
            else
                ret.Direction = grp[2].GetIntValue();
            ret.Speed = grp[3].GetIntValue();
            ret.GustSpeed = grp[5].Success ? (int?)grp[5].GetIntValue() : null;
            ret.Unit = (SpeedUnit)Enum.Parse(typeof(SpeedUnit), grp[6].Value.ToLower(), false);

            return ret;
        }
    }
}
