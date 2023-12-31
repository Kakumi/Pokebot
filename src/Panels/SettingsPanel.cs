﻿using BizHawk.Client.Common;
using Pokebot.Factories.Versions;
using Pokebot.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Pokebot.Panels
{
    public partial class SettingsPanel : UserControl
    {
        public delegate void SettingsConfigChangedEventHandler(SettingsConfig settingsConfig);
        public event SettingsConfigChangedEventHandler? SettingsConfigChanged;

        public delegate void PauseToggleEventHandler();
        public event PauseToggleEventHandler? PauseClicked;

        public SettingsConfig SettingsConfig { get; }

        public SettingsPanel()
        {
            InitializeComponent();
            SettingsConfig = SettingsConfig.Load();

            _accelerateCheckbox.Checked = SettingsConfig.Speed;
            _soundCheckbox.Checked = SettingsConfig.Sound;
            _discordWebhookText.Text = SettingsConfig.DiscordWebhook;
            _discordUserID.Text = SettingsConfig.DiscordUserID;
            _delayUpDown.Value = (decimal)SettingsConfig.DelayBetweenActions;
            _delayTooltip.SetToolTip(_delayLabel, Messages.Tooltip_Delay);
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
    }
}
