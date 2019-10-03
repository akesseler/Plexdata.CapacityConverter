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

namespace Plexdata.Converters.Constants
{
    /// <summary>
    /// Defines all default values.
    /// </summary>
    /// <remarks>
    /// This class simply defines all necessary default values.
    /// </remarks>
    public static class Defaults
    {
        /// <summary>
        /// The static class constructor.
        /// </summary>
        /// <remarks>
        /// This constructor does actually nothing.
        /// </remarks>
        static Defaults() { }

        /// <summary>
        /// The default number of decimal digits.
        /// </summary>
        /// <remarks>
        /// This field provides the default number of decimal digits, which is zero.
        /// </remarks>
        public static readonly Int32 Decimals = 0;

        /// <summary>
        /// The default calculation behaviour.
        /// </summary>
        /// <remarks>
        /// This field provides the default calculation behaviour, which is true.
        /// </remarks>
        public static readonly Boolean Calculate = true;

        /// <summary>
        /// The default intercept behaviour.
        /// </summary>
        /// <remarks>
        /// This field provides the default intercept behaviour, which is false.
        /// Intercept means that all decimal digits are cut off if they consist 
        /// only of zeros.
        /// </remarks>
        public static readonly Boolean Intercept = false;

        /// <summary>
        /// The default culture.
        /// </summary>
        /// <remarks>
        /// This field provides the default culture, which is Current UI Culture.
        /// </remarks>
        public static readonly CultureInfo Culture = CultureInfo.CurrentUICulture;
    }
}
