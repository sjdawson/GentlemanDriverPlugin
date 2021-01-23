using System.Collections.Generic;

namespace sjdawson.GentlemanDriverPlugin
{
    public class GentlemanDriverPluginSettings
    {
        public Dictionary<string, Dictionary<string, int>> OptimalTyreTemps = new Dictionary<string, Dictionary<string, int>>
        {
            { "Default", new Dictionary<string, int>{ {"Default", 80 } } },
        };

    }
}
