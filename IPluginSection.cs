using GameReaderCommon;

namespace sjdawson.GentlemanDriverPlugin
{
    public interface IPluginSection
    {
        void Init(GentlemanDriverPlugin gentlemanDriverPlugin);

        void End();

        void GameRunningDataUpdate(ref GameData data);

        void DataUpdate(ref GameData data);
    }
}
