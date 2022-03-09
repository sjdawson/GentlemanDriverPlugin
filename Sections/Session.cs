using GameReaderCommon;
using System;

namespace sjdawson.GentlemanDriverPlugin.Sections
{
    public class Session: IPluginSection
    {
        private GentlemanDriverPlugin Base;

        private string CurrentSession = null;
        private TimeSpan CurrentSessionDuration = new TimeSpan();

        public void Init(GentlemanDriverPlugin gentlemanDriverPlugin)
        {
            Base = gentlemanDriverPlugin;

            Base.AddProp("Session.Duration", new TimeSpan());
            Base.AddProp("Session.TimeLeftPercent", 0);
        }

        public void GameRunningDataUpdate(ref GameData data)
        {
            Base.SetProp("Session.Duration", SessionDuration(data));
            Base.SetProp("Session.TimeLeftPercent", TimeLeftPercent(data));
        }

        public void DataUpdate(ref GameData data)
        {
            Base.SetProp("Session.Duration", new TimeSpan());
            Base.SetProp("Session.TimeLeftPercent", 0);
        }

        public void End()
        {
            // Do nothing
        }

        private TimeSpan SessionDuration(GameData data)
        {
            // F1 20XX
            string[] f1Games = { "F12018", "F12019", "F12020", "F12021" };
            if (Array.Exists(f1Games, n => n == data.GameName))
            {
                return TimeSpan.FromSeconds((float)Base.GetProp("PacketSessionData.m_sessionDuration"));
            }

            // RRRE
            if (data.GameName == "RRRE")
            {
                return TimeSpan.FromSeconds((float)Base.GetProp("SessionTimeDuration"));
            }

            // Calculate it from the first timespan seen on session load.
            if (CurrentSession != data.NewData.SessionTypeName)
            {
                CurrentSession = data.NewData.SessionTypeName;
                CurrentSessionDuration = data.NewData.SessionTimeLeft;
            }

            if (data.GameRunning == false)
            {
                CurrentSessionDuration = new TimeSpan();
            }

            return CurrentSessionDuration;
        }

        private double TimeLeftPercent(GameData data) => (data.NewData.SessionTimeLeft.TotalSeconds / CurrentSessionDuration.TotalSeconds) * 100;
    }
}
