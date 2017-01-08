using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class RePhenomInfoDecoder : CustomDecoder<RePhenomInfo>
    {
        private const string PrefixPattern = "^RE([^ ]+)";

        protected override RePhenomInfo DecodeCore(ref string source)
        {
            RePhenomInfo ret = new RePhenomInfo();

            Match m = Regex.Match(source, PrefixPattern);
            while (m.Success)
            {
                source = source.Substring(m.Groups[0].Length).TrimStart();
                string p = m.Groups[0].Value.Substring(2);
                PhenomInfo pi = new PhenomInfoDecoder().Decode(ref p);
                ret.AddRange(pi);

                m = Regex.Match(source, PrefixPattern);
            }

            return ret;
        }

        public override string Description => "Recent phenomena";
    }
}
