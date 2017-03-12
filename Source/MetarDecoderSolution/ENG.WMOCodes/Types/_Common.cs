using System;
using ENG.WMOCodes.Extensions;

namespace ENG.WMOCodes.Types
{

    /// <summary>
    /// Represents static class with common info
    /// </summary>
    public static class Convertions
    {
        private static readonly double[,] DistanceUnitConversionTable = {
            {1, 1000, 0.621371192, 3280.8399},
            {0.001, 1, 0.000621371192, 3.2808399},
            {1.609344 , 1609.344, 1, 5280 },
            {0.0003048, 0.3048, 0.000189393939 ,1}
        };

        private static readonly double[,] SpeedUnitConversionTable = {
            { 1, 1.85200, 0.5144444444444445, 1.1508},
            { 0.539956803, 1, 0.539956803, 0.6214},
            {0.539956803, 3.6, 1, 2.2369},
            {0.539956803, 0.539956803, 0.539956803, 1}
        };

        /// <summary>
        /// Converts direction as integer into enum eDirection. <see cref="Direction"/>
        /// </summary>
        /// <param name="heading">Heading, values from 0 to 360</param>
        /// <returns></returns>
        public static Direction HeadingToDirection(int heading)
        {
            if (!heading.IsBetween(0, 360))
                throw new ArgumentException("Invalid heading. Should be between 0 to 360.");

            if (heading < 22 || heading > 337)
                return Direction.N;
            if (heading < 67)
                return Direction.NE;
            if (heading < 117)
                return Direction.E;
            if (heading < 157)
                return Direction.SE;
            if (heading < 202)
                return Direction.S;
            if (heading < 247)
                return Direction.SW;
            if (heading < 292)
                return Direction.W;
            if (heading < 338)
                return Direction.NW;
            throw new Exception("Invalid program state - unable recognize direction");
        }

        /// <summary>
        /// Converts value from one distance unit to other.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sourceUnit"></param>
        /// <param name="targetUnit"></param>
        /// <returns></returns>
        public static double Convert(double value, DistanceUnit sourceUnit, DistanceUnit targetUnit)
        {
            double ret = value * DistanceUnitConversionTable[(int)sourceUnit, (int)targetUnit];

            return ret;
        }

        /// <summary>
        /// Converts value from one speed unit to other.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sourceUnit"></param>
        /// <param name="targetUnit"></param>
        /// <returns></returns>
        public static double Convert(double value, SpeedUnit sourceUnit, SpeedUnit targetUnit)
        {
            double ret = value * SpeedUnitConversionTable[(int)sourceUnit, (int)targetUnit];

            return ret;
        }
    }
}
