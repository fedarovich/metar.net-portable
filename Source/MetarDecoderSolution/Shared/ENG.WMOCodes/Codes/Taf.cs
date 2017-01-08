using System;
using System.Collections.Generic;
using System.Text;
using ENG.WMOCodes.Extensions;
using ENG.WMOCodes.Types.DateTimeTypes;
using ENG.WMOCodes.Types;

namespace ENG.WMOCodes.Codes
{
    /// <summary>
    /// Represents TAF report representing the weather forecast.
    /// </summary>
    public class Taf : TrendReport
    {
        #region Nested

        #endregion Nested

        #region Properties

        private TafGroups _tafType = TafGroups.None;

        private List<TrendInfoForTaf> _trends = new List<TrendInfoForTaf>();
        ///<summary>
        /// Sets/gets SubReports value.
        ///</summary>
        public List<TrendInfoForTaf> Trends
        {
            get
            {
                return _trends;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "Property Trends cannot be null. Invoke Clear() method or set new empty instance.");

                _trends = value;
            }
        }

        /// <summary>
        /// Gets/sets if TAF is ammended. <seealso cref="TafGroups"/>
        /// </summary>
        public bool IsAmmended
        {
            get
            {
                return GetFlag(TafGroups.Ammended);
            }
            set
            {
                UpdateFlag(TafGroups.Ammended, value);
            }
        }
        /// <summary>
        /// Gets/sets if TAF is cancelled. <seealso cref="TafGroups"/>
        /// </summary>
        public bool IsCancelled
        {
            get
            {
                return GetFlag(TafGroups.Cancelled);
            }
            set
            {
                UpdateFlag(TafGroups.Cancelled, value);
            }
        }
        
        /// <summary>
        /// Gets/sets if TAF is corrected. <seealso cref="TafGroups"/>
        /// </summary>
        public bool IsCorrected
        {
            get
            {
                return GetFlag(TafGroups.Corrected);
            }
            set
            {
                UpdateFlag(TafGroups.Corrected, value);
            }
        }

        /// <summary>
        /// Gets/sets if TAF is missing. <seealso cref="TafGroups"/>
        /// </summary>
        public bool IsMissing
        {
            get
            {
                return GetFlag(TafGroups.Missing);
            }
            set
            {
                UpdateFlag(TafGroups.Missing, value);
            }
        }


        ///<summary>
        /// Sets/gets ICAO value.
        ///</summary>
        public string ICAO { get; set; }


        ///<summary>
        /// Sets/gets DayTime value.
        ///</summary>
        public DayHourMinute DayTime { get; set; }


        ///<summary>
        /// Sets/gets Period value.
        ///</summary>
        public DayHourDayHour Period { get; set; }


        ///<summary>
        /// Sets/gets Remark value. Default value is "".
        ///</summary>
        public string Remark { get; set; } = "";


        ///<summary>
        /// Sets/gets MaxTemperature value. Default value is null.
        ///</summary>
        public TemperatureExtremeTX MaxTemperature { get; set; } = null;

        ///<summary>
        /// Sets/gets MinTemperature value. Default value is null.
        ///</summary>
        public TemperatureExtremeTN MinTemperature { get; set; } = null;

        #endregion Properties

        private bool GetFlag(TafGroups tafType) => _tafType.HasFlag(tafType);

        private void UpdateFlag(TafGroups tafType, bool value)
        {
            if (value)
                _tafType = _tafType | tafType;
            else
                _tafType = _tafType & ~tafType;
        }

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public override string ToCode()
        {
            StringBuilder ret = new StringBuilder();

            ret.Append("TAF");
            if (IsCorrected) ret.AppendPreSpaced("COR");
            if (IsAmmended) ret.AppendPreSpaced("AMD");
            ret.AppendPreSpaced(ICAO);
            ret.AppendPreSpaced(DayTime.ToCode() + "Z");
            if (IsMissing) ret.AppendPreSpaced("NIL");

            if (IsMissing == false)
            {
                ret.AppendPreSpaced(Period.ToCode());
                if (IsCancelled) ret.AppendPreSpaced("CNL");
                ret.AppendPreSpaced(base.ToCode());
                if (MaxTemperature != null) ret.AppendPreSpaced(MaxTemperature.ToCode());
                if (MinTemperature != null) ret.AppendPreSpaced(MinTemperature.ToCode());

                foreach (var fItem in Trends)
                {
                    ret.AppendPreSpaced(fItem.ToCode());
                }
            }

            return ret.ToString();
        }
    }
}