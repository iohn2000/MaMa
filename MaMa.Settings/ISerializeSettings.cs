namespace MaMa.Settings
{
    public interface ISerializeSettings
    {
        string SerializeSettings(SettingsFile settings);
        SettingsFile DeserializeSettings(string settingsStr);
    }
}