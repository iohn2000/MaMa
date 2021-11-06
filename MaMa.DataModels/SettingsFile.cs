using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MaMa.DataModels
{
    public class SettingsFile
    {
        [JsonPropertyName("RuleSets")]
        public Dictionary<string,RuleSet> RuleSets { get; set; }

        public SettingsFile() => this.RuleSets = new Dictionary<string,RuleSet>();
    }
}
