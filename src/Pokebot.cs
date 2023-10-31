using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using BizHawk.Common;
using BizHawk.Emulation.Common;
using Pokebot.Properties;
using Pokebot.Utils;
using Pokebot.Models.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Log = Pokebot.Utils.Log;
using Pokebot.Panels;
using Pokebot.Factories;
using Pokebot.Models.Pokemons;
using Pokebot.Models.Player;
using Pokebot.Models;
using BizHawk.Common.IOExtensions;
using System.Reflection;
using static BizHawk.Client.EmuHawk.BatchRunner.Result;
using Pokebot.Factories.Bots;
using Pokebot.Factories.Versions;
using Pokebot.Exceptions;
using System.Numerics;

namespace Pokebot
{
    [ExternalTool("Pokebot")]
    public partial class Pokebot : ToolFormBase, IExternalToolForm
    {
        private ApiContainer? _apiContainer;
        public ApiContainer? APIContainer
        {
            get => _apiContainer;
            set
            {
                _apiContainer = value;
                if (value != null)
                {
                    InitAPIContainer();
                }
            }
        }

        public AppConfig AppConfig { get; }

        private bool _romLoaded;
        public bool IsRomLoaded
        {
            get => _romLoaded;
            private set
            {
                _romLoaded = value;
                RomLoadedUpdated();
            }
        }

        public bool IsReady
        {
            get => IsLoaded && IsRomLoaded && APIContainer != null && GameVersion != null;
        }

        public bool IsBotReady
        {
            get => IsReady && Bot != null;
        }

        public IGameVersion? GameVersion { get; private set; }
        public IBot? Bot { get; private set; }

        protected override string WindowTitleStatic => "Pokebot";
        private PokemonViewerPanel PokemonViewerPanel { get; }

        #region Init

        public Pokebot()
        {
            InitializeComponent();

            var configText = Encoding.UTF8.GetString(Resources.appconfig);
            AppConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<AppConfig>(configText)!;

            Log.LogReceived += Log_LogReceived;

            PokemonViewerPanel = new PokemonViewerPanel();
            PokemonViewerPanel.Dock = DockStyle.Fill;
            PokemonViewerPanel.Hide();
            _tabPagePokemon.Controls.Clear();
            _tabPagePokemon.Controls.Add(PokemonViewerPanel);

            _versionLabel.Text = $"{WindowTitleStatic} v{GetType().Assembly.GetName().Version}";
        }

        private void InitAPIContainer()
        {
            _soundCheckbox.Checked = APIContainer?.EmuClient.GetSoundOn() ?? true;
            _pauseCheckbox.Checked = APIContainer?.EmuClient.IsPaused() ?? false;
            _tabControl.Visible = false;

            _botComboBox.DataSource = AppConfig.BotTypes;
            _botComboBox.ValueMember = nameof(BotType.Code);
            _botComboBox.DisplayMember = nameof(BotType.Name);
        }

        private void RomLoadedUpdated()
        {
            _tabControl.Visible = IsRomLoaded;
            Bot = null;
            _botPanel.Controls.Clear();
            _statsListView.Items.Clear();
            _botComboBox.SelectedIndex = 0;
            _statusBot.Text = "Bot is not running";
            _startBotButton.Enabled = false;
            _stopBotButton.Enabled = false;
        }

        #endregion

        #region Events Callback

        private void Log_LogReceived(LogEventArgs e)
        {
            var item = new ListViewItem(e.Level.ToString());
            switch (e.Level)
            {
                case LogLevel.Debug:
                case LogLevel.Info:
                    item.ForeColor = Color.Black;
                    break;
                case LogLevel.Warn:
                    item.ForeColor = Color.Orange;
                    break;
                case LogLevel.Error:
                case LogLevel.Fatal:
                    item.ForeColor = Color.Red;
                    break;
            }

            item.SubItems.Add(e.Message);

            _logsListView.Items.Add(item);

            for(int i = 0; i < _logsListView.Columns.Count; i++)
            {
                _logsListView.Columns[i].Width = -1;
            }
        }

        #endregion

        #region Pokebot Main

        public override void Restart()
        {
            IsRomLoaded = false;
            GameVersion = null;

            try
            {
                if (APIContainer != null)
                {
                    var gameInfo = APIContainer.Emulation.GetGameInfo();

                    if (gameInfo != null)
                    {
                        var romName = gameInfo.Name;
                        var isEmpty = string.IsNullOrEmpty(romName) || romName == "Null";

                        if (isEmpty)
                        {
                            SetStatus("No ROM loaded", Color.Red);
                        } else
                        {
                            GameVersion = VersionFactory.Create(APIContainer, gameInfo.Hash);
                            SetStatus(romName);
                            Log.Info($"ROM {romName} loaded");
                            IsRomLoaded = true;
                        }
                    }
                }
            } catch (NotSupportedException ex) 
            {
                SetStatus(ex.Message, Color.Red);
            } catch (Exception ex)
            {
                Log.Error(ex.Message);
                IsRomLoaded = false;
            }
        }

