﻿using GameReaderCommon;
using SimHub.Plugins;

namespace sjdawson.GentlemanDriverPlugin
{
    public interface IPluginSection
    {
        void Init(GentlemanDriverPlugin gentlemanDriverPlugin);
        void End();
        void DataUpdate(ref GameData data);
    }
}