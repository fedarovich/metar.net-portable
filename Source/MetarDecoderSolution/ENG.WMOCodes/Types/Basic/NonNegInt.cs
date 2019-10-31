using System;

namespace ENG.WMOCodes.Types.Basic
{
    /// <summary>
    /// Zero-value or positive integer.
    /// </summary>
    public struct NonNegInt
    {

        private int _value;
        ///<summary>
        /// Sets/gets Value value.
        ///</summary>
        public int Value
        {
            get => _value;
            set => _value = value >= 0 
                ? value 
                : throw new ArgumentOutOfRangeException(nameof(value), "Unable to set negative value into this type.");
        }

        /// <summary>
        /// Implicit conversion into integer.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator int(NonNegInt value)
        {
            return value.Value;
        }

        /// <summary>
        /// Explicit conversion from integer.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static implicit operator NonNegInt(int value)
        {
            return new NonNegInt { Value = value };
        }

        /// <summary>
        /// Returns value of the instance as string.
        /// </summary>
        public override string ToString()
        {
            return Value.ToString();
        }

        /// <summary>
        /// Returns string represention of this value.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public string ToString(string format)
        {
            return Value.ToString(format);
        }
    }
}
