using MaMa.CalcGenerator;
using MaMa.DataModels;
using MaMa.DataModels.Interfaces;
using MaMa.Settings;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using ConceptStepsAndSvg;

namespace ConsoleMath
{
    class Program
    {
        private static Serilog.Core.Logger logger;
        /// <summary>
        /// Generate Random math problems
        /// </summary>
        /// <param name="showRules">show the content of rule sets json</param>
        /// <param name="r">full path to a json config file containing the rule sets</param>
        static void Main(bool showRules, string r = "RuleSets.json")
        {
            logger = new LoggerConfiguration()
                                             .MinimumLevel.Error()
                                             .WriteTo.Console()
                                             .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                                             .CreateLogger();

            Console.WriteLine("MaMa - Mathe Maker");

            ServiceProvider serviceProvider = AddServices();

            ICalculator cc = serviceProvider.GetService<ICalculator>();
            ISettingsManager sMgr = serviceProvider.GetRequiredService<ISettingsManager>();
            ConsoleFormatter cf = serviceProvider.GetRequiredService<ConsoleFormatter>();
            Concept concept = serviceProvider.GetRequiredService<Concept>();
            CalculationSteps steps = serviceProvider.GetRequiredService<CalculationSteps>();
            SvgRenderer renderer = serviceProvider.GetRequiredService<SvgRenderer>();

            Console.WriteLine($"Load rulesSets from file: {r}");
            SettingsFile settingsFile = sMgr.GetSettings(r);

            foreach (var ruleSet in settingsFile.BasicArithmeticalOperationSets)
            {
                try
                {
                    cc.GenerateNumbers(ruleSet.Value, ruleSet.Key);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "cannot generate numbers for this rule set.");
                    
                }
            }

            List<CalculationItem> result = cc.GetGeneratedNumbers();

            if (showRules)
            {
                cf.ShowSettings(settingsFile.BasicArithmeticalOperationSets); 
            }
            cf.ShowRechnungen(result);

            // testing out svg html rendering
            // Concept c = new(steps, renderer);
            Stopwatch w = new Stopwatch();
            w.Start();
            concept.Start(result);
            var time = w.Elapsed;
            w.Stop();
            Console.WriteLine($"Count:{result.Count} Time:{time.Milliseconds} ms");
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
                .AddSingleton<INumberClassifier,SolutionChecker>()
                .AddSingleton<Concept>()
                .AddSingleton<CalculationSteps>()
                .AddSingleton<SvgRenderer>()
                .AddLogging(builder =>
                {
                    builder.AddSerilog(logger);
                })

                .BuildServiceProvider();
        }
    }
}
