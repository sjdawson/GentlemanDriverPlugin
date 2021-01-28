using GameReaderCommon;
using SimHub.Plugins;

namespace sjdawson.GentlemanDriverPlugin
{
    public interface IPluginSection
    {
        void Init(GentlemanDriverPlugin gentlemanDriverPlugin);
        void End();
        /// <summary>
        /// DataUpdate method that runs only when a game is connected and data is available
        /// </summary>
        /// <param name="data"></param>
        void GameDataUpdate(ref GameData data);
        void DataUpdate(ref GameData data);
    }
}