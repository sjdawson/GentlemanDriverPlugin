using GameReaderCommon;
using System;

namespace sjdawson.GentlemanDriverPlugin.Sections
{
    public class Laps: IPluginSection
    {
        private GentlemanDriverPlugin Base;

        private int LastOutLap = 0;
        private DateTime LastOutLapTime = DateTime.Now;

        public void Init(GentlemanDriverPlugin gentlemanDriverPlugin)
        {
            Base = gentlemanDriverPlugin;

            Base.AddProp("Laps.PredictedLapTime", new TimeSpan());
            Base.AddProp("Laps.StintTotal", 0);
            Base.AddProp("Laps.StintTime", new TimeSpan());
            Base.AddProp("Laps.LastOutLap", 0);
            Base.AddProp("Laps.LastInLap", 0);
            Base.AddProp("Laps.Display", "-");
        }

        public void GameRunningDataUpdate(ref GameData data)
        {
            Base.SetProp("Laps.PredictedLapTime", PredictedLapTime(data));
            Base.SetProp("Laps.StintTotal", LapsStintTotal(data));
            Base.SetProp("Laps.StintTime", LapsStintTime());
            Base.SetProp("Laps.LastOutLap", LastOutLapCalc());
            Base.SetProp("Laps.LastInLap", LastInLapCalc());
            Base.SetProp("Laps.Display", LapsDisplay(data));
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
            if (data.NewData.IsInPitLane > 0) {
                LastOutLap = data.NewData.CurrentLap;
                LastOutLapTime = DateTime.Now;
            }

            return data.NewData.CurrentLap - LastOutLap;
        }

        private TimeSpan LapsStintTime()
        {
            return DateTime.Now.Subtract(LastOutLapTime);
        }

        private int LastOutLapCalc()
        {
            return LastOutLap;
        }

        private int LastInLapCalc()
        {
            return LastOutLap - 1;
        }

        private string LapsDisplay(GameData data)
        {
            if (data.NewData.TotalLaps > 0)
                return string.Format("{0}/{1}", data.NewData.CurrentLap, data.NewData.TotalLaps);

            return string.Format("{0}", data.NewData.CurrentLap);
        }

        private TimeSpan PredictedLapTime(GameData data) => data.NewData.BestLapTime.Add(TimeSpan.FromSeconds((double)Base.PluginManager.GetPropertyValue("PersistantTrackerPlugin.SessionBestLiveDeltaSeconds")));
    }
}
