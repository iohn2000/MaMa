namespace MaMa.DataModels.Interfaces
{
    public interface ISerializeSettings
    {
        string SerializeSettings(SettingsFile settings);
        SettingsFile DeserializeSettings(string settingsStr);
    }
}