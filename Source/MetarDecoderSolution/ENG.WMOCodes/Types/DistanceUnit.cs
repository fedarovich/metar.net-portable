using System.Diagnostics.CodeAnalysis;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Distance units.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum DistanceUnit
    {
        /// <summary>
        /// Kilometres
        /// </summary>
        km = 0,
        /// <summary>
        /// Metres
        /// </summary>
        m = 1,
        /// <summary>
        /// Miles
        /// </summary>
        mi = 2,
        /// <summary>
        /// Feet
        /// </summary>
        ft = 3
    }
}