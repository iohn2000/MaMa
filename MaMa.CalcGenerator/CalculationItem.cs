using MaMa.DataModels;
using MaMa.Settings;

namespace MaMa.CalcGenerator
{
    public class CalculationItem
    {
        /// <summary>
        /// first number
        /// </summary>
        /// <returns></returns>
        public decimal FirstNumber { get; set; }
        /// <summary>
        /// second number
        /// </summary>
        /// <returns></returns>
        public decimal SecondNumber { get; set; }
        /// <summary>
        /// type of calculation
        /// </summary>
        /// <returns></returns>
        public EnumRechenArt RechenArt { get; set; }

        public CalculationItem(decimal nr1, decimal nr2, EnumRechenArt rechnArt)
        {
            this.FirstNumber = nr1;
            this.SecondNumber = nr2;
            this.RechenArt = rechnArt;
        }
    }
}