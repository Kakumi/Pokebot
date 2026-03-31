using Newtonsoft.Json;
using Pokebot.Utils;
using System;
using System.Globalization;
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
        public string Language { get; set; }

        public SettingsConfig()
        {
            Speed = false;
            Sound = false;
            DiscordWebhook = string.Empty;
            DiscordUserID = string.Empty;
            DelayBetweenActions = 0.1;
            Language = "en";
        }

        /// <summary>
        /// Applies the language from the saved config file to <see cref="Messages.Culture"/>
        /// without performing a full config load. Call this as early as possible (before
        /// any UI is constructed) so all Messages.* lookups use the correct language.
        /// </summary>
        public static void ApplyLanguageFromSavedConfig()
        {
            try
            {
                var file = GetFile();
                if (!File.Exists(file))
                {
                    return;
                }

                var json = File.ReadAllText(file);
                var config = JsonConvert.DeserializeObject<SettingsConfig>(json);
                if (config != null)
                {
                    ApplyLanguage(config.Language);
                }
            }
            catch
            {
                // Config unreadable — stay with the default culture.
            }
        }

        /// <summary>Sets <see cref="Messages.Culture"/> to the given language code (e.g. "en", "fr").</summary>
        public static void ApplyLanguage(string language)
        {
            try
            {
                var culture = new CultureInfo(string.IsNullOrWhiteSpace(language) ? "en" : language);
                Messages.Culture = culture;
            }
            catch
            {
                // Invalid culture code — stay with the current culture.
            }
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
                Log.Error(ex);
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
                Log.Error(ex);
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
