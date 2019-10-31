namespace ENG.WMOCodes.Types
{
    public struct RunwayVisibilityDistance
    {
        public RunwayVisibilityDistance(int value, DeviceMeasurementRestriction? restriction = null)
        {
            Value = value;
            Restriction = restriction;
        }

        public int Value { get; }

        public DeviceMeasurementRestriction? Restriction { get; }

        public override string ToString()
        {
            return $"{Restriction}{Value}";
        }

        /// <summary>
        /// Returns string represention of this value.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string ToString(string format)
        {
            return $"{Restriction}{Value.ToString(format)}";
        }
    }
}
