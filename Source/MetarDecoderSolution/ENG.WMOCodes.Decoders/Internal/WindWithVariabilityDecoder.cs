using System;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class WindWithVariabilityDecoder : CustomDecoder<WindWithVariability>
    {
        protected override WindWithVariability DecodeCore(ref string source)
        {
            Wind w = new WindDecoder { Required = Required }.Decode(ref source);
            WindVariable wv = new WindVariableDecoder { Required = false }.Decode(ref source);

            WindWithVariability ret;
            if (w == null)
            {
                if (wv != null)
                    throw new Exception("No wind definition found, but wind variability definition found.  Possible invalid data?");
                ret = null;
            }
            else
            {
                ret = new WindWithVariability();
                w.CopyPropertiesTo(ret, "IsVariable");
                ret.Variability = wv;
            }

            return ret;
        }

        public override string Description => "Wind with variability";
    }
}
