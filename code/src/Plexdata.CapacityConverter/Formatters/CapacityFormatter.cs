/*
 * MIT License
 * 
 * Copyright (c) 2019 plexdata.de
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using Plexdata.Converters;
using Plexdata.Converters.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Plexdata.Formatters
{
    /// <summary>
    /// Capacity formatter provides functionality to easily format capacity values.
    /// </summary>
    /// <remarks>
    /// Capacity formatter is intended to be used in any string format operation that 
    /// wants to display a size in an appropriated byte format. Such an example could 
    /// be to display a file size in its megabyte representation.
    /// </remarks>
    /// <example>
    /// Assuming a file has a size of 9,876,543 bytes and this size should be shown in 
    /// its megabyte representation with three decimal digits. Is this case the capacity 
    /// formatter would be used as shown as follows.
    /// <code>
    /// // Result: "The file size is 9.419 MB."
    /// String.Format(new CapacityFormatter(), "The file size is {0:MB3}.", 9876543);
    /// </code>
    /// Keep in mind, this usage as shown above is the recommended way. Other ways may 
    /// work but without guarantee.
    /// </example>
    public class CapacityFormatter : IFormatProvider, ICustomFormatter
    {
        #region Construction

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <remarks>
        /// The default constructor just initializes its properties with default values.
        /// </remarks>
        public CapacityFormatter()
            : this(null)
        {
        }

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <remarks>
        /// The parameterized constructor initializes its properties. Keep in mind, current 
        /// UI culture is used if parameter <paramref name="culture"/> is <c>null</c>.
        /// </remarks>
        /// <param name="culture">
        /// The culture to be used or null if current UI culture is wanted.
        /// </param>
        /// <seealso cref="CapacityFormatter()"/>
        public CapacityFormatter(CultureInfo culture)
            : base()
        {
            this.Supported = new List<Type>()
            {
                typeof(SByte),
                typeof(Byte),
                typeof(Int16),
                typeof(UInt16),
                typeof(Int32),
                typeof(UInt32),
                typeof(Int64),
                typeof(UInt64),
                typeof(Single),
                typeof(Double),
                typeof(Decimal),
            };

            this.Culture = culture;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the list of supported types.
        /// </summary>
        /// <remarks>
        /// The list of supported types consists of all integer types (byte, short, 
        /// int, long; signed and unsigned) and all floating point types (float and 
        /// double), as well as of decimal.
        /// </remarks>
        /// <value>
        /// The list of supported types.
        /// </value>
        public IEnumerable<Type> Supported { get; private set; }

        /// <summary>
        /// Gets the used culture.
        /// </summary>
        /// <remarks>
        /// This getter returns the used culture. Keep in mind, this culture might be 
        /// <c>null</c>. In such a case, current UI culture is used instead.
        /// </remarks>
        /// <value>
        /// The used culture or <c>null</c>.
        /// </value>
        public CultureInfo Culture { get; private set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns an object that provides formatting services for the specified type.
        /// </summary>
        /// <remarks>
        /// This method represents the implementation of interface <see cref="IFormatProvider"/>.
        /// </remarks>
        /// <param name="type">
        /// An object that specifies the type of format object to return.
        /// </param>
        /// <returns>
        /// An instance of the object specified by format type, if the this implementation 
        /// can supply that type of object; otherwise, <c>null</c>.
        /// </returns>
        public Object GetFormat(Type type)
        {
            return (type == typeof(ICustomFormatter)) ? this : null;
        }

        /// <summary>
        /// Converts the value of a specified object to an equivalent string representation
        /// using specified format and culture-specific formatting information.
        /// </summary>
        /// <remarks>
        /// This method converts the value of a specified object to an equivalent string 
        /// representation using specified format and culture-specific formatting information.
        /// An empty string is returned in case of the <paramref name="value"/> is <c>null</c>.
        /// In contrast to that, the standard object formatting behaviour takes place in case of 
        /// the <paramref name="provider"/> is not an instance of this class.
        /// </remarks>
        /// <param name="format">
        /// A format string containing formatting specifications.
        /// </param>
        /// <param name="value">
        /// An object to format.
        /// </param>
        /// <param name="provider">
        /// An object that supplies format information about the current instance.
        /// </param>
        /// <returns>
        /// The string representation of the <paramref name="value"/>, formatted as specified 
        /// by <paramref name="format"/> and <paramref name="provider"/>.
        /// </returns>
        public String Format(String format, Object value, IFormatProvider provider)
        {
            if (value == null)
            {
                return String.Empty;
            }

            if (!this.Equals(provider) || !this.Supported.Contains(value.GetType()))
            {
                if (value is IFormattable)
                {
                    return (value as IFormattable).ToString(format, provider ?? this.GetCulture());
                }

                return value.ToString();
            }

            return CapacityConverter.Convert(
                Convert.ToDecimal(value),
                this.GetUnit(format),
                this.GetDecimals(format),
                this.GetCalculate(format),
                this.GetIntercept(format),
                this.GetCulture());
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Extracts the unit name from provided format string.
        /// </summary>
        /// <remarks>
        /// This method extracts the unit name from provided <paramref name="format"/> 
        /// string by stripping out any digits. This actually has the effect that something 
        /// like "1m2b" would become "mb".
        /// </remarks>
        /// <param name="format">
        /// The format string to extract the unit name from.
        /// </param>
        /// <returns>
        /// The unit name or an empty string.
        /// </returns>
        private String GetUnit(String format)
        {
            if (String.IsNullOrWhiteSpace(format))
            {
                return String.Empty;
            }

            // NOTE: Something like "un!it15" would end in "unit", which actually should cause a format exception.
            return Regex.Replace(format, "[^a-zA-Z]", String.Empty);
        }

        /// <summary>
        /// Extracts the decimal digits from provided format string.
        /// </summary>
        /// <remarks>
        /// This method extracts the decimal digits from provided <paramref name="format"/> 
        /// string by stripping out all non-digit-characters. This actually has the effect 
        /// that something like "1m2b" would become "12".
        /// </remarks>
        /// <param name="format">
        /// The format string to extract the decimal digits from.
        /// </param>
        /// <returns>
        /// The number of decimal digits or <c>zero</c>.
        /// </returns>
        private Int32 GetDecimals(String format)
        {
            if (String.IsNullOrWhiteSpace(format))
            {
                return Defaults.Decimals;
            }

            // NOTE: Something like "unit1!5" would end in "15", which actually should cause a format exception.
            String value = Regex.Replace(format, "[^0-9]", String.Empty);

            if (Int32.TryParse(value, out Int32 result))
            {
                return result;
            }

            return Defaults.Decimals;
        }

        /// <summary>
        /// Gets current calculate mode from provided format string.
        /// </summary>
        /// <remarks>
        /// This method gets current calculate mode from provided <paramref name="format"/> 
        /// string. The calculate mode is disabled if provided <paramref name="format"/> 
        /// string includes character "exclamation mark (!)". Otherwise calculate mode is 
        /// enabled.
        /// </remarks>
        /// <param name="format">
        /// The format string to get calculate mode from.
        /// </param>
        /// <returns>
        /// True is returned if calculate mode is not disabled and false otherwise.
        /// </returns>
        /// <seealso cref="Defaults.Calculate"/>
        private Boolean GetCalculate(String format)
        {
            if (String.IsNullOrWhiteSpace(format))
            {
                return Defaults.Calculate;
            }

            // Calculate is true if '!' is not included.
            return format.IndexOf("!", 0, StringComparison.InvariantCultureIgnoreCase) < 0;
        }

        /// <summary>
        /// Gets current intercept mode from provided format string.
        /// </summary>
        /// <remarks>
        /// This method gets current intercept mode from provided <paramref name="format"/> 
        /// string. The intercept mode is enabled if provided <paramref name="format"/> 
        /// string includes character "swung dash (~)". Otherwise intercept mode is disabled.
        /// </remarks>
        /// <param name="format">
        /// The format string to get intercept mode from.
        /// </param>
        /// <returns>
        /// True is returned if intercept mode is enabled and false otherwise.
        /// </returns>
        /// <seealso cref="Defaults.Intercept"/>
        private Boolean GetIntercept(String format)
        {
            if (String.IsNullOrWhiteSpace(format))
            {
                return Defaults.Intercept;
            }

            // Intercept is true if '~' is included.
            return format.IndexOf("~", 0, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

        /// <summary>
        /// Determines the used culture.
        /// </summary>
        /// <remarks>
        /// This method determines the culture to be used. Either this culture 
        /// is set by the constructor or it is the current UI culture.
        /// </remarks>
        /// <returns>
        /// The culture to be used.
        /// </returns>
        /// <seealso cref="Culture"/>
        private CultureInfo GetCulture()
        {
            return this.Culture ?? Defaults.Culture;
        }

        #endregion
    }
}
