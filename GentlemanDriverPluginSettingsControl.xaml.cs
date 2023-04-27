using SimHub.Plugins.Styles;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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
            Plugin.Settings.OptimalTyreTemps["Default"]["Default"] = (int)OptimalTyreTemperature.Value;
        }

        public void OptimalTyreTemperatureChangedCurrentGame(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            var game = (string)Plugin.PluginManager.GetPropertyValue("DataCorePlugin.CurrentGame");
            Plugin.Settings.OptimalTyreTemps[game]["Default"] = (int)OptimalTyreTemperatureCurrentGame.Value;
        }

        private void WledControlEnabled_Click(object sender, RoutedEventArgs e)
        {
            Plugin.Settings.WledControlEnabled = (bool)WledControlEnabled.IsChecked;
        }

        private void WledIpChanged(object sender, TextChangedEventArgs e)
        {
            Plugin.Settings.WledIp = (string)WledIp.Text;
        }

        private void FlagJsonChanged(object sender, TextChangedEventArgs e)
        {
            TextBox FlagJson = (TextBox) sender;
            Plugin.Settings.FlagsJson[FlagJson.Name] = FlagJson.Text;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Tyre temp config
            OptimalTyreTemperature.Value = Plugin.Settings.OptimalTyreTemps["Default"]["Default"];

            var game = (string)Plugin.PluginManager.GetPropertyValue("DataCorePlugin.CurrentGame");
            OptimalTyreTemperatureCurrentGameLabel.Text = String.Format("Current game: {0}", game);
            OptimalTyreTemperatureCurrentGame.Value = Plugin.Settings.OptimalTyreTemps[game]["Default"];

            // Wled config
            WledControlEnabled.IsChecked = Plugin.Settings.WledControlEnabled;
            WledIp.Text = Plugin.Settings.WledIp;

            BlackFlagJson.Text = Plugin.Settings.FlagsJson["BlackFlagJson"];
            BlueFlagJson.Text = Plugin.Settings.FlagsJson["BlueFlagJson"];
            GreenFlagJson.Text = Plugin.Settings.FlagsJson["GreenFlagJson"];
            WhiteFlagJson.Text = Plugin.Settings.FlagsJson["WhiteFlagJson"];
            YellowFlagJson.Text = Plugin.Settings.FlagsJson["YellowFlagJson"];
            NoFlagJson.Text = Plugin.Settings.FlagsJson["NoFlagJson"];
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
