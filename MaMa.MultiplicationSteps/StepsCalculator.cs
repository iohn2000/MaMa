using MaMa.DataModels;
using MaMa.DataModels.MultiplicationSteps;
using System.Globalization;

namespace MaMa.MultiplicationSteps
{
    public class StepsCalculator
    {
        /// <summary>
        /// as first step only works for integers
        /// </summary>
        /// <param name="item"></param>
        public MuiltiplicationStepsSolution CalculateMultiplicationSteps(CalculationItem item)
        {
            // handle numbers that are decimals (and not integers)
            // move comma so far that they are integers and remember the amount of places comma needs to move

            (int commaMoveCountFirstNumber, string firstNrStr) = this.ConvertToIntegerNumber(item.FirstNumber);
            (int commaMoveCountSecondNumber, string secondNrStr) = this.ConvertToIntegerNumber(item.SecondNumber);

            List<RowMultiplication> rows = new();

            // go from the back
            int stellenwert = 0;
            foreach (char c2 in secondNrStr.Reverse())
            {
                int digitFaktor2 = int.Parse(c2.ToString());
                int order = 0;
                List<DigitMultiplication> digits = new List<DigitMultiplication>();
                int prevCarryOver = 0;

                foreach (char c1 in firstNrStr.Reverse())
                {
                    int digitFaktor1 = int.Parse(c1.ToString());
                    int produkt = digitFaktor2 * digitFaktor1 + prevCarryOver;
                    int carryOver = produkt / 10; // get ZehnerStelle

                    int einerStelle = produkt - (10 * carryOver);
                    DigitMultiplication theDigit = new DigitMultiplication()
                    {
                        DigitValue = einerStelle,
                        CarryOver = carryOver,
                        Order = order
                    };
                    digits.Add(theDigit);
                    prevCarryOver = carryOver; // remember for next round
                    order++;
                }
                // add last (left most) digit if carry over > 0 
                // digit equas carry over (zehner stelle)
                if (prevCarryOver > 0)
                {
                    digits.Add(new DigitMultiplication { CarryOver = 0, DigitValue = prevCarryOver, Order = order });
                }

                int rowValue = digitFaktor2 * int.Parse(firstNrStr);
                int produktWithStellenwert = rowValue * (int)Math.Pow(10, stellenwert);
                rows.Add(new RowMultiplication
                {
                    Digits = digits,
                    RowValueWithStellenwert = produktWithStellenwert,
                    RowValue = rowValue,
                    OrderRevers = stellenwert
                });
                stellenwert++;
            }

            return new MuiltiplicationStepsSolution
            {
                Steps = rows,
                CommaMoveCount = commaMoveCountFirstNumber + commaMoveCountSecondNumber
            };
        }

        private (int, string) ConvertToIntegerNumber(decimal decimalNr)
        {
            // get comma count --> should use https://learn.microsoft.com/en-us/dotnet/api/system.decimal.scale?view=net-7.0
            int commaCount = (int)BitConverter.GetBytes(Decimal.GetBits(decimalNr)[3])[2];
            // move away comma 1.23 --> 123
            int intNumber = (int)(decimalNr * (decimal)Math.Pow(10f, (float)commaCount));
            return (commaCount, intNumber.ToString(CultureInfo.InvariantCulture));
        }
    }
}
