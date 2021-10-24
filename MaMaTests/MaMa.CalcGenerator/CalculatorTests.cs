using NUnit.Framework;
using FakeItEasy;
using MaMa.CalcGenerator;
using MaMa.DataModels;

namespace MaMaTests.CalcGenerator
{
    public class CalculatorTests
    {
        [Test]
        [TestCase(EnumRechenArt.Multiplikation, 6.0d, 2.0d, 12d)]
        [TestCase(EnumRechenArt.Division, 12.0d, 2.0d, 6d)]
        [TestCase(EnumRechenArt.Addition, 7.1d, 1.3d, 8.4d)]
        public void CaclulationsDoneCorrectly(EnumRechenArt rechenArt, double nr1, double nr2, double sln)
        {
            NumberProperties nr1Prop = new NumberProperties() { AllowNegative = true };
            NumberProperties nr2Prop = new NumberProperties() { AllowNegative = false };
            SolutionProperties slnProp = new SolutionProperties()
            {
                AllowNegative = false,
                AllowRational = true,
                ShowAsRechenArt = rechenArt
            };
            // use pre set numbers and test if div, mul,.. is done correctly
            // need fake rnd gen so that numbers are always the same
            var fakeRnd = A.Fake<IRandomNumber>();
            A.CallTo(() => fakeRnd.GetRandomNr(nr1Prop)).Returns<decimal>((decimal)nr1);
            A.CallTo(() => fakeRnd.GetRandomNr(nr2Prop)).Returns<decimal>((decimal)nr2);

            Calculator sut = new Calculator(fakeRnd);
            sut.GenerateNumbers(nr1Prop, nr2Prop, slnProp, 1);

            var result = sut.GetGeneratedNumbers();

            Assert.AreEqual((decimal)nr1, result[0].FirstNumber);
            Assert.AreEqual((decimal)nr2, result[0].SecondNumber);
            Assert.AreEqual((decimal)sln, result[0].Solution);
        }

        [Test]
        public void ConformToSolutionProperties()
        {
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
                AllowRational = false,
                ShowAsRechenArt = EnumRechenArt.Division
            };

            Calculator sut = new Calculator(new RandomNumberGenerator());
            sut.GenerateNumbers(nr1Prop, nr2Prop, slnProp, 55);

            var results = sut.GetGeneratedNumbers();
            foreach (var r in results)
            {
                    Assert.IsTrue(r.Solution > 0,$"Assert if not negativ failed. {r.FirstNumber} / {r.SecondNumber} = {r.Solution}");
                    // check sln is integer
                    Assert.IsTrue(condition: (int)r.Solution == r.Solution,$"Assert if not rational failed. {r.FirstNumber} / {r.SecondNumber} = {r.Solution}");
            }
        }
    }
}