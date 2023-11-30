using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using System.Diagnostics;
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
        public SettingsPanel SettingsPanel { get; private set; }

        public PokemonWatcher? PokemonWatcher { get; private set; }
        public PokebotDebug? DebugWindow { get; private set; }
        public GithubServices GithubServices { get; private set; }
        public DiscordWebhookServices? DiscordWebhookServices { get; private set; }

        WaitTask? _waitTask;

        #region Init

        public Pokebot()
        {
            InitializeComponent();

            var configText = Encoding.UTF8.GetString(Resources.appconfig);
            AppConfig = JsonConvert.DeserializeObject<AppConfig>(configText)!;
            GithubServices = new GithubServices(AppConfig.Github.Url);

            _versionLabel.Text = $"{WindowTitleStatic} v{GetType().Assembly.GetName().Version}";
            _newVersionLabel.Hide();
            _tabControl.Hide();

            CreateTabPages();

            var worker = new BackgroundWorker();
            worker.DoWork += GetGithubLatestReleaseWorker;
            worker.RunWorkerAsync();
        }

        private async void GetGithubLatestReleaseWorker(object sender, DoWorkEventArgs e)
        {
            try
            {
                var latestRelease = await GithubServices.GetLatestRelease(AppConfig.Github.Owner, AppConfig.Github.Repository);
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
            SettingsPanel_SettingsConfigChanged(SettingsPanel!.SettingsConfig);

#if DEBUG
            if (APIContainer != null && DebugWindow == null)
            {
                DebugWindow = new PokebotDebug(APIContainer);
                DebugWindow.Show();
            }
#endif
        }

        private void CreateTab(UserControl userControl, string name)
        {
            TabPage tabPage = new TabPage(name);
            tabPage.Controls.Add(userControl);
            _tabControl.TabPages.Add(tabPage);
        }

        private void CreateTabPages()
        {
            LogsPanel = new LogsPanel();

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

            SettingsPanel = new SettingsPanel();
            SettingsPanel.SettingsConfigChanged += SettingsPanel_SettingsConfigChanged;
            SettingsPanel.PauseClicked += SettingsPanel_PauseClicked;
            SettingsPanel.SeedClicked += SettingsPanel_SeedClicked;
            SettingsPanel.Dock = DockStyle.Fill;

            CreateTab(SettingsPanel, Messages.Tab_SettingsPanel);
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
                SettingsPanel.SetSeed(GameVersion!.Memory.GetSeed());
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
#if DEBUG
                            if (DebugWindow != null)
                            {
                                DebugWindow.SetGameVersion(GameVersion);
                            }
#endif
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

#if DEBUG
                        if (DebugWindow != null)
                        {
                            DebugWindow.Execute(state);
                        }
#endif
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

        #region Pokebot Events

        private void _bugButton_Click(object sender, EventArgs e)
        {
            GithubServices.OpenIssues(AppConfig.Github.Owner, AppConfig.Github.Repository);
        }

        private void _joinDiscordButton_Click(object sender, EventArgs e)
        {
            Process.Start(AppConfig.DiscordInvite);
        }

        #endregion

        #region Settings Events

        private void SettingsPanel_SettingsConfigChanged(SettingsConfig settingsConfig)
        {
            APIContainer?.Emulation.LimitFramerate(!settingsConfig.Speed);
            APIContainer?.EmuClient.SetSoundOn(settingsConfig.Sound);
            _waitTask = new WaitTask(settingsConfig.DelayBetweenActions);

            if (string.IsNullOrWhiteSpace(settingsConfig.DiscordWebhook))
            {
                DiscordWebhookServices = null;
            } else
            {
                DiscordWebhookServices = new DiscordWebhookServices(settingsConfig.DiscordWebhook, settingsConfig.DiscordUserID);
            }
        }

        private void SettingsPanel_PauseClicked()
        {
            APIContainer?.EmuClient.TogglePause();
        }

        private void SettingsPanel_SeedClicked(uint seed)
        {
            GameVersion!.Memory.SetSeed(seed);
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
            if (DiscordWebhookServices != null)
            {
                DiscordWebhookServices.SendPokemonWebhook(pokemon);
            }
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
