using System.Diagnostics.CodeAnalysis;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// List of types of clouds. Comparable by "int" value.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum CloudType
    {
        /// <summary>
        /// Few clouds.
        /// </summary>
        FEW = 1,
        /// <summary>
        /// Scattered clouds.
        /// </summary>
        SCT = 2,
        /// <summary>
        /// Broken clouds.
        /// </summary>
        BKN = 3,
        /// <summary>
        /// Overcast clouds.
        /// </summary>
        OVC = 4
    }
}