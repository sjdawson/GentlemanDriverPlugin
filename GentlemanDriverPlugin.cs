using GameReaderCommon;
using SimHub.Plugins;
using System;

namespace sjdawson.GentlemanDriverPlugin
{
    [PluginDescription("Additional properties, actions and events for use in various racing games.")]
    [PluginAuthor("sjdawson")]
    [PluginName("Gentleman Driver Plugin")]

    public class GentlemanDriverPlugin: IPlugin, IDataPlugin, IWPFSettings
    {
        public GentlemanDriverPluginSettings Settings;
        public PluginManager PluginManager { get; set; }

        public Sections.Laps Laps;
        public Sections.TyreCompound TyreCompound;
        public Sections.TyreTemps TyreTemps;

        /// <summary>
        /// Initialise the plugin preparing all settings, properties, events and triggers.
        /// </summary>
        /// <param name="pluginManager"></param>
        public void Init(PluginManager pluginManager)
        {
            Settings = this.ReadCommonSettings("GentlemanDriverPluginSettings", () => new GentlemanDriverPluginSettings());

            Laps = new Sections.Laps(this);
            TyreCompound = new Sections.TyreCompound(this);
            TyreTemps = new Sections.TyreTemps(this);
        }

        /// <param name="pluginManager"></param>
        /// <param name="data"></param>
        public void DataUpdate(PluginManager pluginManager, ref GameData data)
        {
            if (data.GameRunning)
            {     
                if (data.OldData != null && data.NewData != null)
                {
                    Laps.DataUpdate(ref data);
                    TyreCompound.DataUpdate();
                    TyreTemps.DataUpdate();
                }
            }
        }

        public void End(PluginManager pluginManager) => this.SaveCommonSettings("GentlemanDriverPluginSettings", Settings);
        public System.Windows.Controls.Control GetWPFSettingsControl(PluginManager pluginManager) => new GentlemanDriverPluginSettingsControl(this);

        /// <summary>
        /// Calculate input as a percentage of the range min->max.
        /// </summary>
        /// <param name="input">The value to convert to a percentage</param>
        /// <param name="min">The minumum value where input would be equivalent 0%</param>
        /// <param name="max">The maximum value where input would be equivalent 100%</param>
        /// <returns>Float between 0-1 to represent 0-100%</returns>
        public float InputAsPercentageOfRange(float input, float min, float max) => input > min && input < max ? (input - min) / (max - min) : input > max ? 1 : 0;

        public void AddProp(string PropertyName, dynamic defaultValue) => PluginManager.AddProperty(PropertyName, GetType(), defaultValue);
        public void SetProp(string PropertyName, dynamic value) => PluginManager.SetPropertyValue(PropertyName, GetType(), value);
        public dynamic GetProp(string PropertyName) => PluginManager.GetPropertyValue("DataCorePlugin.GameRawData."+PropertyName);

        public void AddEvent(string EventName) => PluginManager.AddEvent(EventName, GetType());
        public void TriggerEvent(string EventName) => PluginManager.TriggerEvent(EventName, GetType());

        public void AddAction(string ActionName, Action<PluginManager, string> ActionBody) => PluginManager.AddAction(ActionName, GetType(), ActionBody);
    }
}
