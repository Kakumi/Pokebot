using BizHawk.Client.Common;
using Pokebot.Factories.Versions;
using Pokebot.Factories;
using Pokebot.Models.Config;
using Pokebot.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pokebot.Factories.Bots;
using Pokebot.Models.Pokemons;

namespace Pokebot.Panels
{
    public partial class BotPanel : UserControl
    {
        public delegate void BotSelectionChangedEventHandler(BotCode code);
        public event BotSelectionChangedEventHandler? BotChanged;

        public IBot Bot { get; private set; }

        public BotPanel()
        {
            InitializeComponent();

            _botComboBox.ValueMember = nameof(BotType.Code);
            _botComboBox.DisplayMember = nameof(BotType.Name);
        }

        public void SetBot(IBot bot)
        {
            Bot = bot;
            Bot.StateChanged += Bot_StateChanged;
            ClearPanel();
            _botPanel.Controls.Add(bot.GetPanel());
            _startBotButton.Enabled = true;
            _stopBotButton.Enabled = false;
        }

        private void Bot_StateChanged(bool enabled)
        {
            if (enabled)
            {
                _stopBotButton.Enabled = true;
                _startBotButton.Enabled = false;
                SetBotStatus(Messages.Bot_Running);
            }
            else
            {
                _startBotButton.Enabled = true;
                _stopBotButton.Enabled = false;
                SetBotStatus(Messages.Bot_NotRunning);
            }
        }

        public void StartBot()
        {
            try
            {
                if (Bot != null && !Bot!.Enabled)
                {
                    Bot.Start();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                StopBot();
                SetBotStatus(ex.Message, Color.Red);
            }
        }

        public void StopBot()
        {
            try
            {
                if (Bot != null && Bot!.Enabled)
                {
                    Bot.Stop();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                SetBotStatus(ex.Message, Color.Red);
            }
        }

        private void SetBotStatus(string message, Color? color = null)
        {
            _statusBot.Text = message;
            _statusBot.ForeColor = color ?? Color.Black;
        }

        public void Reset(List<BotType> types)
        {
            ClearPanel();

            _botComboBox.DataSource = types;
            _botComboBox.SelectedIndex = 0;
            _statusBot.Text = Messages.Bot_NotRunning;
            _startBotButton.Enabled = false;
            _stopBotButton.Enabled = false;
        }

        public void ClearPanel()
        {
            _botPanel.Controls.Clear();
            SetBotStatus(Messages.Bot_NotRunning);
        }

        public BotCode GetBotCode()
        {
            var value = (BotType)_botComboBox.SelectedItem;
            return (BotCode)value.Code;
        }

        private void _startBotButton_Click(object sender, EventArgs e)
        {
            StartBot();
        }

        private void _stopBotButton_Click(object sender, EventArgs e)
        {
            StopBot();
        }

        private void _botComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var type = GetBotCode();
                BotChanged?.Invoke(type);
            }
            catch (NotSupportedException ex)
            {
                Log.Error(ex.Message);
            }
            catch (Exception)
            {
                Log.Error(Messages.Error_ReadingBotType);
            }
        }
    }
}
