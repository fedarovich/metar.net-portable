using System;
using System.Collections.Generic;
using System.Text;
using ENG.WMOCodes.Extensions;
using ENG.WMOCodes.Types.Basic;

namespace ENG.WMOCodes.Types
{
    /// <summary>
    /// Represents one cloud (e.g. OVC040TCU).
    /// </summary>
    public class Cloud : ICodeItem
    {
        #region Properties

        /// <summary>
        /// Sets/gets if cloud is cumulonimbus. That is, if there is prefix with CB, e.g. OVC040CB.
        /// </summary>
        /// <remarks>
        /// This property cannot be true if <see cref="IsToweringCumulus"/> is true.
        /// </remarks>
#pragma warning disable 618
        /// <seealso cref="IsCB"/>
#pragma warning restore 618
        public bool IsCumulonimbus { get; set; }

        /// <summary>
        /// Sets/gets if cloud is towering cumulus. That is, if there is prefix with TCU, e.g. OVC040TCU.
        /// </summary>
        /// <remarks>
        /// This property cannot be true if <see cref="IsCumulonimbus"/> is true.
        /// </remarks>
#pragma warning disable 618
        /// <seealso cref="IsTCU"/>
#pragma warning restore 618
        public bool IsToweringCumulus { get; set; }

        /// <summary>
        /// <para>Sets/gets if cloud is cumulonimbus. That is, if there is prefix with CB, e.g. OVC040CB.</para>
        /// <para>This property is deprecated. Use <see cref="IsCumulonimbus"/> property instead.</para>
        /// </summary>
        /// <remarks>
        /// This property cannot be true if IsTCU is true.
        /// </remarks>
        /// <seealso cref="IsCumulonimbus"/>
        [Obsolete("Deprecated. Use IsCumulonimbus property instead.")]
        // ReSharper disable once InconsistentNaming
        public bool IsCB
        {
            get { return IsCumulonimbus; }
            set { IsCumulonimbus = value; }
        }


        /// <summary>
        /// <para>Sets/gets if cloud is towering cumulus. That is, if there is prefix with TCU, e.g. OVC040TCU.</para>
        /// <para>This property is deprecated. Use <see cref="IsToweringCumulus"/> property instead.</para>
        /// </summary>
        /// <remarks>
        /// This property cannot be true if IsCB is true.
        /// </remarks>
        /// <seealso cref="IsToweringCumulus"/>
        [Obsolete("Deprecated. Use IsToweringCumulus property instead.")]
        // ReSharper disable once InconsistentNaming
        public bool IsTCU
        {
            get { return IsToweringCumulus; }
            set { IsToweringCumulus = value; }
        }


        ///<summary>
        /// Sets/gets Altitude value, in hundreds of feet, e.g. OVC040 for 4000 ft above airport or station.
        /// To get altitude in other unit, see method <seealso cref="GetAltitudeIn"/>
        ///</summary>
        public NonNegInt Altitude { get; private set; }


        ///<summary>
        /// Sets/gets type of clouds. <see cref="CloudType"/>
        ///</summary>
        public CloudType Type { get; private set; }

        #endregion Properties

        #region .ctor

        /// <summary>
        /// Initializes a new Instance of ENG.Metar.Decoder.Cloud
        /// </summary>
        /// <param name="type"></param>
        /// <param name="altitude"></param>
        public Cloud(CloudType type, NonNegInt altitude) : this(type, altitude, false, false)
        {
        }

        /// <summary>
        /// Initializes a new Instance of ENG.Metar.Decoder.Cloud
        /// </summary>
        /// <param name="type"></param>
        /// <param name="altitude"></param>
        /// <param name="isCumulonimbus"></param>
        /// <param name="isToweringCumulus"></param>
        public Cloud(CloudType type, NonNegInt altitude, bool isCumulonimbus, bool isToweringCumulus)
        {
            SetClouds(type, altitude, isCumulonimbus, isToweringCumulus);
        }

        /// <summary>
        /// Initializes a new Instance of ENG.Metar.Decoder.Cloud
        /// </summary>
        /// <param name="type"></param>
        /// <param name="altitude"></param>
        /// <param name="isCumulonimbus"></param>
        /// <param name="isToweringCumulus"></param>
        public Cloud(string type, NonNegInt altitude, bool isCumulonimbus, bool isToweringCumulus)
        {
            SetClouds(type, altitude, isCumulonimbus, isToweringCumulus);
        }

