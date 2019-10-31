using System.Diagnostics.CodeAnalysis;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Pressure unit.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum PressureUnit
    {
        /// <summary>
        /// Hectopascals. E.g. for Q1013
        /// </summary>
        hPa,
        /// <summary>
        /// Inches of Mercury. E.g. for A29.92
        /// </summary>
        inHg,
    }
}