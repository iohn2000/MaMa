using System.Text.Json;

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
    string ISerializeSettings.SerializeSettings(SettingsFile settings)
    {
        JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
        var str = JsonSerializer.Serialize(settings, options);
        return str;
    }
}    
}
