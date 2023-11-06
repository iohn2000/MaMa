using ConceptStepsAndSvg;
using MaMa.DataModels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MaMaTests.Concept.CalcSteps
{
    public class MultiplicationStepsTests
    {
        [Test]
        [TestCaseSource(nameof(MultiplicationTestCases))]
        public void MultiplicationTestGoodCases((decimal factor1, decimal factor2, decimal product, List<Row> stepsSln) testCase) 
        { 
            CalculationSteps cstep = new CalculationSteps();
   
            CalculationItem item = new(testCase.factor1, testCase.factor2, testCase.product, EnumRechenArt.Multiplikation, "RuleSet");
            List<Row> sln = cstep.CalculateMultiplicationSteps(item);
           
            Assert.That(sln.Sum(s => s.RowValueWithStellenwert) == testCase.product);

            for (int i = 0; i < testCase.stepsSln.Count; i++)
            {
                CollectionAssert.AreEqual(testCase.stepsSln[i].Digits, sln[i].Digits, "Digits Wrong");
                Assert.AreEqual(testCase.stepsSln[i].OrderRevers, sln[i].OrderRevers);
                Assert.AreEqual(testCase.stepsSln[i].RowValueWithStellenwert, sln[i].RowValueWithStellenwert);
                Assert.AreEqual(testCase.stepsSln[i].RowValue, sln[i].RowValue);
            }
        }

        private static IEnumerable<(decimal factor1, decimal factor2, decimal product, List<Row> stepsSln)> MultiplicationTestCases()
        {
            List<(decimal factor1, decimal factor2, decimal product, List<Row> stepsSln)> testCases = new()
            {
                (factor1: 53, factor2: 67, product: 3551, stepsSln: TestCase53_67()),
                (factor1: 765, factor2: 349, product: 266985, stepsSln: TestCase765_349()),
                (factor1: 100, factor2: 100, product: 10000, stepsSln: TestCase100_100())
            };
            return testCases;
        }

        private static List<Row> TestCase100_100()
        {
            return new List<Row>
            {
                new Row
                {
                    OrderRevers = 0, RowValue = 0, RowValueWithStellenwert = 0,
                    Digits = new List<Digit>
                    {
                        new Digit{ Order = 0, DigitValue = 0, CarryOver = 0 },
                        new Digit{ Order = 1, DigitValue = 0, CarryOver = 0 },
                        new Digit{ Order = 2, DigitValue = 0, CarryOver = 0 },
                    }
                },
                new Row
                {
                    OrderRevers = 1, RowValue = 0, RowValueWithStellenwert = 0,
                    Digits = new List<Digit>
                    {
                        new Digit{ Order = 0, DigitValue = 0, CarryOver = 0 },
                        new Digit{ Order = 1, DigitValue = 0, CarryOver = 0 },
                        new Digit{ Order = 2, DigitValue = 0, CarryOver = 0 },
                    }
                },
                new Row
                {
                    OrderRevers = 2, RowValue = 100, RowValueWithStellenwert = 10000,
                    Digits = new List<Digit>
                    {
                        new Digit{ Order = 0, DigitValue = 0, CarryOver = 0 },
                        new Digit{ Order = 1, DigitValue = 0, CarryOver = 0 },
                        new Digit{ Order = 2, DigitValue = 1, CarryOver = 0 },
                    }
                }
            };
        }

        private static List<Row> TestCase765_349()
        {
            return new List<Row>
            {
                new Row
                {
                    OrderRevers = 0, RowValue = 6885, RowValueWithStellenwert = 6885,
                    Digits = new List<Digit>
                    {
                        new Digit{ Order = 0, DigitValue = 5, CarryOver = 4 },
                        new Digit{ Order = 1, DigitValue = 8, CarryOver = 5 },
                        new Digit{ Order = 2, DigitValue = 8, CarryOver = 6 },
                        new Digit{ Order = 3, DigitValue = 6, CarryOver = 0 },
                    }
                },
                new Row
                {
                    OrderRevers = 1, RowValue = 3060, RowValueWithStellenwert = 30600,
                    Digits = new List<Digit>
                    {
                        new Digit{ Order = 0, DigitValue = 0, CarryOver = 2 },
                        new Digit{ Order = 1, DigitValue = 6, CarryOver = 2 },
                        new Digit{ Order = 2, DigitValue = 0, CarryOver = 3 },
                        new Digit{ Order = 3, DigitValue = 3, CarryOver = 0 },
                    }
                },
                new Row
                {
                    OrderRevers = 2, RowValue = 2295, RowValueWithStellenwert = 229500,
                    Digits = new List<Digit>
                    {
                        new Digit{ Order = 0, DigitValue = 5, CarryOver = 1 },
                        new Digit{ Order = 1, DigitValue = 9, CarryOver = 1 },
                        new Digit{ Order = 2, DigitValue = 2, CarryOver = 2 },
                        new Digit{ Order = 3, DigitValue = 2, CarryOver = 0 },
                    }
                }
            };
        }

        private static List<Row> TestCase53_67()
        {
            return new List<Row>
            {
                new Row
                {
                    OrderRevers = 0, RowValue = 371, RowValueWithStellenwert = 371,
                    Digits = new List<Digit>
                    {
                        new Digit{ CarryOver = 2, DigitValue = 1, Order = 0 },
                        new Digit{ CarryOver = 3, DigitValue = 7, Order = 1 },
                        new Digit{ CarryOver = 0, DigitValue = 3, Order = 2 },
                    }
                },
                new Row
                {
                    OrderRevers = 1, RowValue = 318, RowValueWithStellenwert = 3180,
                    Digits = new List<Digit>
                    {
                        new Digit{ CarryOver = 1, DigitValue = 8, Order = 0 },
                        new Digit{ CarryOver = 3, DigitValue = 1, Order = 1 },
                        new Digit{ CarryOver = 0, DigitValue = 3, Order = 2 },
                    }
                }
            };
        }
    }//class
}

