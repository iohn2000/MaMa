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
                .BuildServiceProvider();
            
            ICalculator cc = serviceProvider.GetService<ICalculator>();
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
            cc.GenerateNumbers(p1,p2,sln,10);
            List<CalculationItem> result  = cc.GetGeneratedNumbers();

            ConsoleFormatter cf = new ConsoleFormatter();
            cf.ShowRechnungen(result);
        }
    }
}
