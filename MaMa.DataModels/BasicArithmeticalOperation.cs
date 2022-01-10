using System.Text.Json.Serialization;

namespace MaMa.DataModels
{
    ///<summary>
    /// keeps a set of settings together and is used to serialize
    ///</summary>
    public class BasicArithmeticalOperation
    {
        [JsonPropertyName("firstNumber")]
        public NumberProperties FirstNumber { get; set; }
        [JsonPropertyName("secondNumber")]
        public NumberProperties SecondNumber { get; set; }
        [JsonPropertyName("solutionCriteria")]
        public SolutionProperties SolutionCriteria { get; set; }
        [JsonPropertyName("amount")]
        public int AmountOfCalculations { get; set; }
    }
}


