using System.Text.Json.Serialization;

namespace MaMa.Settings
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EnumRechenArt
    {
        Multiplikation,
        Division,
        Addition
    }
}


