using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using ENG.WMOCodes.Extensions;
using ENG.WMOCodes.Types;
using ENG.WMOCodes.Types.Basic;
using ENG.WMOCodes.Types.DateTimeTypes;

namespace ENG.WMOCodes.Codes
{
    /// <summary>
    /// Represents metar.
    /// </summary>
    public class Metar : ICodeItem
    {
        #region Properties

        ///<summary>
        /// Sets/gets Type value.
        ///</summary>
        ///<remarks>
        ///The code name METAR or SPECI shall be included at the beginning of each individual
        ///report.
        ///When a deterioration of one weather element is accompanied by an improvement in
        ///another element (for example, lowering of clouds and an improvement in visibility), a single
        ///SPECI report shall be issued.
        ///</remarks>
        public MetarType Type { get; set; } = MetarType.METAR;

        private string _icao = "----";
        ///<summary>
        /// Sets/gets ICAO value.
        ///</summary>
        ///<remarks>
        ///The identiﬁcation of the reporting station in each individual report shall be indicated by
        ///means of the ICAO location indicator.
        ///</remarks>
        public string ICAO
        {
            get => _icao;
            set => _icao = value ?? throw new ArgumentNullException(nameof(value));
        }

        private DayHourMinute _date = new DayHourMinute();
        ///<summary>
        /// Sets/gets Date value.
        ///</summary>
        ///<remarks>
        ///The day of the month and the time of observation in hours and minutes UTC followed, with-
        ///out a space, by the letter indicator Z shall be included in each individual METAR report.
        ///This group shall be included in each individual SPECI report. In SPECI reports, this group
        ///shall indicate the time of occurrence of the change(s) which justiﬁed the issue of the report.
        ///</remarks>
        public DayHourMinute Date
        {
            get => _date;
            set => _date = value ?? throw new ArgumentNullException(nameof(value));
        }

        ///<summary>
        /// Sets/gets IsAUTO value. The optional code word AUTO shall be inserted before the wind group 
        /// when a report contains fully automated observations without human intervention. 
        ///</summary>
        ///<remarks>
        ///  The optional code word AUTO shall be inserted before the wind group when a report contains
        ///  fully automated observations without human intervention. The ICAO requirement is
        ///  that all of the speciﬁed elements shall be reported. However, if any element cannot be
        ///  observed, the group in which it would have been encoded shall be replaced by the appropriate
        ///  number of solidi. The number of solidi depends on the number of symbolic
        ///  letters for the speciﬁc group which is not able to be reported; i.e. four for the visibility
        ///  group, two for the present weather group and three or six for the cloud group, as appropriate.
        ///</remarks>
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public bool IsAUTO { get; set; }

        private WindWithVariability _wind = new WindWithVariability();
        ///<summary>
        /// Sets/gets Wind value, including VRB wind, wind varying, and gusts.
        ///</summary>
        public WindWithVariability Wind
        {
            get => _wind;
            set => _wind = value ?? throw new ArgumentNullException(nameof(value));
        }


        private VisibilityForMetar _visibility = new VisibilityForMetar();

        ///<summary>
        /// Sets/gets Visibility value, including directions.
        ///</summary>
        public VisibilityForMetar Visibility
        {
            get => _visibility;
            set => _visibility = value ?? throw new ArgumentNullException(nameof(value));
        }

        private PhenomInfo _phenomena;
        ///<summary>
        /// Sets/gets Phenomens value.
        ///</summary>
        public PhenomInfo Phenomena
        {
            get => _phenomena;
            set => _phenomena = value ?? throw new ArgumentNullException(nameof(value), "Property Phenomena cannot be null. Use empty object/collection instead.");
        }


        private CloudInfoWithNCD _clouds;
        ///<summary>
        /// Sets/gets Clouds value. Cannot be null.
        ///</summary>
        public CloudInfoWithNCD Clouds
        {
            get => _clouds;
            set => _clouds = value ?? throw new ArgumentNullException(nameof(value), "Property Clouds cannot be null. Use empty object/collection instead.");
        }

        ///<summary>
        /// Sets/gets Temperature value.
        ///</summary>
        public int Temperature { get; set; } = 20;

        ///<summary>
        /// Sets/gets DewPoint value.
        ///</summary>
        public int DewPoint { get; set; } = 10;

        private PressureInfo _pressure = new PressureInfo();
        ///<summary>
        /// Sets/gets Pressure value.
        ///</summary>
        public PressureInfo Pressure
        {
            get => _pressure;
            set => _pressure = value ?? throw new ArgumentNullException(nameof(value));
        }

        private RePhenomInfo _rePhenomens;
        ///<summary>
        /// Sets/gets RePhenoms value. Cannot be null
        ///</summary>
        public RePhenomInfo RePhenomens
        {
            get => _rePhenomens;
            set => _rePhenomens = value ?? throw new ArgumentNullException(nameof(value), "Property RePhenomens cannot be null. Use empty object/collection instead.");
        }

        private WindShearInfo _windShears;
        ///<summary>
        /// Sets/gets WindShears value. Cannot be null.
        ///</summary>
        public WindShearInfo WindShears
        {
            get => _windShears;
            set => _windShears = value ?? throw new ArgumentNullException(nameof(value), "Property WindShears cannot be null. Use empty object/collection instead.");
        }

        private RunwayConditionInfo _runwayConditions;
        ///<summary>
        /// Sets/gets RunwayConditions value. Cannot be null.
        ///</summary>
        public RunwayConditionInfo RunwayConditions
        {
            get => _runwayConditions;
            set => _runwayConditions = value ?? throw new ArgumentNullException(nameof(value), "Property Runways cannot be null. Use empty object/collection instead.");
        }

