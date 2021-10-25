using System;
namespace MaMa.DataModels
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
        /// solution of rechenart
        /// </summary>
        /// <value></value>
        public decimal Solution { get; set; }
        /// <summary>
        /// type of calculation
        /// </summary>
        /// <returns></returns>
        public EnumRechenArt RechenArt { get; set; }
        public CalculationItem(decimal nr1, decimal nr2, decimal sln, EnumRechenArt rechnArt)
        {
            this.FirstNumber = nr1;
            this.SecondNumber = nr2;
            this.Solution = sln;
            this.RechenArt = rechnArt;
        }
    }
}