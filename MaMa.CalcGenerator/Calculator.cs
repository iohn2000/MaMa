using System;
using System.Collections.Generic;
using MaMa.DataModels;
using Microsoft.Extensions.Logging;

namespace MaMa.CalcGenerator
{
    public class Calculator : ICalculator
    {
        private const int MAXTRIES = 2000;
        private List<CalculationItem> calcList = new List<CalculationItem>();
        private readonly ILogger<Calculator> logger;
        private readonly IRandomNumber rndGenerator;
        private readonly INumberClassifier soltionChecker;

        public Calculator(ILogger<Calculator> logger, IRandomNumber rndGenerator, INumberClassifier nrClass)
        {
            this.calcList = new List<CalculationItem>();
            this.logger = logger;
            this.rndGenerator = rndGenerator;
            this.soltionChecker = nrClass;
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
                int attempts = 0;
                do
                {
                    attempts++;
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
                while (slnCriteriaMet != true && attempts <= MAXTRIES);
                
                if (attempts < MAXTRIES)
                {
                    this.logger.LogDebug($"WORKED: {firstNumber} | {secondNumber} = { solution}");
                    calcList.Add(new CalculationItem(firstNumber, secondNumber, solution, ruleSet.SolutionCriteria.ShowAsRechenArt, ruleSetName));
                    amountCalculations = amountCalculations - 1;
                }
                else
                {
                    throw new Exception($"could not generate a solution that meets criteria.");
                }
                
            }
            while (amountCalculations > 0);
        }

        public bool SolutionCriteriaMet(decimal firstNr, decimal secondNr, decimal slnValue, SolutionProperties slnCfg)
        {
            // criteria allow negative
            bool alloeNegMet = true;
            if (!slnCfg.AllowNegative & slnValue < 0m)
                alloeNegMet = false;

            //
            // nr classification checks
            //
            bool nrClassificationMet = false;
            bool isNonPeriodic = true;
            int commaCount = -1;


            switch (slnCfg.ShowAsRechenArt)
            {
                case EnumRechenArt.Multiplikation:
                    (isNonPeriodic, commaCount) = this.soltionChecker.CalcPeriodicity(firstNr, 1 / secondNr);
                    break;
                case EnumRechenArt.Division:
                    (isNonPeriodic, commaCount) = this.soltionChecker.CalcPeriodicity(firstNr, secondNr);
                    break;
                case EnumRechenArt.Addition:
                    isNonPeriodic = true;
                    (_, commaCount) = this.soltionChecker.MakeInteger(slnValue);
                    break;
                default:
                    break;
            }

            // any 
            if (slnCfg.NumberClass == EnumNumberClassification.Any)
                nrClassificationMet = true;

            // integer
            else if (slnCfg.NumberClass == EnumNumberClassification.Integer)
            {
                if (!isNonPeriodic || commaCount > 0)
                    nrClassificationMet = false;
                else
                    nrClassificationMet = true;
            }
            // rational periodic
            else if (slnCfg.NumberClass == EnumNumberClassification.RationalNonPeriodic)
            {
                if (isNonPeriodic)
                {
                    if (slnCfg.AmountOfDigitsAfterComma > -1) // check amount commas
                    {
                        // get amount of commas make sure its wihtin limits
                        if (isNonPeriodic && commaCount <= slnCfg.AmountOfDigitsAfterComma) // all ok
                        {
                            return true;
                        }
                        else // comma criteria not met
                        {
                            return false;
                        }
                    }
                    else // class is ok, commas dont matter
                    {
                        nrClassificationMet = true;
                    }

                }
                else // wrong class of nr
                {
                    nrClassificationMet = false;
                }

            }
            else // unkown class ?
            {
                nrClassificationMet = false;
            }

            // still here
            return alloeNegMet && nrClassificationMet;
        }

        public List<CalculationItem> GetGeneratedNumbers()
        {
            return this.calcList;
        }
    }
}
