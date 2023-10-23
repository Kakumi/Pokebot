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
using Pokebot.Models.Bots;
using Pokebot.Panels;
using Pokebot.Models.Versions;
using Pokebot.Factories;
using Pokebot.Models.Pokemons;

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
            get => IsLoaded && IsRomLoaded && APIContainer != null && GameVersion != null && !APIContainer.EmuClient.IsPaused();
        } 

        public IGameVersion? GameVersion { get; private set; }
        public IBot? Bot { get; private set; }

        protected override string WindowTitleStatic => "Pokébot";

        public Pokebot()
        {
            InitializeComponent();

            var configText = Encoding.UTF8.GetString(Resources.appconfig);
            AppConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<AppConfig>(configText)!;

            Log.LogReceived += Log_LogReceived;
        }

        private void Log_LogReceived(LogEventArgs e)
        {
            var item = new ListViewItem(e.Level.ToString());
            item.SubItems.Add(e.Message);

            _logsListView.Items.Add(item);

            for(int i = 0; i < _logsListView.Columns.Count; i++)
            {
                _logsListView.Columns[i].Width = -1;
            }
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
        }

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
                            _statusLabel.Text = isEmpty ? "No ROM loaded" : romName;
                        }
                        else if (AppConfig.Versions.Any(x => x.Hash.Equals(gameInfo.Hash, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            GameVersion = VersionFactory.Create(APIContainer, gameInfo.Hash);
                            _statusLabel.Text = romName;
                            Log.Info($"ROM {romName} loaded");
                            IsRomLoaded = true;
                        }
                        else
                        {
                            _statusLabel.Text = "This ROM is not supported";
                        }
                    }
                }
            } catch(Exception ex)
            {
                Log.Error(ex.Message);
                IsRomLoaded = false;
            }
        }

        protected override void UpdateAfter() //Or UpdateAfterFast
        {
            if (IsReady)
            {
                //Up
                //Down
                //Left
                //Right
                //A
                //B
                //L
                //Power
                //R
                //Select
                //Start

                //APIContainer?.Joypad.Set("Up", true);
            }
        }

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

        private void LoadTestClick(object sender, EventArgs e)
        {
            var success = APIContainer?.EmuClient.OpenRom(@$"D:\VisualStudioProjects\Pokebot\BizHawk\Roms\Pokemon - Version Emeraude (FR).gba");
            if (success ?? false)
            {
                IsRomLoaded = APIContainer?.EmuClient.LoadState("wild") ?? false; //starter
            }
        }

        private void TestStateClick(object sender, EventArgs e)
        {
            if (IsReady)
            {
                var symbols = SymbolReader.LoadSymbols(Resources.pokeemerald);

                var gMain = symbols.First(x => x.Name == "gMain");
                var gPlayerAvatar = symbols.First(x => x.Name == "gPlayerAvatar");

                var tests = new Dictionary<int, int>
            {
                { 0, 4 },
                { 4, 4 },
                { 0x438, 1 }
            };

                var cb1 = SymbolReader.ReadSymbol(APIContainer!, gMain.Address, 0, 4);
                var cb2 = SymbolReader.ReadSymbol(APIContainer!, gMain.Address, 4, 4);
                var playerState = SymbolReader.ReadSymbol(APIContainer!, gPlayerAvatar.Address, 2, 1);

                var addrCB1 = cb1.ToUInt32() - 1;
                var addrCB2 = cb2.ToUInt32() - 1;
                var gameState1 = symbols.FirstOrDefault(x => x.Address == addrCB1);
                var gameState2 = symbols.FirstOrDefault(x => x.Address == addrCB2);
                var playerStateIndex = (int)playerState[0];

                //var pokemonStorage = symbols.First(x => x.Name == "gPokemonStorage");
                //var bytesStorage = SymbolReader.ReadSymbol(APIContainer!, pokemonStorage.Address, 0, 100);
                //var currentBox = (int)bytesStorage[0];
                //var bytesPokemon = bytesStorage.Take(100);
                //var pokemon = GameVersion!.ParsePokemon(bytesPokemon.ToArray());

                var party = GameVersion!.GetParty();
                var opponent = GameVersion!.GetOpponent();

                AddPokemonStat(party[0]);
                AddPokemonStat(opponent);
            }
        }

        private void BotSelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (sender is ComboBox comboBox)
                {
                    _botPanel.Controls.Clear();

                    var value = (BotType)comboBox.SelectedItem;
                    var type = (BotCode)value.Code;
                    Control? control = null;

                    switch (type)
                    {
                        case BotCode.None:
                            break;
                        case BotCode.Starter:
                            control = new StarterControl();
                            break;
                        case BotCode.Spin:
                            break;
                        case BotCode.Egg:
                            break;
                    }

                    if (control != null)
                    {
                        control.Dock = DockStyle.Fill;
                        _botPanel.Controls.Add(control);
                    } else
                    {
                        Log.Error($"Bot type is not supported");
                    }
                }
            } catch(Exception)
            {
                Log.Error($"Error occurred while reading bot type.");
            }
        }

        public void AddPokemonStat(Pokemon pokemon)
        {
            var encounters = 1;
            var shinyEncounters = pokemon.Shiny ? 1 : 0;
            var found = false;

            for(int i = 0; i < listViewStats.Items.Count; i++)
            {
                var item = listViewStats.Items[i];
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

                listViewStats.Items.Add(item);
            }
        }
    }
}
