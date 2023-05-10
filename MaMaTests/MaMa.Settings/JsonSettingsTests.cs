using NUnit.Framework;
using FakeItEasy;
using MaMa.DataModels;
using MaMa.Settings;
using System.Linq;
using MaMa.DataModels.Interfaces;
using System.Collections.Generic;

namespace MaMaTests.Settings
{
    public class JsonSettingsTests
    {
        private string fakeJsonSetting = @"
{
""BasicArithmeticalOperation"": {
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
            BasicArithmeticalOperation rs1 = sf.BasicArithmeticalOperationSets.Values.ElementAt(0);
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
        public void JsonSerializeTest()
        {
            ISerializeSettings ss = new JsonSerializeSettings();
            SettingsFile sf = new SettingsFile();


            Dictionary<string, BasicArithmeticalOperation> b = new Dictionary<string, BasicArithmeticalOperation>();
            b.Add("basicAdd", new BasicArithmeticalOperation {
                FirstNumber = new NumberProperties { MinValue = 12, MaxValue = 142 },
                SecondNumber = new NumberProperties { MinValue = 13, MaxValue = 312 },
                AmountOfCalculations = 5,
                SolutionCriteria = new SolutionProperties { ElementaryArithmetic = EnumRechenArt.Addition }
            });
            sf.BasicArithmeticalOperationSets = b;

            Dictionary<string,Fractions> f = new Dictionary<string, Fractions>();
            f.Add("f1", new Fractions{ 
                Numerator = new NumberProperties{ MinValue = 1, MaxValue = 12 }, 
                Denominator = new NumberProperties{ MinValue = 2, MaxValue =12},
                AmountOfCalculations = 4,
                SolutionCriteria = new SolutionProperties { ElementaryArithmetic = EnumRechenArt.Multiplikation }
                });
            f.Add("f2", new Fractions
            {
                Numerator = new NumberProperties { MinValue = 23, MaxValue = 76 },
                Denominator = new NumberProperties { MinValue = 13, MaxValue = 43 },
                AmountOfCalculations = 4,
                SolutionCriteria = new SolutionProperties { ElementaryArithmetic = EnumRechenArt.Multiplikation }
            });
            sf.FractionSets = f;
            string strSettings = ss.SerializeSettings(sf);
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
            Assert.AreEqual(2, f.BasicArithmeticalOperationSets.Count);
        }
    }
}