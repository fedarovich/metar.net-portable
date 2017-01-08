using System.Diagnostics.CodeAnalysis;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Type of trend time.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum TrendTimeType
    {
        /// <summary>
        /// From date/time
        /// </summary>
        FM = 0,
        /// <summary>
        /// Until date/time
        /// </summary>
        TL = 1,
        /// <summary>
        /// At date/time
        /// </summary>
        AT = 2,
    }
}