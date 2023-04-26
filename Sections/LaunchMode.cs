using GameReaderCommon;

namespace sjdawson.GentlemanDriverPlugin.Sections
{
    public class LaunchMode: IPluginSection
    {
        private GentlemanDriverPlugin Base;

        private bool LaunchModeActive = false;

        public void Init(GentlemanDriverPlugin gentlemanDriverPlugin)
        {
            Base = gentlemanDriverPlugin;

            Base.AddProp("LaunchMode.Active", LaunchModeActive);

            Base.AddAction("LaunchModeToggle", (a, b) =>
            {
                LaunchModeActive = !LaunchModeActive;
            });
        }

        public void GameRunningDataUpdate(ref GameData data)
        {
            Base.SetProp("LaunchMode.Active", LaunchModeUpdate(data));
        }

        public void DataUpdate(ref GameData data)
        {
            // Do nothing
        }

        public void End()
        {
            // Do nothing
        }

        private bool LaunchModeUpdate(GameData data)
        {
            if (!data.OldData.Gear.Equals(data.NewData.Gear))
            {
                LaunchModeActive = false;
            }

            return LaunchModeActive;
        }
    }
}
