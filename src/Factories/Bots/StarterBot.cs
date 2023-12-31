﻿using BizHawk.Client.Common;
using Pokebot.Exceptions;
using Pokebot.Factories.Versions;
using Pokebot.Models;
using Pokebot.Models.Player;
using Pokebot.Panels;
using Pokebot.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Pokebot.Factories.Bots
{
    public class StarterBot : IBot
    {
        public bool Enabled { get; private set; }
        public ApiContainer APIContainer { get; }
        public GameVersion GameVersion { get; }
        public StarterControl Control { get; }

        public event IBot.PokemonEncounterEventHandler? PokemonEncountered;
        public event IBot.StateChangedEventHandler? StateChanged;
        public event IBot.PokemonFoundEventHandler? PokemonFound;

        private List<uint> _seedsHistory;

        public StarterBot(ApiContainer apiContainer, GameVersion gameVersion)
        {
            Enabled = false;
            APIContainer = apiContainer;
            GameVersion = gameVersion;

            _seedsHistory = new List<uint>();

            Control = new StarterControl();
            Control.Dock = DockStyle.Fill;
            Control.SetFilterPanel(GameVersion.GenerationInfo);
            Control.FilterPanel.SetPokemons(GameVersion.VersionInfo.Starters.Select(x =>
            {
                return GameVersion.GenerationInfo.Pokemons.FirstOrDefault(y => y.Id == x.PokemonId);
            }).Where(x => x != null), false);
        }

        public UserControl GetPanel()
        {
            return Control;
        }

        public void Start()
        {
            Enabled = true;
            StateChanged?.Invoke(Enabled);

            var playerData = GameVersion.Memory.GetPlayer();

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
                var starterBotConfig = GameVersion.VersionInfo.BotsConfig.Starter;
                //Check if the player is at the right position
                if (playerData.Position == starterBotConfig.Position && starterBotConfig.Facing == PlayerFacingDirection.Up)
                {
                    APIContainer.EmuClient.SaveState(GetSaveStateName());
                }
                else
                {
                    throw new BotException(Messages.BotStarter_WrongStartPosition);
                }
            }

            if (GameVersion.Memory.GetPartyCount() != 0)
            {
                throw new BotException(Messages.BotStarter_PartyNotEmpty);
            }

            UpdateRNG();
        }

        public void Stop()
        {
            Enabled = false;
            StateChanged?.Invoke(Enabled);
        }

        public void Execute(PlayerData playerData, GameState state)
        {
            var starter = GameVersion.VersionInfo.Starters.FirstOrDefault(x => x.PokemonId == Control.FilterPanel.Comparator.IndexPokemon);
            if (starter != null && GameVersion.Runner.ExecuteStarter(starter.PositionIndex))
            {
                var pokemon = GameVersion.Memory.GetParty()[0];
                PokemonEncountered?.Invoke(pokemon);
                if (Control.FilterPanel.Comparator.Compare(pokemon))
                {
                    Log.Info(Messages.Pokemon_Found);
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
        }

        private string GetSaveStateName()
        {
            return $"{GameVersion.VersionInfo.Name}_starter";
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

        public bool UseDelay()
        {
            return true;
        }

        public void UpdateUI(GameState state)
        {
        }
    }
}
