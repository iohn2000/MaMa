using System.Collections.Generic;
using MaMa.DataModels;

namespace MaMa.CalcGenerator
{
    public interface ICalculator
    {
        //void GenerateNumbers(NumberProperties firstNumberConfig, NumberProperties secondNumberConfig, SolutionProperties solutionConfig, int amountCalculations);
        void GenerateNumbers(RuleSet ruleSet, string ruleSetName);
        bool SolutionCriteriaMet(decimal firstNr, decimal secondNr, decimal slnValue, SolutionProperties slnCfg);
        List<CalculationItem> GetGeneratedNumbers();
    }
}