        #endregion .ctor

        #region Methods

        /// <summary>
        /// Sets cloud type.
        /// </summary>
        /// <param name="type">Type of cloud.</param>
        /// <param name="altitude">Altitude in hunderds of ft.</param>
        /// <param name="isCumulonimbus">True if cloud is cumulonimbus (CB).</param>
        /// <param name="isToweringCumulus">True if cloud is towering cumulus (TCU).</param>
        public void SetClouds(CloudType type, int altitude, bool isCumulonimbus, bool isToweringCumulus)
        {
            if (isCumulonimbus && isToweringCumulus)
                throw new ArgumentException("Unable to set both CB and TCU flags to true.", nameof(isToweringCumulus));

            IsToweringCumulus = isToweringCumulus;
            IsCumulonimbus = isCumulonimbus;

            Type = type;

            if (altitude > 999)
                throw new ArgumentException("Invalid altitude value. Maximum is 999.", nameof(altitude));
            Altitude = altitude;
        }

        /// <summary>
        /// Sets cloud type.
        /// </summary>
        /// <param name="type">Type of cloud.</param>
        /// <param name="altitude">Altitude in hunderds of ft.</param>
        public void SetClouds(CloudType type, int altitude)
        {
            SetClouds(type, altitude, false, false);
        }

        /// <summary>
        /// Sets cloud type.
        /// </summary>
        /// <param name="type">Type of cloud (as string).</param>
        /// <param name="altitude">Altitude in hunderds of ft.</param>
        /// <param name="isCumulonimbus">True if cloud is cumulonimbus (CB).</param>
        /// <param name="isToweringCumulus">True if cloud is towering cumulus (TCU).</param>
        public void SetClouds(string type, int altitude, bool isCumulonimbus, bool isToweringCumulus)
        {
            CloudType t = (CloudType)Enum.Parse(typeof(CloudType), type, false);
            SetClouds(t, altitude, isCumulonimbus, isToweringCumulus);
        }

        /// <summary>
        /// Sets cloud type.
        /// </summary>
        /// <param name="type">Type of cloud (as string).</param>
        /// <param name="altitude">Altitude in hunderds of ft.</param>
        public void SetClouds(string type, int altitude)
        {
            CloudType t = (CloudType)Enum.Parse(typeof(CloudType), type, false);
            SetClouds(t, altitude);
        }

        /// <summary>
        /// Returns cloud altitude in selected distance unit.
        /// </summary>
        /// <param name="distanceUnit"></param>
        /// <returns></returns>
        public double GetAltitudeIn(DistanceUnit distanceUnit)
        {
            int inFt = Altitude * 100;
            double ret = distanceUnit == DistanceUnit.ft 
                ? inFt 
                : Convertions.Convert(inFt, DistanceUnit.ft, distanceUnit);

            return ret;
        }

        #endregion Methods

        #region Inherited

        /// <summary>
        /// Returns item in code string.
        /// </summary>
        /// <returns></returns>
        public string ToCode()
        {
            StringBuilder ret = new StringBuilder();

            ret.Append(Type);
            ret.Append(Altitude.ToString("000"));
            if (IsToweringCumulus)
                ret.Append("TCU");
            else if (IsCumulonimbus)
                ret.Append("CB");

            return ret.ToString();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current instance.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current instance.</returns>
        public override string ToString()
        {
            return this.ToInlineInfoString();
        }

        /// <summary>
        /// Proceed sanity check of inserted values.
        /// </summary>
        /// <param name="errors">Found errors.</param>
        /// <param name="warnings">Found warnings.</param>
        public void SanityCheck(ref List<string> errors, ref List<string> warnings)
        {
            if (Altitude > 400)
                warnings.Add("Altitude value over 400 is probably incorrect.");
            if (IsToweringCumulus && IsCumulonimbus)
                errors.Add("IsTCU and IsCB flags cannot be set both at same time. Only one of them can be used.");
        }

        #endregion Inherited

    }
}
