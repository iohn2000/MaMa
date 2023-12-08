using MaMa.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaMa.CalcGenerator
{
    public class SolutionChecker : INumberClassifier
    {
        //public EnumNumberClassification GetClassOfNumber(decimal theNumber)
        //{
        //    if ((int)theNumber == theNumber)
        //    {
        //        return EnumNumberClassification.Integer;
        //    }
        //    else
        //    {
        //        return EnumNumberClassification.RationalNonPeriodic;
        //    }
        //    throw new Exception($"cannot calculate class of number: {theNumber}");
        //}
        
        /// <summary>
        /// return amount of commas if number is non periodic, if number is periodic returns -1
        /// </summary>
        /// <param name="dividend"></param>
        /// <param name="divisor"></param>
        /// <returns></returns>
        public (bool isNonPeriodic, int commaCount) CalcPeriodicity(decimal dividend, decimal divisor)
        {
            // prep fraction so calcs can be done on it
            // 1) remove commas  1.23 -> 123 -> 10^2
            var divisorResult = MakeInteger(divisor);
            var dividendResult = MakeInteger(dividend);
            var integerFactor = (int)Math.Pow(10, Math.Max(dividendResult.potenzenCount, divisorResult.potenzenCount));
            var dividendInteger = (long)(dividend * integerFactor);
            var divisorInteger = (long)(divisor * integerFactor);

            // 2) bruch kürzen
            (_, divisorInteger) = this.BruchKürzen(dividendInteger, divisorInteger);

            // 3) non periodic?
            bool isNonPeriodic;
            int commaCount = -1;
            if (divisorInteger == 1) // keine kommastellen
            {
                isNonPeriodic = true;
                commaCount = 0;
            }
            else
            {
                // http://www.arndt-bruenner.de/mathe/scripts/periodenlaenge.htm
                List<long> primeFactors = GetPrimeFactors(divisorInteger);
                List<long> otherNumbers = new List<long> { 1, 3, 4, 6, 7, 8, 9 };

                // The List<T>.Intersect method in C# is used to find the common elements between two lists.
                bool containsNumberNot_2_or_5 = primeFactors.Intersect(otherNumbers).Any() || primeFactors.Exists(e=>e>9);

                isNonPeriodic = !containsNumberNot_2_or_5;
                if (isNonPeriodic)
                {
                    // 4) amount of commas - only when there is commas
                    commaCount = Math.Max(primeFactors.Count(p => p == 2),primeFactors.Count(p => p == 5));
                }
                else
                {
                    commaCount = -1;
                }
            }

            return (isNonPeriodic, commaCount);
        }

        public (long dividend, long divisor) BruchKürzen(long dividendInteger, long divisorInteger)
        {
            var ggT = this.GetGGT(dividendInteger, divisorInteger);
            dividendInteger = dividendInteger / ggT;
            divisorInteger = divisorInteger / ggT;
            return (dividendInteger, divisorInteger);
        }

        public (int integerNr, int potenzenCount) MakeInteger(decimal divisor)
        
            {
            int potenzen = 0;
            decimal newNr = divisor;

            if ((int)newNr == newNr)
            {
                return ((int)newNr, potenzen);
            }

            do
            {
                potenzen += 1;
                newNr *= 10;
            } while ((int)newNr != newNr);
            return ((int)newNr, potenzen);
        }

        /// <summary>
        /// if prime factors only contain 2 or 5 its a non periodic number
        /// http://www.arndt-bruenner.de/mathe/scripts/periodenlaenge.htm
        /// </summary>
        /// <param name="theNumber"></param>
        /// <returns></returns>
        //private (bool isNonPeriodic, long commaCount) isNumberNonPeriodic(long theNumber)
        //{

        //    return (twoFiveOnly, 0);
        //}

        public List<long> GetPrimeFactors(long theNumber)
        {
            List<long> result = new List<long>();

            // Take out the 2s.
            while (theNumber % 2 == 0)
            {
                result.Add(2);
                theNumber /= 2;
            }

            // Take out other primes.
            long factor = 3;
            while (factor * factor <= theNumber)
            {
                if (theNumber % factor == 0)
                {
                    // This is a factor.
                    result.Add(factor);
                    theNumber /= factor;
                }
                else
                {
                    // Go to the next odd number.
                    factor += 2;
                }
            }

            // If num is not 1, then whatever is left is prime.
            if (theNumber > 1) result.Add(theNumber);

            return result;
        }

        public long GetGGT(long a, long b)
        {
            long c = 1;
            while (c != 0)
            {
                c = a % b;
                a = b;
                b = c;
            }
            return a;
        }

        public bool IsInRange(int theNumber, string theRange)
        {
            if (string.IsNullOrWhiteSpace(theRange) && theRange.IndexOf("-") == -1)
            {
                throw new Exception($"Specified range in solution properties is not valid. range:'{theRange}'");
            }
            string[] range = theRange.Split("-",StringSplitOptions.RemoveEmptyEntries);
            if (range.Length != 2)
            {
                throw new Exception($"Specified range in solution properties is not valid. range:'{theRange}'");
            }
            return theNumber >= int.Parse(range[0]) && theNumber <= int.Parse(range[1]);
        }
    }
}
