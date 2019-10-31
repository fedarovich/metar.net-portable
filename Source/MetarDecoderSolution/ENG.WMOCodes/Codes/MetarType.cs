using System;
using System.Collections.Generic;
using System.Text;

namespace ENG.WMOCodes.Codes
{
    /// <summary>
    /// Types of metar. Now METAR only supported, SPECI not supported.
    /// </summary>
    ///<remarks>
    /// METAR is the name of the code for an aerodrome routine meteorological report. SPECI is the name of the code
    /// for an aerodrome special meteorological report. A METAR report and a SPECI report may have a trend forecast
    /// appended.
    ///</remarks>
    public enum MetarType
    {
        /// <summary>
        /// Metar type - Aerodrome routine meteorological report 
        /// </summary>
        METAR,
        /// <summary>
        /// Speci type - Aerodrome special meteorological report
        /// </summary>
        SPECI
    }
}
