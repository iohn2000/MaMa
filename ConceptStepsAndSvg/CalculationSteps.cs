using System.Globalization;
using MaMa.DataModels;

namespace ConceptStepsAndSvg;

public class CalculationSteps
{
    public CalculationSteps()
    {
        
    }

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

        List<Row> rows = new List<Row>();
            
        // go from the back
        int stellenwert = 0;
        foreach (char c2 in secondNrStr.Reverse())
        {
            int digitFaktor2 = int.Parse(c2.ToString());
            int order = 0;
            List<Digit> digits = new List<Digit>();
            int prevCarryOver = 0;
            
            foreach (char c1 in firstNrStr.Reverse())
            {
                int digitFaktor1 = int.Parse(c1.ToString());
                int produkt = digitFaktor2 * digitFaktor1 + prevCarryOver;
                int carryOver = produkt / 10; // get ZehnerStelle
               
                int einerStelle = produkt - (10 * carryOver);
                Digit theDigit = new Digit()
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
                digits.Add(new Digit{ CarryOver = 0, DigitValue = prevCarryOver, Order = order});
            }

            int rowValue = digitFaktor2 * int.Parse(firstNrStr);
            int produktWithStellenwert = rowValue * (int)Math.Pow(10, stellenwert);
            rows.Add(new Row
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
            CommaMoveCount = commaMoveCountFirstNumber+ commaMoveCountSecondNumber 
        };
    }

    private (int, string) ConvertToIntegerNumber(decimal decimalNr)
    {
        // get comma count
        int commaCount = (int)BitConverter.GetBytes(Decimal.GetBits(decimalNr)[3])[2];
        // move away comma 1.23 --> 123
        int intNumber = (int)(decimalNr * (decimal)Math.Pow(10f, (float)commaCount));
        return (commaCount,intNumber.ToString(CultureInfo.InvariantCulture));
    }
}

public struct MuiltiplicationStepsSolution
{
    public List<Row> Steps {  get; set; }
    public int CommaMoveCount { get; set; }

    public decimal GetProduct()
    {
        return (decimal) Steps.Sum(s=>s.RowValueWithStellenwert) / (decimal)Math.Pow(10f, CommaMoveCount); 
    }
}

public record Row
{
    public int OrderRevers { get; set; }
    public int RowValueWithStellenwert { get; set; }
    public int RowValue { get; set; }
    public List<Digit> Digits { get; set; }
}
public record Digit
{
    public int Order { get; set; }
    public int DigitValue { get; set; }
    public int CarryOver { get; set; }
}