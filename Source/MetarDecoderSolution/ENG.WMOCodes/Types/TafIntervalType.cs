using System.Diagnostics.CodeAnalysis;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Represents type of the information, e.g. BECMG, PROB30 TEMPO, etc. 
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum TafIntervalType
    {
        /// <summary>
        /// Becoming
        /// </summary>
        BECMG,
        /// <summary>
        /// Temporarily
        /// </summary>
        TEMPO,
        /// <summary>
        /// Temporarily with 30% prob.
        /// </summary>
        TEMPO_PROB30,
        /// <summary>
        /// Temporarily with 40% prob.
        /// </summary>
        TEMPO_PROB40,
        /// <summary>
        /// Unknown, but used in US tafs.
        /// </summary>
        INTER,
        /// <summary>
        /// With 30% prob.
        /// </summary>
        PROB30,
        /// <summary>
        /// With 40% prob.
        /// </summary>
        PROB40
    }
}