using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using Newtonsoft.Json;
using Pokebot.Exceptions;
using Pokebot.Factories;
using Pokebot.Factories.Bots;
using Pokebot.Factories.Versions;
using Pokebot.Models;
using Pokebot.Models.Config;
using Pokebot.Models.Player;
using Pokebot.Models.Pokemons;
using Pokebot.Panels;
using Pokebot.Properties;
using Pokebot.Services.DiscordWebhook;
using Pokebot.Services.Github;
using Pokebot.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using static System.Windows.Forms.AxHost;
using Log = Pokebot.Utils.Log;

namespace Pokebot
{
    [ExternalTool("Pokebot")]
    public partial class Pokebot : ToolFormBase, IExternalToolForm
    {
        protected override string WindowTitleStatic => Messages.AppName;

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

        public GameVersion? GameVersion { get; private set; }
        public IBot? Bot { get; private set; }

        public PokemonViewerPanel OpponentViewerPanel { get; private set; }
        public PartyPokemonViewer PartyViewerPanel { get; private set; }
        public EncounterStatsPanel EncounterStatsPanel { get; private set; }
        public BotPanel BotPanel { get; private set; }
        public LogsPanel LogsPanel { get; private set; }

        public PokemonWatcher? PokemonWatcher { get; private set; }
        private SettingsConfig? _settingsConfig;
        public SettingsConfig SettingsConfig
        {
            get => _settingsConfig!;
            set
            {
                _settingsConfig = value;
                UpdateSettings();
            }
        }

        WaitTask? _waitTask;

        #region Init

        public Pokebot()
        {
            InitializeComponent();

            var configText = Encoding.UTF8.GetString(Resources.appconfig);
            AppConfig = JsonConvert.DeserializeObject<AppConfig>(configText)!;

            _seedText.Minimum = 0;
            _seedText.Maximum = uint.MaxValue;

            _versionLabel.Text = $"{WindowTitleStatic} v{GetType().Assembly.GetName().Version}";
            _newVersionLabel.Hide();
            _delayTooltip.SetToolTip(_delayLabel, Messages.Tooltip_Delay);

            SettingsConfig = SettingsConfig.Load();
            LogsPanel = new LogsPanel();

            CreateTabPages();

            var worker = new BackgroundWorker();
            worker.DoWork += GetGithubLatestReleaseWorker;
            worker.RunWorkerAsync();
        }

        private async void GetGithubLatestReleaseWorker(object sender, DoWorkEventArgs e)
        {
            try
            {
                var githubService = new GithubServices(AppConfig.Github.Url);
                var latestRelease = await githubService.GetLatestRelease(AppConfig.Github.Owner, AppConfig.Github.Repository);
                var currentVersion = $"v{GetType().Assembly.GetName().Version}";
                if (latestRelease.Name != currentVersion)
                {
                    _newVersionLabel.BeginInvoke(() =>
                    {
                        _newVersionLabel.Show();
                        _newVersionLabel.Text = string.Format(Messages.NewVersion_MessageLabel, latestRelease.Name);
                    });

                    string message = string.Format(Messages.NewVersion_Message, latestRelease.Name);
                    string title = Messages.NewVersion_Title;
                    MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                    DialogResult result = MessageBox.Show(message, title, buttons);
                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(latestRelease.Url);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void InitAPIContainer()
        {
            _soundCheckbox.Checked = APIContainer?.EmuClient.GetSoundOn() ?? true;
            _pauseCheckbox.Checked = APIContainer?.EmuClient.IsPaused() ?? false;
            _tabControl.Visible = false;
        }

        private void CreateTab(UserControl userControl, string name)
        {
            TabPage tabPage = new TabPage(name);
            tabPage.Controls.Add(userControl);
            _tabControl.TabPages.Add(tabPage);
        }

        private void CreateTabPages()
        {
            OpponentViewerPanel = new PokemonViewerPanel();
            OpponentViewerPanel.Dock = DockStyle.Fill;
            OpponentViewerPanel.Hide();

            PartyViewerPanel = new PartyPokemonViewer();
            PartyViewerPanel.Dock = DockStyle.Fill;

            EncounterStatsPanel = new EncounterStatsPanel();
            EncounterStatsPanel.Dock = DockStyle.Fill;

            BotPanel = new BotPanel(AppConfig.BotTypes);
            BotPanel.BotChanged += BotPanel_BotChanged;
            BotPanel.Dock = DockStyle.Fill;

            CreateTab(BotPanel, Messages.Tab_BotPanel);
            CreateTab(EncounterStatsPanel, Messages.Tab_EncounterStats);
            CreateTab(OpponentViewerPanel, Messages.Tab_ViewerOpponentName);
            CreateTab(PartyViewerPanel, Messages.Tab_ViewerPartyName);
            CreateTab(LogsPanel, Messages.Tab_LogsPanel);
        }

        private void RomLoadedUpdated()
        {
            _tabControl.Visible = IsRomLoaded;
            BotPanel.Reset();
            EncounterStatsPanel.Clear();
            OpponentViewerPanel.Hide();
            PartyViewerPanel.Clear();

            Bot = null;

            if (IsReady)
            {
                _seedText.Value = GameVersion!.Memory.GetSeed();
                CreateBot(BotPanel.GetBotCode());
                PokemonWatcher = new PokemonWatcher(GameVersion);
                PokemonWatcher.OpponentChanged += PokemonWatcher_OpponentChanged;
                PokemonWatcher.PartyChanged += PokemonWatcher_PartyChanged;
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
                            SetStatus(Messages.Rom_NotLoaded, Color.Red);
                        }
                        else
                        {
                            GameVersion = VersionFactory.Create(APIContainer, gameInfo.Hash);
                            SetStatus(romName);
                            Log.Info(string.Format(Messages.Rom_Loaded, romName));
                            IsRomLoaded = true;
                        }
                    }
                }
            }
            catch (NotSupportedException ex)
            {
                SetStatus(ex.Message, Color.Red);
            }
            catch (Exception ex)
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
                    GameState state = GameVersion!.Memory.GetGameState();

                    if (PokemonWatcher != null)
                    {
                        PokemonWatcher.Execute(state);
                    }

                    var executeBot = false;
                    if (_waitTask != null)
                    {
                        if (_waitTask.Seconds == 0 || (!Bot?.UseDelay() ?? false))
                        {
                            executeBot = true;
                        }
                        else if (!_waitTask.IsBusy)
                        {
                            executeBot = true;
                            _waitTask.Start();
                        }
                    }

                    if (executeBot)
                    {
                        ExecuteBot(state);
                    }
                }
            }
            catch (BotException ex)
            {
                BotPanel.StopBot();
                Log.Error(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void ExecuteBot(GameState state)
        {
            //Main call for bots
            if (IsReady && Bot != null && Bot.Enabled)
            {
                PlayerData player = GameVersion!.Memory.GetPlayer();
                Bot.Execute(player, state);
            }
        }

        private void SetStatus(string message, Color? color = null)
        {
            _statusLabel.Text = message;
            _statusLabel.ForeColor = color ?? Color.Black;
        }

        #endregion

        #region Settings

        private void UpdateSettings()
        {
            _accelerateCheckbox.Checked = SettingsConfig.Speed;
            _soundCheckbox.Checked = SettingsConfig.Sound;
            _discordWebhookText.Text = SettingsConfig.DiscordWebhook;
            _delayUpDown.Value = (decimal)SettingsConfig.DelayBetweenActions;
            _waitTask = new WaitTask(SettingsConfig.DelayBetweenActions);
        }

        private void AccelerateCheckChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                SettingsConfig.Speed = checkBox.Checked;
                APIContainer?.Emulation.LimitFramerate(!SettingsConfig.Speed);
                SettingsConfig.Save();
            }
        }

