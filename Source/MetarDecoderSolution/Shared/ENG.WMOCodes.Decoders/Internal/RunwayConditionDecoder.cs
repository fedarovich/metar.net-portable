using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class RunwayConditionDecoder : TypeDecoder<RunwayCondition>
    {
        public override string Description => "Runway condition";

        private const string RegexPattern = @"^R(\d{2}(L|R|C)*)/((\d|/)(\d|/)(\d{2}|/{2})(\d{2}|/{2})|(CLRD//))";

        public override string RegEx => RegexPattern;

        protected override RunwayCondition DecodeCore(GroupCollection groups)
        {
            RunwayCondition ret = new RunwayCondition {Runway = groups[1].Value};

            if (groups[8].Success)
            {
                ret.IsCleared = true;
            }
            else
            {
                ret.Deposit = (groups[4].Value == "/" ? null : (RunwayDeposit?)groups[4].GetIntValue());
                ret.Contamination = (groups[5].Value == "/" ? null : (RunwayContamination?)groups[5].GetIntValue());
                ret.Depth = (groups[6].Value == "//" ? null : (RunwayContaminationDepth?)groups[6].GetIntValue());
                ret.Friction = (groups[7].Value == "//" ? null : (RunwayFriction?)groups[7].GetIntValue());
            }

            return ret;
        }
    }
}
