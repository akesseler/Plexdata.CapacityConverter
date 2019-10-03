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

using NUnit.Framework;
using Plexdata.Converters.Constants;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Examinee = Plexdata.Converters.CapacityConverter;

namespace Plexdata.CapacityConverter.Tests.Converters
{
    [SetCulture("en-US")]
    [SetUICulture("en-US")]
    [ExcludeFromCodeCoverage]
    public class CapacityConverterTests
    {
        [Test]
        public void CapacityConverter_CapacitiesEvaluation_ResultAsExpected()
        {
            Assert.That(Examinee.Capacities.Count(), Is.EqualTo(9));

            Assert.That(Examinee.Capacities.ElementAt(0).Value, Is.EqualTo(Capacities.YiB));
            Assert.That(Examinee.Capacities.ElementAt(0).Unit1, Is.EqualTo("YB"));
            Assert.That(Examinee.Capacities.ElementAt(0).Unit2, Is.EqualTo("YiB"));

            Assert.That(Examinee.Capacities.ElementAt(1).Value, Is.EqualTo(Capacities.ZiB));
            Assert.That(Examinee.Capacities.ElementAt(1).Unit1, Is.EqualTo("ZB"));
            Assert.That(Examinee.Capacities.ElementAt(1).Unit2, Is.EqualTo("ZiB"));

            Assert.That(Examinee.Capacities.ElementAt(2).Value, Is.EqualTo(Capacities.EiB));
            Assert.That(Examinee.Capacities.ElementAt(2).Unit1, Is.EqualTo("EB"));
            Assert.That(Examinee.Capacities.ElementAt(2).Unit2, Is.EqualTo("EiB"));

            Assert.That(Examinee.Capacities.ElementAt(3).Value, Is.EqualTo(Capacities.PiB));
            Assert.That(Examinee.Capacities.ElementAt(3).Unit1, Is.EqualTo("PB"));
            Assert.That(Examinee.Capacities.ElementAt(3).Unit2, Is.EqualTo("PiB"));

            Assert.That(Examinee.Capacities.ElementAt(4).Value, Is.EqualTo(Capacities.TiB));
            Assert.That(Examinee.Capacities.ElementAt(4).Unit1, Is.EqualTo("TB"));
            Assert.That(Examinee.Capacities.ElementAt(4).Unit2, Is.EqualTo("TiB"));

            Assert.That(Examinee.Capacities.ElementAt(5).Value, Is.EqualTo(Capacities.GiB));
            Assert.That(Examinee.Capacities.ElementAt(5).Unit1, Is.EqualTo("GB"));
            Assert.That(Examinee.Capacities.ElementAt(5).Unit2, Is.EqualTo("GiB"));

            Assert.That(Examinee.Capacities.ElementAt(6).Value, Is.EqualTo(Capacities.MiB));
            Assert.That(Examinee.Capacities.ElementAt(6).Unit1, Is.EqualTo("MB"));
            Assert.That(Examinee.Capacities.ElementAt(6).Unit2, Is.EqualTo("MiB"));

            Assert.That(Examinee.Capacities.ElementAt(7).Value, Is.EqualTo(Capacities.KiB));
            Assert.That(Examinee.Capacities.ElementAt(7).Unit1, Is.EqualTo("KB"));
            Assert.That(Examinee.Capacities.ElementAt(7).Unit2, Is.EqualTo("KiB"));

            Assert.That(Examinee.Capacities.ElementAt(8).Value, Is.EqualTo(Capacities.BiB));
            Assert.That(Examinee.Capacities.ElementAt(8).Unit1, Is.EqualTo("Bytes"));
            Assert.That(Examinee.Capacities.ElementAt(8).Unit2, Is.EqualTo("BiB"));
        }

