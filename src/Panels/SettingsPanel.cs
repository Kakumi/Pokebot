using Pokebot.Models;
using System;
using System.Windows.Forms;

namespace Pokebot.Panels
{
    public partial class SettingsPanel : UserControl
    {
        public delegate void SettingsConfigChangedEventHandler(SettingsConfig settingsConfig);
        public event SettingsConfigChangedEventHandler? SettingsConfigChanged;

        public delegate void PauseToggleEventHandler();
        public event PauseToggleEventHandler? PauseClicked;

        public SettingsConfig SettingsConfig { get; }

        private static readonly string[] _languageCodes = { "en", "fr", "de", "it", "es", "ja" };

        public SettingsPanel()
        {
            InitializeComponent();
            ApplyTranslations();
            SettingsConfig = SettingsConfig.Load();

            _accelerateCheckbox.Checked = SettingsConfig.Speed;
            _soundCheckbox.Checked = SettingsConfig.Sound;
            _discordWebhookText.Text = SettingsConfig.DiscordWebhook;
            _discordUserID.Text = SettingsConfig.DiscordUserID;
            _delayUpDown.Value = (decimal)SettingsConfig.DelayBetweenActions;
            _delayTooltip.SetToolTip(_delayLabel, Messages.Tooltip_Delay);

            // Populate language dropdown. Add handler AFTER setting SelectedIndex so
            // the initial assignment does not trigger the changed event.
            _languageComboBox.Items.Add("English");
            _languageComboBox.Items.Add("Français");
            _languageComboBox.Items.Add("Deutsch");
            _languageComboBox.Items.Add("Italiano");
            _languageComboBox.Items.Add("Español");
            _languageComboBox.Items.Add("日本語");
            int langIndex = Array.IndexOf(_languageCodes, SettingsConfig.Language);
            _languageComboBox.SelectedIndex = langIndex >= 0 ? langIndex : 0;
            _languageComboBox.SelectedIndexChanged += _languageComboBox_SelectedIndexChanged;
        }

        private void ApplyTranslations()
        {
            _accelerateCheckbox.Text = Messages.Settings_Speed;
            _soundCheckbox.Text = Messages.Settings_Sound;
            _pauseCheckbox.Text = Messages.Settings_Pause;
            _delayLabel.Text = Messages.Settings_DelayLabel;
            _languageLabel.Text = Messages.Settings_LanguageLabel;
        }

        private void _accelerateCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            SettingsConfig.Speed = _accelerateCheckbox.Checked;
            SettingsConfigChanged?.Invoke(SettingsConfig);
            SettingsConfig.Save();
        }

        private void _soundCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            SettingsConfig.Sound = _soundCheckbox.Checked;
            SettingsConfigChanged?.Invoke(SettingsConfig);
            SettingsConfig.Save();
        }

        private void _pauseCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            PauseClicked?.Invoke();
        }

        private void _discordWebhookText_TextChanged(object sender, EventArgs e)
        {
            SettingsConfig.DiscordWebhook = _discordWebhookText.Text;
            SettingsConfigChanged?.Invoke(SettingsConfig);
            SettingsConfig.Save();
        }

        private void _discordUserID_TextChanged(object sender, EventArgs e)
        {
            SettingsConfig.DiscordUserID = _discordUserID.Text;
            SettingsConfigChanged?.Invoke(SettingsConfig);
            SettingsConfig.Save();
        }

        private void _delayUpDown_ValueChanged(object sender, EventArgs e)
        {
            SettingsConfig.DelayBetweenActions = (double)_delayUpDown.Value;
            SettingsConfigChanged?.Invoke(SettingsConfig);
            SettingsConfig.Save();
        }

        private void _languageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = _languageComboBox.SelectedIndex;
            if (index < 0 || index >= _languageCodes.Length)
            {
                return;
            }

            string code = _languageCodes[index];
            SettingsConfig.Language = code;
            SettingsConfig.Save();
            SettingsConfig.ApplyLanguage(code);

            MessageBox.Show(
                "Language saved. Please restart Pokebot to apply all translations.",
                "Language / Langue",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}
