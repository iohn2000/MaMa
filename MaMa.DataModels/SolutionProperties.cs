using System;

namespace MaMa.DataModels
{
    public class SolutionProperties
    {
        /// <summary>
        /// spefifies if solution is an integer, rational or irrational
        /// </summary>
        public EnumNumberClassification NumberClass { get; set; } = EnumNumberClassification.Integer;


        /// <summary>
        /// in case number class == <see cref="EnumNumberClassification.RationalNonPeriodic"></see> you can limit the number of digits after the comma
        /// -1 means doesnt matter
        /// </summary>
        public int AmountOfDigitsAfterComma { get; set; } = -1; 

        ///<summary>
        ///allow negative numbers
        ///</summary>
        ///<returns></returns>
        public bool AllowNegative { get; set; } = false;

        ///<summary>
        /// do you want to show numbes as division or multiplikation, uses number2 as divisor
        ///</summary>
        ///<returns></returns>
        public EnumRechenArt ShowAsRechenArt { get; set; }
    }
}
