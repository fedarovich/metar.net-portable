namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Represents measuring device restriction.
    /// </summary>
    public enum DeviceMeasurementRestriction
    {
        /// <summary>
        /// If used, visibility is at best at this value.
        /// Device cannot measure less value.
        /// </summary>
        M = 0,
        /// <summary>
        /// If used, visibility is at worse at this value.
        /// Device cannot measure bigger value.
        /// </summary>
        P = 1,
    }
}