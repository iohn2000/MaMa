namespace MaMa.Settings
{
    public interface ISettingsManager
    {
        SettingsFile GetSettings(string fileName);
        void SaveSettings(string fileName, SettingsFile allRules);
    }
}