        ///<summary>
        /// Sets/gets Trend value. Allways value is in here, when no info found, trend type is null.
        /// Trend in metar is required, (NOSIG text is minimum).
        ///</summary>
        public TrendInfoForMetar Trend { get; set; } = new TrendInfoForMetar();

        ///<summary>
        /// Sets/gets Remark value (without RMK prefix), or null if not presented in metar.
        ///</summary>
        public string Remark { get; set; } = null;


        ///<summary>
        /// Sets/gets IsCorrected value. Default value is false.
        ///</summary>
        public bool IsCorrected { get; set; } = false;

        ///<summary>
        /// Sets/gets IsMissing value. Default value is false.
        ///</summary>
        public bool IsMissing { get; set; } = false;


        ///<summary>
        /// Sets/gets SeaState value. Default value is null.
        ///</summary>
        public SeaState? SeaState { get; set; } = null;


        ///<summary>
        /// Sets/gets SeaSurfaceTemperature value. Default value is null.
        ///</summary>
        public int? SeaSurfaceTemperature { get; set; } = null;

        /// <summary>
        /// <para>Calculates humidity. Very rough aproximation: 100 - 5d * (Temperature - DewPoint)</para> 
        /// <para>
        /// This method is deprecated. 
        /// Use <see cref="HumidityCalculator.CalculateRelativeHumidity(Metar,RelativeHumidityFormula)"/>
        /// method instead.
        /// </para>
        /// </summary>
        [Obsolete("Use HumidityCalculator.CalculateRelativeHumidity extension method instead.")]
        public double Humidity => 100 - 5d * (Temperature - DewPoint);

        #endregion Properties

        #region Inherited

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public string ToCode()
        {
            StringBuilder ret = new StringBuilder();

            //    ENG.Metar.Decoder.Metar x =
            //ENG.Metar.Decoder.Metar.Create(
            //"METAR LKPR 312300Z VRB03KT 0800 R08/0300 R09/0400D -SN +RABR FEW020CB OVC040TCU M01/M10 Q0997 " +
            //"RERA RESN WS RWY24C RWY04 R04C/012345 R22/////// " +
            //"TEMPO FM1010 TL2020 AT1330 VRB03G20KT 010V040 M1/8SM RA SNBR FEW040 OVC050TCU RMK HOTOVO");

            ret.AppendPreSpaced(Type.ToString());
            ret.AppendPreSpaced(ICAO);
            ret.AppendPreSpaced(Date.ToCode() + "Z");
            if (IsAUTO) ret.AppendPreSpaced("AUTO");
            ret.AppendPreSpaced(Wind.ToCode());
            ret.AppendPreSpaced(Visibility.ToCode());
            if (Phenomena != null)
                ret.AppendPreSpaced(Phenomena.ToCode());
            if (Clouds != null)
                ret.AppendPreSpaced(Clouds.ToCode());
            ret.AppendPreSpaced(IntToMetarString(Temperature) + "/" + IntToMetarString(DewPoint));
            ret.AppendPreSpaced(Pressure.ToCode());
            if (RePhenomens != null)
                ret.AppendPreSpaced(RePhenomens.ToCode());
            if (WindShears != null)
                ret.AppendPreSpaced(WindShears.ToCode());
            if (RunwayConditions != null)
                ret.AppendPreSpaced(RunwayConditions.ToCode());
            if (Trend != null)
                ret.AppendPreSpaced(Trend.ToCode());
            if (!string.IsNullOrEmpty(Remark))
                ret.AppendPreSpaced("RMK " + Remark);

            return ret.ToString().TrimEnd();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current instance.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current instance.</returns>
        public override string ToString()
        {
            return this.ToInlineInfoString();
        }

        #region MetarItem Members

        /// <summary>
        /// Proceed sanity check of inserted values.
        /// </summary>
        /// <param name="errors">Found errors.</param>
        /// <param name="warnings">Found warnings.</param>
        public void SanityCheck(ref List<string> errors, ref List<string> warnings)
        {
            Date.SanityCheck(ref errors, ref warnings);
            Wind.SanityCheck(ref errors, ref warnings);
            Visibility.SanityCheck(ref errors, ref warnings);
            if (Phenomena != null)
            {
                if (Phenomena.IsEmpty()) warnings.Add("Phenomens are used but are empty.");
                Phenomena.SanityCheck(ref errors, ref warnings);
            }
            if (Clouds != null)
            {
                if (Clouds.IsEmpty()) warnings.Add("Clouds are used but are empty.");
                Clouds.SanityCheck(ref errors, ref warnings);
            }
            Pressure.SanityCheck(ref errors, ref warnings);
            if (RePhenomens != null)
            {
                if (RePhenomens.IsEmpty()) warnings.Add("Re-phenomens are used but are empty.");
                RePhenomens.SanityCheck(ref errors, ref warnings);
            }
            if (WindShears != null)
            {
                if (WindShears.IsEmpty()) warnings.Add("Windshears are used but are empty.");
                WindShears.SanityCheck(ref errors, ref warnings);
            }
            if (RunwayConditions != null)
            {
                if (RunwayConditions.IsEmpty()) warnings.Add("Runway conditions are used but are empty.");
                RunwayConditions.SanityCheck(ref errors, ref warnings);
            }
            Trend?.SanityCheck(ref errors, ref warnings);
        }

        #endregion MetarItem Members

        #endregion Inherited

        #region Private methods
        private static string IntToMetarString(int p)
        {
            if (p <= 0)
                return "M" + (-p).ToString("00");
            return p.ToString("00");
        }

        #endregion Private methods
    }
}
