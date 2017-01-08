namespace ENG.WMOCodes.Types.Basic
{
    /// <summary>
    /// Defines how the method ToString formats output.
    /// </summary>
    public enum RationalFormat
    {
        /// <summary>
        /// If defined, friction format will be used even if the value is integer, e.g. 3/1 will create output 3/1. If not used, 
        /// output will be 3.
        /// </summary>
        ForceToUseFriction,
        /// <summary>
        /// If defined, preceeding whole integer part will be derived, e.g. 7/3 will create 2 1/3. If not used,
        /// output will be 7/3.
        /// </summary>
        UsePreceedingWhole,
    }
}