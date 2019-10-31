using System.Diagnostics.CodeAnalysis;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// World directions
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum Direction
    {
        /// <summary>
        /// North
        /// </summary>
        N,
        /// <summary>
        /// NorthEast
        /// </summary>
        NE,
        /// <summary>
        /// East
        /// </summary>
        E,
        /// <summary>
        /// SouthEast
        /// </summary>
        SE,
        /// <summary>
        /// South
        /// </summary>
        S,
        /// <summary>
        /// SouthWest
        /// </summary>
        SW,
        /// <summary>
        /// West
        /// </summary>
        W,
        /// <summary>
        /// NortWest
        /// </summary>
        NW
    }
}