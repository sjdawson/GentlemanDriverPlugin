using GameReaderCommon;
using System;

namespace sjdawson.GentlemanDriverPlugin.Sections
{
    public class Laps
    {
        private readonly GentlemanDriverPlugin Base;

        private int LastOutLap = 0;

        public Laps(GentlemanDriverPlugin gentlemanDriverPlugin)
        {
            Base = gentlemanDriverPlugin;

            Base.AddProp("Laps.PredictedLapTime", new TimeSpan());
            Base.AddProp("Laps.StintTotal", 0);
        }

        public void DataUpdate(ref GameData data)
        {
            Base.SetProp("Laps.PredictedLapTime", PredictedLapTime(data));
            Base.SetProp("Laps.StintTotal", LapsStintTotal(data));
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
