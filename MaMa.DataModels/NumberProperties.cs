using System;
using System.Text.Json.Serialization;

namespace MaMa.DataModels
{
    ///<summary>
    /// define properties for a randomly generated number
    /// Min,Max Values has priority over MaxDigits
    ///</summary>
    public class NumberProperties
    {
        ///<summary>
        /// if > -1 then this sets the amount of digits in the number, e.g. 3 = anything from 100 to 999
        ///</summary>
        ///<returns></returns>
        [JsonPropertyName("MaxDigits")]
        public Nullable<int> MaxDigits { get; set; } = default(int?);

        ///<summary>
        /// maximum places to move the comma to the left
        ///</summary>
        ///<returns></returns>
        [JsonPropertyName("MoveKomma")]
        public byte MaxMoveKomma { get; set; }

        ///<summary>
        /// max value for number BEFORE the komme is inserted
        /// this way divisor can be kept below a certain value, e.g. i want 2 digits but less than 50 and the comma 3 to the left --> 0,033
        /// size of number is only relevant before komma move, divide by 44 or 4,4 or 0,44 is the same difficulty level
        /// if maxvalue is null use this instead of MaxDigits
        ///</summary>
        ///<returns></returns>
        [JsonPropertyName("MaxValue")]
        public Nullable<int> MaxValue { get; set; } = default(int?);

        ///<summary>
        /// if != null then minimum value for number BEFORE the komme is inserted
        /// size of number is only relevant before komma move, divide by 44 or 4,4 or 0,44 is the same difficulty level
        ///</summary>
        ///<returns></returns>
        [JsonPropertyName("MinValue")]
        public Nullable<int> MinValue { get; set; } = default(int?);

        ///<summary>
        /// can number be negativ, the cfg values <see cref="MinValue"/> and <see cref="MaxValue"/>
        /// are mirror then,so Max value means -MaxValue, e.g. max 143 would be -143
        ///</summary>
        ///<returns></returns>
        [JsonPropertyName("AllowNegative")]
        public bool AllowNegative { get; set; } = false;
    }
}



