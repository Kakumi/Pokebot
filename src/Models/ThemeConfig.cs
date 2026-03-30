using Newtonsoft.Json;
using Pokebot.Utils;
using System;
using System.Drawing;
using System.IO;

namespace Pokebot.Models
{
    public class ThemeConfig
    {
        public string BackgroundColor { get; set; }
        public string InputBackgroundColor { get; set; }
        public string TextColor { get; set; }
        public string AccentColor { get; set; }
        public string ButtonTextColor { get; set; }
        public string WarningColor { get; set; }
        public string ErrorColor { get; set; }
        public string SuccessColor { get; set; }

        /// <summary>
        /// When true, ThemeManager resets all controls to OS/visual-style defaults.
        /// No colors are applied — Windows takes full control.
        /// </summary>
        public bool IsBasic { get; set; }

        public ThemeConfig()
        {
            BackgroundColor = "#F0F0F0";
            InputBackgroundColor = "#FFFFFF";
            TextColor = "#000000";
            AccentColor = "#0078D7";
            ButtonTextColor = "#FFFFFF";
            WarningColor = "#FF8C00";
            ErrorColor = "#CC0000";
            SuccessColor = "#008000";
        }

        public Color BackColorValue => ParseColor(BackgroundColor);

        /// <summary>Alias — panels and controls share the same background.</summary>
        public Color ControlBackColorValue => BackColorValue;

        public Color InputBackColorValue => ParseColor(InputBackgroundColor);
        public Color TextColorValue => ParseColor(TextColor);
        public Color AccentColorValue => ParseColor(AccentColor);
        public Color ButtonTextColorValue
            => string.IsNullOrWhiteSpace(ButtonTextColor) ? TextColorValue : ParseColor(ButtonTextColor);
        public Color WarningColorValue => ParseColor(WarningColor);
        public Color ErrorColorValue => ParseColor(ErrorColor);
        public Color SuccessColorValue => ParseColor(SuccessColor);

        private static Color ParseColor(string hex)
        {
            try
            {
                return ColorTranslator.FromHtml(hex);
            }
            catch
            {
                return Color.Black;
            }
        }

        /// <summary>Resets every control to OS/visual-style defaults — equivalent to no theme.</summary>
        public static ThemeConfig CreateBasic()
        {
            return new ThemeConfig { IsBasic = true };
        }

        public static ThemeConfig CreateDark()
        {
            return new ThemeConfig
            {
                BackgroundColor = "#252B35",
                InputBackgroundColor = "#2A3140",
                TextColor = "#DCE1EC",
                AccentColor = "#E3350D",
                ButtonTextColor = "#FFFFFF",
                WarningColor = "#F0A500",
                ErrorColor = "#FF4C4C",
                SuccessColor = "#50C878"
            };
        }

        // ── Pokémon themes ───────────────────────────────────────────────────────

        public static ThemeConfig CreatePikachu()
        {
            return new ThemeConfig
            {
                BackgroundColor = "#1C1A05",
                InputBackgroundColor = "#2A2708",
                TextColor = "#F5E642",
                AccentColor = "#FFD700",
                ButtonTextColor = "#1C1A05",
                WarningColor = "#FF8C00",
                ErrorColor = "#FF4444",
                SuccessColor = "#88BB00"
            };
        }

        public static ThemeConfig CreateGengar()
        {
            return new ThemeConfig
            {
                BackgroundColor = "#1A0A2A",
                InputBackgroundColor = "#250F3A",
                TextColor = "#E0CCFF",
                AccentColor = "#8B44CC",
                ButtonTextColor = "#FFFFFF",
                WarningColor = "#CC8800",
                ErrorColor = "#FF4477",
                SuccessColor = "#44BB88"
            };
        }

        public static ThemeConfig CreateDragonite()
        {
            return new ThemeConfig
            {
                BackgroundColor = "#0A1020",
                InputBackgroundColor = "#101828",
                TextColor = "#E8D4A0",
                AccentColor = "#FF8C00",
                ButtonTextColor = "#0A1020",
                WarningColor = "#FFD700",
                ErrorColor = "#FF4444",
                SuccessColor = "#44AA66"
            };
        }

