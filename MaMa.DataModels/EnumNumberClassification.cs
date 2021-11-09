using System.Text.Json.Serialization;

namespace MaMa.DataModels
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EnumNumberClassification
    {
        Integer, 
        RationalNonPeriodic,
        RationalPeriodic
    }
}
