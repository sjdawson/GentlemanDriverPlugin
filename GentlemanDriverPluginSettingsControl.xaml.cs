using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace sjdawson.GentlemanDriverPlugin
{
    /// <summary>
    /// Interaction logic for GentlemanDriverPluginSettingsControl.xaml
    /// </summary>
    public partial class GentlemanDriverPluginSettingsControl : UserControl
    {
        public GentlemanDriverPlugin Plugin { get; }

        public GentlemanDriverPluginSettingsControl()
        {
            InitializeComponent();
        }

        public GentlemanDriverPluginSettingsControl(GentlemanDriverPlugin plugin) : this()
        {
            this.Plugin = plugin;
        }

        public void OptimalTyreTemperatureChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            Plugin.Settings.OptimalTyreTemperature = (int)OptimalTyreTemperature.Value;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            OptimalTyreTemperature.Value = Plugin.Settings.OptimalTyreTemperature;
        }
    }
}
