using System.Diagnostics.CodeAnalysis;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// All types of phenomens.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum Phenomenon
    {
        /// <summary>
        /// Light. Value "-"
        /// </summary>
        Light = 100,
        /// <summary>
        /// Heavy. Value "+"
        /// </summary>
        Heavy,
        /// <summary>
        /// In vicinity
        /// </summary>
        VC,
        /// <summary>
        /// Shallow
        /// </summary>
        MI = 200,
        /// <summary>
        /// Patches
        /// </summary>
        BC,
        /// <summary>
        /// Partial
        /// </summary>
        PR, 
        /// <summary>
        /// Low drifting
        /// </summary>
        DR, 
        /// <summary>
        /// Blowing
        /// </summary>
        BL, 
        /// <summary>
        /// Shower
        /// </summary>
        SH, 
        /// <summary>
        /// Thunderstorm
        /// </summary>
        TS, 
        /// <summary>
        /// Freezing
        /// </summary>
        FZ,
        /// <summary>
        /// Drizzle
        /// </summary>
        DZ = 300,
        /// <summary>
        /// Rain
        /// </summary>
        RA,
        /// <summary>
        /// Snow
        /// </summary>
        SN,
        /// <summary>
        /// Snow grains
        /// </summary>
        SG, 
        /// <summary>
        /// Ice crystals
        /// </summary>
        IC, 
        /// <summary>
        /// Ice pellets
        /// </summary>
        PL, 
        /// <summary>
        /// Hail
        /// </summary>
        GR, 
        /// <summary>
        /// Snow pellets
        /// </summary>
        GS,
        /// <summary>
        /// Mist
        /// </summary>
        BR = 400, 
        /// <summary>
        /// Fog
        /// </summary>
        FG, 
        /// <summary>
        /// Smoke
        /// </summary>
        FU, 
        /// <summary>
        /// Volcanic ash.
        /// </summary>
        VA, 
        /// <summary>
        /// Dust
        /// </summary>
        DU, 
        /// <summary>
        /// Sand
        /// </summary>
        SA, 
        /// <summary>
        /// Haze
        /// </summary>
        HZ, 
        /// <summary>
        /// Dust/sand whirls 
        /// </summary>
        PO = 500, 
        /// <summary>
        /// Squalls
        /// </summary>
        SQ, 
        /// <summary>
        /// Funnel cloud
        /// </summary>
        FC, 
        /// <summary>
        /// Sand storm
        /// </summary>
        SS, 
        /// <summary>
        /// Dust storm
        /// </summary>
        DS 
    }
}