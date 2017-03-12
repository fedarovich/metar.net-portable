namespace ENG.WMOCodes.Codes
{
    /// <summary>
    /// Species relative humidity formulas to be used by <see cref="HumidityCalculator"/>.
    /// </summary>
    public enum RelativeHumidityFormula
    {
        /// <summary>
        /// <para>WMO formulas are used for calculation.</para>
        /// <para>If the dew point is greater than -60 °C the over water formula is used (<see cref="WMOOverWater"/>).</para>
        /// <para>If the dew point is less than -60 °C the over water ice is used (<see cref="WMOOverIce"/>).</para>
        /// <para>The typical accuracy is about ±2%.</para>
        /// </summary>
        WMO = 0x0000,
        
        /// <summary>
        /// <para>WMO over water formula is used.</para>
        /// <para>The typical accuracy is about ±2%.</para>
        /// </summary>
        WMOOverWater = 0x0001,
        
        /// <summary>
        /// <para>WMO over ice formula is used.</para>
        /// <para>This formula give inaccurate results for positive temperatures.</para>
        /// </summary>
        WMOOverIce = 0x0002,

        /// <summary>
        /// <para>Formulas decribed in NASA TN D-8401 are used for calculation.</para>
        /// <para>If the dew point is greater than 0 °C the over water formula is used (<see cref="NasaOverWater"/>).</para>
        /// <para>If the dew point is less than 0 °C the over water ice is used (<see cref="NasaOverIce"/>).</para>
        /// <para>The typical accuracy is about ±2%.</para>
        /// </summary>
        Nasa = 0x1000,

        /// <summary>
        /// <para>Over water formula decribed in NASA TN D-8401 is used.</para>
        /// <para>The typical accuracy is about ±2%.</para>
        /// </summary>
        NasaOverWater = 0x1001,

        /// <summary>
        /// <para>Over ice formula decribed in NASA TN D-8401 is used.</para>
        /// <para>This formula give inaccurate results for positive temperatures.</para>
        /// </summary>
        NasaOverIce = 0x1002,
        
        /// <summary>
        /// Very rough but fast calculation. RH = 100 - 5 * (temperature - dewPoint)
        /// </summary>
        Rough = 0xFFFF,
    }
}
