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
using Plexdata.Formatters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Plexdata.CapacityConverter.Tests.Formatters
{
    [SetCulture("en-US")]
    [SetUICulture("en-US")]
    [ExcludeFromCodeCoverage]
    public class CapacityFormatterTests
    {
        [Test]
        public void CapacityFormatter_DefaultConstruction_ThrowsNothing()
        {
            Assert.That(() => new CapacityFormatter(), Throws.Nothing);
        }

        [Test]
        public void CapacityFormatter_CultureConstruction_ThrowsNothing()
        {
            Assert.That(() => new CapacityFormatter(null), Throws.Nothing);
        }

        [Test]
        public void CapacityFormatter_SupportedTypes_SupportedTypesAsExpected()
        {
            List<Type> actual = this.GetInstance().Supported.ToList();

            Assert.That(actual.Count, Is.EqualTo(11));
            Assert.That(actual[0], Is.EqualTo(typeof(SByte)));
            Assert.That(actual[1], Is.EqualTo(typeof(Byte)));
            Assert.That(actual[2], Is.EqualTo(typeof(Int16)));
            Assert.That(actual[3], Is.EqualTo(typeof(UInt16)));
            Assert.That(actual[4], Is.EqualTo(typeof(Int32)));
            Assert.That(actual[5], Is.EqualTo(typeof(UInt32)));
            Assert.That(actual[6], Is.EqualTo(typeof(Int64)));
            Assert.That(actual[7], Is.EqualTo(typeof(UInt64)));
            Assert.That(actual[8], Is.EqualTo(typeof(Single)));
            Assert.That(actual[9], Is.EqualTo(typeof(Double)));
            Assert.That(actual[10], Is.EqualTo(typeof(Decimal)));
        }

        [Test]
        public void CapacityFormatter_DefaultCulture_DefaultCultureIsNull()
        {
            Assert.That(this.GetInstance().Culture, Is.Null);
        }

        [Test]
        [TestCase("en-US")]
        [TestCase("de-DE")]
        [TestCase("fr-FR")]
        public void CapacityFormatter_OtherCulture_UsedCultureAsExpected(String culture)
        {
            Assert.That(this.GetInstance(new CultureInfo(culture)).Culture.Name, Is.EqualTo(culture));
        }

        [Test]
        [TestCase(typeof(IFormatProvider))]
        [TestCase(typeof(CultureInfo))]
        [TestCase(typeof(NumberFormatInfo))]
        [TestCase(typeof(DateTimeFormatInfo))]
        public void GetFormat_InvalidTypes_ResultIsNull(Type candidate)
        {
            Assert.That(this.GetInstance().GetFormat(candidate), Is.Null);
        }

        [Test]
        [TestCase(typeof(ICustomFormatter), typeof(CapacityFormatter))]
        public void GetFormat_ValidTypes_ResultIsExpectedType(Type candidate, Type expected)
        {
            Assert.That(this.GetInstance().GetFormat(candidate), Is.InstanceOf(expected));
        }

        [Test]
        public void Format_NullValue_ResultIsEmpty()
        {
            CapacityFormatter instance = this.GetInstance();
            Assert.That(instance.Format("format", null, instance), Is.Empty);
        }

        [Test]
        public void Format_ProviderIsNullValueIsFormattable_ResultAsExpected()
        {
            DateTime value = new DateTime(2019, 5, 23, 17, 5, 23);
            Assert.That(this.GetInstance().Format("yyyy-MM-dd HH:mm:ss", value, null), Is.EqualTo("2019-05-23 17:05:23"));
        }

        [Test]
        public void Format_ProviderIsNullValueIsNotFormattable_ResultAsExpected()
        {
            String value = "Hello, World!";
            Assert.That(this.GetInstance().Format(null, value, null), Is.EqualTo("Hello, World!"));
        }

        [Test]
        public void Format_ProviderIsNumberFormatterValueIsFormattable_ResultAsExpected()
        {
            Int32 value = 1234;
            Assert.That(this.GetInstance().Format("N", value, NumberFormatInfo.CurrentInfo), Is.EqualTo("1,234.00"));
        }

        [Test]
        public void Format_ProviderIsCapacityFormatterValueIsUnsupported_ResultAsExpected()
        {
            CapacityFormatter instance = this.GetInstance();
            DateTime value = new DateTime(2019, 5, 23, 17, 5, 23);
            Assert.That(instance.Format("yyyy-MM-dd HH:mm:ss", value, instance), Is.EqualTo("2019-05-23 17:05:23"));
        }

        [Test]
        [TestCase("Unit", 1234, "1,234\u00A0Unit")]
        [TestCase("Unit5", 1234, "1,234\u00A0Unit")]
        public void Format_ProviderIsCapacityFormatterValueIsSupported_ResultAsExpected(String format, Int32 value, String expected)
        {
            CapacityFormatter instance = this.GetInstance();
            Assert.That(instance.Format(format, value, instance), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("{0}", 0, "0\u00A0Bytes")]
        [TestCase("{0}", 123, "123\u00A0Bytes")]
        [TestCase("{0}", 1234, "1\u00A0KB")]
        [TestCase("{0}", 12345, "12\u00A0KB")]
        [TestCase("{0}", 123456, "121\u00A0KB")]
        [TestCase("{0}", 1234567, "1\u00A0MB")]
        [TestCase("{0}", 12345678, "12\u00A0MB")]
        [TestCase("{0}", 123456789, "118\u00A0MB")]
        [TestCase("{0}", 1234567890, "1\u00A0GB")]
        [TestCase("{0}", 12345678901, "11\u00A0GB")]
        [TestCase("{0}", 123456789012, "115\u00A0GB")]
        [TestCase("{0}", 1234567890123, "1\u00A0TB")]
        [TestCase("{0}", 12345678901234, "11\u00A0TB")]
        [TestCase("{0}", 123456789012345, "112\u00A0TB")]
        [TestCase("{0}", 1234567890123456, "1\u00A0PB")]
        [TestCase("{0}", 12345678901234567, "11\u00A0PB")]
        [TestCase("{0}", 123456789012345678, "110\u00A0PB")]
        public void Format_DefaultUsageIntegrationTestNoUnitNoDigits_ResultAsExpected(String format, Object value, String expected)
        {
            String actual = String.Format(this.GetInstance(), format, value);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("{0:3}", 0, "0\u00A0Bytes")]
        [TestCase("{0:3}", 123, "123\u00A0Bytes")]
        [TestCase("{0:3}", 1234, "1.205\u00A0KB")]
        [TestCase("{0:3}", 12345, "12.056\u00A0KB")]
        [TestCase("{0:3}", 123456, "120.562\u00A0KB")]
        [TestCase("{0:3}", 1234567, "1.177\u00A0MB")]
        [TestCase("{0:3}", 12345678, "11.774\u00A0MB")]
        [TestCase("{0:3}", 123456789, "117.738\u00A0MB")]
        [TestCase("{0:3}", 1234567890, "1.150\u00A0GB")]
        [TestCase("{0:3}", 12345678901, "11.498\u00A0GB")]
        [TestCase("{0:3}", 123456789012, "114.978\u00A0GB")]
        [TestCase("{0:3}", 1234567890123, "1.123\u00A0TB")]
        [TestCase("{0:3}", 12345678901234, "11.228\u00A0TB")]
        [TestCase("{0:3}", 123456789012345, "112.283\u00A0TB")]
        [TestCase("{0:3}", 1234567890123456, "1.097\u00A0PB")]
        [TestCase("{0:3}", 12345678901234567, "10.965\u00A0PB")]
        [TestCase("{0:3}", 123456789012345678, "109.652\u00A0PB")]
        public void Format_DefaultUsageIntegrationTestNoUnitThreeDigits_ResultAsExpected(String format, Object value, String expected)
        {
            String actual = String.Format(this.GetInstance(), format, value);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("{0:one}", 0, "0\u00A0Bytes")]
        [TestCase("{0:One}", 0, "0\u00A0Bytes")]
        [TestCase("{0:ONE}", 0, "0\u00A0Bytes")]
        [TestCase("{0:one}", 123, "123\u00A0Bytes")]
        [TestCase("{0:One}", 123, "123\u00A0Bytes")]
        [TestCase("{0:ONE}", 123, "123\u00A0Bytes")]
        [TestCase("{0:one}", 1234, "1\u00A0KB")]
        [TestCase("{0:One}", 1234, "1\u00A0KB")]
        [TestCase("{0:ONE}", 1234, "1\u00A0KB")]
        [TestCase("{0:one}", 12345, "12\u00A0KB")]
        [TestCase("{0:One}", 12345, "12\u00A0KB")]
        [TestCase("{0:ONE}", 12345, "12\u00A0KB")]
        [TestCase("{0:one}", 123456, "121\u00A0KB")]
        [TestCase("{0:One}", 123456, "121\u00A0KB")]
        [TestCase("{0:ONE}", 123456, "121\u00A0KB")]
        [TestCase("{0:one}", 1234567, "1\u00A0MB")]
        [TestCase("{0:One}", 1234567, "1\u00A0MB")]
        [TestCase("{0:ONE}", 1234567, "1\u00A0MB")]
        [TestCase("{0:one}", 12345678, "12\u00A0MB")]
        [TestCase("{0:One}", 12345678, "12\u00A0MB")]
        [TestCase("{0:ONE}", 12345678, "12\u00A0MB")]
        [TestCase("{0:one}", 123456789, "118\u00A0MB")]
        [TestCase("{0:One}", 123456789, "118\u00A0MB")]
        [TestCase("{0:ONE}", 123456789, "118\u00A0MB")]
        [TestCase("{0:one}", 1234567890, "1\u00A0GB")]
        [TestCase("{0:One}", 1234567890, "1\u00A0GB")]
        [TestCase("{0:ONE}", 1234567890, "1\u00A0GB")]
        [TestCase("{0:one}", 12345678901, "11\u00A0GB")]
        [TestCase("{0:One}", 12345678901, "11\u00A0GB")]
        [TestCase("{0:ONE}", 12345678901, "11\u00A0GB")]
        [TestCase("{0:one}", 123456789012, "115\u00A0GB")]
        [TestCase("{0:One}", 123456789012, "115\u00A0GB")]
        [TestCase("{0:ONE}", 123456789012, "115\u00A0GB")]
        [TestCase("{0:one}", 1234567890123, "1\u00A0TB")]
        [TestCase("{0:One}", 1234567890123, "1\u00A0TB")]
        [TestCase("{0:ONE}", 1234567890123, "1\u00A0TB")]
        [TestCase("{0:one}", 12345678901234, "11\u00A0TB")]
        [TestCase("{0:One}", 12345678901234, "11\u00A0TB")]
        [TestCase("{0:ONE}", 12345678901234, "11\u00A0TB")]
        [TestCase("{0:one}", 123456789012345, "112\u00A0TB")]
        [TestCase("{0:One}", 123456789012345, "112\u00A0TB")]
        [TestCase("{0:ONE}", 123456789012345, "112\u00A0TB")]
        [TestCase("{0:one}", 1234567890123456, "1\u00A0PB")]
        [TestCase("{0:One}", 1234567890123456, "1\u00A0PB")]
        [TestCase("{0:ONE}", 1234567890123456, "1\u00A0PB")]
        [TestCase("{0:one}", 12345678901234567, "11\u00A0PB")]
        [TestCase("{0:One}", 12345678901234567, "11\u00A0PB")]
        [TestCase("{0:ONE}", 12345678901234567, "11\u00A0PB")]
        [TestCase("{0:one}", 123456789012345678, "110\u00A0PB")]
        [TestCase("{0:One}", 123456789012345678, "110\u00A0PB")]
        [TestCase("{0:ONE}", 123456789012345678, "110\u00A0PB")]
        public void Format_DefaultUsageIntegrationTestUnitOneNoDigits_ResultAsExpected(String format, Object value, String expected)
        {
            String actual = String.Format(this.GetInstance(), format, value);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("{0:one3}", 0, "0\u00A0Bytes")]
        [TestCase("{0:One3}", 0, "0\u00A0Bytes")]
        [TestCase("{0:ONE3}", 0, "0\u00A0Bytes")]
        [TestCase("{0:one3}", 123, "123\u00A0Bytes")]
        [TestCase("{0:One3}", 123, "123\u00A0Bytes")]
        [TestCase("{0:ONE3}", 123, "123\u00A0Bytes")]
        [TestCase("{0:one3}", 1234, "1.205\u00A0KB")]
        [TestCase("{0:One3}", 1234, "1.205\u00A0KB")]
        [TestCase("{0:ONE3}", 1234, "1.205\u00A0KB")]
        [TestCase("{0:one3}", 12345, "12.056\u00A0KB")]
        [TestCase("{0:One3}", 12345, "12.056\u00A0KB")]
        [TestCase("{0:ONE3}", 12345, "12.056\u00A0KB")]
        [TestCase("{0:one3}", 123456, "120.562\u00A0KB")]
        [TestCase("{0:One3}", 123456, "120.562\u00A0KB")]
        [TestCase("{0:ONE3}", 123456, "120.562\u00A0KB")]
        [TestCase("{0:one3}", 1234567, "1.177\u00A0MB")]
        [TestCase("{0:One3}", 1234567, "1.177\u00A0MB")]
        [TestCase("{0:ONE3}", 1234567, "1.177\u00A0MB")]
        [TestCase("{0:one3}", 12345678, "11.774\u00A0MB")]
        [TestCase("{0:One3}", 12345678, "11.774\u00A0MB")]
        [TestCase("{0:ONE3}", 12345678, "11.774\u00A0MB")]
        [TestCase("{0:one3}", 123456789, "117.738\u00A0MB")]
        [TestCase("{0:One3}", 123456789, "117.738\u00A0MB")]
        [TestCase("{0:ONE3}", 123456789, "117.738\u00A0MB")]
        [TestCase("{0:one3}", 1234567890, "1.150\u00A0GB")]
        [TestCase("{0:One3}", 1234567890, "1.150\u00A0GB")]
        [TestCase("{0:ONE3}", 1234567890, "1.150\u00A0GB")]
        [TestCase("{0:one3}", 12345678901, "11.498\u00A0GB")]
        [TestCase("{0:One3}", 12345678901, "11.498\u00A0GB")]
        [TestCase("{0:ONE3}", 12345678901, "11.498\u00A0GB")]
        [TestCase("{0:one3}", 123456789012, "114.978\u00A0GB")]
        [TestCase("{0:One3}", 123456789012, "114.978\u00A0GB")]
        [TestCase("{0:ONE3}", 123456789012, "114.978\u00A0GB")]
        [TestCase("{0:one3}", 1234567890123, "1.123\u00A0TB")]
        [TestCase("{0:One3}", 1234567890123, "1.123\u00A0TB")]
        [TestCase("{0:ONE3}", 1234567890123, "1.123\u00A0TB")]
        [TestCase("{0:one3}", 12345678901234, "11.228\u00A0TB")]
        [TestCase("{0:One3}", 12345678901234, "11.228\u00A0TB")]
        [TestCase("{0:ONE3}", 12345678901234, "11.228\u00A0TB")]
        [TestCase("{0:one3}", 123456789012345, "112.283\u00A0TB")]
        [TestCase("{0:One3}", 123456789012345, "112.283\u00A0TB")]
        [TestCase("{0:ONE3}", 123456789012345, "112.283\u00A0TB")]
        [TestCase("{0:one3}", 1234567890123456, "1.097\u00A0PB")]
        [TestCase("{0:One3}", 1234567890123456, "1.097\u00A0PB")]
        [TestCase("{0:ONE3}", 1234567890123456, "1.097\u00A0PB")]
        [TestCase("{0:one3}", 12345678901234567, "10.965\u00A0PB")]
        [TestCase("{0:One3}", 12345678901234567, "10.965\u00A0PB")]
        [TestCase("{0:ONE3}", 12345678901234567, "10.965\u00A0PB")]
        [TestCase("{0:one3}", 123456789012345678, "109.652\u00A0PB")]
        [TestCase("{0:One3}", 123456789012345678, "109.652\u00A0PB")]
        [TestCase("{0:ONE3}", 123456789012345678, "109.652\u00A0PB")]
        public void Format_DefaultUsageIntegrationTestUnitOneThreeDigits_ResultAsExpected(String format, Object value, String expected)
        {
            String actual = String.Format(this.GetInstance(), format, value);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("{0:two}", 0, "0\u00A0BiB")]
        [TestCase("{0:Two}", 0, "0\u00A0BiB")]
        [TestCase("{0:TWO}", 0, "0\u00A0BiB")]
        [TestCase("{0:two}", 123, "123\u00A0BiB")]
        [TestCase("{0:Two}", 123, "123\u00A0BiB")]
        [TestCase("{0:TWO}", 123, "123\u00A0BiB")]
        [TestCase("{0:two}", 1234, "1\u00A0KiB")]
        [TestCase("{0:Two}", 1234, "1\u00A0KiB")]
        [TestCase("{0:TWO}", 1234, "1\u00A0KiB")]
        [TestCase("{0:two}", 12345, "12\u00A0KiB")]
        [TestCase("{0:Two}", 12345, "12\u00A0KiB")]
        [TestCase("{0:TWO}", 12345, "12\u00A0KiB")]
        [TestCase("{0:two}", 123456, "121\u00A0KiB")]
        [TestCase("{0:Two}", 123456, "121\u00A0KiB")]
        [TestCase("{0:TWO}", 123456, "121\u00A0KiB")]
        [TestCase("{0:two}", 1234567, "1\u00A0MiB")]
        [TestCase("{0:Two}", 1234567, "1\u00A0MiB")]
        [TestCase("{0:TWO}", 1234567, "1\u00A0MiB")]
        [TestCase("{0:two}", 12345678, "12\u00A0MiB")]
        [TestCase("{0:Two}", 12345678, "12\u00A0MiB")]
        [TestCase("{0:TWO}", 12345678, "12\u00A0MiB")]
        [TestCase("{0:two}", 123456789, "118\u00A0MiB")]
        [TestCase("{0:Two}", 123456789, "118\u00A0MiB")]
        [TestCase("{0:TWO}", 123456789, "118\u00A0MiB")]
        [TestCase("{0:two}", 1234567890, "1\u00A0GiB")]
        [TestCase("{0:Two}", 1234567890, "1\u00A0GiB")]
        [TestCase("{0:TWO}", 1234567890, "1\u00A0GiB")]
        [TestCase("{0:two}", 12345678901, "11\u00A0GiB")]
        [TestCase("{0:Two}", 12345678901, "11\u00A0GiB")]
        [TestCase("{0:TWO}", 12345678901, "11\u00A0GiB")]
        [TestCase("{0:two}", 123456789012, "115\u00A0GiB")]
        [TestCase("{0:Two}", 123456789012, "115\u00A0GiB")]
        [TestCase("{0:TWO}", 123456789012, "115\u00A0GiB")]
        [TestCase("{0:two}", 1234567890123, "1\u00A0TiB")]
        [TestCase("{0:Two}", 1234567890123, "1\u00A0TiB")]
        [TestCase("{0:TWO}", 1234567890123, "1\u00A0TiB")]
        [TestCase("{0:two}", 12345678901234, "11\u00A0TiB")]
        [TestCase("{0:Two}", 12345678901234, "11\u00A0TiB")]
        [TestCase("{0:TWO}", 12345678901234, "11\u00A0TiB")]
        [TestCase("{0:two}", 123456789012345, "112\u00A0TiB")]
        [TestCase("{0:Two}", 123456789012345, "112\u00A0TiB")]
        [TestCase("{0:TWO}", 123456789012345, "112\u00A0TiB")]
        [TestCase("{0:two}", 1234567890123456, "1\u00A0PiB")]
        [TestCase("{0:Two}", 1234567890123456, "1\u00A0PiB")]
        [TestCase("{0:TWO}", 1234567890123456, "1\u00A0PiB")]
        [TestCase("{0:two}", 12345678901234567, "11\u00A0PiB")]
        [TestCase("{0:Two}", 12345678901234567, "11\u00A0PiB")]
        [TestCase("{0:TWO}", 12345678901234567, "11\u00A0PiB")]
        [TestCase("{0:two}", 123456789012345678, "110\u00A0PiB")]
        [TestCase("{0:Two}", 123456789012345678, "110\u00A0PiB")]
        [TestCase("{0:TWO}", 123456789012345678, "110\u00A0PiB")]
        public void Format_DefaultUsageIntegrationTestUnitTwoNoDigits_ResultAsExpected(String format, Object value, String expected)
        {
            String actual = String.Format(this.GetInstance(), format, value);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("{0:two3}", 0, "0\u00A0BiB")]
        [TestCase("{0:Two3}", 0, "0\u00A0BiB")]
        [TestCase("{0:TWO3}", 0, "0\u00A0BiB")]
        [TestCase("{0:two3}", 123, "123\u00A0BiB")]
        [TestCase("{0:Two3}", 123, "123\u00A0BiB")]
        [TestCase("{0:TWO3}", 123, "123\u00A0BiB")]
        [TestCase("{0:two3}", 1234, "1.205\u00A0KiB")]
        [TestCase("{0:Two3}", 1234, "1.205\u00A0KiB")]
        [TestCase("{0:TWO3}", 1234, "1.205\u00A0KiB")]
        [TestCase("{0:two3}", 12345, "12.056\u00A0KiB")]
        [TestCase("{0:Two3}", 12345, "12.056\u00A0KiB")]
        [TestCase("{0:TWO3}", 12345, "12.056\u00A0KiB")]
        [TestCase("{0:two3}", 123456, "120.562\u00A0KiB")]
        [TestCase("{0:Two3}", 123456, "120.562\u00A0KiB")]
        [TestCase("{0:TWO3}", 123456, "120.562\u00A0KiB")]
        [TestCase("{0:two3}", 1234567, "1.177\u00A0MiB")]
        [TestCase("{0:Two3}", 1234567, "1.177\u00A0MiB")]
        [TestCase("{0:TWO3}", 1234567, "1.177\u00A0MiB")]
        [TestCase("{0:two3}", 12345678, "11.774\u00A0MiB")]
        [TestCase("{0:Two3}", 12345678, "11.774\u00A0MiB")]
        [TestCase("{0:TWO3}", 12345678, "11.774\u00A0MiB")]
        [TestCase("{0:two3}", 123456789, "117.738\u00A0MiB")]
        [TestCase("{0:Two3}", 123456789, "117.738\u00A0MiB")]
        [TestCase("{0:TWO3}", 123456789, "117.738\u00A0MiB")]
        [TestCase("{0:two3}", 1234567890, "1.150\u00A0GiB")]
        [TestCase("{0:Two3}", 1234567890, "1.150\u00A0GiB")]
        [TestCase("{0:TWO3}", 1234567890, "1.150\u00A0GiB")]
        [TestCase("{0:two3}", 12345678901, "11.498\u00A0GiB")]
        [TestCase("{0:Two3}", 12345678901, "11.498\u00A0GiB")]
        [TestCase("{0:TWO3}", 12345678901, "11.498\u00A0GiB")]
        [TestCase("{0:two3}", 123456789012, "114.978\u00A0GiB")]
        [TestCase("{0:Two3}", 123456789012, "114.978\u00A0GiB")]
        [TestCase("{0:TWO3}", 123456789012, "114.978\u00A0GiB")]
        [TestCase("{0:two3}", 1234567890123, "1.123\u00A0TiB")]
        [TestCase("{0:Two3}", 1234567890123, "1.123\u00A0TiB")]
        [TestCase("{0:TWO3}", 1234567890123, "1.123\u00A0TiB")]
        [TestCase("{0:two3}", 12345678901234, "11.228\u00A0TiB")]
        [TestCase("{0:Two3}", 12345678901234, "11.228\u00A0TiB")]
        [TestCase("{0:TWO3}", 12345678901234, "11.228\u00A0TiB")]
        [TestCase("{0:two3}", 123456789012345, "112.283\u00A0TiB")]
        [TestCase("{0:Two3}", 123456789012345, "112.283\u00A0TiB")]
        [TestCase("{0:TWO3}", 123456789012345, "112.283\u00A0TiB")]
        [TestCase("{0:two3}", 1234567890123456, "1.097\u00A0PiB")]
        [TestCase("{0:Two3}", 1234567890123456, "1.097\u00A0PiB")]
        [TestCase("{0:TWO3}", 1234567890123456, "1.097\u00A0PiB")]
        [TestCase("{0:two3}", 12345678901234567, "10.965\u00A0PiB")]
        [TestCase("{0:Two3}", 12345678901234567, "10.965\u00A0PiB")]
        [TestCase("{0:TWO3}", 12345678901234567, "10.965\u00A0PiB")]
        [TestCase("{0:two3}", 123456789012345678, "109.652\u00A0PiB")]
        [TestCase("{0:Two3}", 123456789012345678, "109.652\u00A0PiB")]
        [TestCase("{0:TWO3}", 123456789012345678, "109.652\u00A0PiB")]
        public void Format_DefaultUsageIntegrationTestUnitTwoThreeDigits_ResultAsExpected(String format, Object value, String expected)
        {
            String actual = String.Format(this.GetInstance(), format, value);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("{0:bytes}", 0, "0\u00A0Bytes")]
        [TestCase("{0:Bytes}", 0, "0\u00A0Bytes")]
        [TestCase("{0:BYTES}", 0, "0\u00A0Bytes")]
        [TestCase("{0:bytes}", 1234, "1,234\u00A0Bytes")]
        [TestCase("{0:Bytes}", 1234, "1,234\u00A0Bytes")]
        [TestCase("{0:BYTES}", 1234, "1,234\u00A0Bytes")]
        [TestCase("{0:kb}", 1234, "1\u00A0KB")]
        [TestCase("{0:Kb}", 1234, "1\u00A0KB")]
        [TestCase("{0:KB}", 1234, "1\u00A0KB")]
        [TestCase("{0:mb}", 123456789, "118\u00A0MB")]
        [TestCase("{0:Mb}", 123456789, "118\u00A0MB")]
        [TestCase("{0:MB}", 123456789, "118\u00A0MB")]
        [TestCase("{0:gb}", 12345678912, "11\u00A0GB")]
        [TestCase("{0:Gb}", 12345678912, "11\u00A0GB")]
        [TestCase("{0:GB}", 12345678912, "11\u00A0GB")]
        [TestCase("{0:tb}", 12345678912345, "11\u00A0TB")]
        [TestCase("{0:Tb}", 12345678912345, "11\u00A0TB")]
        [TestCase("{0:TB}", 12345678912345, "11\u00A0TB")]
        [TestCase("{0:pb}", 123456789123456789, "110\u00A0PB")]
        [TestCase("{0:Pb}", 123456789123456789, "110\u00A0PB")]
        [TestCase("{0:PB}", 123456789123456789, "110\u00A0PB")]
        public void Format_StandardUsageIntegrationTestUnitOne_ResultAsExpected(String format, Object value, String expected)
        {
            String actual = String.Format(this.GetInstance(), format, value);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("{0:bib}", 0, "0\u00A0BiB")]
        [TestCase("{0:BiB}", 0, "0\u00A0BiB")]
        [TestCase("{0:BIB}", 0, "0\u00A0BiB")]
        [TestCase("{0:bib}", 1234, "1,234\u00A0BiB")]
        [TestCase("{0:BiB}", 1234, "1,234\u00A0BiB")]
        [TestCase("{0:BIB}", 1234, "1,234\u00A0BiB")]
        [TestCase("{0:kib}", 1234, "1\u00A0KiB")]
        [TestCase("{0:KiB}", 1234, "1\u00A0KiB")]
        [TestCase("{0:KIB}", 1234, "1\u00A0KiB")]
        [TestCase("{0:mib}", 123456789, "118\u00A0MiB")]
        [TestCase("{0:MiB}", 123456789, "118\u00A0MiB")]
        [TestCase("{0:MIB}", 123456789, "118\u00A0MiB")]
        [TestCase("{0:gib}", 12345678912, "11\u00A0GiB")]
        [TestCase("{0:GiB}", 12345678912, "11\u00A0GiB")]
        [TestCase("{0:GIB}", 12345678912, "11\u00A0GiB")]
        [TestCase("{0:tib}", 12345678912345, "11\u00A0TiB")]
        [TestCase("{0:TiB}", 12345678912345, "11\u00A0TiB")]
        [TestCase("{0:TIB}", 12345678912345, "11\u00A0TiB")]
        [TestCase("{0:pib}", 123456789123456789, "110\u00A0PiB")]
        [TestCase("{0:PiB}", 123456789123456789, "110\u00A0PiB")]
        [TestCase("{0:PIB}", 123456789123456789, "110\u00A0PiB")]
        public void Format_StandardUsageIntegrationTestUnitTwo_ResultAsExpected(String format, Object value, String expected)
        {
            String actual = String.Format(this.GetInstance(), format, value);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("{0:bytes3}", 0, "0\u00A0Bytes")]
        [TestCase("{0:Bytes3}", 0, "0\u00A0Bytes")]
        [TestCase("{0:BYTES3}", 0, "0\u00A0Bytes")]
        [TestCase("{0:bytes3}", 1234, "1,234\u00A0Bytes")]
        [TestCase("{0:Bytes3}", 1234, "1,234\u00A0Bytes")]
        [TestCase("{0:BYTES3}", 1234, "1,234\u00A0Bytes")]
        [TestCase("{0:kb3}", 1234, "1.205\u00A0KB")]
        [TestCase("{0:Kb3}", 1234, "1.205\u00A0KB")]
        [TestCase("{0:KB3}", 1234, "1.205\u00A0KB")]
        [TestCase("{0:mb3}", 123456789, "117.738\u00A0MB")]
        [TestCase("{0:Mb3}", 123456789, "117.738\u00A0MB")]
        [TestCase("{0:MB3}", 123456789, "117.738\u00A0MB")]
        [TestCase("{0:gb3}", 12345678912, "11.498\u00A0GB")]
        [TestCase("{0:Gb3}", 12345678912, "11.498\u00A0GB")]
        [TestCase("{0:GB3}", 12345678912, "11.498\u00A0GB")]
        [TestCase("{0:tb3}", 12345678912345, "11.228\u00A0TB")]
        [TestCase("{0:Tb3}", 12345678912345, "11.228\u00A0TB")]
        [TestCase("{0:TB3}", 12345678912345, "11.228\u00A0TB")]
        [TestCase("{0:pb3}", 123456789123456789, "109.652\u00A0PB")]
        [TestCase("{0:Pb3}", 123456789123456789, "109.652\u00A0PB")]
        [TestCase("{0:PB3}", 123456789123456789, "109.652\u00A0PB")]
        public void Format_StandardUsageIntegrationTestUnitOnePlusDigits_ResultAsExpected(String format, Object value, String expected)
        {
            String actual = String.Format(this.GetInstance(), format, value);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("{0:bib4}", 0, "0\u00A0BiB")]
        [TestCase("{0:BiB4}", 0, "0\u00A0BiB")]
        [TestCase("{0:BIB4}", 0, "0\u00A0BiB")]
        [TestCase("{0:bib4}", 1234, "1,234\u00A0BiB")]
        [TestCase("{0:BiB4}", 1234, "1,234\u00A0BiB")]
        [TestCase("{0:BIB4}", 1234, "1,234\u00A0BiB")]
        [TestCase("{0:kib4}", 1234, "1.2051\u00A0KiB")]
        [TestCase("{0:KiB4}", 1234, "1.2051\u00A0KiB")]
        [TestCase("{0:KIB4}", 1234, "1.2051\u00A0KiB")]
        [TestCase("{0:mib4}", 123456789, "117.7376\u00A0MiB")]
        [TestCase("{0:MiB4}", 123456789, "117.7376\u00A0MiB")]
        [TestCase("{0:MIB4}", 123456789, "117.7376\u00A0MiB")]
        [TestCase("{0:gib4}", 12345678912, "11.4978\u00A0GiB")]
        [TestCase("{0:GiB4}", 12345678912, "11.4978\u00A0GiB")]
        [TestCase("{0:GIB4}", 12345678912, "11.4978\u00A0GiB")]
        [TestCase("{0:tib4}", 12345678912345, "11.2283\u00A0TiB")]
        [TestCase("{0:TiB4}", 12345678912345, "11.2283\u00A0TiB")]
        [TestCase("{0:TIB4}", 12345678912345, "11.2283\u00A0TiB")]
        [TestCase("{0:pib4}", 123456789123456789, "109.6517\u00A0PiB")]
        [TestCase("{0:PiB4}", 123456789123456789, "109.6517\u00A0PiB")]
        [TestCase("{0:PIB4}", 123456789123456789, "109.6517\u00A0PiB")]
        public void Format_StandardUsageIntegrationTestUnitTwoPlusDigits_ResultAsExpected(String format, Object value, String expected)
        {
            String actual = String.Format(this.GetInstance(), format, value);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("{0:bib0}", 0, "0\u00A0BiB")]
        [TestCase("{0:bib0}", 1234, "1,234\u00A0BiB")]
        [TestCase("{0:kib0}", 1234, "1\u00A0KiB")]
        [TestCase("{0:mib0}", 123456789, "118\u00A0MiB")]
        [TestCase("{0:gib0}", 12345678912, "11\u00A0GiB")]
        [TestCase("{0:tib0}", 12345678912345, "11\u00A0TiB")]
        [TestCase("{0:pib0}", 123456789123456789, "110\u00A0PiB")]
        public void Format_StandardUsageIntegrationTestUnitTwoAndZeroDigits_ResultAsExpected(String format, Object value, String expected)
        {
            String actual = String.Format(this.GetInstance(), format, value);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("en-US", "{0:MB3}", 123456789123456789, "117,737,568,973.023\u00A0MB")]
        [TestCase("de-DE", "{0:MB3}", 123456789123456789, "117.737.568.973,023\u00A0MB")]
        [TestCase("fr-FR", "{0:MB3}", 123456789123456789, "117\u00A0737\u00A0568\u00A0973,023\u00A0MB")]
        public void Format_CultureDependentFormatting_ResultAsExpected(String culture, String format, Object value, String expected)
        {
            String actual = String.Format(this.GetInstance(new CultureInfo(culture)), format, value);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("{0:mib!5}", 123456789012345678, "123,456,789,012,345,678.00000\u00A0MiB")]
        [TestCase("{0:mib5}", 123456789012345678, "117,737,568,867.05940\u00A0MiB")]
        [TestCase("{0:!mib!!!!}", 123456789012345678, "123,456,789,012,345,678\u00A0MiB")]
        [TestCase("{0:mib}", 123456789012345678, "117,737,568,867\u00A0MiB")]
        [TestCase("{0:1!5}", 123456789012345678, "123,456,789,012,345,678.000000000000000\u00A0PB")]
        public void Format_CalculateDependentFormatting_ResultAsExpected(String format, Object value, String expected)
        {
            String actual = String.Format(this.GetInstance(), format, value);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("{0:MB~5}", 1099511627776, "1,048,576\u00A0MB")]
        [TestCase("{0:MB5}", 1099511627776, "1,048,576.00000\u00A0MB")]
        [TestCase("{0:MB~5}", 1099511628288, "1,048,576.00049\u00A0MB")]
        [TestCase("{0:MB5}", 1099511628288, "1,048,576.00049\u00A0MB")]
        [TestCase("{0:MB~5}", 3298534883328, "3,145,728\u00A0MB")]
        [TestCase("{0:MB5}", 3298534883328, "3,145,728.00000\u00A0MB")]
        [TestCase("{0:MB~5}", 3298534889197, "3,145,728.00560\u00A0MB")]
        [TestCase("{0:MB5}", 3298534889197, "3,145,728.00560\u00A0MB")]
        public void Format_InterceptDependentFormatting_ResultAsExpected(String format, Object value, String expected)
        {
            String actual = String.Format(this.GetInstance(), format, value);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Examples_RecommendedUsage_ResultsInOutput()
        {
            CapacityFormatter formatter = this.GetInstance();

            Debug.WriteLine("Auto-format, auto-unit-selection, zero decimal digits.");
            Debug.WriteLine(String.Format(formatter, "{0}", 0));
            Debug.WriteLine(String.Format(formatter, "{0}", 123));
            Debug.WriteLine(String.Format(formatter, "{0}", 1234));
            Debug.WriteLine(String.Format(formatter, "{0}", 12345));
            Debug.WriteLine(String.Format(formatter, "{0}", 123456));
            Debug.WriteLine(String.Format(formatter, "{0}", 1234567));
            Debug.WriteLine(String.Format(formatter, "{0}", 12345678));
            Debug.WriteLine(String.Format(formatter, "{0}", 123456789));
            Debug.WriteLine(String.Format(formatter, "{0}", 1234567890));
            Debug.WriteLine(String.Format(formatter, "{0}", 12345678901));
            Debug.WriteLine(String.Format(formatter, "{0}", 123456789012));
            Debug.WriteLine(String.Format(formatter, "{0}", 1234567890123));
            Debug.WriteLine(String.Format(formatter, "{0}", 12345678901234));
            Debug.WriteLine(String.Format(formatter, "{0}", 123456789012345));
            Debug.WriteLine(String.Format(formatter, "{0}", 1234567890123456));
            Debug.WriteLine(String.Format(formatter, "{0}", 12345678901234567));
            Debug.WriteLine(String.Format(formatter, "{0}", 123456789012345678));

            Debug.WriteLine("Auto-format, auto-unit-selection, three decimal digits.");
            Debug.WriteLine(String.Format(formatter, "{0:3}", 0));
            Debug.WriteLine(String.Format(formatter, "{0:3}", 123));
            Debug.WriteLine(String.Format(formatter, "{0:3}", 1234));
            Debug.WriteLine(String.Format(formatter, "{0:3}", 12345));
            Debug.WriteLine(String.Format(formatter, "{0:3}", 123456));
            Debug.WriteLine(String.Format(formatter, "{0:3}", 1234567));
            Debug.WriteLine(String.Format(formatter, "{0:3}", 12345678));
            Debug.WriteLine(String.Format(formatter, "{0:3}", 123456789));
            Debug.WriteLine(String.Format(formatter, "{0:3}", 1234567890));
            Debug.WriteLine(String.Format(formatter, "{0:3}", 12345678901));
            Debug.WriteLine(String.Format(formatter, "{0:3}", 123456789012));
            Debug.WriteLine(String.Format(formatter, "{0:3}", 1234567890123));
            Debug.WriteLine(String.Format(formatter, "{0:3}", 12345678901234));
            Debug.WriteLine(String.Format(formatter, "{0:3}", 123456789012345));
            Debug.WriteLine(String.Format(formatter, "{0:3}", 1234567890123456));
            Debug.WriteLine(String.Format(formatter, "{0:3}", 12345678901234567));
            Debug.WriteLine(String.Format(formatter, "{0:3}", 123456789012345678));

            Debug.WriteLine("Auto-format, unit one preferred, zero decimal digits.");
            Debug.WriteLine(String.Format(formatter, "{0:one}", 0));
            Debug.WriteLine(String.Format(formatter, "{0:one}", 123));
            Debug.WriteLine(String.Format(formatter, "{0:one}", 1234));
            Debug.WriteLine(String.Format(formatter, "{0:one}", 12345));
            Debug.WriteLine(String.Format(formatter, "{0:one}", 123456));
            Debug.WriteLine(String.Format(formatter, "{0:one}", 1234567));
            Debug.WriteLine(String.Format(formatter, "{0:one}", 12345678));
            Debug.WriteLine(String.Format(formatter, "{0:one}", 123456789));
            Debug.WriteLine(String.Format(formatter, "{0:one}", 1234567890));
            Debug.WriteLine(String.Format(formatter, "{0:one}", 12345678901));
            Debug.WriteLine(String.Format(formatter, "{0:one}", 123456789012));
            Debug.WriteLine(String.Format(formatter, "{0:one}", 1234567890123));
            Debug.WriteLine(String.Format(formatter, "{0:one}", 12345678901234));
            Debug.WriteLine(String.Format(formatter, "{0:one}", 123456789012345));
            Debug.WriteLine(String.Format(formatter, "{0:one}", 1234567890123456));
            Debug.WriteLine(String.Format(formatter, "{0:one}", 12345678901234567));
            Debug.WriteLine(String.Format(formatter, "{0:one}", 123456789012345678));

            Debug.WriteLine("Auto-format, unit one preferred, six decimal digits.");
            Debug.WriteLine(String.Format(formatter, "{0:one6}", 0));
            Debug.WriteLine(String.Format(formatter, "{0:one6}", 123));
            Debug.WriteLine(String.Format(formatter, "{0:one6}", 1234));
            Debug.WriteLine(String.Format(formatter, "{0:one6}", 12345));
            Debug.WriteLine(String.Format(formatter, "{0:one6}", 123456));
            Debug.WriteLine(String.Format(formatter, "{0:one6}", 1234567));
            Debug.WriteLine(String.Format(formatter, "{0:one6}", 12345678));
            Debug.WriteLine(String.Format(formatter, "{0:one6}", 123456789));
            Debug.WriteLine(String.Format(formatter, "{0:one6}", 1234567890));
            Debug.WriteLine(String.Format(formatter, "{0:one6}", 12345678901));
            Debug.WriteLine(String.Format(formatter, "{0:one6}", 123456789012));
            Debug.WriteLine(String.Format(formatter, "{0:one6}", 1234567890123));
            Debug.WriteLine(String.Format(formatter, "{0:one6}", 12345678901234));
            Debug.WriteLine(String.Format(formatter, "{0:one6}", 123456789012345));
            Debug.WriteLine(String.Format(formatter, "{0:one6}", 1234567890123456));
            Debug.WriteLine(String.Format(formatter, "{0:one6}", 12345678901234567));
            Debug.WriteLine(String.Format(formatter, "{0:one6}", 123456789012345678));

            Debug.WriteLine("Auto-format, unit two preferred, zero decimal digits.");
            Debug.WriteLine(String.Format(formatter, "{0:two}", 0));
            Debug.WriteLine(String.Format(formatter, "{0:two}", 123));
            Debug.WriteLine(String.Format(formatter, "{0:two}", 1234));
            Debug.WriteLine(String.Format(formatter, "{0:two}", 12345));
            Debug.WriteLine(String.Format(formatter, "{0:two}", 123456));
            Debug.WriteLine(String.Format(formatter, "{0:two}", 1234567));
            Debug.WriteLine(String.Format(formatter, "{0:two}", 12345678));
            Debug.WriteLine(String.Format(formatter, "{0:two}", 123456789));
            Debug.WriteLine(String.Format(formatter, "{0:two}", 1234567890));
            Debug.WriteLine(String.Format(formatter, "{0:two}", 12345678901));
            Debug.WriteLine(String.Format(formatter, "{0:two}", 123456789012));
            Debug.WriteLine(String.Format(formatter, "{0:two}", 1234567890123));
            Debug.WriteLine(String.Format(formatter, "{0:two}", 12345678901234));
            Debug.WriteLine(String.Format(formatter, "{0:two}", 123456789012345));
            Debug.WriteLine(String.Format(formatter, "{0:two}", 1234567890123456));
            Debug.WriteLine(String.Format(formatter, "{0:two}", 12345678901234567));
            Debug.WriteLine(String.Format(formatter, "{0:two}", 123456789012345678));

            Debug.WriteLine("Auto-format, unit two preferred, six decimal digits.");
            Debug.WriteLine(String.Format(formatter, "{0:two6}", 0));
            Debug.WriteLine(String.Format(formatter, "{0:two6}", 123));
            Debug.WriteLine(String.Format(formatter, "{0:two6}", 1234));
            Debug.WriteLine(String.Format(formatter, "{0:two6}", 12345));
            Debug.WriteLine(String.Format(formatter, "{0:two6}", 123456));
            Debug.WriteLine(String.Format(formatter, "{0:two6}", 1234567));
            Debug.WriteLine(String.Format(formatter, "{0:two6}", 12345678));
            Debug.WriteLine(String.Format(formatter, "{0:two6}", 123456789));
            Debug.WriteLine(String.Format(formatter, "{0:two6}", 1234567890));
            Debug.WriteLine(String.Format(formatter, "{0:two6}", 12345678901));
            Debug.WriteLine(String.Format(formatter, "{0:two6}", 123456789012));
            Debug.WriteLine(String.Format(formatter, "{0:two6}", 1234567890123));
            Debug.WriteLine(String.Format(formatter, "{0:two6}", 12345678901234));
            Debug.WriteLine(String.Format(formatter, "{0:two6}", 123456789012345));
            Debug.WriteLine(String.Format(formatter, "{0:two6}", 1234567890123456));
            Debug.WriteLine(String.Format(formatter, "{0:two6}", 12345678901234567));
            Debug.WriteLine(String.Format(formatter, "{0:two6}", 123456789012345678));

            Debug.WriteLine("Auto-format, unit one preferred, four decimal digits, mixed interception.");
            Debug.WriteLine(String.Format(formatter, "{0:One4}", 1073741824m));
            Debug.WriteLine(String.Format(formatter, "{0:One~4}", 1073741824m));
            Debug.WriteLine(String.Format(formatter, "{0:One4}", 2213102268m));
            Debug.WriteLine(String.Format(formatter, "{0:One~4}", 2213102268m));

            Debug.WriteLine("Unit-one-driven format, zero decimal digits.");
            Debug.WriteLine(String.Format(formatter, "{0:bytes}", 123456789012345678));
            Debug.WriteLine(String.Format(formatter, "{0:kb}", 123456789012345678));
            Debug.WriteLine(String.Format(formatter, "{0:mb}", 123456789012345678));
            Debug.WriteLine(String.Format(formatter, "{0:gb}", 123456789012345678));
            Debug.WriteLine(String.Format(formatter, "{0:tb}", 123456789012345678));
            Debug.WriteLine(String.Format(formatter, "{0:pb}", 123456789012345678));

            Debug.WriteLine("Unit-one-driven format, four decimal digits.");
            Debug.WriteLine(String.Format(formatter, "{0:bytes4}", 123456789012345678));
            Debug.WriteLine(String.Format(formatter, "{0:kb4}", 123456789012345678));
            Debug.WriteLine(String.Format(formatter, "{0:mb4}", 123456789012345678));
            Debug.WriteLine(String.Format(formatter, "{0:gb4}", 123456789012345678));
            Debug.WriteLine(String.Format(formatter, "{0:tb4}", 123456789012345678));
            Debug.WriteLine(String.Format(formatter, "{0:pb4}", 123456789012345678));

            Debug.WriteLine("Unit-two-driven format, zero decimal digits.");
            Debug.WriteLine(String.Format(formatter, "{0:bib}", 123456789012345678));
            Debug.WriteLine(String.Format(formatter, "{0:kib}", 123456789012345678));
            Debug.WriteLine(String.Format(formatter, "{0:mib}", 123456789012345678));
            Debug.WriteLine(String.Format(formatter, "{0:gib}", 123456789012345678));
            Debug.WriteLine(String.Format(formatter, "{0:tib}", 123456789012345678));
            Debug.WriteLine(String.Format(formatter, "{0:pib}", 123456789012345678));

            Debug.WriteLine("Unit-two-driven format, four decimal digits.");
            Debug.WriteLine(String.Format(formatter, "{0:bib4}", 123456789012345678));
            Debug.WriteLine(String.Format(formatter, "{0:kib4}", 123456789012345678));
            Debug.WriteLine(String.Format(formatter, "{0:mib4}", 123456789012345678));
            Debug.WriteLine(String.Format(formatter, "{0:gib4}", 123456789012345678));
            Debug.WriteLine(String.Format(formatter, "{0:tib4}", 123456789012345678));
            Debug.WriteLine(String.Format(formatter, "{0:pib4}", 123456789012345678));

            Debug.WriteLine("Custom-unit-format, zero decimal digits.");
            Debug.WriteLine(String.Format(formatter, "{0:my-size}", 123456789012345678));

            Debug.WriteLine("Custom-unit-format, six decimal digits.");
            Debug.WriteLine(String.Format(formatter, "{0:my-size6}", 123456789012345678));

            Debug.WriteLine("Unit-two-driven format, four decimal digits, but no calculation.");
            Debug.WriteLine(String.Format(formatter, "{0:!bib4}", 1152921504606846976m));
            Debug.WriteLine(String.Format(formatter, "{0:!kib4}", 1152921504606846976m));
            Debug.WriteLine(String.Format(formatter, "{0:!mib4}", 1152921504606846976m));
            Debug.WriteLine(String.Format(formatter, "{0:!gib4}", 1152921504606846976m));
            Debug.WriteLine(String.Format(formatter, "{0:!tib4}", 1152921504606846976m));
            Debug.WriteLine(String.Format(formatter, "{0:!pib4}", 1152921504606846976m));

            Debug.WriteLine("Unit-two-driven format, four decimal digits, but interception.");
            Debug.WriteLine(String.Format(formatter, "{0:bib~4}", 1152921504606846976m));
            Debug.WriteLine(String.Format(formatter, "{0:kib~4}", 1152921504606846976m));
            Debug.WriteLine(String.Format(formatter, "{0:mib~4}", 1152921504606846976m));
            Debug.WriteLine(String.Format(formatter, "{0:gib~4}", 1152921504606846976m));
            Debug.WriteLine(String.Format(formatter, "{0:tib~4}", 1152921504606846976m));
            Debug.WriteLine(String.Format(formatter, "{0:pib~4}", 1152921504606846976m));

            Debug.WriteLine("Unit-two-driven format, four decimal digits, with interception, but no calculation.");
            Debug.WriteLine(String.Format(formatter, "{0:!bib~4}", 1152921504606846976m));
            Debug.WriteLine(String.Format(formatter, "{0:!kib~4}", 1152921504606846976m));
            Debug.WriteLine(String.Format(formatter, "{0:!mib~4}", 1152921504606846976m));
            Debug.WriteLine(String.Format(formatter, "{0:!gib~4}", 1152921504606846976m));
            Debug.WriteLine(String.Format(formatter, "{0:!tib~4}", 1152921504606846976m));
            Debug.WriteLine(String.Format(formatter, "{0:!pib~4}", 1152921504606846976m));

            Assert.That(true);
        }

        private CapacityFormatter GetInstance(CultureInfo culture = null)
        {
            return new CapacityFormatter(culture);
        }
    }
}
