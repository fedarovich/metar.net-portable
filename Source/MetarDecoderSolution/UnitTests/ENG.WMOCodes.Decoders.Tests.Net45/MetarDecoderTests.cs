using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ENG.WMOCodes.Decoders.Tests.Net45
{
    public class MetarDecoderTests
    {
        [Fact]
        public void Bug1()
        {
            var decoder = new MetarDecoder();
            var report = decoder.Decode(
                "METAR KEWR 142108Z 30017G26KT 1SM R04R/5000VP6000FT -SN BLSN SCT009 OVC017 M01/M03 A2926 RMK AO2 PK WND 31031/2053 SFC VIS 1 1/2 P0000 T10111033");
        }
    }
}
