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
    public List<Row> CalculateMultiplicationSteps(CalculationItem item)
    {
        // komma zahlen :-o
        string firstNrSTr = item.FirstNumber.ToString(CultureInfo.InvariantCulture);
        string secondNrStr = item.SecondNumber.ToString(CultureInfo.InvariantCulture);

        List<Row> rows = new List<Row>();
            
        // go from the back
        int stellenwert = 0;
        foreach (char c2 in secondNrStr.Reverse())
        {
            int digitFaktor2 = int.Parse(c2.ToString());
            int order = 0;
            List<Digit> digits = new List<Digit>();
            int prevCarryOver = 0;
            
            foreach (char c1 in firstNrSTr.Reverse())
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

            int rowValue = digitFaktor2 * int.Parse(firstNrSTr);
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

        return rows;
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