using GameReaderCommon;
using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace sjdawson.GentlemanDriverPlugin.Sections
{
    public class WLedControl: IPluginSection
    {
        private GentlemanDriverPlugin Base;

        private bool wledControlEnabled = false;

        private String apiUrlBase = "http://{0}/json";
        
        private JObject wledCurrentState;
        private bool wledCurrentStateOn = true;
        
        /// <summary>
        /// Store the states of the properties first change to on/off, so we don't send constant WLED messages
        /// </summary>
        private Dictionary<string, bool> PropStates = new Dictionary<string, bool>();

        public void Init(GentlemanDriverPlugin gentlemanDriverPlugin)
        {
            Base = gentlemanDriverPlugin;

            apiUrlBase = string.Format(apiUrlBase, Base.Settings.WledIp);

            wledControlEnabled = Base.Settings.WledControlEnabled && Regex.IsMatch(Base.Settings.WledIp, @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");

            if (wledControlEnabled)
            {
                // Store the current state of the lighting so that it can be returned to later
                wledCurrentState = JObject.Parse(GetWledCurrentState());
            }

            Base.AddProp("WLED.ControlEnabled", wledControlEnabled);
            Base.AddProp("WLED.ApiUrl", apiUrlBase);
        }

        public void GameRunningDataUpdate(ref GameData data)
        {
            if (!wledControlEnabled)
            {
                return;
            }

            if (wledCurrentStateOn == true)
            {
                wledCurrentStateOn = false;
                UpdateLight(Base.Settings.FlagsJson["NoFlagJson"]);
            }

            ReactToBoolProp("Flag_Black", Base.Settings.FlagsJson["BlackFlagJson"], Base.Settings.FlagsJson["NoFlagJson"]);
            ReactToBoolProp("Flag_Yellow", Base.Settings.FlagsJson["YellowFlagJson"], Base.Settings.FlagsJson["NoFlagJson"]);
            ReactToBoolProp("Flag_White", Base.Settings.FlagsJson["WhiteFlagJson"], Base.Settings.FlagsJson["NoFlagJson"]);
            ReactToBoolProp("Flag_Blue", Base.Settings.FlagsJson["BlueFlagJson"], Base.Settings.FlagsJson["NoFlagJson"]);
            ReactToBoolProp("Flag_Green", Base.Settings.FlagsJson["GreenFlagJson"], Base.Settings.FlagsJson["NoFlagJson"]);
        }

        public void DataUpdate(ref GameData data)
        {
            if (wledControlEnabled && !data.GameRunning && !wledCurrentStateOn)
            {
                UpdateLight(wledCurrentState["state"].ToString());
                wledCurrentStateOn = true;
            }
        }

        public void End()
        {
        }
            
        /**
         * Get the current state of the WLED device.
         */
        private string GetWledCurrentState()
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(apiUrlBase);
            httpRequest.Method = "GET";
            httpRequest.ContentType = "application/json";
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            var result = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            return result;
        }

        private void UpdateLight(string data)
        {
            var httpRequest = (HttpWebRequest)WebRequest.Create(apiUrlBase);
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
        
        /// <summary>
        /// Have the WLED instance react to a boolean property switching between on and off.
        /// </summary>
        /// <param name="prop">The property that is being looked out for</param>
        /// <param name="onState">What JSON string should be sent to WLED for this property switching on</param>
        /// <param name="offState">What JSON string should be sent to WLED for this property switching off</param>
        private void ReactToBoolProp(string prop, string onState, string offState)
        {
            // If the property state isn't in the dict, put it there with a false default
            if (!PropStates.ContainsKey(prop)) { PropStates.Add(prop, false); };

            if (Base.GetNormalisedProp(prop) == 1 && !PropStates[prop])
            {
                PropStates[prop] = !PropStates[prop];
                UpdateLight(onState);
            }

            if (Base.GetNormalisedProp(prop) == 0 && PropStates[prop])
            {
                PropStates[prop] = !PropStates[prop];
                UpdateLight(offState);
            }
        }
    }
}
