using System.IO;
using MaMa.DataModels;

namespace MaMa.Settings
{
    public class SettingsFileReader : ISettingsReader
    {
        /// <summary>
        /// read settings from file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string Load(string fileName)
        {
            return File.ReadAllText(fileName);
        }

        public void Save(string fileName, string settingsString)
        {
            File.WriteAllText(fileName, settingsString);
        }
    }
}
