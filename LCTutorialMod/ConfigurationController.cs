using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCTutorialMod
{
    /// <summary>
    /// This will hold and manage all configs used by the config manager
    /// </summary>
    internal class ConfigurationController
    {
        private ConfigEntry<string> ServerNameCfg;
        private ConfigEntry<bool> GodModeCfg;
        private ConfigEntry<float> PlayerSpeedCfg;
        private ConfigEntry<bool> CustomSprintCfg;


        internal string ServerName
        {
            get
            {
                if(ServerNameCfg.Value == null)
                {
                    return (string)ServerNameCfg.DefaultValue;
                }
                return ServerNameCfg.Value;
            }
            set => ServerNameCfg.Value = value;
        }
        internal bool GodMode { get => GodModeCfg.Value; set => GodModeCfg.Value = value; }
        internal float PlayerSpeed
        {
            get
            {
                if(PlayerSpeedCfg.Value < 0)
                {
                    return (float)PlayerSpeedCfg.DefaultValue;
                }
                return PlayerSpeedCfg.Value;
            }
            set => PlayerSpeedCfg.Value = value;
        }
        internal bool CustomSprint { get => CustomSprintCfg.Value; set => CustomSprintCfg.Value = value; }
        
        public ConfigurationController(ConfigFile Config) 
        {
            ServerNameCfg = Config.Bind("Server Settings", "Server Name", "Default Server Name",
                "The name used when creating a server. Overwrites the in game menu input.");
            GodModeCfg = Config.Bind("Host Settings", "God Mode", false,
                "Skibidi God Mode Toilet.");
            PlayerSpeedCfg = Config.Bind("Host Settings", "Player Speed", 5f,
                "Sigma Speed.");
            CustomSprintCfg = Config.Bind("Host Settings", "Custom Sprint", false,
                "Fartsy lungs. Enables infinite sprint and allows for custom move speed.");
        }
    }
}
