using GameReaderCommon;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sjdawson.GentlemanDriverPlugin.Sections
{
    public class Pits: IPluginSection
    {
        private GentlemanDriverPlugin Base;

        private List<TimeSpan> PreviousPitLaneTimes = new List<TimeSpan>();
        private List<TimeSpan> PreviousPitBoxTimes = new List<TimeSpan>();

        private TimeSpan AveragePitLaneTime = TimeSpan.Zero;
        private TimeSpan AveragePitBoxTime = TimeSpan.Zero;

        private String CurrentSessionTypeName = "";
        private Boolean HasBeenOutOfPits = false;

        private DateTime PitEntryTime = DateTime.Now;

        public void Init(GentlemanDriverPlugin gentlemanDriverPlugin)
        {
            Base = gentlemanDriverPlugin;

            for (int i = 1; i <= 10; i++)
            {
                Base.AddProp(String.Format("Pits.Lane.PreviousTime.{0:d2}", i), new TimeSpan());
                Base.AddProp(String.Format("Pits.Box.PreviousTime.{0:d2}", i), new TimeSpan());
            }

            Base.AddProp("Pits.Lane.AverageTime", new TimeSpan());
            Base.AddProp("Pits.Box.AverageTime", new TimeSpan());
        }

        public void GameRunningDataUpdate(ref GameData data)
        {
            if (data.NewData.SessionTypeName != CurrentSessionTypeName)
            {
                CurrentSessionTypeName = data.NewData.SessionTypeName;

                PreviousPitLaneTimes.Clear();
                PreviousPitBoxTimes.Clear();

                AveragePitLaneTime = TimeSpan.Zero;
                AveragePitBoxTime = TimeSpan.Zero;
            }

            if (HasBeenOutOfPits)
            {
                UpdatePreviousPitTimes(data);

                for (int i = 1; i <= 10; i++)
                {
                    Base.SetProp(String.Format("Pits.Lane.PreviousTime.{0:d2}", i), PreviousPitLaneTimes.ElementAtOrDefault(i - 1));
                    Base.SetProp(String.Format("Pits.Box.PreviousTime.{0:d2}", i), PreviousPitBoxTimes.ElementAtOrDefault(i - 1));
                }

                Base.SetProp("Pits.Lane.AverageTime", AveragePitLaneTime);
                Base.SetProp("Pits.Box.AverageTime", AveragePitBoxTime);
            }

            // Ignore the time starting the race from the pits
            if (data.NewData.IsInPitLane == 0 && data.OldData.IsInPitLane == 1)
            {
                HasBeenOutOfPits = true;
            }
        }

        public void DataUpdate(ref GameData data)
        {
            // Do nothing
        }

        public void End()
        {
            // Do nothing
        }

        private void UpdatePreviousPitTimes(GameData data)
        {
            if (data.NewData.IsInPitLane == 1 && data.OldData.IsInPitLane == 0)
            {
                PitEntryTime = DateTime.Now;
            }

            if (data.NewData.IsInPitLane == 0 && data.OldData.IsInPitLane == 1)
            {
                PreviousPitLaneTimes.Insert(0, DateTime.Now.Subtract(PitEntryTime));

                // Keep the list at 10 elements by removing the first.
                if (PreviousPitLaneTimes.Count > 10)
                {
                    PreviousPitLaneTimes.RemoveAt(10);
                }

                CalculateAveragePitLaneTime();
            }

            if (data.NewData.LastPitStopDuration != data.OldData.LastPitStopDuration)
            {   
                // Ignore any sub-1 second pit boxes
                if (data.NewData.LastPitStopDuration > 1)
                { 
                    PreviousPitBoxTimes.Insert(0, TimeSpan.FromSeconds(data.NewData.LastPitStopDuration));

                    // Keep the list at 10 elements by removing the first.
                    if (PreviousPitBoxTimes.Count > 10)
                    {
                        PreviousPitBoxTimes.RemoveAt(10);
                    }

                    CalculateAveragePitBoxTime();
                }
            }
        }

        private void CalculateAveragePitLaneTime()
        {
            var TimeSpansForCalculation = PreviousPitLaneTimes.Where(ts => ts.Ticks > 0).ToList();

            if (TimeSpansForCalculation.Count > 0)
            {
                AveragePitLaneTime = TimeSpan.FromTicks((long)TimeSpansForCalculation.Select(ts => ts.Ticks).Average());
            }
            else
            {
                AveragePitLaneTime = TimeSpan.Zero;
            }
        }

        private void CalculateAveragePitBoxTime()
        {
            var TimeSpansForCalculation = PreviousPitBoxTimes.Where(ts => ts.Ticks > 0).ToList();

            if (TimeSpansForCalculation.Count > 0)
            {
                AveragePitBoxTime = TimeSpan.FromTicks((long)TimeSpansForCalculation.Select(ts => ts.Ticks).Average());
            }
            else
            {
                AveragePitBoxTime = TimeSpan.Zero;
            }
        }
    }
}
