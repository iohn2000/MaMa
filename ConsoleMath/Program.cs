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

            ServiceProvider serviceProvider = AddServices();

            ICalculator cc = serviceProvider.GetService<ICalculator>();
            ISettingsManager sMgr = serviceProvider.GetRequiredService<ISettingsManager>();
            ConsoleFormatter cf = serviceProvider.GetRequiredService<ConsoleFormatter>();

            SettingsFile settingsFile = sMgr.GetSettings(args.Length==1 ? args[0] : "RuleSets.json");

            foreach (var ruleSet in settingsFile.RuleSets)
            {
                cc.GenerateNumbers(ruleSet.Value, ruleSet.Key);
            }

            List<CalculationItem> result = cc.GetGeneratedNumbers();

            cf.ShowSettings(settingsFile.RuleSets);
            cf.ShowRechnungen(result);
        }

        private static ServiceProvider AddServices()
        {
            return new ServiceCollection()
                .AddSingleton<ICalculator, Calculator>()
                .AddSingleton<NumberProperties>()
                .AddSingleton<SolutionProperties>()
                .AddSingleton<IRandomNumber, MaMa.CalcGenerator.RandomNumberGenerator>()
                .AddSingleton<ConsoleFormatter>()
                .AddSingleton<ISerializeSettings, JsonSerializeSettings>()
                .AddSingleton<ISettingsReader, SettingsFileReader>()
                .AddSingleton<ISettingsManager, JsonSettingsManager>()
                .BuildServiceProvider();
        }
    }
}
