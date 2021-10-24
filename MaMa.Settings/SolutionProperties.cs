namespace MaMa.Settings
{
    public class SolutionProperties
    {
        ///<summary>
        /// if set to false also irrational 
        ///</summary>
        ///<returns></returns>
        public bool AllowRational { get; set; } = true;

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
