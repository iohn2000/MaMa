namespace MaMa.DataModels
{
    public interface ISettingsReader
    {
        string Load(string fileName);
        void Save(string fileName, string settingsString);
    }
}
