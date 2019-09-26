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

using System;
using System.Globalization;

namespace Plexdata.Converters.Entities
{
    /// <summary>
    /// Capacity entity provides functionality to calculate and format capacity strings.
    /// </summary>
    /// <remarks>
    /// This class provides extended functionalities to calculate and format capacity 
    /// strings. Such a capacity string consists of a number (automatically calculated 
    /// or as is) followed by one of the pre-defined units or a user defined unit.
    /// </remarks>
    public class CapacityEntity
    {
        #region Construction

        /// <summary>
        /// Standard constructor with initial arguments.
        /// </summary>
        /// <remarks>
        /// This constructor applies provided arguments to its corresponding properties.
        /// </remarks>
        /// <param name="value">
        /// The value to be used as divisor for result calculations.
        /// </param>
        /// <param name="unit1">
        /// The first unit to be appended at results.
        /// </param>
        /// <param name="unit2">
        /// The alternative unit to be appended at results.
        /// </param>
        public CapacityEntity(Decimal value, String unit1, String unit2)
            : base()
        {
            if (value == Decimal.Zero)
            {
                throw new ArgumentException($"The {nameof(value)} must not be zero.", nameof(value));
            }

            if (String.IsNullOrWhiteSpace(unit1))
            {
                throw new ArgumentException("First unit must not be null or empty, or consists only of white spaces.", nameof(unit1));
            }

            if (String.IsNullOrWhiteSpace(unit2))
            {
                throw new ArgumentException("Second unit must not be null or empty, or consists only of white spaces.", nameof(unit2));
            }

            this.Value = value;
            this.Unit1 = unit1.Trim();
            this.Unit2 = unit2.Trim();
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the value to be used as divisor for result calculations.
        /// </summary>
        /// <remarks>
        /// This property returns the value to be used as divisor for result calculations.
        /// </remarks>
        /// <value>
        /// The divisor for result calculations.
        /// </value>
        public Decimal Value { get; private set; }

        /// <summary>
        /// Gets the first unit to be appended at results.
        /// </summary>
        /// <remarks>
        /// This property returns the first unit to be appended at results.
        /// </remarks>
        /// <value>
        /// The first unit to appended at results.
        /// </value>
        public String Unit1 { get; private set; }

        /// <summary>
        /// Gets the alternative unit to be appended at results.
        /// </summary>
        /// <remarks>
        /// This property returns the alternative unit to be appended at results.
        /// </remarks>
        /// <value>
        /// The alternative unit to appended at results.
        /// </value>
        public String Unit2 { get; private set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Determines whether provided <paramref name="unit"/> is equal to one of the 
        /// assigned units.
        /// </summary>
        /// <remarks>
        /// This method determines whether provided unit is equal to one of the assigned 
        /// units. The string comparison takes place by using <i>invariant culture</i> as 
        /// well as <i>ignoring upper and lower cases</i>.
        /// </remarks>
        /// <param name="unit">
        /// The unit to be compared.
        /// </param>
        /// <returns>
        /// True is returned if provided <paramref name="unit"/> is equal either to 
        /// <see cref="Unit1"/> or to <see cref="Unit2"/>. Otherwise, false is returned.
        /// </returns>
        public Boolean IsEqual(String unit)
        {
            return this.Unit1.Equals(unit, StringComparison.InvariantCultureIgnoreCase) || this.Unit2.Equals(unit, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Calculates the result by using the default number of decimal digits. 
        /// </summary>
        /// <remarks>
        /// This method calculates the result by using the default number of decimal digits. 
        /// The default number of decimal digits is zero.
        /// </remarks>
        /// <param name="value">
        /// The value to be calculated.
        /// </param>
        /// <returns>
        /// The calculated value.
        /// </returns>
        /// <seealso cref="Calculate(Decimal, Int32)"/>
        public Decimal Calculate(Decimal value)
        {
            return this.Calculate(value, this.GetDecimals());
        }

        /// <summary>
        /// Calculates the result by using provided number of decimal digits. 
        /// </summary>
        /// <remarks>
        /// This method calculates the result by using provided number of decimal digits. 
        /// The provided number of decimal digits is limited to a range of [0..28]. 
        /// </remarks>
        /// <param name="value">
        /// The value to be calculated.
        /// </param>
        /// <param name="decimals">
        /// The number of decimal digits to be used for calculation.
        /// </param>
        /// <returns>
        /// The calculated value.
        /// </returns>
        public Decimal Calculate(Decimal value, Int32 decimals)
        {
            const Int32 min = 0;
            const Int32 max = 28;

            decimals = (decimals < min) ? min : ((decimals > max) ? max : decimals);

            // Math.Round() requires decimals in range of [0..28].
            return Math.Round(value / this.Value, decimals);
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value)
        {
            return this.Format(value, this.GetUnit(), this.GetDecimals(), this.GetCalculate(), this.GetFormatter());
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="unit">
        /// The unit to be appended.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, String unit)
        {
            return this.Format(value, unit, this.GetDecimals(), this.GetCalculate(), this.GetFormatter());
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="decimals">
        /// The number of decimal digits to be used for calculation.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, Int32 decimals)
        {
            return this.Format(value, this.GetUnit(), decimals, this.GetCalculate(), this.GetFormatter());
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="unit">
        /// The unit to be appended.
        /// </param>
        /// <param name="decimals">
        /// The number of decimal digits to be used for calculation.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, String unit, Int32 decimals)
        {
            return this.Format(value, this.GetUnit(unit), decimals, this.GetCalculate(), this.GetFormatter());
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="calculate">
        /// True to force a result calculation, otherwise false.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, Boolean calculate)
        {
            return this.Format(value, this.GetUnit(), this.GetDecimals(), calculate, this.GetFormatter());
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="unit">
        /// The unit to be appended.
        /// </param>
        /// <param name="calculate">
        /// True to force a result calculation, otherwise false.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, String unit, Boolean calculate)
        {
            return this.Format(value, unit, this.GetDecimals(), calculate, this.GetFormatter());
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="decimals">
        /// The number of decimal digits to be used for calculation.
        /// </param>
        /// <param name="calculate">
        /// True to force a result calculation, otherwise false.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, Int32 decimals, Boolean calculate)
        {
            return this.Format(value, this.GetUnit(), decimals, calculate, this.GetFormatter());
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="unit">
        /// The unit to be appended.
        /// </param>
        /// <param name="decimals">
        /// The number of decimal digits to be used for calculation.
        /// </param>
        /// <param name="calculate">
        /// True to force a result calculation, otherwise false.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, String unit, Int32 decimals, Boolean calculate)
        {
            return this.Format(value, unit, decimals, calculate, this.GetFormatter());
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="culture">
        /// The culture to be used for calculation.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, CultureInfo culture)
        {
            return this.Format(value, this.GetUnit(), this.GetDecimals(), this.GetCalculate(), this.GetFormatter(culture));
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="unit">
        /// The unit to be appended.
        /// </param>
        /// <param name="culture">
        /// The culture to be used for calculation.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, String unit, CultureInfo culture)
        {
            return this.Format(value, unit, this.GetDecimals(), this.GetCalculate(), this.GetFormatter(culture));
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="decimals">
        /// The number of decimal digits to be used for calculation.
        /// </param>
        /// <param name="culture">
        /// The culture to be used for calculation.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, Int32 decimals, CultureInfo culture)
        {
            return this.Format(value, this.GetUnit(), decimals, this.GetCalculate(), this.GetFormatter(culture));
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="unit">
        /// The unit to be appended.
        /// </param>
        /// <param name="decimals">
        /// The number of decimal digits to be used for calculation.
        /// </param>
        /// <param name="culture">
        /// The culture to be used for calculation.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, String unit, Int32 decimals, CultureInfo culture)
        {
            return this.Format(value, unit, decimals, this.GetCalculate(), this.GetFormatter(culture));
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="calculate">
        /// True to force a result calculation, otherwise false.
        /// </param>
        /// <param name="culture">
        /// The culture to be used for calculation.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, Boolean calculate, CultureInfo culture)
        {
            return this.Format(value, this.GetUnit(), this.GetDecimals(), calculate, this.GetFormatter(culture));
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="unit">
        /// The unit to be appended.
        /// </param>
        /// <param name="calculate">
        /// True to force a result calculation, otherwise false.
        /// </param>
        /// <param name="culture">
        /// The culture to be used for calculation.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, String unit, Boolean calculate, CultureInfo culture)
        {
            return this.Format(value, unit, this.GetDecimals(), calculate, this.GetFormatter(culture));
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="decimals">
        /// The number of decimal digits to be used for calculation.
        /// </param>
        /// <param name="calculate">
        /// True to force a result calculation, otherwise false.
        /// </param>
        /// <param name="culture">
        /// The culture to be used for calculation.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, Int32 decimals, Boolean calculate, CultureInfo culture)
        {
            return this.Format(value, this.GetUnit(), decimals, calculate, this.GetFormatter(culture));
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="unit">
        /// The unit to be appended.
        /// </param>
        /// <param name="decimals">
        /// The number of decimal digits to be used for calculation.
        /// </param>
        /// <param name="calculate">
        /// True to force a result calculation, otherwise false.
        /// </param>
        /// <param name="culture">
        /// The culture to be used for calculation.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, String unit, Int32 decimals, Boolean calculate, CultureInfo culture)
        {
            return this.Format(value, unit, decimals, calculate, this.GetFormatter(culture));
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="formatter">
        /// The number formatter to be used for result formatting.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, NumberFormatInfo formatter)
        {
            return this.Format(value, this.GetUnit(), this.GetDecimals(), this.GetCalculate(), formatter);
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="unit">
        /// The unit to be appended.
        /// </param>
        /// <param name="formatter">
        /// The number formatter to be used for result formatting.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, String unit, NumberFormatInfo formatter)
        {
            return this.Format(value, unit, this.GetDecimals(), this.GetCalculate(), formatter);
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="decimals">
        /// The number of decimal digits to be used for calculation.
        /// </param>
        /// <param name="formatter">
        /// The number formatter to be used for result formatting.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, Int32 decimals, NumberFormatInfo formatter)
        {
            return this.Format(value, this.GetUnit(), decimals, this.GetCalculate(), formatter);
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="unit">
        /// The unit to be appended.
        /// </param>
        /// <param name="decimals">
        /// The number of decimal digits to be used for calculation.
        /// </param>
        /// <param name="formatter">
        /// The number formatter to be used for result formatting.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, String unit, Int32 decimals, NumberFormatInfo formatter)
        {
            return this.Format(value, unit, decimals, this.GetCalculate(), formatter);
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="calculate">
        /// True to force a result calculation, otherwise false.
        /// </param>
        /// <param name="formatter">
        /// The number formatter to be used for result formatting.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, Boolean calculate, NumberFormatInfo formatter)
        {
            return this.Format(value, this.GetUnit(), this.GetDecimals(), calculate, formatter);
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="unit">
        /// The unit to be appended.
        /// </param>
        /// <param name="calculate">
        /// True to force a result calculation, otherwise false.
        /// </param>
        /// <param name="formatter">
        /// The number formatter to be used for result formatting.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, String unit, Boolean calculate, NumberFormatInfo formatter)
        {
            return this.Format(value, unit, this.GetDecimals(), calculate, formatter);
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="decimals">
        /// The number of decimal digits to be used for calculation.
        /// </param>
        /// <param name="calculate">
        /// True to force a result calculation, otherwise false.
        /// </param>
        /// <param name="formatter">
        /// The number formatter to be used for result formatting.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, Int32 decimals, Boolean calculate, NumberFormatInfo formatter)
        {
            return this.Format(value, this.GetUnit(), decimals, calculate, formatter);
        }

        /// <summary>
        /// Formats provided value according to current settings.
        /// </summary>
        /// <remarks>
        /// This method formats provided value according to current settings.
        /// </remarks>
        /// <param name="value">
        /// The value to be formatted.
        /// </param>
        /// <param name="unit">
        /// The unit to be appended.
        /// </param>
        /// <param name="decimals">
        /// The number of decimal digits to be used for calculation.
        /// </param>
        /// <param name="calculate">
        /// True to force a result calculation, otherwise false.
        /// </param>
        /// <param name="formatter">
        /// The number formatter to be used for result formatting.
        /// </param>
        /// <returns>
        /// The string representation of formatted value.
        /// </returns>
        public String Format(Decimal value, String unit, Int32 decimals, Boolean calculate, NumberFormatInfo formatter)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            if (this.Value == Decimal.One)
            {
                decimals = 0;
            }

            formatter = this.GetFormatter(formatter, decimals);

            value = calculate ? this.Calculate(value, decimals) : value;

            String format = $"{{0:N}} {this.GetUnit(unit)}";

            return String.Format(formatter, format, value);
        }

        /// <summary>
        /// Returns a string representation of current instance.
        /// </summary>
        /// <remarks>
        /// This overwritten method returns a string representation of current 
        /// instance consisting of current value and the second unit.
        /// </remarks>
        /// <returns>
        /// The string representation of current instance.
        /// </returns>
        public override String ToString()
        {
            return $"{this.Value.ToString("N0")} {this.Unit2}";
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Returns the default unit.
        /// </summary>
        /// <remarks>
        /// This method returns the default unit.
        /// </remarks>
        /// <returns>
        /// The default unit.
        /// </returns>
        /// <seealso cref="GetUnit(String)"/>
        private String GetUnit()
        {
            return this.GetUnit(null);
        }

        /// <summary>
        /// Returns a valid unit to be used.
        /// </summary>
        /// <remarks>
        /// This method returns a valid unit to be used. The <see cref="Unit1"/> 
        /// is returned if parameter <paramref name="unit"/> is <c>null</c> or 
        /// <c>empty</c>, or consists only of white spaces. The <see cref="Unit1"/> 
        /// is returned if parameter <paramref name="unit"/> is <i>almost</i> equal 
        /// to <see cref="Unit1"/>. The <see cref="Unit2"/> is returned if parameter 
        /// <paramref name="unit"/> is <i>almost</i> equal to <see cref="Unit2"/>. In 
        /// any other case a trimmed version of <paramref name="unit"/> is returned.
        /// Each string comparison takes place by using <i>invariant culture</i> as 
        /// well as <i>ignoring upper and lower cases</i>.
        /// </remarks>
        /// <param name="unit">
        /// The unit to be verified.
        /// </param>
        /// <returns>
        /// The unit to be used.
        /// </returns>
        private String GetUnit(String unit)
        {
            if (String.IsNullOrWhiteSpace(unit))
            {
                return this.Unit1;
            }

            unit = unit.Trim();

            if (this.Unit1.Equals(unit, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.Unit1;
            }

            if (this.Unit2.Equals(unit, StringComparison.InvariantCultureIgnoreCase))
            {
                return this.Unit2;
            }

            return unit;
        }

        /// <summary>
        /// Returns the default number of decimal digits.
        /// </summary>
        /// <remarks>
        /// This method returns the default number of decimal digits, which is zero.
        /// </remarks>
        /// <returns>
        /// The default number of decimal digits.
        /// </returns>
        private Int32 GetDecimals()
        {
            return 0;
        }

        /// <summary>
        /// Returns the default calculation behaviour.
        /// </summary>
        /// <remarks>
        /// This method returns the default calculation behaviour, which is true.
        /// </remarks>
        /// <returns>
        /// The default calculation behaviour.
        /// </returns>
        private Boolean GetCalculate()
        {
            return true;
        }

        /// <summary>
        /// Returns the default culture.
        /// </summary>
        /// <remarks>
        /// This method returns the default culture, which is Current UI Culture.
        /// </remarks>
        /// <returns>
        /// The default culture.
        /// </returns>
        private CultureInfo GetCulture()
        {
            return CultureInfo.CurrentUICulture;
        }

        /// <summary>
        /// Returns the default number formatter.
        /// </summary>
        /// <remarks>
        /// This method returns the default number formatter, which is based on 
        /// default culture..
        /// </remarks>
        /// <returns>
        /// The default number formatter.
        /// </returns>
        /// <seealso cref="GetCulture()"/>
        /// <seealso cref="GetFormatter(CultureInfo)"/>
        private NumberFormatInfo GetFormatter()
        {
            return this.GetFormatter(this.GetCulture());
        }

        /// <summary>
        /// Returns the number formatter based on provided culture.
        /// </summary>
        /// <remarks>
        /// This method returns the number formatter based on provided 
        /// <paramref name="culture"/>.
        /// </remarks>
        /// <param name="culture">
        /// The culture to be used as base.
        /// </param>
        /// <returns>
        /// The derived number formatter.
        /// </returns>
        /// <seealso cref="GetDecimals()"/>
        /// <seealso cref="GetFormatter(CultureInfo, Int32)"/>
        private NumberFormatInfo GetFormatter(CultureInfo culture)
        {
            return this.GetFormatter(culture, this.GetDecimals());
        }

        /// <summary>
        /// Returns the number formatter based on provided culture.
        /// </summary>
        /// <remarks>
        /// This method returns the number formatter based on provided 
        /// <paramref name="culture"/> and adjusts the number of decimal 
        /// digits accordingly.
        /// </remarks>
        /// <param name="culture">
        /// The culture to be used as base. This parameter can be <c>null</c>.
        /// </param>
        /// <param name="decimals">
        /// The number of decimal digits to be used.
        /// </param>
        /// <returns>
        /// The derived and configured number formatter.
        /// </returns>
        /// <seealso cref="GetCulture()"/>
        /// <seealso cref="GetFormatter(NumberFormatInfo, Int32)"/>
        private NumberFormatInfo GetFormatter(CultureInfo culture, Int32 decimals)
        {
            if (culture == null)
            {
                culture = this.GetCulture();
            }

            return this.GetFormatter(culture.NumberFormat, decimals);
        }

        /// <summary>
        /// Returns a clone of provided formatter and adjusts its number of decimal 
        /// digits accordingly.
        /// </summary>
        /// <remarks>
        /// This method returns a clone of provided <paramref name="formatter"/> and 
        /// adjusts its number of decimal digits accordingly. The provided number of 
        /// decimal digits is limited to a range of [0..99]. 
        /// </remarks>
        /// <param name="formatter">
        /// The formatter to be cloned.
        /// </param>
        /// <param name="decimals">
        /// The number of decimal digits to be used.
        /// </param>
        /// <returns>
        /// The derived and configured number formatter.
        /// </returns>
        private NumberFormatInfo GetFormatter(NumberFormatInfo formatter, Int32 decimals)
        {
            const Int32 min = 0;
            const Int32 max = 99;

            if (formatter == null)
            {
                formatter = this.GetCulture().NumberFormat;
            }

            NumberFormatInfo result = formatter.Clone() as NumberFormatInfo;

            // NumberFormatInfo.NumberDecimalDigits requires decimals in range of [0..99].
            result.NumberDecimalDigits = (decimals < min) ? min : ((decimals > max) ? max : decimals);

            return result;
        }

        #endregion
    }
}

