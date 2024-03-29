﻿using System;
using System.ComponentModel;
using System.Text;

namespace ENG.WMOCodes.Types.Basic
{
    /// <summary>
    /// Represents racional number (struct).
    /// </summary>
    public struct Rational : IComparable<Rational>
    {
        /// <summary>
        /// Represents numerator.
        /// </summary>
        public readonly int Numerator;
        /// <summary>
        /// Represents denominator.
        /// </summary>
        public readonly int Denominator;

        /// <summary>
        /// Initializes a new Instance of ENG.Metar.Decoder.Racional
        /// </summary>
        /// <param name="numerator"></param>
        /// <param name="denominator"></param>
        /// <exception cref="ArithmeticException">
        /// Raised when denominator is zero.
        /// </exception>
        public Rational(int numerator, int denominator)
        {
            if (denominator == 0) throw new ArithmeticException("Denominator cannot be 0.");

            this.Numerator = numerator;
            this.Denominator = denominator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rational"/> struct.
        /// </summary>
        /// <param name="wholePart">The whole part before the racional format.</param>
        /// <param name="numerator">The numerator.</param>
        /// <param name="denominator">The denominator.</param>
        /// <remarks>
        /// Whole part means the part before the racional format. E.g. in racional "3 4/5" is wholePart = 3,
        /// numerator = 4 and denominator = 5. Internally is this value converted into 19/5.
        /// </remarks>
        public Rational(int wholePart, int numerator, int denominator) : this(numerator + wholePart * denominator, denominator) { }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Deconstruct(out int numerator, out int denominator)
        {
            numerator = Numerator;
            denominator = Denominator;
        }

        /// <summary>
        /// Converts string to Racional number using / or : as delimiters between numerator and denominator.
        /// Exception is throw in case of failure.
        /// </summary>
        /// <param name="value">String value to convert.</param>
        /// <returns>Returned racional number.</returns>
        /// <exception cref="FormatException">Thrown in case that whole string does not contain
        /// delimiter between numerator and denominator ( / or : are expected), or in case that
        /// numerator or denominator cannot be parsed as integer values.</exception>
        /// <exception cref="ArgumentNullException">Thrown in case that argument "value" is null.</exception>
        /// <exception cref="OutOfMemoryException">Thrown in case that
        /// numerator or denominator parsed values are less than <see cref="int.MinValue"/>
        /// or greater than <see cref="int.MaxValue"/> of int data type.</exception>
        public static Rational Parse(string value)
        {
            Rational ret = Parse(value, System.Globalization.NumberStyles.Integer, null);
            return ret;
        }
        /// <summary>
        /// Converts string to Racional number using / or : as delimiters between numerator and denominator.
        /// Exception is throw in case of failure.
        /// </summary>
        /// <param name="value">String value to convert.</param>
        /// <param name="style">A bitwise combination of enumeration values that indicates the style elements that 
        /// can be present in s. A typical value to specify is 
        /// <see cref="System.Globalization.NumberStyles.Integer"> Integer</see></param>
        /// <returns>Returned racional number.</returns>
        /// <exception cref="FormatException">Thrown in case that whole string does not contain
        /// delimiter between numerator and denominator ( / or : are expected), or in case that
        /// numerator or denominator cannot be parsed as integer values.</exception>
        /// <exception cref="ArgumentNullException">Thrown in case that argument "value" is null.</exception>
        /// <exception cref="OutOfMemoryException">Thrown in case that
        /// numerator or denominator parsed values are less than <see cref="int.MinValue"/>
        /// or greater than <see cref="int.MaxValue"/> of int data type.</exception>
        /// <exception cref="ArgumentException">Thrown when style is not a NumberStyles value. -or- style 
        /// is not a combination of AllowHexSpecifier and HexNumber values accepted for numerator or
        /// denominator parsing to int.</exception>
        public static Rational Parse(string value, System.Globalization.NumberStyles style)
        {
            Rational ret = Parse(value, System.Globalization.NumberStyles.Integer, null);
            return ret;
        }
        /// <summary>
        /// Converts string to Racional number using / or : as delimiters between numerator and denominator.
        /// Exception is throw in case of failure.
        /// </summary>
        /// <param name="value">String value to convert.</param>
        /// <param name="provider">An object that supplies culture-specific information about the format for integer
        /// parsing of numerator and denominator from value.</param>
        /// <returns>Returned racional number.</returns>
        /// <exception cref="FormatException">Thrown in case that whole string does not contain
        /// delimiter between numerator and denominator ( / or : are expected), or in case that
        /// numerator or denominator cannot be parsed as integer values.</exception>
        /// <exception cref="ArgumentNullException">Thrown in case that argument "value" is null.</exception>
        /// <exception cref="OutOfMemoryException">Thrown in case that
        /// numerator or denominator parsed values are less than <see cref="int.MinValue"/>
        /// or greater than <see cref="int.MaxValue"/> of int data type.</exception>
        public static Rational Parse(string value, IFormatProvider provider)
        {
            Rational ret = Parse(value, System.Globalization.NumberStyles.Integer, provider);
            return ret;
        }
        /// <summary>
        /// Converts string to Racional number using / or : as delimiters between numerator and denominator.
        /// Exception is throw in case of failure.
        /// </summary>
        /// <param name="value">String value to convert.</param>
        /// <param name="style">A bitwise combination of enumeration values that indicates the style elements that 
        /// can be present in s. A typical value to specify is 
        /// <see cref="System.Globalization.NumberStyles.Integer"> Integer</see></param>
        /// <param name="provider">An object that supplies culture-specific information about the format for integer
        /// parsing of numerator and denominator from value.</param>
        /// <returns>Returned racional number.</returns>
        /// <exception cref="FormatException">Thrown in case that whole string does not contain
        /// delimiter between numerator and denominator ( / or : are expected), or in case that
        /// numerator or denominator cannot be parsed as integer values.</exception>
        /// <exception cref="ArgumentNullException">Thrown in case that argument "value" is null.</exception>
        /// <exception cref="OutOfMemoryException">Thrown in case that
        /// numerator or denominator parsed values are less than <see cref="int.MinValue"/>
        /// or greater than <see cref="int.MaxValue"/> of int data type.</exception>
        /// <exception cref="ArgumentException">Thrown when style is not a NumberStyles value. -or- style 
        /// is not a combination of AllowHexSpecifier and HexNumber values accepted for numerator or
        /// denominator parsing to int.</exception>
        public static Rational Parse(string value, System.Globalization.NumberStyles style, IFormatProvider provider)
        {
            if (value == null)
                throw new ArgumentNullException();


            int ind;

            ind = value.IndexOf('/');
            if (ind < 0)
                ind = value.IndexOf(':');
            if (ind < 0)
                throw new FormatException("Delimiter \"/\" or \":\" not found in string.");

            string numString = value.Substring(0, ind);
            string denString = value.Substring(ind + 1);

            int num;
            int den;
            try
            {
                num = int.Parse(numString, style, provider);
            }
            catch (FormatException ex)
            {
                throw new FormatException("Value \"" + numString + "\" is not correct value for numerator.", ex);
            }
            catch (OutOfMemoryException ex)
            {
                throw new OutOfMemoryException("Value of numerator \"" + numString + "\": " + ex.Message, ex);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Value of numerator \"" + numString + "\": " + ex.Message, ex);
            }

            try
            {
                den = int.Parse(denString, style, provider);
            }
            catch (FormatException ex)
            {
                throw new FormatException("Value \"" + denString + "\" is not correct value for denominator.", ex);
            }
            catch (OutOfMemoryException ex)
            {
                throw new OutOfMemoryException("Value of denominator \"" + denString + "\": " + ex.Message, ex);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Value of denominator \"" + denString + "\": " + ex.Message, ex);
            }

