using System.Collections.Generic;
using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class RunwayVisibilityListDecoder : CustomDecoder<List<RunwayVisibility>>
    {
        private const string RegexPattern = @"^R\d{2}";

        protected override List<RunwayVisibility> DecodeCore(ref string source)
        {
            List<RunwayVisibility> ret = new List<RunwayVisibility>();

            Match m = Regex.Match(source, RegexPattern);
            while (m.Success)
            {
                var rv = new RunwayVisibilityDecoder().Decode(ref source);
                ret.Add(rv);
                m = Regex.Match(source, RegexPattern);
            }

            return ret;
        }

        public override string Description => "Runways visibility";
    }
}
