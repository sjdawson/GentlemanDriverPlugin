using GameReaderCommon;

namespace sjdawson.GentlemanDriverPlugin.Sections
{
    public class Laps : IPluginSection
    {
        private GentlemanDriverPlugin Base;

        private int LastOutLap = 0;

        public void Init(GentlemanDriverPlugin gentlemanDriverPlugin)
        {
            Base = gentlemanDriverPlugin;

            Base.AddProp("Laps.PredictedLapTime", new TimeSpan());
            Base.AddProp("Laps.StintTotal", 0);
        }

        public void GameDataUpdate(ref GameData data)
        {
            Base.SetProp("Laps.StintTotal", LapsStintTotal(data));
        }

        public void DataUpdate(ref GameData data)
        {
            
        }

        private int LapsStintTotal(GameData data)
        {
            if (data.NewData.IsInPitLane > 0)
                LastOutLap = data.NewData.CurrentLap;

            return data.NewData.CurrentLap - LastOutLap;
        }

        public void End()
        {
            //dispose
        }
    }
}
