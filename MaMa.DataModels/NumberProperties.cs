using System;

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
        public Nullable<int> MaxDigits { get; set; } = default(int?);

        ///<summary>
        /// how many places to move the comma to the left
        ///</summary>
        ///<returns></returns>
        public byte MoveKomma { get; set; }

        ///<summary>
        /// if maxvalue is null use this instead of MaxDigits
        /// this way divisor can be kept below a certain value, e.g. i want 2 digits but less than 50 and the comma 3 to the left --> 0,033
        ///</summary>
        ///<returns></returns>
        public Nullable<int> MaxValue { get; set; } = default(int?);

        ///<summary>
        /// if != null then minimum value for number
        ///</summary>
        ///<returns></returns>
        public Nullable<int> MinValue { get; set; } = default(int?);

        ///<summary>
        /// can number be negativ, the cfg values <see cref="MinValue"/> and <see cref="MaxValue"/>
        /// are mirror then,so Max value means -MaxValue, e.g. max 143 would be -143
        ///</summary>
        ///<returns></returns>
        public bool AllowNegative { get; set; } = false;
    }
}



