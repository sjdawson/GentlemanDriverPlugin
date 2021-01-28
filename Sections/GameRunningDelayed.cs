using GameReaderCommon;
using System;

namespace sjdawson.GentlemanDriverPlugin.Sections
{
    public class GameRunningDelayed : IPluginSection
    {
        private GentlemanDriverPlugin Base;

        private DateTime Latch = DateTime.Now;

        public void Init(GentlemanDriverPlugin gentlemanDriverPlugin)
        {
            Base = gentlemanDriverPlugin;

            Base.AddProp("GameRunning.Delayed05s", false);
            Base.AddProp("GameRunning.Delayed10s", false);
            Base.AddProp("GameRunning.Delayed15s", false);
            Base.AddProp("GameRunning.Delayed20s", false);
            Base.AddProp("GameRunning.Delayed25s", false);
            Base.AddProp("GameRunning.Delayed30s", false);
        }

        public void GameDataUpdate(ref GameData data)
        {
            Base.SetProp("GameRunning.Delayed05s", GameRunningDelayedCalc(data, 5000));
        }

        public void DataUpdate(ref GameData data)
        {
            
        }

        public void End()
        {
            //dispose
        }

        /// <summary>
        /// Show as true if the game is running, and for milliseconds after thats no longer the case
        /// </summary>
        /// <param name="data">Game data to find out if game is running</param>
        /// <param name="milliseconds">Number of milliseconds to keep the value alive for</param>
        private int GameRunningDelayedCalc(GameData data, int milliseconds)
        {
            return 4;
        }
    }
}
