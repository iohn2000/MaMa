using MaMa.DataModels;
using MaMa.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleMath
{
    /// <summary>
    /// format calculations for console
    /// </summary>
    public class ConsoleFormatter
    {
        //private string fileName;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="filename"></param>
        public ConsoleFormatter(string filename = null)
        {
            //this.fileName = filename;
            //if (this.fileName != null)
            //    File.AppendAllText(this.fileName, "--------------" + Environment.NewLine + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + Environment.NewLine);
        }

        /// <summary>
        /// write out calculations to console
        /// </summary>
        /// <param name="calcList"></param>
        public void ShowRechnungen(List<CalculationItem> calcList)
        {
            int firstMax = GetMaxDigitCount(calcList, c => c.FirstNumber);
            int secondMax = GetMaxDigitCount(calcList, c => c.SecondNumber);
            int solutionMax = GetMaxDigitCount(calcList, c => c.Solution);
            int ruleNameMax = GetMaxDigitCount(calcList,c=>c.RuleSetName);

            string lineTemplate = "";
            string ruleSetNameTemplate = "  -  {3," + ruleNameMax.ToString() + "}";
            foreach (CalculationItem calcItem in calcList)
            {
                switch (calcItem.RechenArt)
                {
                    case EnumRechenArt.Multiplikation:
                        {
                            lineTemplate = "{0,-" + firstMax.ToString() + "} x {1,-" + secondMax.ToString() + "} = {2,-" + solutionMax.ToString() + "}";
                            break;
                        }

                    case EnumRechenArt.Division:
                        {
                            lineTemplate = "{0,-" + firstMax.ToString() + "} / {1,-" + secondMax.ToString() + "} = {2,-" + solutionMax.ToString() + "}";
                            break;
                        }

                    case EnumRechenArt.Addition:
                        {
                            lineTemplate = "{0,-" + firstMax.ToString() + "} + {1,-" + secondMax.ToString() + "} = {2,-" + solutionMax.ToString() + "}";
                            break;
                        }
                    case EnumRechenArt.Subtraction:
                        {
                            lineTemplate = "{0,-" + firstMax.ToString() + "} - {1,-" + secondMax.ToString() + "} = {2,-" + solutionMax.ToString() + "}";
                            break;
                        }
                }


                string formattedLine = string.Format(lineTemplate + ruleSetNameTemplate,
                    calcItem.FirstNumber, calcItem.SecondNumber, calcItem.Solution, calcItem.RuleSetName);
                Console.WriteLine(formattedLine);
            }
        }

        /// <summary>
        /// display rules for generating calculations
        /// </summary>
        /// <param name="ruleSets"></param>
        public void ShowSettings(Dictionary<string,BasicArithmeticalOperation> ruleSets)
        {
            JsonSerializeSettings jSettings = new JsonSerializeSettings();
            string settingJson = jSettings.SerializeSettings(new SettingsFile
            {
                BasicArithmeticalOperationSets =  ruleSets
            });
            Console.WriteLine(settingJson);
        }

        private int GetMaxDigitCount(List<CalculationItem> calcList, Func<CalculationItem, object> member)
        {
            var biggestItem = calcList.OrderByDescending(p => member(p).ToString().Length).FirstOrDefault();
            var m = member(biggestItem).ToString().Length;
            return m;
        }

        private string KommaAlign(decimal number, int maxDigits)
        {
            string numberStr = number.ToString();
            int pos = numberStr.IndexOf(",");
            if (pos == -1)
                pos = numberStr.Length;

            return new string(' ', maxDigits - pos) + numberStr;
        }
    }
}
