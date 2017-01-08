using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class VisibilityDecoder : TypeDecoder<Visibility>
    {
        public override string Description => "Trend visibility";

        public override string RegEx => @"^((CAVOK)|(\d{4})|(((M)?|(\d+) )(\d+)(/(\d\d?))?SM))";

        protected override Visibility DecodeCore(GroupCollection grp)
        {
            if (!grp[0].Success)
                return null;

            var ret = new Visibility();
            if (grp[2].Success)
            {
                ret.SetCAVOK();
            }
            else if (grp[3].Success)
            {
                ret.SetMeters(grp[3].GetIntValue());
            }
            else
            {
                ret.SetMiles(
                    new Rational(
                        grp[7].Success ? grp[7].GetIntValue() : 0,
                        grp[8].GetIntValue(),
                        grp[9].Success ? grp[10].GetIntValue() : 1),
                    grp[6].Success);
            }

            return ret;
        }
    }
}
