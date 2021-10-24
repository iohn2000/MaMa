namespace MaMa.DataModels
{
    public interface ISerializeSettings
    {
        string SerializeSettings(SettingsFile settings);
        SettingsFile DeserializeSettings(string settingsStr);
    }
}