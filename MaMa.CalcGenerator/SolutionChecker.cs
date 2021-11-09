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
        public EnumNumberClassification GetClassOfNumber(decimal theNumber)
        {
            if ((int)theNumber == theNumber)
            {
                return EnumNumberClassification.Integer;
            }
            else
            {
                return EnumNumberClassification.RationalNonPeriodic;
            }
            throw new Exception($"cannot calculate class of number: {theNumber}");
        }
        /// <summary>
        /// return amount of commas if number is non periodic, if number is periodic returns -1
        /// </summary>
        /// <param name="dividend"></param>
        /// <param name="divisor"></param>
        /// <returns></returns>
        public int GetNonPeriodicCommaCount(decimal dividend, decimal divisor)
        {
            // prep fraction so calcs can be done on it
            // 1) remove commas  1.23 -> 123 -> 10^2
            var disisorResult = MakeInteger(divisor);
            var divedendResult = MakeInteger(dividend);

            //    // 2) bruch kürzen
            //    // 3) non periodic?
            //    List<long> primeFactors = GetPrimeFactors(divisor);
            //    bool twoFiveOnly = primeFactors.Exists(c => c != 2 || c != 5);
            //    // 4) amount of commas

            return 1;
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
        /// if prime factors onyl contain 2 or 5 its a non periodic number
        /// http://www.arndt-bruenner.de/mathe/scripts/periodenlaenge.htm
        /// </summary>
        /// <param name="theNumber"></param>
        /// <returns></returns>
        //private (bool isNonPeriodic, long commaCount) isNumberNonPeriodic(long theNumber)
        //{

        //    return (twoFiveOnly, 0);
        //}

        private List<long> GetPrimeFactors(long theNumber)
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

        private long GetGGT(long a, long b)
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
    }
}
