using BizHawk.Client.Common;
using Pokebot.Exceptions;
using Pokebot.Factories.Versions;
using Pokebot.Models;
using Pokebot.Models.Player;
using Pokebot.Models.Pokemons;
using Pokebot.Panels;
using Pokebot.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokebot.Factories.Bots
{
    public class SpinBot : IBot
    {
        public bool Enabled { get; private set; }
        public ApiContainer APIContainer { get; }
        public IGameVersion GameVersion { get; }
        public SpinControl Control { get; }

        public event IBot.PokemonEncounterEventHandler? PokemonEncountered;
        public event IBot.StateChangedEventHandler? StateChanged;
        public event IBot.PokemonFoundEventHandler? PokemonFound;

        private Pokemon? _lastEncountered;

        public SpinBot(ApiContainer apiContainer, IGameVersion gameVersion)
        {
            Enabled = false;
            APIContainer = apiContainer;
            GameVersion = gameVersion;
            _lastEncountered = null;

            Control = new SpinControl();
            Control.Dock = DockStyle.Fill;
            Control.SetFilterPanel(gameVersion.GenerationInfo);
        }

        public void Execute(PlayerData playerData, GameState state)
        {
            if (state == GameState.Overworld)
            {
                if (!GameVersion.ActionRunner.Spin())
                {
                    throw new BotException(Messages.SpinBot_UnknowNextMove);
                }
            }
            else if (state == GameState.Battle || state == GameState.BagMenu)
            {
                Pokemon pokemon = GameVersion.GetOpponent();
                if (_lastEncountered?.Checksum != pokemon.Checksum)
                {
                    _lastEncountered = pokemon;
                    PokemonEncountered?.Invoke(pokemon);
                }

                if (Control.FilterPanel.Comparator.Compare(pokemon))
                {
                    Log.Warn(Messages.Pokemon_FoundCatch);
                    PokemonFound?.Invoke(pokemon);
                    Stop();
                } else
                {
                    GameVersion.ActionRunner.Escape();
                }
            }
        }

        public void Start()
        {
            Enabled = true;
            StateChanged?.Invoke(Enabled);

            var state = GameVersion.GetGameState();
            if (state != GameState.Overworld)
            {
                throw new BotException(Messages.SpinBot_StartOnlyMap);
            }
        }

        public void Stop()
        {
            Enabled = false;
            StateChanged?.Invoke(Enabled);
        }

        public UserControl GetPanel()
        {
            return Control;
        }
    }
}
