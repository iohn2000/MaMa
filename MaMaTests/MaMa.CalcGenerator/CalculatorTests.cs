using NUnit.Framework;
using FakeItEasy;
using MaMa.CalcGenerator;
using MaMa.DataModels;
using System;
using Serilog;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;

namespace MaMaTests.CalcGenerator
{
    public class CalculatorTests
    {
        private ILogger<Calculator> logger;

        [OneTimeSetUp]
        public void Setup()
        {
            var xx = new LoggerConfiguration().MinimumLevel.Debug()
                                             .WriteTo.Console()
                                             .WriteTo.File("c:\\temp\\unit-tests-log-.txt", rollingInterval: RollingInterval.Day)
                                             .CreateLogger();
            this.logger = new SerilogLoggerFactory(xx).CreateLogger<Calculator>();
        }

        [Test]
        [TestCase(EnumRechenArt.Multiplikation, 6.3d, 2.0d, 12d, EnumNumberClassification.Integer)]
        public void CannotFindRightSolution_AbortTest(EnumRechenArt rechenArt, double nr1, double nr2, double sln, EnumNumberClassification nrClass)
        {
            var ex = Assert.Throws<Exception>(() =>
            {
                
                int rawNr = 0;
                NumberProperties nr1Prop = new NumberProperties() { AllowNegative = true };
                NumberProperties nr2Prop = new NumberProperties() { AllowNegative = false };
                SolutionProperties slnProp = new SolutionProperties()
                {
                    AllowNegative = false,
                    NumberClass = nrClass,
                    ElementaryArithmetic = rechenArt
                };
                BasicArithmeticalOperation ruleSet = new BasicArithmeticalOperation
                {
                    FirstNumber = nr1Prop,
                    SecondNumber = nr2Prop,
                    SolutionCriteria = slnProp,
                    AmountOfCalculations = 1
                };
                // use pre set numbers and test if div, mul,.. is done correctly
                // need fake rnd gen so that numbers are always the same
                var fakeRnd = A.Fake<IRandomNumber>();
                A.CallTo(() => fakeRnd.GetRandomNr(ruleSet.FirstNumber, out rawNr)).Returns<decimal>((decimal)nr1);
                A.CallTo(() => fakeRnd.GetRandomNr(ruleSet.SecondNumber, out rawNr)).Returns<decimal>((decimal)nr2);

                Calculator sut = new Calculator(this.logger, fakeRnd, new SolutionChecker());
                sut.GenerateNumbers(ruleSet, "test");

                var result = sut.GetGeneratedNumbers();
            });
        }


        [Test]
        [TestCase(EnumRechenArt.Multiplikation, 6.0d, 2.0d, 12d, EnumNumberClassification.Integer)]
        [TestCase(EnumRechenArt.Division, 12.0d, 2.0d, 6d, EnumNumberClassification.RationalTerminatingDecimals)]
        [TestCase(EnumRechenArt.Division, 11.0d, 2.0d, 5.5d, EnumNumberClassification.RationalTerminatingDecimals)]
        [TestCase(EnumRechenArt.Addition, 7.1d, 1.3d, 8.4d, EnumNumberClassification.RationalTerminatingDecimals)]
        public void CaclulationsDoneCorrectly(EnumRechenArt rechenArt, double nr1, double nr2, double sln, EnumNumberClassification nrClass)
        {
            int rawNr = 0;
            NumberProperties nr1Prop = new NumberProperties() { AllowNegative = true };
            NumberProperties nr2Prop = new NumberProperties() { AllowNegative = false };
            SolutionProperties slnProp = new SolutionProperties()
            {
                AllowNegative = false,
                NumberClass = nrClass,
                ElementaryArithmetic = rechenArt
            };
            BasicArithmeticalOperation ruleSet = new BasicArithmeticalOperation
            {
                FirstNumber = nr1Prop,
                SecondNumber = nr2Prop,
                SolutionCriteria = slnProp,
                AmountOfCalculations = 1
            };
            // use pre set numbers and test if div, mul,.. is done correctly
            // need fake rnd gen so that numbers are always the same
            var fakeRnd = A.Fake<IRandomNumber>();
            A.CallTo(() => fakeRnd.GetRandomNr(ruleSet.FirstNumber, out rawNr)).Returns<decimal>((decimal)nr1);
            A.CallTo(() => fakeRnd.GetRandomNr(ruleSet.SecondNumber, out rawNr)).Returns<decimal>((decimal)nr2);

            Calculator sut = new Calculator(this.logger, fakeRnd, new SolutionChecker());
            sut.GenerateNumbers(ruleSet, "test");

            var result = sut.GetGeneratedNumbers();

            Assert.AreEqual((decimal)nr1, result[0].FirstNumber);
            Assert.AreEqual((decimal)nr2, result[0].SecondNumber);
            Assert.AreEqual((decimal)sln, result[0].Solution);
        }