        public static ThemeConfig CreateCharizard()
        {
            return new ThemeConfig
            {
                BackgroundColor = "#1A0800",
                InputBackgroundColor = "#280D00",
                TextColor = "#FFD0A0",
                AccentColor = "#FF4500",
                ButtonTextColor = "#FFFFFF",
                WarningColor = "#FF8C00",
                ErrorColor = "#FF2222",
                SuccessColor = "#88BB44"
            };
        }

        public static ThemeConfig CreateBlastoise()
        {
            return new ThemeConfig
            {
                BackgroundColor = "#051525",
                InputBackgroundColor = "#0A2035",
                TextColor = "#B0D8FF",
                AccentColor = "#1E90FF",
                ButtonTextColor = "#FFFFFF",
                WarningColor = "#FFB000",
                ErrorColor = "#FF4444",
                SuccessColor = "#00CC88"
            };
        }

        public static ThemeConfig CreateVenusaur()
        {
            return new ThemeConfig
            {
                BackgroundColor = "#051505",
                InputBackgroundColor = "#0A220A",
                TextColor = "#C0E8A0",
                AccentColor = "#44BB22",
                ButtonTextColor = "#051505",
                WarningColor = "#FFB000",
                ErrorColor = "#FF4444",
                SuccessColor = "#00DD66"
            };
        }

        public static ThemeConfig CreateMew()
        {
            return new ThemeConfig
            {
                BackgroundColor = "#1A0A15",
                InputBackgroundColor = "#250F20",
                TextColor = "#FFB8E0",
                AccentColor = "#FF69B4",
                ButtonTextColor = "#1A0A15",
                WarningColor = "#FFB000",
                ErrorColor = "#FF2255",
                SuccessColor = "#44CC88"
            };
        }

        public static ThemeConfig CreateMewtwo()
        {
            return new ThemeConfig
            {
                BackgroundColor = "#0D0D18",
                InputBackgroundColor = "#141420",
                TextColor = "#C8C8E8",
                AccentColor = "#7766CC",
                ButtonTextColor = "#FFFFFF",
                WarningColor = "#CC8800",
                ErrorColor = "#FF4455",
                SuccessColor = "#44BB88"
            };
        }

        public bool Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(this);
                Directory.CreateDirectory(GetDirectory());
                File.WriteAllText(GetFile(), json);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
        }

        public static ThemeConfig Load()
        {
            try
            {
                var file = GetFile();
                if (File.Exists(file))
                {
                    var json = File.ReadAllText(file);
                    var config = JsonConvert.DeserializeObject<ThemeConfig>(json);
                    if (config != null)
                    {
                        config.Normalize();
                        return config;
                    }
                }
            }
            catch { }

            return new ThemeConfig();
        }

        private void Normalize()
        {
            if (string.IsNullOrWhiteSpace(BackgroundColor)) BackgroundColor = "#F0F0F0";
            if (string.IsNullOrWhiteSpace(InputBackgroundColor)) InputBackgroundColor = "#FFFFFF";
            if (string.IsNullOrWhiteSpace(TextColor)) TextColor = "#000000";
            if (string.IsNullOrWhiteSpace(AccentColor)) AccentColor = "#0078D7";
            if (string.IsNullOrWhiteSpace(ButtonTextColor)) ButtonTextColor = "#FFFFFF";
            if (string.IsNullOrWhiteSpace(WarningColor)) WarningColor = "#FF8C00";
            if (string.IsNullOrWhiteSpace(ErrorColor)) ErrorColor = "#CC0000";
            if (string.IsNullOrWhiteSpace(SuccessColor)) SuccessColor = "#008000";
        }

        private static string GetDirectory()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, "Pokebot");
        }

        private static string GetFile()
        {
            return Path.Combine(GetDirectory(), "theme.json");
        }
    }
}
