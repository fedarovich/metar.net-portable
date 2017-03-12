using System.Diagnostics.CodeAnalysis;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class RunwayConditionInfoDecoder : CustomDecoder<RunwayConditionInfo>
    {
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private const string SNOCLO = "SNOCLO";

        protected override RunwayConditionInfo DecodeCore(ref string source)
        {
            RunwayConditionInfo ret = null;
            RunwayCondition rc;

            if (source.StartsWith(SNOCLO))
            {
                ret = new RunwayConditionInfo { IsSNOCLO = true };
                source = source.Substring(SNOCLO.Length).TrimStart();
            }
            else
            {
                ret = new RunwayConditionInfo();
                bool found = true;
                while (found)
                {
                    rc = new RunwayConditionDecoder { Required = false }.Decode(ref source);
                    if (rc == null)
                        found = false;
                    else
                        ret.Add(rc);
                }
            }

            return ret;
        }

        public override string Description => "Runways' conditions";
    }
}
