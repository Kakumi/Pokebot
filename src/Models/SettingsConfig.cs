using Newtonsoft.Json;
using Pokebot.Utils;
using System;
using System.IO;

namespace Pokebot.Models
{
    public class SettingsConfig
    {
        public bool Speed { get; set; }
        public bool Sound { get; set; }
        public string DiscordWebhook { get; set; }
        public string DiscordUserID { get; set; }
        public double DelayBetweenActions { get; set; }

        public SettingsConfig()
        {
            Speed = false;
            Sound = false;
            DiscordWebhook = string.Empty;
            DiscordUserID = string.Empty;
            DelayBetweenActions = 0.1;
        }

        public bool Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(this);
                Directory.CreateDirectory(GetDirectory());
                File.WriteAllText(GetFile(), json);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return false;
            }

            return true;
        }

        public static SettingsConfig Load()
        {
            try
            {
                SettingsConfig? config = null;

                var file = GetFile();
                if (File.Exists(file))
                {
                    var json = File.ReadAllText(file);
                    config = JsonConvert.DeserializeObject<SettingsConfig>(json);
                }

                if (config == null)
                {
                    config = new SettingsConfig();
                }

                return config;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return new SettingsConfig();
            }
        }

        private static string GetDirectory()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, "Pokebot");
        }

        private static string GetFile()
        {
            return Path.Combine(GetDirectory(), "config.json");
        }
    }
}
