using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENG.WMOCodes.Types;
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
            var rv = report.Visibility.Runways[0];
            Assert.Equal("04R", rv.Runway);
            Assert.Equal(5000, rv.Distance.Value);
            Assert.Equal(null, rv.Distance.Restriction);
            Assert.Equal(6000, rv.VariableDistance.Value.Value);
            Assert.Equal(DeviceMeasurementRestriction.P, rv.VariableDistance.Value.Restriction);
            Assert.Equal(DistanceUnit.ft, rv.Unit);
            Assert.Equal(null, rv.Tendency);
        }

        [Fact]
        public void Bug2()
        {
            var decoder = new MetarDecoder();
            var report = decoder.Decode(
                "METAR RJCC 232115Z 17004KT 1200 R19R/0750VP1800U R19L/1000VP1800U -SHRA BR FEW001 BKN002 BKN003 10/10 Q1008 RMK 1ST001 5ST002 7ST003 A2978");

            var rv1 = report.Visibility.Runways[0];
            Assert.Equal("19R", rv1.Runway);
            Assert.Equal(750, rv1.Distance.Value);
            Assert.Equal(null, rv1.Distance.Restriction);
            Assert.Equal(1800, rv1.VariableDistance.Value.Value);
            Assert.Equal(DeviceMeasurementRestriction.P, rv1.VariableDistance.Value.Restriction);
            Assert.Equal(DistanceUnit.m, rv1.Unit);
            Assert.Equal(RunwayVisibilityTendency.U, rv1.Tendency);

            var rv2 = report.Visibility.Runways[1];
            Assert.Equal("19L", rv2.Runway);
            Assert.Equal(1000, rv2.Distance.Value);
            Assert.Equal(null, rv2.Distance.Restriction);
            Assert.Equal(1800, rv2.VariableDistance.Value.Value);
            Assert.Equal(DeviceMeasurementRestriction.P, rv2.VariableDistance.Value.Restriction);
            Assert.Equal(DistanceUnit.m, rv2.Unit);
            Assert.Equal(RunwayVisibilityTendency.U, rv2.Tendency);
        }
    }
}