        [Test]
        [TestCase("YB", "1208925819614629174706176")]
        [TestCase("yb", "1208925819614629174706176")]
        [TestCase("YiB", "1208925819614629174706176")]
        [TestCase("yib", "1208925819614629174706176")]
        [TestCase("ZB", "1180591620717411303424")]
        [TestCase("zb", "1180591620717411303424")]
        [TestCase("ZiB", "1180591620717411303424")]
        [TestCase("zib", "1180591620717411303424")]
        [TestCase("EB", "1152921504606846976")]
        [TestCase("eb", "1152921504606846976")]
        [TestCase("EiB", "1152921504606846976")]
        [TestCase("eib", "1152921504606846976")]
        [TestCase("PB", "1125899906842624")]
        [TestCase("pb", "1125899906842624")]
        [TestCase("PiB", "1125899906842624")]
        [TestCase("pib", "1125899906842624")]
        [TestCase("TB", "1099511627776")]
        [TestCase("tb", "1099511627776")]
        [TestCase("TiB", "1099511627776")]
        [TestCase("tib", "1099511627776")]
        [TestCase("GB", "1073741824")]
        [TestCase("gb", "1073741824")]
        [TestCase("GiB", "1073741824")]
        [TestCase("gib", "1073741824")]
        [TestCase("MB", "1048576")]
        [TestCase("mb", "1048576")]
        [TestCase("MiB", "1048576")]
        [TestCase("mib", "1048576")]
        [TestCase("KB", "1024")]
        [TestCase("kb", "1024")]
        [TestCase("KiB", "1024")]
        [TestCase("kib", "1024")]
        [TestCase("Bytes", "1")]
        [TestCase("bytes", "1")]
        [TestCase("BiB", "1")]
        [TestCase("bib", "1")]
        [TestCase("Byte", "1")]
        [TestCase("byte", "1")]
        [TestCase("Other", "1")]
        [TestCase("other", "1")]
        public void Find_ByUnitValidation_UnitFitsValue(String unit, String expected)
        {
            Assert.That(Examinee.Find(unit).Value, Is.EqualTo(Convert.ToDecimal(expected)));
        }

        [Test]
        [TestCase("10000000000000000000000000", "1208925819614629174706176")]
        [TestCase("10000000000000000000000", "1180591620717411303424")]
        [TestCase("10000000000000000000", "1152921504606846976")]
        [TestCase("10000000000000000", "1125899906842624")]
        [TestCase("10000000000000", "1099511627776")]
        [TestCase("10000000000", "1073741824")]
        [TestCase("10000000", "1048576")]
        [TestCase("10000", "1024")]
        [TestCase("100", "1")]
        [TestCase("0", "1")]
        public void Find_BydValueValidation_ValueFitsValue(String value, String expected)
        {
            Assert.That(Examinee.Find(Convert.ToDecimal(value)).Value, Is.EqualTo(Convert.ToDecimal(expected)));
        }

