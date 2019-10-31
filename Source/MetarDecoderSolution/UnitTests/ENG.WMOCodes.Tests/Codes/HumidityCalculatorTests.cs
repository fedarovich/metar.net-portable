using System;
using ENG.WMOCodes.Codes;
using Xunit;
using static ENG.WMOCodes.Codes.RelativeHumidityFormula;

namespace ENG.WMOCodes.Tests.Net45.Codes
{
    public class HumidityCalculatorTests
    {
        [Theory]
        [InlineData(0, 0, 100, 1e-2)]
        [InlineData(10, 0, 49.8, 1e-2)]
        [InlineData(10, 5, 71.1, 1e-2)]
        [InlineData(10, 10, 100, 1e-2)]
        [InlineData(20, 0, 26.1, 1e-2)]
        [InlineData(20, 5, 37.3, 1e-2)]
        [InlineData(20, 10, 52.5, 1e-2)]
        [InlineData(20, 15, 72.9, 1e-2)]
        [InlineData(20, 20, 100, 1e-2)]
        [InlineData(50, 0, 4.9, 2e-2)]
        [InlineData(50, 10, 9.9, 1e-2)]
        [InlineData(50, 20, 18.9, 1e-2)]
        [InlineData(50, 30, 34.3, 1e-2)]
        [InlineData(50, 40, 59.8, 1e-2)]
        [InlineData(50, 50, 100, 1e-2)]
        [InlineData(100, 10, 1.2, 2e-2)]
        [InlineData(100, 50, 12.2, 3e-2)]
        [InlineData(100, 90, 69.2, 1e-2)]
        public void PositiveWMO(double t, double d, double expectedRh, double accuracy)
        {
            var rh = HumidityCalculator.CalculateRelativeHumidity(t, d, WMO);
            var min = Math.Max(expectedRh * (1 - accuracy), 0);
            var max = Math.Min(expectedRh * (1 + accuracy), 100);
            Assert.InRange(rh, min, max);
        }

        [Theory]
        [InlineData(0, 0, 100, 1e-2)]
        [InlineData(10, 0, 49.8, 1e-2)]
        [InlineData(10, 5, 71.1, 1e-2)]
        [InlineData(10, 10, 100, 1e-2)]
        [InlineData(20, 0, 26.1, 1e-2)]
        [InlineData(20, 5, 37.3, 1e-2)]
        [InlineData(20, 10, 52.5, 1e-2)]
        [InlineData(20, 15, 72.9, 1e-2)]
        [InlineData(20, 20, 100, 1e-2)]
        [InlineData(50, 0, 4.9, 2e-2)]
        [InlineData(50, 10, 9.9, 1e-2)]
        [InlineData(50, 20, 18.9, 1e-2)]
        [InlineData(50, 30, 34.3, 1e-2)]
        [InlineData(50, 40, 59.8, 1e-2)]
        [InlineData(50, 50, 100, 1e-2)]
        [InlineData(100, 10, 1.2, 3e-2)]
        [InlineData(100, 50, 12.2, 3e-2)]
        [InlineData(100, 90, 69.2, 1e-2)]
        public void PositiveNasa(double t, double d, double expectedRh, double accuracy)
        {
            var rh = HumidityCalculator.CalculateRelativeHumidity(t, d, Nasa);
            var min = Math.Max(expectedRh * (1 - accuracy), 0);
            var max = Math.Min(expectedRh * (1 + accuracy), 100);
            Assert.InRange(rh, min, max);
        }

        [Theory]
        [InlineData(-18, -22, 70, 2e-2, WMO)]
        [InlineData(-18, -22, 70, 2e-2, WMOOverWater)]
        [InlineData(-18, -22, 70, 3e-2, WMOOverIce)]
        [InlineData(-18, -22, 70, 19e-2, Nasa)]
        [InlineData(-18, -22, 70, 2e-2, NasaOverWater)]
        [InlineData(-18, -22, 70, 19e-2, NasaOverIce)]
        [InlineData(-27, -31, 68, 2e-2, WMO)]
        [InlineData(-27, -31, 68, 2e-2, WMOOverWater)]
        [InlineData(-27, -31, 68, 3e-2, WMOOverIce)]
        [InlineData(-27, -31, 68, 30e-2, Nasa)]
        [InlineData(-27, -31, 68, 2e-2, NasaOverWater)]
        [InlineData(-27, -31, 68, 30e-2, NasaOverIce)]
        public void NegativeCompareToNoaaGov(double t, double d, double expectedRh, double accuracy, RelativeHumidityFormula formula)
        {
            var rh = HumidityCalculator.CalculateRelativeHumidity(t, d, formula);
            var min = Math.Max(expectedRh * (1 - accuracy), 0);
            var max = Math.Min(expectedRh * (1 + accuracy), 100);
            Assert.InRange(rh, min, max);
        }

        [Theory]
        [InlineData(20, -10, 12.25, 1e-2, WMOOverWater)]
        [InlineData(20, -10, 12.25, 1e-2, NasaOverWater)]
        [InlineData(20, -10, 12.25, 1e-2, WMO)]
        [InlineData(20, -10, 11.11, 1e-2, Nasa)]
        [InlineData(20, -10, 11.11, 1e-2, WMOOverIce)]
        [InlineData(20, -10, 11.11, 1e-2, NasaOverIce)]
        public void Mixed(double t, double d, double expectedRh, double accuracy, RelativeHumidityFormula formula)
        {
            var rh = HumidityCalculator.CalculateRelativeHumidity(t, d, formula);
            var min = Math.Max(expectedRh * (1 - accuracy), 0);
            var max = Math.Min(expectedRh * (1 + accuracy), 100);
            Assert.InRange(rh, min, max);
        }
    }
}
