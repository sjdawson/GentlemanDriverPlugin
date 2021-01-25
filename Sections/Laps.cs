using GameReaderCommon;

namespace sjdawson.GentlemanDriverPlugin.Sections
{
    public class Laps : IGameExtension
    {
        private GentlemanDriverPlugin Base;

        private int LastOutLap = 0;

        public void Init(GentlemanDriverPlugin gentlemanDriverPlugin)
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

        public void End()
        {
            //dispose
        }
    }
}
