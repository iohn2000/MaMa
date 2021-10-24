using System.Collections.Generic;
using MaMa.DataModels;

namespace MaMa.CalcGenerator
{
    public interface ICalculator
    {
        void GenerateNumbers(NumberProperties firstNumberConfig, NumberProperties secondNumberConfig, SolutionProperties solutionConfig, int amountCalculations);
        List<CalculationItem> GetGeneratedNumbers();
    }
}