            Rational ret = new Rational(num, den);

            return ret;
        }

        /// <summary>
        /// Try to convert value string to result racional number.
        /// Returns true in case of success, false otherwise.
        /// </summary>
        /// <param name="value">Input string value with / or : as delimiters between numerator and denominator.</param>
        /// <param name="result">If method returs true, here is parsed racional number. If methods returns false, 
        /// value is undefined.</param>
        /// <returns>True if parse was successful, false otherwise.</returns>
        public static bool TryParse(string value, out Rational result)
        {
            bool ret = TryParse(value, System.Globalization.NumberStyles.Integer, null, out result);

            return ret;

        }

        /// <summary>
        /// Try to convert value string to result racional number.
        /// Returns true in case of success, false otherwise.
        /// </summary>
        /// <param name="value">Input string value with / or : as delimiters between numerator and denominator.</param>
        /// <param name="style">A bitwise combination of enumeration values that indicates the style elements that 
        /// can be present in s. A typical value to specify is 
        /// <see cref="System.Globalization.NumberStyles.Integer"> Integer</see></param>
        /// <param name="provider">An object that supplies culture-specific information about the format for integer
        /// parsing of numerator and denominator from value.</param>
        /// <param name="result">If method returs true, here is parsed racional number. If methods returns false, 
        /// value is undefined.</param>
        /// <returns>True if parse was successful, false otherwise.</returns>
        public static bool TryParse(string value, System.Globalization.NumberStyles style,
          IFormatProvider provider, out Rational result)
        {
            bool ret;
            try
            {
                result = Parse(value, style, provider);
                ret = true;
            }
            catch (Exception)
            {
                result = default(Rational);
                ret = false;
            }

            return ret;
        }

