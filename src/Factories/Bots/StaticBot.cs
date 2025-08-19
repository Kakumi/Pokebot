using BizHawk.Client.Common;
using Pokebot.Factories.Versions;
using Pokebot.Models.Player;
using Pokebot.Models.Pokemons;
using Pokebot.Models;
using Pokebot.Panels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pokebot.Utils;

namespace Pokebot.Factories.Bots
{
    public abstract class StaticBot : IBot
    {
        public bool Enabled { get; private set; }
        public ApiContainer APIContainer { get; }
        public GameVersion GameVersion { get; }
        public StaticControl Control { get; }

        public event IBot.PokemonEncounterEventHandler? PokemonEncountered;
        public event IBot.PokemonFoundEventHandler? PokemonFound;
        public event IBot.StateChangedEventHandler? StateChanged;

        private List<uint> _seedsHistory;

        public StaticBot(ApiContainer apiContainer, GameVersion gameVersion)
        {
            Enabled = false;
            APIContainer = apiContainer;
            GameVersion = gameVersion;

            _seedsHistory = new List<uint>();

            Control = new StaticControl();
            Control.Dock = DockStyle.Fill;
            Control.SetFilterPanel(GameVersion.GenerationInfo);
        }

        public void Start()
        {
            Enabled = true;
            StateChanged?.Invoke(Enabled);

            bool shouldLoad = false;
            if (APIContainer.EmuClient.HasSaveState(GetSaveStateName()))
            {
                var result = MessageBox.Show(Messages.Bot_FileExistReplaceMessage, Messages.Bot_FileExistReplaceTitle, MessageBoxButtons.YesNo);
                shouldLoad = result == DialogResult.Yes;
            }

            bool loaded = false;
            if (shouldLoad)
            {
                try
                {
                    loaded = APIContainer.EmuClient.LoadState(GetSaveStateName());
                }
                catch (FileNotFoundException) //If the save state doesn't exists
                {
                }
            }

            if (!loaded)
            {
                APIContainer.EmuClient.SaveState(GetSaveStateName());
            }

            UpdateRNG();
        }

        public void Stop()
        {
            Enabled = false;
            StateChanged?.Invoke(Enabled);
        }

        protected void Check(Pokemon pokemon)
        {
            if (pokemon != null)
            {
                PokemonEncountered?.Invoke(pokemon);
            }

            if (pokemon != null && Control.FilterPanel.Comparator.Compare(pokemon))
            {
                Log.Warn(Messages.Pokemon_FoundCatch);
                PokemonFound?.Invoke(pokemon);
                Stop();
            }
            else
            {
                if (APIContainer.EmuClient.LoadOrStop(GetSaveStateName()))
                {
                    UpdateRNG();
                }
            }
        }

        public abstract void Execute(PlayerData playerData, GameState state);

        protected virtual string GetSaveStateName()
        {
            return $"{GameVersion.VersionInfo.Name}_static";
        }

        private void UpdateRNG()
        {
            uint random;
            do
            {
                random = GameVersion.Memory.RandomizeCurrentSeed();
            } while (_seedsHistory.Contains(random));

            _seedsHistory.Add(random);
        }

        public UserControl GetPanel()
        {
            return Control;
        }

        public bool UseDelay()
        {
            return true;
        }

        public void UpdateUI(GameState state)
        {
        }
    }
}
