namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Represents state of the sea, as specified in WMO, CodeTable 3700
    /// </summary>
    public enum SeaState
    {
        /// <summary>
        /// Calm (glassy)
        /// </summary>
        CalmGlassy = 0,
        /// <summary>
        /// Calm (rippled)
        /// </summary>
        CalmRippled = 1,
        /// <summary>
        /// Smooth
        /// </summary>
        Smooth = 2,
        /// <summary>
        /// Slight
        /// </summary>
        Slight = 3,
        /// <summary>
        /// Moderate
        /// </summary>
        Moderate = 4,
        /// <summary>
        /// Rought
        /// </summary>
        Rought = 5,
        /// <summary>
        /// Very rought
        /// </summary>
        VeryRough = 6,
        /// <summary>
        /// High
        /// </summary>
        High = 7,
        /// <summary>
        /// Very high
        /// </summary>
        VeryHigh = 8,
        /// <summary>
        /// Phenomenal
        /// </summary>
        PhenomenalOver = 9
    }
}