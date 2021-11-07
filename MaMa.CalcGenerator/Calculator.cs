using System.Collections.Generic;
using MaMa.DataModels;
using Microsoft.Extensions.Logging;

namespace MaMa.CalcGenerator
{
    public class Calculator : ICalculator
    {
        private List<CalculationItem> calcList = new List<CalculationItem>();
        private readonly ILogger<Calculator> logger;
        private readonly IRandomNumber rndGenerator;

        public Calculator(ILogger<Calculator> logger, IRandomNumber rndGenerator)
        {
            this.calcList = new List<CalculationItem>();
            this.logger = logger;
            this.rndGenerator = rndGenerator;
        }

        public void GenerateNumbers(RuleSet ruleSet, string ruleSetName)
        {
            int amountCalculations = ruleSet.AmountOfCalculations;
            if (amountCalculations == 0) return;
            do
            {
                int rawNr = 0;
                bool slnCriteriaMet = false;
                decimal firstNumber, secondNumber, solution = decimal.Zero;
                bool errorFlag = false;
                do
                {
                    firstNumber = rndGenerator.GetRandomNr(ruleSet.FirstNumber, out rawNr);
                    secondNumber = rndGenerator.GetRandomNr(ruleSet.SecondNumber, out rawNr);

                    // check if solution meets criteria
                    switch (ruleSet.SolutionCriteria.ShowAsRechenArt)
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
                                    this.logger.LogDebug($"errorflag = true");
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
                        slnCriteriaMet = this.SolutionCriteriaMet(firstNumber, secondNumber, solution, ruleSet.SolutionCriteria);
                    }
                    else
                    {
                        slnCriteriaMet = false;
                    }
                    if (!slnCriteriaMet)
                    {
                        this.logger.LogDebug($"not valid, needs retry: {firstNumber} / {secondNumber} = { solution}");
                    }
                }
                while (slnCriteriaMet != true);
                // generate numbers

                this.logger.LogDebug($"WORKED: {firstNumber} / {secondNumber} = { solution}");
                calcList.Add(new CalculationItem(firstNumber, secondNumber, solution, ruleSet.SolutionCriteria.ShowAsRechenArt, ruleSetName));
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
