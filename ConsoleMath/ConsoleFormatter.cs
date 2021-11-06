using MaMa.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMath
{
    public class ConsoleFormatter
    {
        //private string fileName;

        public ConsoleFormatter(string filename = null)
        {
            //this.fileName = filename;
            //if (this.fileName != null)
            //    File.AppendAllText(this.fileName, "--------------" + Environment.NewLine + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss") + Environment.NewLine);
        }

        public void ShowRechnungen(List<CalculationItem> calcList)
        {
            int firstMax = GetMaxDigitCount(calcList, c => c.FirstNumber);
            int secondMax = GetMaxDigitCount(calcList, c => c.SecondNumber);

            string lineTemplate = "";

            foreach (CalculationItem calcItem in calcList)
            {
                switch (calcItem.RechenArt)
                {
                    case EnumRechenArt.Multiplikation:
                        {
                            int productMax = GetMaxDigitCount(calcList, c =>
                            {
                                return c.FirstNumber * c.SecondNumber;
                            }
    );
                            lineTemplate = "{0,-" + firstMax.ToString() + "} x {1,-" + secondMax.ToString() + "} = {2,-" + productMax.ToString() + "}";
                            break;
                        }

                    case EnumRechenArt.Division:
                        {
                            int productMax = GetMaxDigitCount(calcList, c => c.FirstNumber * c.SecondNumber);
                            lineTemplate = "{2,-" + productMax.ToString() + "} / {1,-" + secondMax.ToString() + "} = {0,-" + firstMax.ToString() + "}";
                            break;
                        }

                    case EnumRechenArt.Addition:
                        {
                            int summeMax = GetMaxDigitCount(calcList, c => c.FirstNumber + c.SecondNumber);
                            lineTemplate = "{0,-" + firstMax.ToString() + "} + {1,-" + secondMax.ToString() + "} = {0,-" + summeMax.ToString() + "}";
                            break;
                        }
                }


                string formattedLine = string.Format(lineTemplate, 
                    calcItem.FirstNumber, calcItem.SecondNumber, calcItem.FirstNumber * calcItem.SecondNumber);
                Console.WriteLine(formattedLine);
                //if ((this.fileName != null))
                //    File.AppendAllText(this.fileName, formattedLine + Environment.NewLine);
            }
        }

        public void ShowSettings()
        {

        }

        private int GetMaxDigitCount(List<CalculationItem> calcList, Func<CalculationItem, decimal> member)
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
