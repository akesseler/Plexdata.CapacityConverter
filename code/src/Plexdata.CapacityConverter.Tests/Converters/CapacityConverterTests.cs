﻿/*
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
        [TestCase("10000000000000000000000000", "8 YB")]
        [TestCase("10000000000000000000000", "8 ZB")]
        [TestCase("10000000000000000000", "9 EB")]
        [TestCase("10000000000000000", "9 PB")]
        [TestCase("10000000000000", "9 TB")]
        [TestCase("10000000000", "9 GB")]
        [TestCase("10000000", "10 MB")]
        [TestCase("10000", "10 KB")]
        [TestCase("100", "100 Bytes")]
        [TestCase("0", "0 Bytes")]
        public void Convert_ByValue_ResultAsExpected(String value, String expected)
        {
            Assert.That(Examinee.Convert(Convert.ToDecimal(value)), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("10000000000000000000000000", "YB", "8.27 YB")]
        [TestCase("10000000000000000000000", "ZB", "8.47 ZB")]
        [TestCase("10000000000000000000", "EB", "8.67 EB")]
        [TestCase("10000000000000000", "PB", "8.88 PB")]
        [TestCase("10000000000000", "TB", "9.09 TB")]
        [TestCase("10000000000", "GB", "9.31 GB")]
        [TestCase("10000000", "MB", "9.54 MB")]
        [TestCase("10000", "KB", "9.77 KB")]
        [TestCase("100", "Bytes", "100 Bytes")]
        [TestCase("100", "Other", "100 Other")]
        [TestCase("123456789", "Other", "123,456,789 Other")]
        [TestCase("0", "Bytes", "0 Bytes")]
        [TestCase("0", "Other", "0 Other")]
        public void Convert_ByValueUnit_ResultAsExpected(String value, String unit, String expected)
        {
            Assert.That(Examinee.Convert(Convert.ToDecimal(value), unit), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("10000000000000000000000000", "YB", 3, "8.272 YB")]
        [TestCase("10000000000000000000000", "ZB", 3, "8.470 ZB")]
        [TestCase("10000000000000000000", "EB", 3, "8.674 EB")]
        [TestCase("10000000000000000", "PB", 3, "8.882 PB")]
        [TestCase("10000000000000", "TB", 3, "9.095 TB")]
        [TestCase("10000000000", "GB", 3, "9.313 GB")]
        [TestCase("10000000", "MB", 3, "9.537 MB")]
        [TestCase("10000", "KB", 3, "9.766 KB")]
        [TestCase("100", "Bytes", 3, "100 Bytes")]
        [TestCase("100", "Other", 3, "100 Other")]
        [TestCase("123456789", "Other", 3, "123,456,789 Other")]
        [TestCase("0", "Bytes", 3, "0 Bytes")]
        [TestCase("0", "Other", 3, "0 Other")]
        public void Convert_ByValueUnitDecimals_ResultAsExpected(String value, String unit, Int32 decimals, String expected)
        {
            Assert.That(Examinee.Convert(Convert.ToDecimal(value), unit, decimals), Is.EqualTo(expected));
        }
    }
}