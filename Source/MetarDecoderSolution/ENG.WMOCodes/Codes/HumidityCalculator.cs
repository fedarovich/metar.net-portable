using System;

namespace ENG.WMOCodes.Codes
{
    public static class HumidityCalculator
    {
        #region NASA coeffs

        private const double a = -4.9283;
        private const double b = -2937.4;
        private const double c = 23.5518;
        private const double ai = -0.32286;
        private const double bi = -2705.21;
        private const double ci = 11.4816;

        #endregion

        /// <summary>
        /// Calculates the relative humidity (RH) from the METAR report.
        /// </summary>
        /// <param name="metar">The metar report.</param>
        /// <param name="formula">The folmula to use for calculation.</param>
        /// <returns>The relative humidity in range from 0% to 100%.</returns>
        public static double CalculateRelativeHumidity(this Metar metar, 
            RelativeHumidityFormula formula = RelativeHumidityFormula.WMO)
        {
            return CalculateRelativeHumidity(metar.Temperature, metar.DewPoint, formula);
        }

        /// <summary>
        /// Calculates the relative humidity (RH) for the given ambient temperature and dew/frost point.
        /// </summary>
        /// <param name="temperature">The ambient temperature.</param>
        /// <param name="dewPoint">The dewpoint or frostpoint temperature.</param>
        /// <param name="formula">The folmula to use for calculation.</param>
        /// <returns>The relative humidity in range from 0% to 100%.</returns>
        public static double CalculateRelativeHumidity(double temperature, double dewPoint, 
            RelativeHumidityFormula formula = RelativeHumidityFormula.WMO)
        {
            switch (formula)
            {
                case RelativeHumidityFormula.Rough:
                    return CalculateRelativeHumidityRough(temperature, dewPoint);
                case RelativeHumidityFormula.Nasa:
                    return dewPoint >= 0 
                        ? CalculateRelativeHumidityNasaOverWater(temperature, dewPoint)
                        : CalculateRelativeHumidityNasaOverIce(temperature, dewPoint);
                case RelativeHumidityFormula.NasaOverWater:
                    return CalculateRelativeHumidityNasaOverWater(temperature, dewPoint);
                case RelativeHumidityFormula.NasaOverIce:
                    return CalculateRelativeHumidityNasaOverIce(temperature, dewPoint);
                case RelativeHumidityFormula.WMO:
                    return dewPoint >= -60
                        ? CalculateRelativeHumidityWMOOverWater(temperature, dewPoint)
                        : CalculateRelativeHumidityWMOOverIce(temperature, dewPoint);
                case RelativeHumidityFormula.WMOOverWater:
                    return CalculateRelativeHumidityWMOOverWater(temperature, dewPoint);
                case RelativeHumidityFormula.WMOOverIce:
                    return CalculateRelativeHumidityWMOOverIce(temperature, dewPoint);
                default:
                    throw new ArgumentOutOfRangeException(nameof(formula), formula, null);
            }
        }

        private static double CalculateRelativeHumidityRough(double temperature, double dewPoint)
        {
            return 100 - 5d * (temperature - dewPoint);
        }

        private static double CalculateRelativeHumidityNasaOverWater(double temperature, double dewPoint)
        {
            double T = temperature.ToKelvin();
            double D = dewPoint.ToKelvin();
            // U(T, D) = (D/T)^a * 10^[b * (1/D - 1/T)]
            return 100 * Math.Pow((D / T), a) * Math.Pow(10, b * (1 / D - 1 / T));
        }

        private static double CalculateRelativeHumidityNasaOverIce(double temperature, double dewPoint)
        {
            double T = temperature.ToKelvin();
            double D = dewPoint.ToKelvin();
            // U(T, D) = (D^ai / T^a) * 10^[(ci - c) + bi/D - b/T]
            return 100 * Math.Pow(D, ai) * Math.Pow(T, -a) * Math.Pow(10, (ci - c) + bi / D - b / T);
        }

        private static double CalculateRelativeHumidityWMOOverWater(double temperature, double dewPoint)
        {
            double e = VaporPressureOverWater(temperature);
            double ew = VaporPressureOverWater(dewPoint);
            return 100 * ew / e;
        }

        private static double CalculateRelativeHumidityWMOOverIce(double temperature, double dewPoint)
        {
            double e = temperature > 0 ? VaporPressureOverWater(temperature) : VaporPressureOverIce(temperature);
            double ei = VaporPressureOverIce(dewPoint);
            return 100 * ei / e;
        }

        private static double ToKelvin(this double temperature) => temperature + 273.15;

        private static double VaporPressureOverWater(double t) => 611.2 * Math.Exp((17.62 * t) / (243.12 + t));

        private static double VaporPressureOverIce(double t) => 611.2 * Math.Exp((22.46 * t) / (272.62 + t));
    }
}

