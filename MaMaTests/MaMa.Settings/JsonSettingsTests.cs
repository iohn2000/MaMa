using NUnit.Framework;
using FakeItEasy;
using MaMa.DataModels;
using MaMa.Settings;
using System.Linq;

namespace MaMaTests.Settings
{
    public class JsonSettingsTests
    {
        private string fakeJsonSetting = @"
{
  ""RuleSets"": {
    ""DivisionKomma"": {
      ""firstNumber"": {
        ""maxDigits"": null,
        ""moveKomma"": 2,
        ""maxValue"": 9999,
        ""minValue"": 1,
        ""allowNegative"": false
      },
      ""secondNumber"": {
        ""maxDigits"": null,
        ""moveKomma"": 2,
        ""maxValue"": 20,
        ""minValue"": 1,
        ""allowNegative"": false
      },
      ""solutionCriteria"": {
        ""allowNegative"": false,
        ""numberClass"" : ""Integer"",
        ""elementaryArithmetic"": ""Division""
      },
      ""amount"": 56
    },

    ""Multiplik. Komma"": {
      ""firstNumber"": {
        ""MaxDigits"": null,
        ""MoveKomma"": 5,
        ""MaxValue"": 999,
        ""MinValue"": 10,
        ""AllowNegative"": false
      },
      ""secondNumber"": {
        ""maxDigits"": null,
        ""moveKomma"": 5,
        ""maxValue"": 999,
        ""minValue"": 10,
        ""allowNegative"": false
      },
      ""solutionCriteria"": {
        ""allowNegative"": false,
        ""numberClass"" : ""Integer"",
        ""elementaryArithmetic"": ""Multiplikation""
      },
      ""amount"": 0
    }
  }
}
";

        [Test]
        public void TestDictionaryJson()
        {
string fakeDictJson = @"
{
  ""RuleSets"": {
    ""DivisionKomma"": {
      ""firstNumber"": {
        ""maxDigits"": null,
        ""moveKomma"": 2,
        ""maxValue"": 9999,
        ""minValue"": 1,
        ""allowNegative"": false
      },
      ""secondNumber"": {
        ""maxDigits"": null,
        ""moveKomma"": 2,
        ""maxValue"": 20,
        ""minValue"": 1,
        ""allowNegative"": false
      },
      ""solutionCriteria"": {
        ""allowNegative"": false,
        ""numberClass"" : ""Integer"",
        ""elementaryArithmetic"": ""Division""
      },
      ""amount"": 56
    },

    ""Multiplik. Komma"": {
      ""firstNumber"": {
        ""MaxDigits"": null,
        ""MoveKomma"": 5,
        ""MaxValue"": 999,
        ""MinValue"": 10,
        ""AllowNegative"": false
      },
      ""secondNumber"": {
        ""maxDigits"": null,
        ""moveKomma"": 5,
        ""maxValue"": 999,
        ""minValue"": 10,
        ""allowNegative"": false
      },
      ""solutionCriteria"": {
        ""allowNegative"": false,
        ""numberClass"" : ""Integer"",
        ""elementaryArithmetic"": ""Multiplikation""
      },
      ""amount"": 0
    }
  }
}
";

            ISerializeSettings ss = new JsonSerializeSettings();
            SettingsFile sf = ss.DeserializeSettings(fakeDictJson);
        }

        [Test]
        public void JsonDeserialiseTest()
        {
            ISerializeSettings ss = new JsonSerializeSettings();
            SettingsFile sf = ss.DeserializeSettings(fakeJsonSetting);

            // do some test if fiels are correctly deseralised
            RuleSet rs1 = sf.RuleSets.Values.ElementAt(0);
            NumberProperties nr1 = rs1.FirstNumber;
            Assert.IsTrue(nr1.MaxDigits == null);
            Assert.AreEqual(2,nr1.MaxMoveKomma);
            Assert.IsFalse(nr1.AllowNegative);
            // sln criteria
            Assert.IsFalse(rs1.SolutionCriteria.AllowNegative);
            Assert.AreEqual(EnumRechenArt.Division, rs1.SolutionCriteria.ElementaryArithmetic);  
            // ruleSet
            Assert.AreEqual(56,rs1.AmountOfCalculations);
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
            Assert.AreEqual(2, f.RuleSets.Count);
        }
    }
}