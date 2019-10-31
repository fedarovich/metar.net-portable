using System;

namespace ENG.WMOCodes.Codes
{
    /// <summary>
    /// Enum used to describe special flag of TAF report.
    /// </summary>
    [Flags]
    public enum TafGroups
    {
        /// <summary>
        /// No flag
        /// </summary>
        None = 0,
        /// <summary>
        /// Ammended, keyword AMD in TAF report.
        /// </summary>
        Ammended,
        /// <summary>
        /// Cancelled, keyword CNL in TAF report.
        /// </summary>
        Cancelled,
        /// <summary>
        /// Corrected, keyword COR in TAF report.
        /// </summary>
        Corrected,
        /// <summary>
        /// Missing, keyword NIL in TAF report
        /// </summary>
        Missing
    }
}