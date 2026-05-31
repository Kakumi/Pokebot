using BizHawk.Client.Common;
using Pokebot.Exceptions;
using Pokebot.Factories.Versions;
using Pokebot.Models;
using Pokebot.Models.Player;
using Pokebot.Models.Pokemons;
using Pokebot.Panels;
using Pokebot.Utils;
using System;
using System.Windows.Forms;

namespace Pokebot.Factories.Bots
{
    internal class PathBot : IBot
    {
        public bool Enabled { get; private set; }

        public event IBot.PokemonEncounterEventHandler? PokemonEncountered;
        public event IBot.PokemonFoundEventHandler? PokemonFound;
        public event IBot.StateChangedEventHandler? StateChanged;

        public ApiContainer APIContainer { get; }
        public GameVersion GameVersion { get; }
        public PathControl Control { get; }

        private int _pathIndex;
        private Pokemon? _lastEncountered;

        public PathBot(ApiContainer apiContainer, GameVersion gameVersion)
        {
            Enabled = false;
            APIContainer = apiContainer;
            GameVersion = gameVersion;

            _pathIndex = 0;
            _lastEncountered = null;

            Control = new PathControl();
            Control.Dock = DockStyle.Fill;
            Control.SetFilterPanel(gameVersion.GenerationInfo);
            Control.FilterPanel.SetShinyHackVisible(gameVersion.Memory.CanSetShiny());
        }

        public void Start()
        {
            if (Control.GetPath().Count == 0)
            {
                throw new BotException(Messages.PathBot_EmptyPath);
            }

            var state = GameVersion.Memory.GetGameState();
            if (state != GameState.Overworld)
            {
                throw new BotException(Messages.SpinBot_StartOnlyMap);
            }

            _pathIndex = 0;
            _lastEncountered = null;
            Enabled = true;
            StateChanged?.Invoke(Enabled);
        }

        public void Stop()
        {
            Enabled = false;
            StateChanged?.Invoke(Enabled);
        }

        public void Execute(PlayerData playerData, GameState state)
        {
            if (state == GameState.Overworld)
            {
                ExecuteOverworld();
                return;
            }

            if (state == GameState.Battle || state == GameState.BagMenu)
            {
                ExecuteBattle();
            }
        }

        private void ExecuteOverworld()
        {
            var path = Control.GetPath();
            if (_pathIndex < path.Count)
            {
                ExecutePathAction(path[_pathIndex]);
                _pathIndex++;
                _lastEncountered = null;
            }
            else
            {
                APIContainer.Joypad.SetWhenInactive("A");
            }
        }

        private void ExecutePathAction(PathAction action)
        {
            switch (action)
            {
                case PathAction.Up:
                    APIContainer.Joypad.SetWhenInactive("Up");
                    break;
                case PathAction.Down:
                    APIContainer.Joypad.SetWhenInactive("Down");
                    break;
                case PathAction.Left:
                    APIContainer.Joypad.SetWhenInactive("Left");
                    break;
                case PathAction.Right:
                    APIContainer.Joypad.SetWhenInactive("Right");
                    break;
                default:
                    throw new NotSupportedException(Messages.BotFactory_NotSupported);
            }
        }

        private void ExecuteBattle()
        {
            Pokemon pokemon = GameVersion.Memory.GetOpponent();
            if (GameVersion.Memory.CanSetShiny() && Control.FilterPanel.IsShinyHackEnabled() && !pokemon.IsShiny)
            {
                pokemon = GameVersion.Memory.SetShiny(pokemon);
                return;
            }

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
            }
            else
            {
                GameVersion.Runner.Escape();
            }
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
