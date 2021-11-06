using System;
using Microsoft.Extensions.DependencyInjection;
using MaMa.CalcGenerator;
using MaMa.DataModels;
using System.Collections.Generic;
using MaMa.Settings;

namespace ConsoleMath
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MaMa - Mathe Maker");

            var serviceProvider = new ServiceCollection()
                .AddSingleton<ICalculator,Calculator>()
                .AddSingleton<NumberProperties>()
                .AddSingleton<SolutionProperties>()
                .AddSingleton<IRandomNumber,MaMa.CalcGenerator.RandomNumberGenerator>()
                .AddSingleton<ConsoleFormatter>()
                .AddSingleton<ISerializeSettings, JsonSerializeSettings>()
                .AddSingleton<ISettingsReader, SettingsFileReader>()
                .AddSingleton<ISettingsManager, JsonSettingsManager>()
                .BuildServiceProvider();
            
            ICalculator cc = serviceProvider.GetService<ICalculator>();
            ConsoleFormatter cf = serviceProvider.GetRequiredService<ConsoleFormatter>();
            //ISettingsReader sReader = serviceProvider.GetRequiredService<ISettingsReader>();
            ISettingsManager sMgr = serviceProvider.GetRequiredService<ISettingsManager>();


            SettingsFile settingsFile = sMgr.GetSettings("RuleSets.json");

            #region hard coded settings
            //NumberProperties p1 = new NumberProperties()
            //{
            //    MaxDigits = null,
            //    MinValue = 1,
            //    MaxValue = 100,
            //    AllowNegative = false,
            //    MaxMoveKomma = 2
            //};
            //NumberProperties p2 = new NumberProperties()
            //{
            //    MaxDigits = null,
            //    MinValue = 1,
            //    MaxValue = 100,
            //    AllowNegative = false,
            //    MaxMoveKomma = 2
            //};
            //SolutionProperties sln = new SolutionProperties()
            //{
            //    AllowRational = true,
            //    AllowNegative = false,
            //    ShowAsRechenArt = EnumRechenArt.Division
            //};
            //RuleSet ruleSet = new RuleSet
            //{
            //    FirstNumber = p1,
            //    SecondNumber = p2,
            //    SolutionCriteria = sln,
            //    AmountOfCalculations = 10
            //}; 
            #endregion

            foreach(var ruleSet in settingsFile.RuleSets)
            {
                cc.GenerateNumbers(ruleSet.Value, ruleSet.Key);
            }
            
            
            List<CalculationItem> result  = cc.GetGeneratedNumbers();

            
            cf.ShowSettings(settingsFile.RuleSets);
            cf.ShowRechnungen(result);
        }
    }
}
