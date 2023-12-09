using System;
using MaMa.DataModels;

namespace MaMa.CalcGenerator
{

    public class RandomNumberGenerator : IRandomNumber
    {
        private Random randomiser;

        public RandomNumberGenerator()
        {
            this.randomiser = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        }
        
        public decimal GetRandomNr(NumberProperties nrCfg, out int rawNumber)
        {
            decimal genNr;
            int rndNr;
            var commaDivisor = Convert.ToInt32(Math.Pow(10, randomiser.Next(0, nrCfg.MaxMoveKomma + 1)));

            if (nrCfg.MaxValue != null & nrCfg.MinValue != null)
            {
                // use min/max
                rndNr = randomiser.Next(nrCfg.MinValue.Value, nrCfg.MaxValue.Value + 1);
                genNr = (decimal)rndNr / (decimal)commaDivisor;
            }
            else if (nrCfg.MaxDigits != null)
            {
                // use max digits
                var stellenFaktor = (int)(Math.Pow(10, nrCfg.MaxDigits.Value - 1));
                rndNr = randomiser.Next(stellenFaktor, stellenFaktor * 10 - 1);
                genNr = rndNr / commaDivisor;
            }
            else
                throw new ArgumentException("please set min/max value or maxdigits");

            // randomize negativ or not, if allowed
            if (nrCfg.AllowNegative)
            {
                var makeNegative = randomiser.Next(0, 2) == 1;
                if (makeNegative)
                    genNr *= (-1);
            }
            rawNumber = rndNr;
            return genNr;
        }
    }
}