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
        }

        public GentlemanDriverPluginSettingsControl(GentlemanDriverPlugin plugin) : this()
        {
            this.Plugin = plugin;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
