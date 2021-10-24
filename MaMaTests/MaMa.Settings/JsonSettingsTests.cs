using NUnit.Framework;
using FakeItEasy;
using MaMa.Settings;

namespace MaMaTests.Settings
{
    public class JsonSettingsTests
    {
        private string fakeJsonSetting = @"
{
  ""RuleSets"": [
    {
      ""firstNumber"": {
        ""MaxDigits"": null,
        ""MoveKomma"": 2,
        ""MaxValue"": 5000,
        ""MinValue"": 1000,
        ""AllowNegative"": false
      },
      ""secondNumber"": {
        ""MaxDigits"": null,
        ""MoveKomma"": 2,
        ""MaxValue"": 30,
        ""MinValue"": 10,
        ""AllowNegative"": false
      },
      ""solutionCriteria"": {
        ""AllowRational"": true,
        ""AllowNegative"": false,
        ""ShowAsRechenArt"": ""Division""
      },
      ""amount"": 1
    },
    {
      ""firstNumber"": {
        ""MaxDigits"": null,
        ""MoveKomma"": 2,
        ""MaxValue"": 5000,
        ""MinValue"": 1000,
        ""AllowNegative"": false
      },
      ""secondNumber"": {
        ""MaxDigits"": null,
        ""MoveKomma"": 2,
        ""MaxValue"": 30,
        ""MinValue"": 10,
        ""AllowNegative"": false
      },
      ""solutionCriteria"": {
        ""AllowRational"": true,
        ""AllowNegative"": false,
        ""ShowAsRechenArt"": ""Division""
      },
      ""amount"": 1
    }
    ,
    {
      ""firstNumber"": {
        ""MaxDigits"": null,
        ""MoveKomma"": 3,
        ""MaxValue"": 5000,
        ""MinValue"": 1000,
        ""AllowNegative"": false
      },
      ""secondNumber"": {
        ""MaxDigits"": null,
        ""MoveKomma"": 3,
        ""MaxValue"": 30,
        ""MinValue"": 10,
        ""AllowNegative"": false
      },
      ""solutionCriteria"": {
        ""AllowRational"": true,
        ""AllowNegative"": false,
        ""ShowAsRechenArt"": ""Multiplikation""
      },
      ""amount"": 2
    }
    ,
    {
      ""firstNumber"": {
        ""MaxDigits"": null,
        ""MoveKomma"": 4,
        ""MaxValue"": 9999,
        ""MinValue"": 8888,
        ""AllowNegative"": false
      },
      ""secondNumber"": {
        ""MaxDigits"": null,
        ""MoveKomma"": 4,
        ""MaxValue"": 99999,
        ""MinValue"": 77777,
        ""AllowNegative"": false
      },
      ""solutionCriteria"": {
        ""AllowRational"": true,
        ""AllowNegative"": false,
        ""ShowAsRechenArt"": ""Addition""
      },
      ""amount"": 2
    }
  ]
}
";
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void JsonDeserialiseTest()
        {
            ISerializeSettings ss = new JsonSerializeSettings();
            SettingsFile sf = ss.DeserializeSettings(fakeJsonSetting);

            // do some test if fiels are correctly deseralised
            RuleSet rs1 = sf.RuleSets[0];
            NumberProperties nr1 = rs1.FirstNumber;
            Assert.IsTrue(nr1.MaxDigits == null);
            Assert.AreEqual(2,nr1.MoveKomma);
            Assert.IsFalse(nr1.AllowNegative);
            // sln criteria
            Assert.IsFalse(rs1.SolutionCriteria.AllowNegative);
            Assert.AreEqual(EnumRechenArt.Division, rs1.SolutionCriteria.ShowAsRechenArt);  
            // ruleSet
            Assert.AreEqual(1,rs1.AmountOfCalculations);
        }

        [Test]
        public void JsonSettingsManagerTest()
        {
            const string V = "TEST";
            // need a fake ISettingsReader 
            ISettingsReader fakeReader = A.Fake<ISettingsReader>();
            A.CallTo(() => fakeReader.Load(V))
             .Returns(fakeJsonSetting);

            ISerializeSettings ss = new JsonSerializeSettings();

            // settings manager is being tested (here it doesn say much because manager only calls serialise settings)
            JsonSettingsManager sut = new JsonSettingsManager(fakeReader, ss);
            SettingsFile f = sut.GetSettings(V);

            //
            // THEN
            //
            // if no error and serialisation produces 4 sets of rules -> good start
            Assert.AreEqual(4, f.RuleSets.Count);
        }
    }
}