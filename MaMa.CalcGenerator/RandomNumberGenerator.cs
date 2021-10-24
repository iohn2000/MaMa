using System;
using MaMa.DataModels;

namespace MaMa.CalcGenerator
{

    public class RandomNumberGenerator : IRandomNumber
    {
        public RandomNumberGenerator()
        {
            this.randomiser = new Random(DateTime.Now.Millisecond);
        }
        private Random randomiser;
        public decimal GetRandomNr(NumberProperties nrCfg)
        {
            decimal genNr;
            var kommaDivisor = Convert.ToInt32(Math.Pow(10, randomiser.Next(0, nrCfg.MaxMoveKomma + 1)));

            if (nrCfg.MaxValue != null & nrCfg.MinValue != null)
            {
                // use min/max
                var rndNumber = randomiser.Next(nrCfg.MinValue.Value, nrCfg.MaxValue.Value + 1);
                genNr = (decimal)rndNumber / (decimal)kommaDivisor;
            }
            else if (nrCfg.MaxDigits != null)
            {
                // use max digits
                var stellenFaktor = (int)(Math.Pow(10, nrCfg.MaxDigits.Value - 1));
                var rndNumber = randomiser.Next(stellenFaktor, stellenFaktor * 10 - 1);
                genNr = rndNumber / kommaDivisor;
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

            return genNr;
        }
    }
}