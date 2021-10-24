using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MaMa.Settings
{
    public class SettingsFile
    {
        [JsonPropertyName("RuleSets")]
        public List<RuleSet> RuleSets { get; set; }

        public SettingsFile() => this.RuleSets = new List<RuleSet>();
    }
}
