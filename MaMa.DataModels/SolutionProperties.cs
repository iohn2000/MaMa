using System;

namespace MaMa.DataModels
{
    public class SolutionProperties
    {
        ///<summary>
        /// if set to false also irrational 
        ///</summary>
        ///<returns></returns>
        //[Obsolete("use EnumNumberClassification instead", true)]
        //public bool AllowRational { get; set; } = true;

        /// <summary>
        /// spefifies if solution is an integer, rational or irrational
        /// </summary>
        public EnumNumberClassification NumberClass { get; set; } = EnumNumberClassification.Integer;

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
