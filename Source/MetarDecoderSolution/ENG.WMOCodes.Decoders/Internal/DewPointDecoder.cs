﻿using System.Text.RegularExpressions;
using ENG.WMOCodes.Decoders.Internal.Basic;

namespace ENG.WMOCodes.Decoders.Internal
{
    internal class DewPointDecoder : TypeDecoder<int>
    {
        public override string Description => "Dew point";

        public override string RegEx => @"/(M)?(\d{2})";

        protected override int DecodeCore(GroupCollection groups)
        {
            int ret = groups[2].GetIntValue();

            if (groups[1].Success)
                ret = -ret;

            return ret;
        }
    }
}
