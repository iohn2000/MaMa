using System;
using Microsoft.Extensions.DependencyInjection;
using MaMa.CalcGenerator;
using MaMa.DataModels;
using System.Collections.Generic;

namespace ConsoleMath
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ICalculator,Calculator>()
                .AddSingleton<NumberProperties>()
                .AddSingleton<SolutionProperties>()
                .AddSingleton<IRandomNumber,MaMa.CalcGenerator.RandomNumberGenerator>()
                .AddSingleton<ConsoleFormatter>()
                .BuildServiceProvider();
            
            ICalculator cc = serviceProvider.GetService<ICalculator>();
            ConsoleFormatter cf = serviceProvider.GetRequiredService<ConsoleFormatter>();

            NumberProperties p1 = new NumberProperties()
            {
                MaxDigits = null,
                MinValue = 1,
                MaxValue = 100,
                AllowNegative = false,
                MaxMoveKomma = 2
            };
            NumberProperties p2 = new NumberProperties()
            {
                MaxDigits = null,
                MinValue = 1,
                MaxValue = 100,
                AllowNegative = false,
                MaxMoveKomma = 2
            };
            SolutionProperties sln = new SolutionProperties()
            {
                AllowRational = true,
                AllowNegative = false,
                ShowAsRechenArt = EnumRechenArt.Division
            };
            RuleSet ruleSet = new RuleSet
            {
                FirstNumber = p1,
                SecondNumber = p2,
                SolutionCriteria = sln,
                AmountOfCalculations = 10
            };
            
            cc.GenerateNumbers(ruleSet);
            
            List<CalculationItem> result  = cc.GetGeneratedNumbers();

            
            cf.ShowSettings(ruleSet);
            cf.ShowRechnungen(result);
        }
    }
}
