using NUnit.Framework;
using FakeItEasy;
using MaMa.CalcGenerator;
using MaMa.DataModels;
using System;
using Serilog;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;


namespace MaMaTests.MaMa.CalcGenerator
{
    public class SolutionCheckerTests
    {
        [Test]
        [TestCase(1.232342d, 1232342, 6)]
        [TestCase(0.0012, 12, 4)]
        [TestCase(22, 22, 0)]
        [TestCase(0, 0, 0)]
        public void MakeIntegerTest(double testNr, int resultNr, int potenzenCount)
        {
            decimal testDecimalNr = (decimal)testNr;
            SolutionChecker sCheck = new SolutionChecker();

            var result = sCheck.MakeInteger(testDecimalNr);
            Assert.AreEqual(resultNr, result.integerNr);
            Assert.AreEqual(potenzenCount, result.potenzenCount);
        }

        [Test]
        [TestCase(58.84, 1.6, true, 3)]
        [TestCase(9406, 20, true, 1)]
        [TestCase(36, 1.8, true, 0)]
        [TestCase(2459, 18, false, -1)]
        [TestCase(801.8, 0.12, false, -1)]
        public void PeriodicityTests(double dividend, double divisor, bool isNonPeriodic, int commaCount)
        {
            SolutionChecker sc = new SolutionChecker();
            var result = sc.CalcPeriodicity((decimal)dividend, (decimal)divisor);
            Assert.AreEqual(isNonPeriodic,result.isNonPeriodic);
            Assert.AreEqual(commaCount,result.commaCount);

        }
    }
}