        [Test]
        [TestCase("10000000000000000000000000", "8\u00A0YB")]
        [TestCase("10000000000000000000000", "8\u00A0ZB")]
        [TestCase("10000000000000000000", "9\u00A0EB")]
        [TestCase("10000000000000000", "9\u00A0PB")]
        [TestCase("10000000000000", "9\u00A0TB")]
        [TestCase("10000000000", "9\u00A0GB")]
        [TestCase("10000000", "10\u00A0MB")]
        [TestCase("10000", "10\u00A0KB")]
        [TestCase("100", "100\u00A0Bytes")]
        [TestCase("0", "0\u00A0Bytes")]
        public void Convert_ByValue_ResultAsExpected(String value, String expected)
        {
            Assert.That(Examinee.Convert(Convert.ToDecimal(value)), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("10000000000000000000000000", "YB", "8.27\u00A0YB")]
        [TestCase("10000000000000000000000", "ZB", "8.47\u00A0ZB")]
        [TestCase("10000000000000000000", "EB", "8.67\u00A0EB")]
        [TestCase("10000000000000000", "PB", "8.88\u00A0PB")]
        [TestCase("10000000000000", "TB", "9.09\u00A0TB")]
        [TestCase("10000000000", "GB", "9.31\u00A0GB")]
        [TestCase("10000000", "MB", "9.54\u00A0MB")]
        [TestCase("10000", "KB", "9.77\u00A0KB")]
        [TestCase("100", "Bytes", "100\u00A0Bytes")]
        [TestCase("100", "Other", "100\u00A0Other")]
        [TestCase("123456789", "Other", "123,456,789\u00A0Other")]
        [TestCase("0", "Bytes", "0\u00A0Bytes")]
        [TestCase("0", "Other", "0\u00A0Other")]
        public void Convert_ByValueUnit_ResultAsExpected(String value, String unit, String expected)
        {
            Assert.That(Examinee.Convert(Convert.ToDecimal(value), unit), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("10000000000000000000000000", "YB", 3, "8.272\u00A0YB")]
        [TestCase("10000000000000000000000", "ZB", 3, "8.470\u00A0ZB")]
        [TestCase("10000000000000000000", "EB", 3, "8.674\u00A0EB")]
        [TestCase("10000000000000000", "PB", 3, "8.882\u00A0PB")]
        [TestCase("10000000000000", "TB", 3, "9.095\u00A0TB")]
        [TestCase("10000000000", "GB", 3, "9.313\u00A0GB")]
        [TestCase("10000000", "MB", 3, "9.537\u00A0MB")]
        [TestCase("10000", "KB", 3, "9.766\u00A0KB")]
        [TestCase("100", "Bytes", 3, "100\u00A0Bytes")]
        [TestCase("100", "Other", 3, "100\u00A0Other")]
        [TestCase("123456789", "Other", 3, "123,456,789\u00A0Other")]
        [TestCase("0", "Bytes", 3, "0\u00A0Bytes")]
        [TestCase("0", "Other", 3, "0\u00A0Other")]
        public void Convert_ByValueUnitDecimals_ResultAsExpected(String value, String unit, Int32 decimals, String expected)
        {
            Assert.That(Examinee.Convert(Convert.ToDecimal(value), unit, decimals), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("10000000000000000000000000", 3, "8.272\u00A0YB")]
        [TestCase("10000000000000000000000", 3, "8.470\u00A0ZB")]
        [TestCase("10000000000000000000", 3, "8.674\u00A0EB")]
        [TestCase("10000000000000000", 3, "8.882\u00A0PB")]
        [TestCase("10000000000000", 3, "9.095\u00A0TB")]
        [TestCase("10000000000", 3, "9.313\u00A0GB")]
        [TestCase("10000000", 3, "9.537\u00A0MB")]
        [TestCase("10000", 3, "9.766\u00A0KB")]
        [TestCase("100", 3, "100\u00A0Bytes")]
        [TestCase("0", 3, "0\u00A0Bytes")]
        public void Convert_NoUnitWithDecimals_ResultAsExpected(String value, Int32 decimals, String expected)
        {
            Assert.That(Examinee.Convert(Convert.ToDecimal(value), null, decimals), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("10000000000000000000000000", "8\u00A0YB")]
        [TestCase("10000000000000000000000", "8\u00A0ZB")]
        [TestCase("10000000000000000000", "9\u00A0EB")]
        [TestCase("10000000000000000", "9\u00A0PB")]
        [TestCase("10000000000000", "9\u00A0TB")]
        [TestCase("10000000000", "9\u00A0GB")]
        [TestCase("10000000", "10\u00A0MB")]
        [TestCase("10000", "10\u00A0KB")]
        [TestCase("100", "100\u00A0Bytes")]
        [TestCase("0", "0\u00A0Bytes")]
        public void Convert_NoUnitNoDecimals_ResultAsExpected(String value, String expected)
        {
            Assert.That(Examinee.Convert(Convert.ToDecimal(value), null, 0), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("10000000000000000000000000", "one", "8\u00A0YB")]
        [TestCase("10000000000000000000000000", "One", "8\u00A0YB")]
        [TestCase("10000000000000000000000000", "ONE", "8\u00A0YB")]
        [TestCase("10000000000000000000000", "one", "8\u00A0ZB")]
        [TestCase("10000000000000000000000", "One", "8\u00A0ZB")]
        [TestCase("10000000000000000000000", "ONE", "8\u00A0ZB")]
        [TestCase("10000000000000000000", "one", "9\u00A0EB")]
        [TestCase("10000000000000000000", "One", "9\u00A0EB")]
        [TestCase("10000000000000000000", "ONE", "9\u00A0EB")]
        [TestCase("10000000000000000", "one", "9\u00A0PB")]
        [TestCase("10000000000000000", "One", "9\u00A0PB")]
        [TestCase("10000000000000000", "ONE", "9\u00A0PB")]
        [TestCase("10000000000000", "one", "9\u00A0TB")]
        [TestCase("10000000000000", "One", "9\u00A0TB")]
        [TestCase("10000000000000", "ONE", "9\u00A0TB")]
        [TestCase("10000000000", "one", "9\u00A0GB")]
        [TestCase("10000000000", "One", "9\u00A0GB")]
        [TestCase("10000000000", "ONE", "9\u00A0GB")]
        [TestCase("10000000", "one", "10\u00A0MB")]
        [TestCase("10000000", "One", "10\u00A0MB")]
        [TestCase("10000000", "ONE", "10\u00A0MB")]
        [TestCase("10000", "one", "10\u00A0KB")]
        [TestCase("10000", "One", "10\u00A0KB")]
        [TestCase("10000", "ONE", "10\u00A0KB")]
        [TestCase("100", "one", "100\u00A0Bytes")]
        [TestCase("100", "One", "100\u00A0Bytes")]
        [TestCase("100", "ONE", "100\u00A0Bytes")]
        [TestCase("0", "one", "0\u00A0Bytes")]
        [TestCase("0", "One", "0\u00A0Bytes")]
        [TestCase("0", "ONE", "0\u00A0Bytes")]
        public void Convert_UnitOneNoDecimals_ResultAsExpected(String value, String unit, String expected)
        {
            Assert.That(Examinee.Convert(Convert.ToDecimal(value), unit, 0), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("10000000000000000000000000", "two", "8\u00A0YiB")]
        [TestCase("10000000000000000000000000", "Two", "8\u00A0YiB")]
        [TestCase("10000000000000000000000000", "TWO", "8\u00A0YiB")]
        [TestCase("10000000000000000000000", "two", "8\u00A0ZiB")]
        [TestCase("10000000000000000000000", "Two", "8\u00A0ZiB")]
        [TestCase("10000000000000000000000", "TWO", "8\u00A0ZiB")]
        [TestCase("10000000000000000000", "two", "9\u00A0EiB")]
        [TestCase("10000000000000000000", "Two", "9\u00A0EiB")]
        [TestCase("10000000000000000000", "TWO", "9\u00A0EiB")]
        [TestCase("10000000000000000", "two", "9\u00A0PiB")]
        [TestCase("10000000000000000", "Two", "9\u00A0PiB")]
        [TestCase("10000000000000000", "TWO", "9\u00A0PiB")]
        [TestCase("10000000000000", "two", "9\u00A0TiB")]
        [TestCase("10000000000000", "Two", "9\u00A0TiB")]
        [TestCase("10000000000000", "TWO", "9\u00A0TiB")]
        [TestCase("10000000000", "two", "9\u00A0GiB")]
        [TestCase("10000000000", "Two", "9\u00A0GiB")]
        [TestCase("10000000000", "TWO", "9\u00A0GiB")]
        [TestCase("10000000", "two", "10\u00A0MiB")]
        [TestCase("10000000", "Two", "10\u00A0MiB")]
        [TestCase("10000000", "TWO", "10\u00A0MiB")]
        [TestCase("10000", "two", "10\u00A0KiB")]
        [TestCase("10000", "Two", "10\u00A0KiB")]
        [TestCase("10000", "TWO", "10\u00A0KiB")]
        [TestCase("100", "two", "100\u00A0BiB")]
        [TestCase("100", "Two", "100\u00A0BiB")]
        [TestCase("100", "TWO", "100\u00A0BiB")]
        [TestCase("0", "two", "0\u00A0BiB")]
        [TestCase("0", "Two", "0\u00A0BiB")]
        [TestCase("0", "TWO", "0\u00A0BiB")]
        public void Convert_UnitTwoNoDecimals_ResultAsExpected(String value, String unit, String expected)
        {
            Assert.That(Examinee.Convert(Convert.ToDecimal(value), unit, 0), Is.EqualTo(expected));
        }
    }
}
