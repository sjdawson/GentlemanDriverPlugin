using GameReaderCommon;
using System;

namespace sjdawson.GentlemanDriverPlugin.Sections
{
    public class Laps: IPluginSection
    {
        private GentlemanDriverPlugin Base;

        private int LastOutLap = 0;

        public void Init(GentlemanDriverPlugin gentlemanDriverPlugin)
        {
            Base = gentlemanDriverPlugin;

            Base.AddProp("Laps.PredictedLapTime", new TimeSpan());
            Base.AddProp("Laps.StintTotal", 0);
        }

        public void GameRunningDataUpdate(ref GameData data)
        {
            Base.SetProp("Laps.PredictedLapTime", PredictedLapTime(data));
            Base.SetProp("Laps.StintTotal", LapsStintTotal(data));
        }

        public void DataUpdate(ref GameData data)
        {
            // Do nothing
        }

        public void End()
        {
            // Do nothing
        }

        private int LapsStintTotal(GameData data)
        {
            if (data.NewData.IsInPitLane > 0)
                LastOutLap = data.NewData.CurrentLap;

            return data.NewData.CurrentLap - LastOutLap;
        }

        private TimeSpan PredictedLapTime(GameData data) => data.NewData.BestLapTime.Add(TimeSpan.FromSeconds((double)Base.PluginManager.GetPropertyValue("PersistantTrackerPlugin.SessionBestLiveDeltaSeconds")));
    }
}
