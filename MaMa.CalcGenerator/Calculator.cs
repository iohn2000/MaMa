using System;
using System.Collections.Generic;
using MaMa.DataModels;

namespace MaMa.CalcGenerator
{
    public class Calculator : ICalculator
    {
        private List<CalculationItem> calcList = new List<CalculationItem>();
        private readonly IRandomNumber rndGenerator;

        public Calculator(IRandomNumber rndGenerator)
        {
            this.calcList = new List<CalculationItem>();
            this.rndGenerator = rndGenerator;
        }

        public void GenerateNumbers(NumberProperties firstNumberConfig, NumberProperties secondNumberConfig, SolutionProperties solutionConfig, int amountCalculations)
        {
            do
            {
                bool slnCriteriaMet = false;
                decimal firstNumber, secondNumber, solution = decimal.Zero;
                bool errorFlag = false;
                do
                {
                    firstNumber = rndGenerator.GetRandomNr(firstNumberConfig);
                    secondNumber = rndGenerator.GetRandomNr(secondNumberConfig);
                    
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
                                if (secondNumber != decimal.Zero)
                                {
                                    solution = firstNumber / secondNumber;
                                }
                                else
                                {
                                    errorFlag = true;
                                }
                                break;
                            }

                        case EnumRechenArt.Addition:
                            {
                                solution = firstNumber + secondNumber;
                                break;
                            }
                    }
                    if (!errorFlag)
                    {
                        slnCriteriaMet = this.SolutionCriteriaMet(firstNumber, secondNumber, solution, solutionConfig);
                    }
                    else
                    {
                        slnCriteriaMet = false;
                    }
                }
                while (slnCriteriaMet != true);
                // generate numbers


                calcList.Add(new CalculationItem(firstNumber, secondNumber, solution, solutionConfig.ShowAsRechenArt));
                amountCalculations = amountCalculations - 1;
            }
            while (amountCalculations > 0);
        }

        public bool SolutionCriteriaMet(decimal firstNr, decimal secondNr, decimal slnValue, SolutionProperties slnCfg)
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
    }
}