        protected override void UpdateAfter()
        {
            try
            {
                if (IsReady)
                {
                    GameState state = GameVersion!.GetGameState();

                    //Show current pokemon in Viewer
                    if (state == GameState.Battle)
                    {
                        var pokemon = GameVersion.GetOpponent();
                        if (pokemon != null)
                        {
                            PokemonViewerPanel.Show();
                            PokemonViewerPanel.SetPokemon(pokemon);
                        }
                    }
                    else
                    {
                        PokemonViewerPanel.Hide();
                    }

                    //Main call for bots
                    if (Bot != null && Bot.Enabled)
                    {
                        PlayerData player = GameVersion!.GetPlayer();
                        Bot.Execute(player, state);
                    }
                }
            }
            catch (BotException ex)
            {
                StopBot();
                Log.Error(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void SetStatus(string message, Color? color = null)
        {
            _statusLabel.Text = message;
            _statusLabel.ForeColor = color ?? Color.Black;
        }

        #endregion

        #region Settings

        private void AccelerateCheckChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                APIContainer?.Emulation.LimitFramerate(!checkBox.Checked);
            }
        }

        private void SoundCheckboxChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                APIContainer?.EmuClient.SetSoundOn(checkBox.Checked);
            }
        }

        private void PauseCheckboxChanged(object sender, EventArgs e)
        {
            APIContainer?.EmuClient.TogglePause();
        }

        #endregion

        private void LoadTestClick(object sender, EventArgs e)
        {
            var success = APIContainer?.EmuClient.OpenRom(@$"D:\VisualStudioProjects\Pokebot\BizHawk\Roms\Pokemon - Version Emeraude (FR).gba");
            if (success ?? false)
            {
                IsRomLoaded = APIContainer?.EmuClient.LoadState("wild") ?? false; //starter
            }
        }

        #region Bot

        private void BotSelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender is ComboBox comboBox)
                {
                    _botPanel.Controls.Clear();

                    var value = (BotType)comboBox.SelectedItem;
                    var type = (BotCode)value.Code;
                    Bot = BotFactory.Create(type, APIContainer!, GameVersion!);
                    Bot.PokemonEncountered += PokemonEncountered;
                    Bot.StateChanged += Bot_StateChanged;
                    _botPanel.Controls.Add(Bot.GetPanel());
                    _startBotButton.Enabled = true;
                    _stopBotButton.Enabled = false;
                }
            }
            catch (NotSupportedException ex)
            {
                Log.Error(ex.Message);
            }
            catch (Exception)
            {
                Log.Error($"Error occurred while reading bot type.");
            }
        }

        private void Bot_StateChanged(bool enabled)
        {
            if (enabled)
            {
                _stopBotButton.Enabled = true;
                _startBotButton.Enabled = false;
                SetBotStatus("Bot is running...");
            } else
            {
                _startBotButton.Enabled = true;
                _stopBotButton.Enabled = false;
                _statsListView.Items.Clear();
                SetBotStatus("Bot is not running");
            }
        }

        private void PokemonEncountered(Pokemon pokemon)
        {
            AddPokemonStat(pokemon);
        }

        private void StartBotClicked(object sender, EventArgs e)
        {
            StartBot();
        }

        private void StopBotClicked(object sender, EventArgs e)
        {
            StopBot();
        }

        private void StartBot()
        {
            try
            {
                if (IsReady && !Bot!.Enabled)
                {
                    Bot.Start();
                }
            } catch(Exception ex)
            {
                Log.Error(ex.Message);
                StopBot();
                SetBotStatus(ex.Message, Color.Red);
            }
        }

        private void StopBot()
        {
            try
            {
                if (IsReady && Bot!.Enabled)
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

        #endregion

        #region Statistiques

        public void AddPokemonStat(Pokemon pokemon)
        {
            var encounters = 1;
            var shinyEncounters = pokemon.IsShiny ? 1 : 0;
            var found = false;

            for(int i = 0; i < _statsListView.Items.Count; i++)
            {
                var item = _statsListView.Items[i];
                if (item != null && item.Text == pokemon.RealName)
                {
                    if (int.TryParse(item.SubItems[1].Text, out encounters) && int.TryParse(item.SubItems[2].Text, out int currentShinyEncounters))
                    {
                        encounters += 1;
                        currentShinyEncounters += shinyEncounters;

                        item.SubItems[1].Text = encounters.ToString();
                        item.SubItems[2].Text = currentShinyEncounters.ToString();
                        item.SubItems[3].Text = $"{(shinyEncounters / encounters) * 100}%";
                    }

                    found = true;
                }
            }

            if (!found)
            {
                var item = new ListViewItem(pokemon.RealName);
                item.SubItems.Add(encounters.ToString());
                item.SubItems.Add(shinyEncounters.ToString());
                item.SubItems.Add($"{(shinyEncounters / encounters) * 100}%");

                _statsListView.Items.Add(item);
            }
        }

        #endregion
    }
}
