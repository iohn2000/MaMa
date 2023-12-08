﻿using System.Text.Json.Serialization;

namespace MaMa.DataModels
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EnumNumberClassification
    {
        Integer, 
        RationalNonPeriodic,
        RationalPeriodic,
        Any,

        /// <summary>
        /// A rational number with a decimal representation that neither terminates nor repeats is called a "non-repeating, non-terminating decimal." 
        /// In other words, the decimal expansion of such a rational number goes on forever without forming a repeating pattern.
        /// An example of a non-repeating, non-terminating decimal is 1/7 , which results in the decimal 0.142857142857...
        /// While the digits don't repeat in a simple block, the decimal expansion continues indefinitely without terminating.
        /// These decimals are less common and often have more complex and seemingly random patterns as they extend further.
        /// </summary>
        RationalNonTerminatingDecimals,

        /// <summary>
        /// A rational number is said to have a terminating decimal representation 
        /// if its decimal representation ends after a finite number of digits.
        /// For example, the fraction 1/4 is equal to 0.25. 
        /// The decimal representation terminates after two digits.
        /// </summary>
        RationalTerminatingDecimals,

        /// <summary>
        /// A rational number is said to have a repeating decimal representation 
        /// if its decimal representation goes on forever but repeats a block of digits indefinitely.
        /// For example, the fraction 1/3 is equal to 0.333... where the digit 3 repeats infinitely.
        /// </summary>
        RationalRepeatingDecimals
    }
}
