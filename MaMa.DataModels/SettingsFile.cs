using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MaMa.DataModels
{
    public class SettingsFile
    {
        [JsonPropertyName("BasicArithmeticalOperation")]
        public Dictionary<string, BasicArithmeticalOperation> BasicArithmeticalOperationSets { get; set; }

        [JsonPropertyName("Fractions")]
        public Dictionary<string, Fractions> FractionSets { get; set; }

        public SettingsFile() => this.BasicArithmeticalOperationSets = new Dictionary<string,BasicArithmeticalOperation>();
    }
}
