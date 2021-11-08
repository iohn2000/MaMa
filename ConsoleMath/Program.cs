using MaMa.CalcGenerator;
using MaMa.DataModels;
using MaMa.Settings;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;

namespace ConsoleMath
{
    class Program
    {
        /// <summary>
        /// Generate Random math problems
        /// </summary>
        /// <param name="showRules">show the content of rule sets json</param>
        /// <param name="r">full path to a json config file containing the rule sets</param>
        static void Main(bool showRules, string r = "RuleSets.json")
        {
            Console.WriteLine("MaMa - Mathe Maker");

            ServiceProvider serviceProvider = AddServices();

            ICalculator cc = serviceProvider.GetService<ICalculator>();
            ISettingsManager sMgr = serviceProvider.GetRequiredService<ISettingsManager>();
            ConsoleFormatter cf = serviceProvider.GetRequiredService<ConsoleFormatter>();

            Console.WriteLine($"Load rulesSets from file: {r}");
            SettingsFile settingsFile = sMgr.GetSettings(r);

            foreach (var ruleSet in settingsFile.RuleSets)
            {
                cc.GenerateNumbers(ruleSet.Value, ruleSet.Key);
            }

            List<CalculationItem> result = cc.GetGeneratedNumbers();

            if (showRules)
            {
                cf.ShowSettings(settingsFile.RuleSets); 
            }
            cf.ShowRechnungen(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
                .AddLogging(builder =>
                {
                    var logger = new LoggerConfiguration()
                                              .MinimumLevel.Error()
                                              .WriteTo.Console()
                                              .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                                              .CreateLogger();

                    builder.AddSerilog(logger);
                })

                .BuildServiceProvider();
        }
    }
}
