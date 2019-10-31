using System;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class TrendReportDecoder : CustomDecoder<TrendReport>
    {
        public override string Description => "Trend report";

        protected override TrendReport DecodeCore(ref string source)
        {
            TrendReport ret = new TrendReport();

            try
            {
                ret.Wind = new WindDecoder { Required = false }.Decode(ref source);
                ret.Visibility = new VisibilityDecoder { Required = false }.Decode(ref source);
                ret.Phenomena = new PhenomInfoWithNSWDecoder { Required = false }.Decode(ref source);
                ret.Clouds = new CloudInfoDecoder { Required = false }.Decode(ref source);
            } // try
            catch (Exception ex)
            {
                throw new DecodeException(Description, ex);
            } // catch (Exception ex)

            return ret;
        }

    }
}
