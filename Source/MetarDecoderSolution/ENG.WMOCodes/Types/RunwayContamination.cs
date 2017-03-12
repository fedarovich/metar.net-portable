namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Represents amount of contamination on runway.
    /// </summary>
    public enum RunwayContamination
    {
        /// <summary>
        /// Reserved, not used.
        /// </summary>
        Reserved0 = 0,
        /// <summary>
        /// Less than 10%.
        /// </summary>
        LessThan10Percent = 1,
        /// <summary>
        /// More than 10% but less than 25%.
        /// </summary>
        LessThan25Percent = 2,
        /// <summary>
        /// Reserved, not used.
        /// </summary>
        Reserved3 = 3,
        /// <summary>
        /// Reserved, not used.
        /// </summary>
        Reserved4 = 4,
        /// <summary>
        /// More than 25%, but less than 50%.
        /// </summary>
        LessThan50Percent = 5,
        /// <summary>
        /// Reserved, not used.
        /// </summary>
        Reserved6 = 6,
        /// <summary>
        /// Reserved, not used.
        /// </summary>
        Reserved7 = 7,
        /// <summary>
        /// Reserved, not used.
        /// </summary>
        Reserved8 = 8,
        /// <summary>
        /// More than 50% including 100%.
        /// </summary>
        MoreThan50Percent = 9
    }
}