        /// <summary>
        /// Returns real value of racional number.
        /// </summary>
        public double Value => Numerator / (double)Denominator;

        /// <summary>
        /// Operator ==
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Rational a, Rational b)
        {
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            bool ret = a.Value == b.Value;
            return ret;
        }
        /// <summary>
        /// Operator !=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Rational a, Rational b)
        {
            return !(a == b);
        }
        /// <summary>
        /// Operator greater-than
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >(Rational a, Rational b)
        {
            return a.CompareTo(b) > 0;
        }
        /// <summary>
        /// Operator less-than
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <(Rational a, Rational b)
        {
            return a.CompareTo(b) < 0;
        }
        /// <summary>
        /// Operator equal-or-more-than
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator >=(Rational a, Rational b)
        {
            return a.CompareTo(b) >= 0;
        }
        /// <summary>
        /// Operator equal-or-less-than
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator <=(Rational a, Rational b)
        {
            return a.CompareTo(b) <= 0;
        }


        /// <summary>
        /// Indicates whether this instance and a specified object are equal by their real values.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>true if <paramref name="obj"/> and this instance have the same real value; otherwise, false.</returns>
        /// <filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (!(obj is Rational))
                throw new InvalidCastException("Requested type: Racional. Found: " + obj.GetType().FullName);
            else
                return this == (Rational)obj;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// Returns value of this instance; param decides if delimited as racional number (e.g. 3/1).
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public string ToString(RationalFormat flag)
        {
            StringBuilder ret = new StringBuilder();
            int n = Numerator;
            int d = Denominator;
            int w = 0;

            if ((flag & RationalFormat.UsePreceedingWhole) > 0)
            {
                if (n > d)
                {
                    w = n / d;
                    n = n - w * d;
                }

                if (w > 0)
                    ret.Append(w.ToString());
            }

            // if rational part of number does make sense (that is, it is not O/1)
            if (n != 0 || d != 1)
            {
                // if there is whole part, we must add space behind its print
                if (w > 0)
                    ret.Append(" ");

                if (n % d == 0
                  &&
                  (flag & RationalFormat.ForceToUseFriction) == 0)
                    ret.Append(n / d);
                else
                    ret.Append(n + "/" + d);
            }

            return ret.ToString();
        }
        /// <summary>
        /// Returns value of this instance, allways delimited as racional number (e.g. 3/1).
        /// </summary>
        /// <returns></returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return ToString(RationalFormat.UsePreceedingWhole);
        }

        /// <summary>
        /// Obsolete! Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <param name="p">if set to <c>true</c> [p].</param>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        [Obsolete("Use other overloads instead of this one.")]
        public string ToString(bool p)
        {
            return ToString(RationalFormat.ForceToUseFriction);
        }

        /// <summary>
        /// Operator plus
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Rational operator +(Rational a, Rational b)
        {
            Rational ret;
            if (a.Denominator == b.Denominator)
            {
                ret = new Rational(a.Numerator + b.Numerator, a.Denominator);
            }
            else
            {
                int dom = a.Denominator * b.Denominator;
                int nom = a.Numerator * (dom / a.Denominator) +
                  b.Numerator * (dom / b.Denominator);
                ret = new Rational(nom, dom);
            }

            ret = ret.Normalize();
            return ret;
        }
        /// <summary>
        /// Operator plus
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Rational operator +(Rational a, int b)
        {
            Rational ret = new Rational(
              a.Numerator + b * a.Denominator, a.Denominator);
            ret = ret.Normalize();
            return ret;
        }
        /// <summary>
        /// Operator plus
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Rational operator -(Rational a, Rational b)
        {
            Rational ret;
            if (a.Denominator == b.Denominator)
                ret = new Rational(a.Numerator - b.Numerator, a.Denominator);
            else
            {
                int dom = a.Denominator * b.Denominator;
                int num = a.Numerator * (dom / a.Denominator) -
                  b.Numerator * (dom / b.Denominator);
                ret = new Rational(num, dom);
            }
            ret = ret.Normalize();
            return ret;
        }
        /// <summary>
        /// Operator minus
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Rational operator -(Rational a, int b)
        {
            Rational ret = new Rational(
              a.Numerator - b * a.Denominator, a.Denominator);
            ret = ret.Normalize();
            return ret;
        }
        /// <summary>
        /// Operator minus
        /// </summary>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Rational operator -(int b, Rational a)
        {
            Rational ret = new Rational(
              b * a.Denominator - a.Numerator, a.Denominator);
            ret = ret.Normalize();
            return ret;
        }
        /// <summary>
        /// Operator multiply
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Rational operator *(Rational a, Rational b)
        {
            Rational ret;
            int dom = a.Denominator * b.Denominator;
            int num = a.Numerator * b.Denominator;
            ret = new Rational(num, dom);
            ret = ret.Normalize();
            return ret;
        }
        /// <summary>
        /// Operator multiply
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Rational operator *(Rational a, int b)
        {
            Rational ret = new Rational(
              a.Numerator * b, a.Denominator);
            ret = ret.Normalize();
            return ret;
        }
        /// <summary>
        /// Operator divide
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Rational operator /(Rational a, Rational b)
        {
            Rational ret =
              a * b.FlipOver();

            ret = ret.Normalize();
            return ret;
        }
        /// <summary>
        /// Operator divide
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Rational operator /(Rational a, int b)
        {
            Rational ret = a / new Rational(b, 1);
            ret = ret.Normalize();
            return ret;
        }
        /// <summary>
        /// Operator divide
        /// </summary>
        /// <param name="b"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Rational operator /(int b, Rational a)
        {
            Rational ret = new Rational(b, 1) / a;
            ret = ret.Normalize();
            return ret;
        }

        /// <summary>
        /// Operator implicit conversion to double
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static explicit operator double(Rational a)
        {
            return a.Value;
        }
        /// <summary>
        /// Operator explicit conversion to integer.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static explicit operator int(Rational a)
        {
            return (int)a.Value;
        }
        /// <summary>
        /// Operator implicit conversion from integer.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static implicit operator Rational(int a)
        {
            return new Rational(a, 1);
        }
        /// <summary>
        /// Creates value from double.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Rational FromDouble(double val)
        {
            Rational ret;

            int mult = 1;
            int intVal = (int)val;

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            while (val != intVal)
            {
                mult *= 10;
                intVal = (int)(val * mult);
            }

            ret = new Rational(intVal, mult);
            ret.Normalize();
            return ret;
        }

        /// <summary>
        /// Abbreviates the racional and movers minus sign into numerator, if exists. E.g. from 2/-4 to -1/2.
        /// </summary>
        /// <returns></returns>
        public Rational Normalize()
        {
            Rational ret;
            int dom = Denominator;
            int num = Numerator;
            int nsd = GetGreatestCommonDivisor(dom, num);
            if (dom < 0)
                nsd = -nsd;
            ret = new Rational(num / nsd, dom / nsd);
            return ret;
        }

        /// <summary>
        /// Returns new instance of racinal number which has flipped numerator and denominator values.
        /// </summary>
        /// <returns>Racional number with flipped over numerator and denominator values.</returns>
        /// <exception cref="ArithmeticException">
        /// Raised when denominator is zero.
        /// </exception>
        public Rational FlipOver()
        {
            return new Rational(Denominator, Numerator);
        }

        private static int GetGreatestCommonDivisor(int u, int v)
        {
            //      Mějme dána dvě přirozená čísla, uložená v proměnných u a v (u>v).
            //Dokud v není nulové, opakuj:
            //  Do r ulož zbytek po dělení čísla u číslem v
            //  Do u ulož v
            //  Do v ulož r
            //Konec algoritmu, v u je uložen největší společný dělitel původních čísel

            //      Mějme dána dvě přirozená čísla, uložená v proměnných u a v (u>v).
            if (!(u > v))
            {
                int pom = v;
                v = u;
                u = pom;
            }

            int r;
            //Dokud v není nulové, opakuj:
            while (v > 0)
            {
                //  Do r ulož zbytek po dělení čísla u číslem v
                r = u % v;
                //  Do u ulož v
                u = v;
                //  Do v ulož r
                v = r;
            }
            //Konec algoritmu, v u je uložen největší společný dělitel původních čísel

            return u;
        }

        #region IComparable<Racional> Members

        /// <summary>
        /// Compares two racional number by their real values.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Rational other)
        {
            if (Value > other.Value)
                return 1;
            if (Value < other.Value)
                return -1;
            return 0;
        }

        #endregion
    }
}
