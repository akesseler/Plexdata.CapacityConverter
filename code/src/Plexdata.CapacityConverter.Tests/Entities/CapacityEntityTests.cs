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
using Plexdata.Converters.Entities;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Plexdata.CapacityConverter.Tests.Entities
{
    [SetCulture("en-US")]
    [SetUICulture("en-US")]
    [ExcludeFromCodeCoverage]
    public class CapacityEntityTests
    {
        [Test]
        public void CapacityEntity_ValueInvalied_ThrowsArgumentException()
        {
            Assert.That(() => new CapacityEntity(0, "unit", "unit"), Throws.ArgumentException);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void CapacityEntity_UnitOneInvalied_ThrowsArgumentException(String unitOne)
        {
            Assert.That(() => new CapacityEntity(42, unitOne, "unit"), Throws.ArgumentException);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void CapacityEntity_UnitTwoInvalied_ThrowsArgumentException(String unitTwo)
        {
            Assert.That(() => new CapacityEntity(42, "unit", unitTwo), Throws.ArgumentException);
        }

        [TestCase("U1", "U2")]
        [TestCase(" U1", "U2 ")]
        [TestCase("U1 ", " U2")]
        [TestCase(" U1 ", " U2 ")]
        public void CapacityEntity_UnitsWithSpaces_UnitsAreTrimmed(String unitOne, String unitTwo)
        {
            CapacityEntity instance = new CapacityEntity(42, unitOne, unitTwo);

            Assert.That(instance.Unit1, Is.EqualTo(unitOne.Trim()));
            Assert.That(instance.Unit2, Is.EqualTo(unitTwo.Trim()));
        }

        [Test]
        [TestCase("Unit1", true)]
        [TestCase("unit1", true)]
        [TestCase("UNIT1", true)]
        [TestCase("Unit2", true)]
        [TestCase("unit2", true)]
        [TestCase("UNIT2", true)]
        [TestCase("other", false)]
        [TestCase("Other", false)]
        [TestCase("OTHER", false)]
        public void IsEqual_UnitEquals_EqualAsExpected(String value, Boolean expected)
        {
            Assert.That(this.GetInstance().IsEqual(value), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("150", 0, "4")]
        [TestCase("150", 2, "3.57")]
        [TestCase("150", 17, "3.57142857142857143")]
        [TestCase("150", 28, "3.5714285714285714285714285714")]
        public void Calculate_MultipleValues_ResultAsExpected(String value, Int32 decimals, String expected)
        {
            if (decimals == 0)
            {
                Assert.That(this.GetInstance().Calculate(Convert.ToDecimal(value)), Is.EqualTo(Convert.ToDecimal(expected)));
            }
            else
            {
                Assert.That(this.GetInstance().Calculate(Convert.ToDecimal(value), decimals), Is.EqualTo(Convert.ToDecimal(expected)));
            }
        }

        [Test]
        [TestCase(-1)]
        [TestCase(30)]
        public void Calculate_DecimalsOutOfRange_ThrowsNothing(Int32 decimals)
        {
            Assert.That(() => this.GetInstance().Calculate(150, decimals), Throws.Nothing);
        }

        [Test]
        public void Format_AllCalls_ThrowsNothing()
        {
            Assert.That(() => this.GetInstance().Format(150), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, "unit"), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, 5), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, "unit", 5), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, false), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, "unit", false), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, 5, false), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, "unit", 5, false), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, (CultureInfo)null), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, "unit", (CultureInfo)null), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, 5, (CultureInfo)null), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, "unit", 5, (CultureInfo)null), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, false, (CultureInfo)null), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, "unit", false, (CultureInfo)null), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, 5, false, (CultureInfo)null), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, "unit", 5, false, (CultureInfo)null), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, this.GetCulture()), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, "unit", this.GetCulture()), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, 5, this.GetCulture()), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, "unit", 5, this.GetCulture()), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, false, this.GetCulture()), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, "unit", false, this.GetCulture()), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, 5, false, this.GetCulture()), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, "unit", 5, false, this.GetCulture()), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, this.GetFormatter()), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, "unit", this.GetFormatter()), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, 5, this.GetFormatter()), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, "unit", 5, this.GetFormatter()), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, false, this.GetFormatter()), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, "unit", false, this.GetFormatter()), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, 5, false, this.GetFormatter()), Throws.Nothing);
            Assert.That(() => this.GetInstance().Format(150, "unit", 5, false, this.GetFormatter()), Throws.Nothing);
        }

        [Test]
        public void Format_NumberFormatInfoIsNull_ThrowsArgumentNullException()
        {
            Assert.That(() => this.GetInstance().Format(150, "unit", 5, false, (NumberFormatInfo)null), Throws.ArgumentNullException);
        }

        [Test]
        [TestCase("150", null, "150\u00A0Unit1")]
        [TestCase("150", "", "150\u00A0Unit1")]
        [TestCase("150", " ", "150\u00A0Unit1")]
        public void Format_UnitInvalid_ResultWithUnitOne(String value, String unit, String expected)
        {
            String actual = this.GetInstance().Format(Convert.ToDecimal(value), unit, 0, false, this.GetFormatter());

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("150", " Other ", 0, "150\u00A0Other")]
        [TestCase("150", " other ", 0, "150\u00A0other")]
        [TestCase("150", " OTHER ", 0, "150\u00A0OTHER")]
        [TestCase("150", " Other ", 2, "150.00\u00A0Other")]
        [TestCase("150", " other ", 2, "150.00\u00A0other")]
        [TestCase("150", " OTHER ", 2, "150.00\u00A0OTHER")]
        [TestCase("150", " Other ", 4, "150.0000\u00A0Other")]
        [TestCase("150", " other ", 4, "150.0000\u00A0other")]
        [TestCase("150", " OTHER ", 4, "150.0000\u00A0OTHER")]
        public void Format_OtherUnitWithSpaces_ResultAsExpected(String value, String unit, Int32 decimals, String expected)
        {
            String actual = this.GetInstance().Format(Convert.ToDecimal(value), unit, decimals, false, this.GetFormatter());

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("150", "Unit1", 0, false, "150\u00A0Unit1")]
        [TestCase("150", "unit1", 0, false, "150\u00A0Unit1")]
        [TestCase("150", "UNIT1", 0, false, "150\u00A0Unit1")]
        [TestCase("150", "Unit2", 0, false, "150\u00A0Unit2")]
        [TestCase("150", "unit2", 0, false, "150\u00A0Unit2")]
        [TestCase("150", "UNIT2", 0, false, "150\u00A0Unit2")]
        [TestCase("150", "Other", 0, false, "150\u00A0Other")]
        [TestCase("150", "other", 0, false, "150\u00A0other")]
        [TestCase("150", "OTHER", 0, false, "150\u00A0OTHER")]
        [TestCase("150", "Unit1", 2, false, "150.00\u00A0Unit1")]
        [TestCase("150", "unit1", 2, false, "150.00\u00A0Unit1")]
        [TestCase("150", "UNIT1", 2, false, "150.00\u00A0Unit1")]
        [TestCase("150", "Unit2", 2, false, "150.00\u00A0Unit2")]
        [TestCase("150", "unit2", 2, false, "150.00\u00A0Unit2")]
        [TestCase("150", "UNIT2", 2, false, "150.00\u00A0Unit2")]
        [TestCase("150", "Other", 2, false, "150.00\u00A0Other")]
        [TestCase("150", "other", 2, false, "150.00\u00A0other")]
        [TestCase("150", "OTHER", 2, false, "150.00\u00A0OTHER")]
        [TestCase("150", "Unit1", 5, false, "150.00000\u00A0Unit1")]
        [TestCase("150", "unit1", 5, false, "150.00000\u00A0Unit1")]
        [TestCase("150", "UNIT1", 5, false, "150.00000\u00A0Unit1")]
        [TestCase("150", "Unit2", 5, false, "150.00000\u00A0Unit2")]
        [TestCase("150", "unit2", 5, false, "150.00000\u00A0Unit2")]
        [TestCase("150", "UNIT2", 5, false, "150.00000\u00A0Unit2")]
        [TestCase("150", "Other", 5, false, "150.00000\u00A0Other")]
        [TestCase("150", "other", 5, false, "150.00000\u00A0other")]
        [TestCase("150", "OTHER", 5, false, "150.00000\u00A0OTHER")]
        [TestCase("150", "Unit1", 0, true, "4\u00A0Unit1")]
        [TestCase("150", "unit1", 0, true, "4\u00A0Unit1")]
        [TestCase("150", "UNIT1", 0, true, "4\u00A0Unit1")]
        [TestCase("150", "Unit2", 0, true, "4\u00A0Unit2")]
        [TestCase("150", "unit2", 0, true, "4\u00A0Unit2")]
        [TestCase("150", "UNIT2", 0, true, "4\u00A0Unit2")]
        [TestCase("150", "Other", 0, true, "4\u00A0Other")]
        [TestCase("150", "other", 0, true, "4\u00A0other")]
        [TestCase("150", "OTHER", 0, true, "4\u00A0OTHER")]
        [TestCase("150", "Unit1", 2, true, "3.57\u00A0Unit1")]
        [TestCase("150", "unit1", 2, true, "3.57\u00A0Unit1")]
        [TestCase("150", "UNIT1", 2, true, "3.57\u00A0Unit1")]
        [TestCase("150", "Unit2", 2, true, "3.57\u00A0Unit2")]
        [TestCase("150", "unit2", 2, true, "3.57\u00A0Unit2")]
        [TestCase("150", "UNIT2", 2, true, "3.57\u00A0Unit2")]
        [TestCase("150", "Other", 2, true, "3.57\u00A0Other")]
        [TestCase("150", "other", 2, true, "3.57\u00A0other")]
        [TestCase("150", "OTHER", 2, true, "3.57\u00A0OTHER")]
        [TestCase("150", "Unit1", 5, true, "3.57143\u00A0Unit1")]
        [TestCase("150", "unit1", 5, true, "3.57143\u00A0Unit1")]
        [TestCase("150", "UNIT1", 5, true, "3.57143\u00A0Unit1")]
        [TestCase("150", "Unit2", 5, true, "3.57143\u00A0Unit2")]
        [TestCase("150", "unit2", 5, true, "3.57143\u00A0Unit2")]
        [TestCase("150", "UNIT2", 5, true, "3.57143\u00A0Unit2")]
        [TestCase("150", "Other", 5, true, "3.57143\u00A0Other")]
        [TestCase("150", "other", 5, true, "3.57143\u00A0other")]
        [TestCase("150", "OTHER", 5, true, "3.57143\u00A0OTHER")]
        public void Format_ValueUnitDecimalsCalculateCombinations_ResultAsExpected(String value, String unit, Int32 decimals, Boolean calculate, String expected)
        {
            String actual = this.GetInstance().Format(Convert.ToDecimal(value), unit, decimals, calculate, this.GetFormatter());

            Assert.That(actual, Is.EqualTo(expected));
        }

        private CultureInfo GetCulture()
        {
            return CultureInfo.CurrentUICulture;
        }

        private NumberFormatInfo GetFormatter()
        {
            return this.GetCulture().NumberFormat;
        }

        private CapacityEntity GetInstance()
        {
            return new CapacityEntity(42, "Unit1", "Unit2");
        }
    }
}
