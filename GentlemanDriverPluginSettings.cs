using System.Collections.Generic;

namespace sjdawson.GentlemanDriverPlugin
{
    public class GentlemanDriverPluginSettings
    {
        public bool WledControlEnabled = false;
        public int WledLedCount = 0;
        public string WledIp = "127.0.0.1";
        public int WledFps = 60; // @todo - make this configurable, 1 min, 120 max.

        public Dictionary<string, string> FlagsRgb = new Dictionary<string, string> {
            { "BlackFlagRgb", "#FF000000" },
            { "BlueFlagRgb", "#FF0000FF" },
            { "CheckeredFlagRgb", "#FFFFFFFF" },
            { "GreenFlagRgb", "#FF00FF00" },
            { "OrangeFlagRgb", "#FFFFA500" },
            { "WhiteFlagRgb", "#FFFFFFFF" },
            { "YellowFlagRgb", "#FFFFFF00" },
            { "NoFlagRgb", "#FF000000" }
        };

        public Dictionary<string, Dictionary<string, int>> OptimalTyreTemps = new Dictionary<string, Dictionary<string, int>>
        {
            { "Default", new Dictionary<string, int>{ {"Default", 80 } } },
        };
    }
}
