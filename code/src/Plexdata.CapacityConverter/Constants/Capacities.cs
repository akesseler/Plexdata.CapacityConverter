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

namespace Plexdata.Converters.Constants
{
    /// <summary>
    /// Defines all capacity size values.
    /// </summary>
    /// <remarks>
    /// This class simply defines all supported capacity size values.
    /// Be aware, any value calculation is based on these values.
    /// </remarks>
    public static class Capacities
    {
        /// <summary>
        /// The static class constructor.
        /// </summary>
        /// <remarks>
        /// This constructor does actually nothing.
        /// </remarks>
        static Capacities() { }

        /// <summary>
        /// BiB = Bibibyte = 2^0 Bytes.
        /// </summary>
        /// <remarks>
        /// The divisor for Bibibyte size calculations.
        /// </remarks>
        public const Decimal BiB = 1m;

        /// <summary>
        /// KiB = Kibibyte = 2^10 Bytes.
        /// </summary>
        /// <remarks>
        /// The divisor for Kibibyte size calculations.
        /// </remarks>
        public const Decimal KiB = 1024m;

        /// <summary>
        /// MiB = Mebibyte = 2^20 Bytes.
        /// </summary>
        /// <remarks>
        /// The divisor for Mebibyte size calculations.
        /// </remarks>
        public const Decimal MiB = 1048576m;

        /// <summary>
        /// GiB = Gibibyte = 2^30 Bytes.
        /// </summary>
        /// <remarks>
        /// The divisor for Gibibyte size calculations.
        /// </remarks>
        public const Decimal GiB = 1073741824m;

        /// <summary>
        /// TiB = Tebibyte = 2^40 Bytes.
        /// </summary>
        /// <remarks>
        /// The divisor for Tebibyte size calculations.
        /// </remarks>
        public const Decimal TiB = 1099511627776m;

        /// <summary>
        /// PiB = Pebibyte = 2^50 Bytes. 
        /// </summary>
        /// <remarks>
        /// The divisor for Pebibyte size calculations.
        /// </remarks>
        public const Decimal PiB = 1125899906842624m;

        /// <summary>
        /// EiB = Exbibyte = 2^60 Bytes. 
        /// </summary>
        /// <remarks>
        /// The divisor for Exbibyte size calculations.
        /// </remarks>
        public const Decimal EiB = 1152921504606846976m;

        /// <summary>
        /// ZiB = Zebibyte = 2^70 Bytes. 
        /// </summary>
        /// <remarks>
        /// The divisor for Zebibyte size calculations.
        /// </remarks>
        public const Decimal ZiB = 1180591620717411303424m;

        /// <summary>
        /// YiB = Yobibyte = 2^80 Bytes. 
        /// </summary>
        /// <remarks>
        /// The divisor for Yobibyte size calculations.
        /// </remarks>
        public const Decimal YiB = 1208925819614629174706176m;
    }
}
