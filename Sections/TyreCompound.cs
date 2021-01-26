using GameReaderCommon;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace sjdawson.GentlemanDriverPlugin.Sections
{
    public class TyreCompound : IPluginSection
    {
        private GentlemanDriverPlugin Base;

        private Dictionary<int, string> ActualCompoundMap;
        private string ActualCompoundProperty;

        private Dictionary<int, string> VisualCompoundMap;
        private string VisualCompoundProperty;

        public void DataUpdate(ref GameData data)
        {
            Base.SetProp("Tyres.ActualTyreCompound", ActualTyreCompound());
            Base.SetProp("Tyres.VisualTyreCompound", VisualTyreCompound());
        }

        public void Init(GentlemanDriverPlugin gentlemanDriverPlugin)
        {
            Base = gentlemanDriverPlugin;
            LoadCompoundMaps();

            Base.AddProp("Tyres.ActualTyreCompound", false);
            Base.AddProp("Tyres.VisualTyreCompound", false);
        }

        public void End()
        {
            //dispose
        }

        private string ActualTyreCompound()
        {
            if (ActualCompoundMap.Count > 0)
                return ActualCompoundMap[Base.GetProp(ActualCompoundProperty)];

            return "N/A";
        }

        private string VisualTyreCompound()
        {
            if (VisualCompoundMap.Count > 0)
                return VisualCompoundMap[Base.GetProp(VisualCompoundProperty)];

            return "N/A";
        }

        private void LoadCompoundMaps()
        {
            var ActualPath = Base.PluginManager.GetGameStoragePath() + "/sjdawson.GentlemanDriverPlugin/ActualTyreCompound.json";
            var VisualPath = Base.PluginManager.GetGameStoragePath() + "/sjdawson.GentlemanDriverPlugin/VisualTyreCompound.json";

            ActualCompoundMap = File.Exists(ActualPath)
                ? JsonConvert.DeserializeObject<Dictionary<int, string>>(File.ReadAllText(ActualPath))
                : new Dictionary<int, string> { };

            VisualCompoundMap = File.Exists(VisualPath)
                ? JsonConvert.DeserializeObject<Dictionary<int, string>>(File.ReadAllText(VisualPath))
                : new Dictionary<int, string> { };

            // Set the properties that have the relevant map connection
            switch (Base.PluginManager.GameName)        
            {
                case "F12020":
                    ActualCompoundProperty = "PlayerCarStatusData.m_actualTyreCompound";
                    VisualCompoundProperty = "PlayerCarStatusData.m_visualTyreCompound";
                    break;

                default:
                    ActualCompoundProperty = "";
                    VisualCompoundProperty = "";
                    break;
            }
        }
    }
}
