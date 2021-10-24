using System;
using System.Collections.Generic;
using MaMa.DataModels;

namespace MaMa.CalcGenerator
{
    public class Calculator : ICalculator
    {
        private List<CalculationItem> calcList = new List<CalculationItem>();

        public Calculator()
        {
            this.calcList = new List<CalculationItem>();
        }

        public void GenerateNumbers(NumberProperties firstNumberConfig, NumberProperties secondNumberConfig, SolutionProperties solutionConfig, int amountCalculations)
        {
            Random randomGenerator = new Random(DateTime.Now.Millisecond);
            do
            {
                bool slnCriteriaMet = false;
                decimal firstNumber, secondNumber;
                do
                {
                    firstNumber = GetRandomNr(firstNumberConfig, randomGenerator);
                    secondNumber = GetRandomNr(secondNumberConfig, randomGenerator);
                    decimal solution = decimal.Zero;
                    // check if solution meets criteria
                    switch (solutionConfig.ShowAsRechenArt)
                    {
                        case EnumRechenArt.Multiplikation:
                            {
                                solution = firstNumber * secondNumber;
                                break;
                            }

                        case EnumRechenArt.Division:
                            {
                                solution = (firstNumber * secondNumber) / secondNumber;
                                break;
                            }

                        case EnumRechenArt.Addition:
                            {
                                solution = firstNumber + secondNumber;
                                break;
                            }
                    }

                    slnCriteriaMet = this.solutionCriteriaMet(firstNumber, secondNumber, solution, solutionConfig);
                }
                while (slnCriteriaMet != true);
                // generate numbers


                calcList.Add(new CalculationItem(firstNumber, secondNumber, solutionConfig.ShowAsRechenArt));
                amountCalculations = amountCalculations - 1;
            }
            while (amountCalculations > 0);
        }

        private bool solutionCriteriaMet(decimal firstNr, decimal secondNr, decimal slnValue, SolutionProperties slnCfg)
        {
            if (!slnCfg.AllowNegative & slnValue < 0)
                return false;

            if (!slnCfg.AllowRational & ((int)slnValue != slnValue))
                return false;

            // still here
            return true;
        }

        public List<CalculationItem> GetGeneratedNumbers()
        {
            return this.calcList;
        }

        private decimal GetRandomNr(NumberProperties nrCfg, Random rnd)
        {
            decimal genNr;
            var kommaDivisor = Convert.ToInt32(Math.Pow(10, rnd.Next(0, nrCfg.MoveKomma + 1)));

            if (nrCfg.MaxValue != null & nrCfg.MinValue != null)
            {
                // use min/max
                var rndNumber = rnd.Next(nrCfg.MinValue.Value, nrCfg.MaxValue.Value + 1);
                genNr = rndNumber / kommaDivisor;
            }
            else if (nrCfg.MaxDigits != null)
            {
                // use max digits
                var stellenFaktor = (int)(Math.Pow(10, nrCfg.MaxDigits.Value - 1));
                var rndNumber = rnd.Next(stellenFaktor, stellenFaktor * 10 - 1);
                genNr = rndNumber / kommaDivisor;
            }
            else
                throw new ArgumentException("please set min/max value or maxdigits");

            // randomize negativ or not, if allowed
            if (nrCfg.AllowNegative)
            {
                var makeNegative = rnd.Next(0, 2) == 1;
                if (makeNegative)
                    genNr *= (-1);
            }

            return genNr;
        }

    }
}
