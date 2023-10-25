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

        private int _indexStarter = 0;
        private List<uint> _seedsHistory;

        public StarterBot(ApiContainer apiContainer, IGameVersion gameVersion)
        {
            APIContainer = apiContainer;
            GameVersion = gameVersion;
            Enabled = false;

            _seedsHistory = new List<uint>();

            Control = new StarterControl();
            Control.SetStarters(GameVersion.GetStarters());
            Control.Dock = DockStyle.Fill;
            Control.StarterChanged += Control_StarterChanged;
        }

        public UserControl GetPanel()
        {
            return Control;
        }

        private void Control_StarterChanged(ComboBox e, int index)
        {
            _indexStarter = index;
        }

        public void Start()
        {
            Enabled = true;

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
                    //Check if the player is at the right position
                    if (playerData.CurrentX == 14 && playerData.CurrentY == 22 && playerData.FacingDirection == PlayerFacingDirection.Up)
                    {
                        APIContainer.EmuClient.SaveState(GetSaveStateName());
                    }
                    else
                    {
                        throw new BotException($"Bot is unable to start because the player is not in front of starters.");
                    }
                }

                if (GameVersion.GetPartyCount() != 0)
                {
                    throw new BotException("This bot cannot be used because the party is not empty.");
                }

                UpdateSeed();
            }
        }

        public void Stop()
        {
            Enabled = false;
        }

        public void Execute(PlayerData playerData, GameState state)
        {
            if (GameVersion.ExecuteStarter(_indexStarter))
            {
                var pokemon = GameVersion.GetParty()[0];
                PokemonEncountered?.Invoke(pokemon);
                if (pokemon.IsShiny)
                {
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
            return $"{GameVersion.GetVersionName()}_starter";
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
                    throw new BotException($"Bot is unable to start because the save state doesn't exists anymore.");
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
