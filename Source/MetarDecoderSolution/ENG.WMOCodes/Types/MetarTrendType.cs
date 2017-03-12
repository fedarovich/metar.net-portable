using System.Diagnostics.CodeAnalysis;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Type of trend.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum MetarTrendType
    {
        /// <summary>
        /// No significant change trend.
        /// </summary>
        NOSIG,
        /// <summary>
        /// Becoming trend.
        /// </summary>
        BECMG,
        /// <summary>
        /// Temporaly trend.
        /// </summary>
        TEMPO
    }
}