        private void SoundCheckboxChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                SettingsConfig.Sound = checkBox.Checked;
                APIContainer?.EmuClient.SetSoundOn(SettingsConfig.Sound);
                SettingsConfig.Save();
            }
        }

        private void PauseCheckboxChanged(object sender, EventArgs e)
        {
            APIContainer?.EmuClient.TogglePause();
        }

        private void DiscordWebhookTextChanged(object sender, EventArgs e)
        {
            try
            {
                SettingsConfig.DiscordWebhook = _discordWebhookText.Text;
                SettingsConfig.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void DelayUpDownChanged(object sender, EventArgs e)
        {
            SettingsConfig.DelayBetweenActions = (double)_delayUpDown.Value;
            _waitTask = new WaitTask(SettingsConfig.DelayBetweenActions);
        }

        private void InjectSeedClicked(object sender, EventArgs e)
        {
            if (IsReady)
            {
                uint value = (uint)_seedText.Value;
                GameVersion!.Memory.SetSeed(value);
            }
        }

        #endregion

        #region Bot Events

        private void BotPanel_BotChanged(BotCode code)
        {
            CreateBot(code);
        }

        private void CreateBot(BotCode code)
        {
            if (IsReady)
            {
                BotPanel.ClearPanel();

                Bot = BotFactory.Create(code, APIContainer!, GameVersion!); 
                Bot.StateChanged += Bot_StateChanged;
                Bot.PokemonEncountered += Bot_PokemonEncountered;
                Bot.PokemonFound += Bot_PokemonFound;
                BotPanel.SetBot(Bot);
            }
        }

        private void Bot_StateChanged(bool enabled)
        {
            if (enabled)
            {
                EncounterStatsPanel.Clear();
            }
        }

        private void Bot_PokemonFound(Pokemon pokemon)
        {
            var trainer = GameVersion!.Memory.GetPlayer();
            new DiscordWebhookServices(SettingsConfig.DiscordWebhook).SendPokemonWebhook(pokemon, trainer);
        }

        private void Bot_PokemonEncountered(Pokemon pokemon)
        {
            EncounterStatsPanel.AddPokemonStat(pokemon);
        }

        #endregion

        #region Pokemon Watcher Events

        private void PokemonWatcher_OpponentChanged(Pokemon? pokemon)
        {
            if (pokemon != null)
            {
                OpponentViewerPanel.Show();
                OpponentViewerPanel.SetPokemon(pokemon);
            }
            else
            {
                OpponentViewerPanel.Hide();
            }
        }

        private void PokemonWatcher_PartyChanged(System.Collections.Generic.IReadOnlyCollection<Pokemon> pokemons)
        {
            PartyViewerPanel.SetParty(pokemons.ToList());
        }

        #endregion
    }
}
