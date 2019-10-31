using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class WindShearInfoDecoder : CustomDecoder<WindShearInfo>
    {
        private const string PrefixPattern = "WS";
        private const string PrefixAllRwy = "WS ALL RWY";

        protected override WindShearInfo DecodeCore(ref string source)
        {
            WindShearInfo ret = new WindShearInfo();

            if (source.StartsWith(PrefixAllRwy))
            {
                ret = new WindShearInfo { IsAllRunways = true };
                source = source.Substring(PrefixAllRwy.Length).TrimStart();
            }
            else if (source.StartsWith(PrefixPattern))
            {
                source = source.Substring(PrefixPattern.Length).TrimStart();
                bool found = true;

                while (found)
                {
                    var ws = new WindShearDecoder { Required = false }.Decode(ref source);
                    if (ws == null)
                    {
                        found = false;
                    }
                    else
                    {
                        ret.Add(ws);
                    }
                }
            }

            return ret;
        }

        public override string Description => "Wind shears";
    }
}
