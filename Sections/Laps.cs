using GameReaderCommon;

namespace sjdawson.GentlemanDriverPlugin.Sections
{
    public class Laps
    {
        private readonly GentlemanDriverPlugin Base;

        private int LastOutLap = 0;

        public Laps(GentlemanDriverPlugin gentlemanDriverPlugin)
        {
            Base = gentlemanDriverPlugin;

            Base.AddProp("Laps.StintTotal", 0);
        }

        public void DataUpdate(ref GameData data)
        {
            Base.SetProp("Laps.StintTotal", LapsStintTotal(data));
        }

        private int LapsStintTotal(GameData data)
        {
            if (data.NewData.IsInPitLane > 0)
                LastOutLap = data.NewData.CurrentLap;

            return data.NewData.CurrentLap - LastOutLap;
        }
    }
}
