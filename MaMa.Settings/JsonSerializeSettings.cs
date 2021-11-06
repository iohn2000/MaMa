using System.Text.Json;
using MaMa.DataModels;

namespace MaMa.Settings
{
public class JsonSerializeSettings : ISerializeSettings
{
    public SettingsFile DeserializeSettings(string settingsStr)
    {
        return JsonSerializer.Deserialize<SettingsFile>(settingsStr);
    }

    /// <summary>
    /// Serialise <see cref="SettingsFile"/> object to string
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    public string SerializeSettings(SettingsFile settings)
    {
        JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
        var str = JsonSerializer.Serialize(settings, options);
        return str;
    }
}    
}
