using GameReaderCommon;
using System;
using System.Collections.Generic;

namespace sjdawson.GentlemanDriverPlugin.Sections
{
    public class TyreTemps : IPluginSection
    {
        private GentlemanDriverPlugin Base;

        private int CurrentOptimalTemperature;

        private string[] TyreTempHexGradient =
        {
            "#0000ff", "#003dff", "#0058ff", "#006dff", "#007eff",
            "#008eff", "#009dff", "#00abff", "#00b7fb", "#56c3f2",

            "#87ceeb", "#5dd6f4", "#00ddf8", "#00e5f5", "#00ebec",
            "#00f1db", "#00f6c4", "#00faa7", "#00fd84", "#00ff58",

            "#00ff00", "#57fc00", "#79f800", "#93f500", "#a9f100",
            "#bbed00", "#cce800", "#dbe400", "#e8e000", "#f4db00",

            "#ffd700", "#ffc700", "#ffb700", "#ffa600", "#ff9400",
            "#ff8100", "#ff6d00", "#ff5700", "#ff3b00", "#ff0000",
        };

        private string[] TyreTempsToMonitor =
        {
            "FrontLeft", "FrontRight", "RearLeft", "RearRight"
        };

        public void Init(GentlemanDriverPlugin gentlemanDriverPlugin)
        {
            Base = gentlemanDriverPlugin;
            CurrentOptimalTemperature = Base.Settings.OptimalTyreTemps["Default"]["Default"];

            if (!Base.Settings.OptimalTyreTemps.ContainsKey(Base.PluginManager.GameName))
                Base.Settings.OptimalTyreTemps.Add(
                    Base.PluginManager.GameName,
                    new Dictionary<string, int> { { "Default", CurrentOptimalTemperature } }
                );

            Base.AddProp("Tyres.OptimalTyreTemperature", CurrentOptimalTemperature);

            foreach (var TyreTempToMonitor in TyreTempsToMonitor)
                Base.AddProp("Tyres.OptimalTyreTemperatureHex" + TyreTempToMonitor, TyreTempHexGradient[0]);

            Base.AddAction("IncreaseOptimalTyreTemp", (a, b) => {
                ChangeOptimalTyreTemperature(1);
            });

            Base.AddAction("DecreaseOptimalTyreTemp", (a, b) => {
                ChangeOptimalTyreTemperature(-1);
            });
        }

        public void DataUpdate()
        {
            Base.SetProp("Tyres.OptimalTyreTemperature", GetOptimalTyreTemperature());

            foreach (var TyreTempToMonitor in TyreTempsToMonitor)
                Base.SetProp(
                    "Tyres.OptimalTyreTemperatureHex" + TyreTempToMonitor, 
                    GetOptimalTyreTemperatureHex("TyreTemperature" + TyreTempToMonitor)
                );
        }

        public void DataUpdate(ref GameData data)
        {
            
        }

        public void End()
        {
            //dispose
        }

        private void ChangeOptimalTyreTemperature(int change)
        {
            var game = (string)Base.PluginManager.GetPropertyValue("DataCorePlugin.CurrentGame");
            var carid = (string)Base.PluginManager.GetPropertyValue("DataCorePlugin.GameData.CarId");
            var newValue = Base.Settings.OptimalTyreTemps["Default"]["Default"] + change;

            // If we're playing F1, append the current tyre compound to the carid, as they have different optimal temps
            if (null != carid && Base.PluginManager.GameName == "F12020")
                carid = carid + "__" + (int)Base.GetProp("PlayerCarStatusData.m_actualTyreCompound");

            if (Base.Settings.OptimalTyreTemps[game].ContainsKey("Default"))
                Base.Settings.OptimalTyreTemps[game]["Default"] = newValue;

            if (null != carid)
                if (Base.Settings.OptimalTyreTemps[game].ContainsKey(carid))
                    Base.Settings.OptimalTyreTemps[game][carid] = newValue;
                else
                    Base.Settings.OptimalTyreTemps[game].Add(carid, newValue);

            // Update the default setting to match
            Base.Settings.OptimalTyreTemps["Default"]["Default"] = newValue;

            Base.End(Base.PluginManager);
        }

        private int GetOptimalTyreTemperature()
        {
            var game = (string)Base.PluginManager.GetPropertyValue("DataCorePlugin.CurrentGame");
            var carid = (string)Base.PluginManager.GetPropertyValue("DataCorePlugin.GameData.CarId");

            if (Base.Settings.OptimalTyreTemps.ContainsKey(game))
                if (Base.Settings.OptimalTyreTemps[game].ContainsKey(carid))
                    return Base.Settings.OptimalTyreTemps[game][carid];
                else
                    return Base.Settings.OptimalTyreTemps[game]["Default"];

            // If all else fails, use the default temperature value.
            return Base.Settings.OptimalTyreTemps["Default"]["Default"];
        }

        private string GetOptimalTyreTemperatureHex(string tyreProp)
        {
            var optimalTyreTemperature = GetOptimalTyreTemperature();
            var currentTyreTemperature = (int)Math.Round((double)Base.PluginManager.GetPropertyValue("DataCorePlugin.GameData." + tyreProp));

            if (currentTyreTemperature < optimalTyreTemperature - 20)
                return TyreTempHexGradient[0];

            if (currentTyreTemperature >= optimalTyreTemperature - 20 && currentTyreTemperature < optimalTyreTemperature + 20)
                return TyreTempHexGradient[currentTyreTemperature - (optimalTyreTemperature - 20)];

            return TyreTempHexGradient[39];
        }
    }
}
