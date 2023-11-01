using BizHawk.Client.Common;
using Pokebot.Models.Config;
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
using Pokebot.Factories.Versions;
using Pokebot.Exceptions;
using System.IO;

namespace Pokebot.Factories.Bots
{
    public class StarterBot : IBot
    {
        public bool Enabled { get; private set; }
        public ApiContainer APIContainer { get; }
        public IGameVersion GameVersion { get; }
        public StarterControl Control { get; }

        public event IBot.PokemonEncounterEventHandler? PokemonEncountered;
        public event IBot.StateChangedEventHandler? StateChanged;

        private List<uint> _seedsHistory;

        public StarterBot(ApiContainer apiContainer, IGameVersion gameVersion)
        {
            APIContainer = apiContainer;
            GameVersion = gameVersion;
            Enabled = false;

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

            var playerData = GameVersion.GetPlayer();

            bool loaded = false;
            try
            {
                loaded = APIContainer.EmuClient.LoadState(GetSaveStateName());
            } catch(FileNotFoundException) //If the save state doesn't exists
            {
            } finally
            {
                if (!loaded)
                {
                    var starterBotConfig = GameVersion.VersionInfo.BotConfig.Starter;
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

                if (GameVersion.GetPartyCount() != 0)
                {
                    throw new BotException(Messages.BotStarter_PartyNotEmpty);
                }

                UpdateSeed();
            }
        }

        public void Stop()
        {
            Enabled = false;
            StateChanged?.Invoke(Enabled);
        }

        public void Execute(PlayerData playerData, GameState state)
        {
            var starter = GameVersion.VersionInfo.Starters.FirstOrDefault(x => x.PokemonId == Control.FilterPanel.Comparator.IndexPokemon);
            if (starter != null && GameVersion.ActionRunner.ExecuteStarter(starter.PositionIndex))
            {
                var pokemon = GameVersion.GetParty()[0];
                PokemonEncountered?.Invoke(pokemon);
                if (Control.FilterPanel.Comparator.Compare(pokemon))
                {
                    Log.Info(Messages.Pokemon_Found);
                    Stop();
                }
                else
                {
                    LoadOrStop();
                }
            }
        }

        private string GetSaveStateName()
        {
            return $"{GameVersion.VersionInfo.Name}_starter";
        }

        private void LoadOrStop()
        {
            bool loaded = false;
            try
            {
                loaded = APIContainer.EmuClient.LoadState(GetSaveStateName());
            }
            catch (FileNotFoundException) //If the save state doesn't exists
            {
                
            } finally
            {
                if (loaded)
                {
                    UpdateSeed();
                } else
                {
                    throw new BotException(Messages.BotStarter_InvalidSaveState);
                }
            }
        }

        private void UpdateSeed()
        {
            uint random;
            do
            {
                random = GameVersion.SetRandomSeed();
            } while (_seedsHistory.Contains(random));

            _seedsHistory.Add(random);
        }
    }
}
