using GameReaderCommon;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Net.Sockets;

namespace sjdawson.GentlemanDriverPlugin.Sections
{
    public class WledControl: IPluginSection
    {
        private GentlemanDriverPlugin Base;

        private bool wledControlEnabled = false;

        private static UdpClient udpClient;

        private static TimeSpan msBetweenUdpPackets = TimeSpan.FromMilliseconds(100);
        private static long timeLastUdpPacketSent = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        
        /// <summary>
        /// Store the states of the properties first change to on/off, so we don't send constant WLED messages
        /// </summary>
        private Dictionary<string, bool> PropStates = new Dictionary<string, bool>();

        public void Init(GentlemanDriverPlugin gentlemanDriverPlugin)
        {
            Base = gentlemanDriverPlugin;

            wledControlEnabled = Base.Settings.WledControlEnabled && Regex.IsMatch(Base.Settings.WledIp, @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
            msBetweenUdpPackets = TimeSpan.FromMilliseconds(1000 / Base.Settings.WledFps);

            udpClient = new UdpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(Base.Settings.WledIp), 21324);
            udpClient.Connect(ep);
            
            // Send an initial hello in SimHub blue
            if (wledControlEnabled)
            {
                sendWarlsPacket(Base.Settings.WledLedCount, 0, 114, 187);
            }
        }

        public void GameRunningDataUpdate(ref GameData data)
        {
            // Don't do anything if WLED control is not enabled
            if (!wledControlEnabled)
            {
                return;
            }

            if (checkFlag("Green", data.GameName)) { forwardHexToWarlsPacket(Base.Settings.FlagsRgb["GreenFlagRgb"]); }
            else if (checkFlag("Red", data.GameName)) { forwardHexToWarlsPacket(Base.Settings.FlagsRgb["RedFlagRgb"]); }
            else if (checkFlag("Yellow", data.GameName)) { forwardHexToWarlsPacket(Base.Settings.FlagsRgb["YellowFlagRgb"]); }
            else if (checkFlag("Blue", data.GameName)) { forwardHexToWarlsPacket(Base.Settings.FlagsRgb["BlueFlagRgb"]); }
            else if (checkFlag("Orange", data.GameName)) { forwardHexToWarlsPacket(Base.Settings.FlagsRgb["OrangeFlagRgb"]); }
            else if (checkFlag("Checkered", data.GameName)) { forwardHexToWarlsPacket(Base.Settings.FlagsRgb["CheckeredFlagRgb"], 1000); }
            else if (checkFlag("White", data.GameName)) { forwardHexToWarlsPacket(Base.Settings.FlagsRgb["WhiteFlagRgb"]); }
            else if (checkFlag("Black", data.GameName)) { forwardHexToWarlsPacket(Base.Settings.FlagsRgb["BlackFlagRgb"], 500); }
            else { forwardHexToWarlsPacket(Base.Settings.FlagsRgb["NoFlagRgb"]); }
        }

        public void DataUpdate(ref GameData data)
        {
        }

        public void End()
        {
        }

        public static void sendWarlsPacket(int ledCount, int r, int g, int b)
        {
            if (DateTimeOffset.Now.ToUnixTimeMilliseconds() <= (timeLastUdpPacketSent + msBetweenUdpPackets.TotalMilliseconds))
            {
                return;
            }
            timeLastUdpPacketSent = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            var packetLength = 2 + (ledCount * 4); // 2 initial config bytes followed by $ledCount quads for the LEDs
            var pkt = new byte[packetLength];
            pkt[0] = 1; // Use WARLS mode
            pkt[1] = 3; // Keep UDP active this long in seconds after last packet received

            var i = 0;
            var ledIndex = 0;
            while (i < (ledCount * 4))
            {
                pkt[i + 2] = (byte)ledIndex;
                pkt[i + 3] = (byte)r;
                pkt[i + 4] = (byte)g;
                pkt[i + 5] = (byte)b;

                i = i + 4;
                ledIndex++;
            }

            udpClient.Send(pkt, pkt.Length);
        }

        public static void blinkWarlsPacket(int ledCount, int r, int g, int b, int blinkDelay)
        {
            if (DateTime.Now.TimeOfDay.TotalMilliseconds % blinkDelay * 2 > blinkDelay)
            {
                sendWarlsPacket(ledCount, r, g, b);
            }
            else
            {
                sendWarlsPacket(ledCount, 0, 0, 0);
            }
        }

        public void forwardHexToWarlsPacket(string hex, int blinkDelay = 0)
        {
            var colour = System.Drawing.ColorTranslator.FromHtml(hex);
            if (blinkDelay > 0) { blinkWarlsPacket(Base.Settings.WledLedCount, colour.R, colour.G, colour.B, blinkDelay); } 
            else { sendWarlsPacket(Base.Settings.WledLedCount, colour.R, colour.G, colour.B); }
        }

        private bool checkFlag(string colour, string game)
        {   
            // ACC has some pretty funky flag handling.
            if (game == "AssettoCorsaCompetizione" && colour != "Red")
            {
                if (colour == "Yellow" || colour == "White")
                {
                    return Base.GetProp(String.Format("Graphics.global{0}", colour)) == 1
                        || Base.GetProp(String.Format("Graphics.FlagsDetails.IsACC_{0}_FLAG", colour.ToUpper()));
                }

                return Base.GetProp(String.Format("Graphics.FlagsDetails.IsACC_{0}_FLAG", colour.ToUpper()));
            }

            return Base.GetNormalisedProp(String.Format("Flag_{0}", colour)) == 1;
        }
    }
}
