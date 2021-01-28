using GameReaderCommon;
using System;

namespace sjdawson.GentlemanDriverPlugin.Sections
{
    public class GameRunningDelayed
    {
        private readonly GentlemanDriverPlugin Base;

        // Removing an hour on init ensures all props stay false until a game has been running.
        private DateTime Latch = DateTime.Now.AddHours(-1); 

        private int[] LatchPeriods = {5, 10, 15, 20, 30, 60};

        public GameRunningDelayed(GentlemanDriverPlugin gentlemanDriverPlugin)
        {
            Base = gentlemanDriverPlugin;

            foreach (int period in LatchPeriods)
                Base.AddProp("GameRunning.Delayed" + period.ToString("D2") + "s", false);
        }

        public void DataUpdate(ref GameData data)
        {
            foreach (int period in LatchPeriods)
                Base.SetProp("GameRunning.Delayed" + period.ToString("D2") + "s", GameRunningDelayedCalc(data, period * 1000));
        }

        /// <summary>
        /// Show as true if the game is running, and for milliseconds after thats no longer the case
        /// </summary>
        /// <param name="data">Game data to find out if game is running</param>
        /// <param name="milliseconds">Number of milliseconds to keep the value alive for</param>
        private bool GameRunningDelayedCalc(GameData data, int milliseconds)
        {
            if (data.GameRunning)
            {
                Latch = DateTime.Now;
                return true;
            }

            if (DateTime.Now.CompareTo(Latch.AddMilliseconds(milliseconds)) > 0)
            {
                return false;
            }
                
            return true;
        }
    }
}
