using System.Collections.Generic;

namespace sjdawson.GentlemanDriverPlugin
{
    public class GentlemanDriverPluginSettings
    {
        public bool WledControlEnabled = false;
        public int WledLedCount = 0;
        public string WledIp = "";

        public Dictionary<string, string> FlagsJson = new Dictionary<string, string> {
            { "BlackFlagJson", "{\"bri\": 255, \"seg\": [{\"col\": [[255,255,255], [0,0,0]], \"pal\": 2, \"fx\": 1, \"sx\": 220, \"ix\": 120}]}" },
            { "BlueFlagJson", "{\"bri\": 255, \"seg\": [{\"col\": [[0,0,255], [0,0,0]], \"pal\": 2, \"fx\": 1, \"sx\": 200, \"ix\": 120}]}" },
            { "CheckeredFlagJson", "{\"bri\": 255, \"seg\": [{\"col\": [[255,255,255], [0,0,0]], \"pal\": 2, \"fx\": 50, \"sx\": 100, \"ix\": 50}]}" },
            { "GreenFlagJson", "{\"bri\": 255, \"seg\": [{\"col\": [[0,255,0]], \"pal\": 2, \"fx\": 0}]}" },
            { "OrangeFlagJson", "{\"bri\": 255, \"seg\": [{\"col\": [[255,130,0]], \"pal\": 2, \"fx\": 0}]}" },
            { "WhiteFlagJson", "{\"bri\": 255, \"seg\": [{\"col\": [[255,255,255]], \"pal\": 2, \"fx\": 0}]}" },
            { "YellowFlagJson", "{\"bri\": 255, \"seg\": [{\"col\": [[255,200,0]], \"pal\": 2, \"fx\": 0}]}" },
            { "NoFlagJson", "{\"bri\": 255, \"seg\": [{\"col\": [[0,0,0]], \"pal\": 2, \"fx\": 0}]}" }
        };

        public Dictionary<string, Dictionary<string, int>> OptimalTyreTemps = new Dictionary<string, Dictionary<string, int>>
        {
            { "Default", new Dictionary<string, int>{ {"Default", 80 } } },
        };
    }
}
