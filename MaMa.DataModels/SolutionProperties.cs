using System;
using System.Text.Json.Serialization;

namespace MaMa.DataModels
{
    public class SolutionProperties
    {
        /// <summary>
        /// spefifies if solution is an integer, rational or irrational
        /// </summary>
        [JsonPropertyName("numberClass")]
        public EnumNumberClassification NumberClass { get; set; } = EnumNumberClassification.Integer;


        /// <summary>
        /// in case number class == <see cref="EnumNumberClassification.RationalTerminatingDecimals"></see> you can limit the number of digits after the comma
        /// empty means no range
        /// </summary>
        [JsonPropertyName("digitsAfterCommaRange")]
        public string DigitsAfterCommaRange { get; set; } = "";

        ///<summary>
        ///allow negative numbers
        ///</summary>
        ///<returns></returns>
        [JsonPropertyName("allowNegative")]
        public bool AllowNegative { get; set; } = false;

        ///<summary>
        /// do you want to show numbes as division or multiplikation, uses number2 as divisor
        ///</summary>
        ///<returns></returns>
        [JsonPropertyName("elementaryArithmetic")]
        public EnumRechenArt ElementaryArithmetic { get; set; }
    }
}
