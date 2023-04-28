using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

            if ((bool)WledControlEnabled.IsChecked)
            {
                CheckConnection();
            }
            else
            {
                UpdateStatusMessage(
                    "Enable to check status…",
                    FontWeights.Normal,
                    "GrayBrush5"
                );
            }
        }

        private void WledIpChanged(object sender, TextChangedEventArgs e)
        {
            Plugin.Settings.WledIp = (string)WledIp.Text;

            if (!Plugin.Settings.WledIp.Equals((string) WledIp.Text))
            {
                Plugin.Settings.WledControlEnabled = false;
                WledControlEnabled.IsChecked = false;
            }         

            UpdateStatusMessage(
                "Enable to check status…",
                FontWeights.Normal,
                "GrayBrush5"
            );
        }

        private void FlagJsonChanged(object sender, TextChangedEventArgs e)
        {
            TextBox FlagJson = (TextBox) sender;
            Plugin.Settings.FlagsJson[FlagJson.Name] = FlagJson.Text;
        }

        private void FlagJsonTestClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button) sender;
            UpdateLight(Plugin.Settings.FlagsJson[button.Name.Remove(button.Name.IndexOf("Test"))]);
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
            CheckeredFlagJson.Text = Plugin.Settings.FlagsJson["CheckeredFlagJson"];
            GreenFlagJson.Text = Plugin.Settings.FlagsJson["GreenFlagJson"];
            OrangeFlagJson.Text = Plugin.Settings.FlagsJson["OrangeFlagJson"];
            WhiteFlagJson.Text = Plugin.Settings.FlagsJson["WhiteFlagJson"];
            YellowFlagJson.Text = Plugin.Settings.FlagsJson["YellowFlagJson"];
            NoFlagJson.Text = Plugin.Settings.FlagsJson["NoFlagJson"];

            if (Plugin.Settings.WledControlEnabled)
            {
                CheckConnection();
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void UpdateLight(string data)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(string.Format("http://{0}/json", WledIp.Text));
            httpRequest.Method = "POST";

            httpRequest.Accept = "application/json";
            httpRequest.ContentType = "application/json";

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }

        private void CheckConnection()
        {
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(string.Format("http://{0}/json", WledIp.Text));
                httpRequest.Method = "GET";
                httpRequest.Accept = "application/json";
                httpRequest.ContentType = "application/json";

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                JObject connectionStatus = new JObject();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    connectionStatus = JObject.Parse(streamReader.ReadToEnd());
                }

                UpdateStatusMessage(
                    String.Format("Connected to “{0}” (v{2}) with {1} LEDs.",
                        connectionStatus.SelectToken("info.name") ?? "Unknown",
                        connectionStatus.SelectToken("info.leds.count") ?? "unknown number",
                        connectionStatus.SelectToken("info.ver") ?? "Unknown"
                    ),
                    FontWeights.Normal,
                    "GrayBrush5"
                );

                Plugin.Settings.WledLedCount = (int)connectionStatus.SelectToken("info.leds.count");
            }
            catch (Exception ex)
            {
                UpdateStatusMessage(
                    String.Format("{0}: Error: Check the IP address and try again.", DateTime.Now.ToString("hh:mm:ss")),
                    FontWeights.Bold,
                    "ValidationSummaryColor3"
                );

                Plugin.Settings.WledControlEnabled = false;
                WledControlEnabled.IsChecked = false;

                SimHub.Logging.Current.Error(ex.Message);
            }
        }
        
        private void UpdateStatusMessage(string message, FontWeight weight, string brush)
        {
            WledStatusMessage.Foreground = (Brush)Application.Current.FindResource(brush);
            WledStatusMessage.FontWeight = weight;
            WledStatusMessage.Text = message;
        }
    }
}
