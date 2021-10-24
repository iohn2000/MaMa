using MaMa.DataModels;

namespace MaMa.Settings
{
    ///<summary>
    /// can load and save settings via file
    ///</summary>
    public class JsonSettingsManager : ISettingsManager
    {
        private readonly ISettingsReader settingsReader;
        private readonly ISerializeSettings serializer;

        /// <summary>
        /// how to get depency injectio working here?
        /// </summary>
        /// <param name="settingsReader"></param>
        /// <param name="serializer"></param>
        public JsonSettingsManager(ISettingsReader settingsReader, ISerializeSettings serializer)
        {
            this.settingsReader = settingsReader;
            this.serializer = serializer;
        }
        /// <summary>
        /// read json settings from file and deserialise to <see cref="SettingsFile"/>
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public SettingsFile GetSettings(string fileName)
        {
            string settingsStr = this.settingsReader.Load(fileName);
            return this.serializer.DeserializeSettings(settingsStr);
        }

        /// <summary>
        /// serialised settings and writes to file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="allRules"></param>
        public void SaveSettings(string fileName, SettingsFile allRules)
        {
            string str = this.serializer.SerializeSettings(allRules);
            this.settingsReader.Save(fileName, str);
        }
    }
}
