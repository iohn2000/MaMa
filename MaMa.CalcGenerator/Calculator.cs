using System;
using System.Collections.Generic;
using MaMa.DataModels;
using Microsoft.Extensions.Logging;

namespace MaMa.CalcGenerator
{
    public class Calculator : ICalculator
    {
        private const int MAXTRIES = 4000;
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
            this.logger.LogDebug($"ctor Calculator");
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
                    switch (ruleSet.SolutionCriteria.ElementaryArithmetic)
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

                        case EnumRechenArt.Subtraction:
                            {
                                solution = firstNumber - secondNumber;
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
                    calcList.Add(new CalculationItem(firstNumber, secondNumber, solution, ruleSet.SolutionCriteria.ElementaryArithmetic, ruleSetName));
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


            switch (slnCfg.ElementaryArithmetic)
            {
                case EnumRechenArt.Multiplikation:
                    isNonPeriodic = true;
                    (_, int c1) = this.soltionChecker.MakeInteger(firstNr);
                    (_, int c2) = this.soltionChecker.MakeInteger(secondNr);
                    commaCount = c1 + c2;
                    break;
                case EnumRechenArt.Division:
                    (isNonPeriodic, commaCount) = this.soltionChecker.CalcPeriodicity(firstNr, secondNr);
                    break;
                case EnumRechenArt.Addition:
                    isNonPeriodic = true;
                    (_, commaCount) = this.soltionChecker.MakeInteger(slnValue);
                    break;
                case EnumRechenArt.Subtraction:
                    isNonPeriodic = true;
                    (_, commaCount) = this.soltionChecker.MakeInteger(slnValue);
                    break;
                default:
                    break;
            }

            // any 
            if (slnCfg.NumberClass == EnumNumberClassification.Any)
            { 
                nrClassificationMet = true;
            }
            // integer
            else if (slnCfg.NumberClass == EnumNumberClassification.Integer)
            {
                if (!isNonPeriodic || commaCount > 0) // !isNonPeriodic == komma zahl mit endlichen komma stellen
                    nrClassificationMet = false;
                else
                    nrClassificationMet = true;
            }
            // rational periodic
            else if (slnCfg.NumberClass == EnumNumberClassification.RationalNonPeriodic)
            {
                if (isNonPeriodic)
                {
                    if (!string.IsNullOrWhiteSpace(slnCfg.DigitsAfterCommaRange)) // check amount commas
                    {
                        // get amount of commas make sure its wihtin limits
                        if (isNonPeriodic && this.soltionChecker.IsInRange(commaCount, slnCfg.DigitsAfterCommaRange)) // all ok
                        {
                            nrClassificationMet = true;
                        }
                        else // comma criteria not met
                        {
                            nrClassificationMet = false;
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
