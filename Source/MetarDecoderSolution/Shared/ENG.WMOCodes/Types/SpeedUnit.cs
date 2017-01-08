using System.Diagnostics.CodeAnalysis;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Speed units
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum SpeedUnit
    {
        /// <summary>
        /// Knots
        /// </summary>
        kt = 0,
        /// <summary>
        /// Kilometers per hour
        /// </summary>
        kph = 1,
        /// <summary>
        /// Miles per hour
        /// </summary>
        mps = 2,
        /// <summary>
        /// Miles per hour
        /// </summary>
        miph = 3
    }
}