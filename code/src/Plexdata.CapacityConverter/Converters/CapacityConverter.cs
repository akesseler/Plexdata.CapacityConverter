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

using Plexdata.Converters.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Plexdata.Converters
{
    /// <summary>
    /// Capacity converter provides functionality to easily convert capacity values into 
    /// formatted strings.
    /// </summary>
    /// <remarks>
    /// This class provides convenient functionality to easily convert capacity values into 
    /// formatted strings. For example, this class can convert a number of 123,456,789 bytes 
    /// into its capacity string representation like <i>117.738 MiB</i>.
    /// </remarks>
    public static class CapacityConverter
    {
        #region Construction

        /// <summary>
        /// The static class constructor.
        /// </summary>
        /// <remarks>
        /// This static class constructor just initializes all its static properties 
        /// with their initial values.
        /// </remarks>
        static CapacityConverter()
        {
            // Intentionally as list in reverse order!
            CapacityConverter.Capacities = new CapacityEntity[]
            {
                new CapacityEntity(Constants.Capacities.YiB, "YB",    "YiB"),    // YiB = Yobibyte = 2^80 Byte
                new CapacityEntity(Constants.Capacities.ZiB, "ZB",    "ZiB"),    // ZiB = Zebibyte = 2^70 Byte
                new CapacityEntity(Constants.Capacities.EiB, "EB",    "EiB"),    // EiB = Exbibyte = 2^60 Byte
                new CapacityEntity(Constants.Capacities.PiB, "PB",    "PiB"),    // PiB = Pebibyte = 2^50 Byte
                new CapacityEntity(Constants.Capacities.TiB, "TB",    "TiB"),    // TiB = Tebibyte = 2^40 Byte
                new CapacityEntity(Constants.Capacities.GiB, "GB",    "GiB"),    // GiB = Gibibyte = 2^30 Byte
                new CapacityEntity(Constants.Capacities.MiB, "MB",    "MiB"),    // MiB = Mebibyte = 2^20 Byte
                new CapacityEntity(Constants.Capacities.KiB, "KB",    "KiB"),    // KiB = Kibibyte = 2^10 Byte
                new CapacityEntity(Constants.Capacities.BiB, "Bytes", "BiB"),    // BiB = Bibibyte = 2^0  Byte
            };
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the default capacity entity.
        /// </summary>
        /// <remarks>
        /// This property returns the default capacity entity. The default capacity is 
        /// intended to be used as fallback. This fallback is actually the last entry 
        /// of all supported <see cref="Capacities"/>.
        /// </remarks>
        /// <value>
        /// The default capacity used as fallback.
        /// </value>
        public static CapacityEntity Default
        {
            get
            {
                return CapacityConverter.Capacities.Last();
            }
        }

        /// <summary>
        /// Gets the list of used capacity entities.
        /// </summary>
        /// <remarks>
        /// This property returns the list of used capacity entities.
        /// </remarks>
        /// <value>
        /// The list of used capacity entities.
        /// </value>
        public static IEnumerable<CapacityEntity> Capacities { get; private set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Finds a capacity entity by unit.
        /// </summary>
        /// <remarks>
        /// This method finds the first capacity entity for provided <paramref name="unit"/>.
        /// The last capacity entity is returned if none of the supported capacity entities 
        /// fits.
        /// </remarks>
        /// <param name="unit">
        /// The unit to get a capacity entity for.
        /// </param>
        /// <returns>
        /// One of the items in the list of used capacity entities.
        /// </returns>
        /// <seealso cref="Capacities"/>
        public static CapacityEntity Find(String unit)
        {
            CapacityEntity result = CapacityConverter.Capacities.FirstOrDefault(x => x.IsEqual(unit));

            return result ?? CapacityConverter.Default;
        }

        /// <summary>
        /// Finds a capacity entity by value.
        /// </summary>
        /// <remarks>
        /// This method finds the best fitting capacity entity by value. Best fitting 
        /// means the capacity entity where the <paramref name="value"/> divided by a 
        /// capacity entity's value is greater than or equal to one. 
        /// </remarks>
        /// <param name="value">
        /// The value to get a capacity entity for.
        /// </param>
        /// <returns>
        /// One of the items in the list of used capacity entities or the default 
        /// capacity entity.
        /// </returns>
        /// <seealso cref="Default"/>
        /// <seealso cref="Capacities"/>
        public static CapacityEntity Find(Decimal value)
        {
            CapacityEntity result = null;

            if (value != 0)
            {
                result = CapacityConverter.Capacities.FirstOrDefault(x => value / x.Value >= 1);
            }

            return result ?? CapacityConverter.Default;
        }

        /// <summary>
        /// Converts provided value into its string representation.
        /// </summary>
        /// <remarks>
        /// This method converts provided value into its string representation 
        /// by finding the best fitting capacity entity and using it to format 
        /// provided value afterwards.
        /// </remarks>
        /// <param name="value">
        /// The value to be converted.
        /// </param>
        /// <returns>
        /// The string representation of converted value.
        /// </returns>
        public static String Convert(Decimal value)
        {
            return CapacityConverter.Find(value).Format(value);
        }

        /// <summary>
        /// Converts provided value into its string representation.
        /// </summary>
        /// <remarks>
        /// This method converts provided value into its string representation 
        /// by finding the best fitting capacity entity and using it to format 
        /// provided value afterwards.
        /// </remarks>
        /// <param name="value">
        /// The value to be converted.
        /// </param>
        /// <param name="decimals">
        /// The number of decimal digits used for calculation.
        /// </param>
        /// <returns>
        /// The string representation of converted value.
        /// </returns>
        public static String Convert(Decimal value, Int32 decimals)
        {
            return CapacityConverter.Find(value).Format(value, decimals);
        }

        /// <summary>
        /// Converts provided value into its string representation.
        /// </summary>
        /// <remarks>
        /// This method converts provided value into its string representation 
        /// by finding the best fitting capacity entity and using it to format 
        /// provided value afterwards. The result includes two decimal digits 
        /// by default.
        /// </remarks>
        /// <param name="value">
        /// The value to be converted.
        /// </param>
        /// <param name="unit">
        /// The unit to get a capacity entity for.
        /// </param>
        /// <returns>
        /// The string representation of converted value.
        /// </returns>
        /// <seealso cref="Convert(Decimal, String, Int32)"/>
        public static String Convert(Decimal value, String unit)
        {
            return CapacityConverter.Convert(value, unit, 2);
        }

        /// <summary>
        /// Converts provided value into its string representation.
        /// </summary>
        /// <remarks>
        /// This method converts provided value into its string representation 
        /// by finding the best fitting capacity entity and using it to format 
        /// provided value afterwards. 
        /// </remarks>
        /// <param name="value">
        /// The value to be converted.
        /// </param>
        /// <param name="unit">
        /// The unit to get a capacity entity for.
        /// </param>
        /// <param name="decimals">
        /// The number of decimal digits used for calculation.
        /// </param>
        /// <returns>
        /// The string representation of converted value.
        /// </returns>
        /// <seealso cref="Convert(Decimal)"/>
        /// <seealso cref="Convert(Decimal, String)"/>
        /// <seealso cref="Convert(Decimal, String, Int32, CultureInfo)"/>
        public static String Convert(Decimal value, String unit, Int32 decimals)
        {
            return CapacityConverter.Convert(value, unit, decimals, null);
        }

        /// <summary>
        /// Converts provided value into its string representation.
        /// </summary>
        /// <remarks>
        /// This method converts provided value into its string representation 
        /// by finding the best fitting capacity entity and using it to format 
        /// provided value afterwards. 
        /// </remarks>
        /// <param name="value">
        /// The value to be converted.
        /// </param>
        /// <param name="unit">
        /// The unit to get a capacity entity for.
        /// </param>
        /// <param name="decimals">
        /// The number of decimal digits used for calculation.
        /// </param>
        /// <param name="culture">
        /// The culture to be used for conversion.
        /// </param>
        /// <returns>
        /// The string representation of converted value.
        /// </returns>
        /// <seealso cref="Convert(Decimal)"/>
        /// <seealso cref="Convert(Decimal, String)"/>
        /// <seealso cref="Convert(Decimal, String, Int32)"/>
        public static String Convert(Decimal value, String unit, Int32 decimals, CultureInfo culture)
        {
            if (!String.IsNullOrWhiteSpace(unit))
            {
                // Special case: Prefere unit one.
                if (unit.Equals("one", StringComparison.InvariantCultureIgnoreCase))
                {
                    CapacityEntity entity = CapacityConverter.Find(value);
                    return entity.Format(value, entity.Unit1, decimals, culture);
                }

                // Special case: Prefere unit two.
                if (unit.Equals("two", StringComparison.InvariantCultureIgnoreCase))
                {
                    CapacityEntity entity = CapacityConverter.Find(value);
                    return entity.Format(value, entity.Unit2, decimals, culture);
                }

                return CapacityConverter.Find(unit).Format(value, unit, decimals, culture);
            }

            if (decimals > 0)
            {
                return CapacityConverter.Find(value).Format(value, decimals, culture);
            }

            return CapacityConverter.Find(value).Format(value, culture);
        }

        #endregion
    }
}