        [Test]
        [TestCase(89.22, 0.1)]
        [TestCase(53.116, 0.07)]
        public void AllowIrrationalWorking(double nr1, double nr2)
        {
            SolutionProperties slnProp = new SolutionProperties()
            {
                AllowNegative = false,
                NumberClass = EnumNumberClassification.Integer,
                ElementaryArithmetic = EnumRechenArt.Division
            };

            var fakeRnd = A.Fake<IRandomNumber>();
            Calculator sut = new Calculator(this.logger, fakeRnd, new SolutionChecker());
            bool isMeet = sut.SolutionCriteriaMet((decimal)nr1, (decimal)nr2, (decimal)(nr1 / nr2), slnProp);
            Assert.IsFalse(isMeet);
        }

        [Test]
        [TestCase(336.2, 0.87)]
        public void ConformToSolutionProperties(double nr1, double nr2)
        {
            //int rawNr = 0;
            //var fakrRndGen = A.Fake<IRandomNumber>();
            //A.CallTo(() => fakrRndGen.GetRandomNr(A.Dummy<NumberProperties>(), out rawNr)).Returns<decimal>((decimal)nr1);
            //A.CallTo(() => fakrRndGen.GetRandomNr(A.Dummy<NumberProperties>(), out rawNr)).Returns<decimal>((decimal)nr2);

            NumberProperties nr1Prop = new NumberProperties()
            {
                AllowNegative = true,
                MaxMoveKomma = 2,
                MinValue = 100,
                MaxValue = 5000
            };
            NumberProperties nr2Prop = new NumberProperties()
            {
                AllowNegative = true,
                MaxMoveKomma = 2,
                MinValue = 2,
                MaxValue = 100
            };
            SolutionProperties slnProp = new SolutionProperties()
            {
                AllowNegative = false,
                NumberClass = EnumNumberClassification.Integer,
                ElementaryArithmetic = EnumRechenArt.Division
            };
            BasicArithmeticalOperation ruleSet = new BasicArithmeticalOperation
            {
                FirstNumber = nr1Prop,
                SecondNumber = nr2Prop,
                SolutionCriteria = slnProp,
                AmountOfCalculations = 2
            };
            Calculator sut = new Calculator(this.logger, new RandomNumberGenerator(), new SolutionChecker());
            sut.GenerateNumbers(ruleSet, "test");

            var results = sut.GetGeneratedNumbers();
            foreach (var r in results)
            {
                Assert.IsTrue(r.Solution > 0, $"Assert if not negativ failed. {r.FirstNumber} / {r.SecondNumber} = {r.Solution}");
                // check sln is integer
                Assert.IsTrue(condition: (int)r.Solution == r.Solution, $"Assert if not rational failed. {r.FirstNumber} / {r.SecondNumber} = {r.Solution}");
            }
        }

        [Test]
        [TestCase(1, 3, false, 0, null)]
        [TestCase(10, 100, false, 3, null)]
        [TestCase(-10, 10, true, 0, null)]
        [TestCase(0, 1000, true, 4, null)]
        [TestCase(null, null, true, 0, 5)]
        public void ConformToNumberProperties(int? minV, int? maxV, bool allowNeg, byte maxMoveK, int? maxD)
        {
            RandomNumberGenerator sut = new RandomNumberGenerator();
            var rndNr = sut.GetRandomNr(new NumberProperties()
            {
                MinValue = minV,
                MaxValue = maxV,
                AllowNegative = allowNeg,
                MaxDigits = maxD,
                MaxMoveKomma = maxMoveK,
            }, out int rawNr);
            if (maxD != null)
            {
                var stellenFaktor = (int)(Math.Pow(10, maxD.Value - 1));
                Assert.IsTrue(rawNr > stellenFaktor, $"MAXD: Nr is too small: {rndNr} >= {maxD.Value}");
                Assert.IsTrue(rawNr < stellenFaktor * 10 - 1, $"MAXD: Nr is too big: {rndNr} >= {maxD.Value}");
            }
            else
            {
                Assert.IsTrue(rawNr >= minV, $"MINMAX: Nr is too small: {rndNr} >= {minV}");
                Assert.IsTrue(rawNr <= maxV, $"MINMAX: Nr is too big: {rndNr} <= {maxV}");
            }

            if (!allowNeg)
            {
                Assert.IsTrue(rndNr >= 0, $"Nr cannot be negativ: {rndNr}");
            }
            // check how many places komma has been moved
            // bei maxMoveK = 2 : 1,2 ; 144,54 ; 32 ; 0,01=> OK  || 1,543 ; 0,001 ==> not ok
            string nrAsString = rndNr.ToString(System.Globalization.CultureInfo.InvariantCulture);
            int decimalPlaces = 0;
            if (nrAsString.Contains("."))
            {
                decimalPlaces = nrAsString.Split('.')[1].Length;
            }
            Assert.IsTrue(decimalPlaces <= maxMoveK, $"Komma moved too far. {rndNr}, maxKomma:{maxMoveK}");
        }
    }
}