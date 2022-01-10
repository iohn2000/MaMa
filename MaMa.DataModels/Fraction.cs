using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MaMa.DataModels
{
    public class Fractions
    {
        [JsonPropertyName("numerator")]
        public NumberProperties Numerator { get; set; }
        [JsonPropertyName("denominator")]
        public NumberProperties Denominator { get; set; }
        [JsonPropertyName("solutionCriteria")]
        public SolutionProperties SolutionCriteria { get; set; }
        [JsonPropertyName("amount")]
        public int AmountOfCalculations { get; set; }
    }
}
