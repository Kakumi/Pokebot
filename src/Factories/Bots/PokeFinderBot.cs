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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokebot.Factories.Bots
{
    public class PokeFinderBot : IBot
    {
        public bool Enabled { get; private set; }

        public event IBot.PokemonEncounterEventHandler? PokemonEncountered;
        public event IBot.PokemonFoundEventHandler? PokemonFound;
        public event IBot.StateChangedEventHandler? StateChanged;

        public ApiContainer APIContainer { get; }
        public GameVersion GameVersion { get; }
        public PokeFinderControl Control { get; }

        public PokeFinderBot(ApiContainer apiContainer, GameVersion gameVersion)
        {
            Enabled = false;
            APIContainer = apiContainer;
            GameVersion = gameVersion;

            Control = new PokeFinderControl();
            Control.RetryClick += Control_RetryClick;
            Control.Dock = DockStyle.Fill;
            Control.SetInfo(gameVersion.Memory.GetTID(), gameVersion.Memory.GetSID());
            Control.SetPokeFinderTypes(GameVersion.VersionInfo.PokeFinderTypes);
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
                if (APIContainer.Emulation.FrameCount() >= Control.GetTargetFrame())
                {
                    throw new BotException(Messages.BotPokeFinder_InvalidFrame);
                }
                else
                {
                    APIContainer.EmuClient.SaveState(GetSaveStateName());
                }
            }
        }

        public void Stop()
        {
            Enabled = false;
            StateChanged?.Invoke(Enabled);
        }

        public void Execute(PlayerData playerData, GameState state)
        {
            var frame = APIContainer.Emulation.FrameCount();
            long targetFrame = Control.GetTargetFrame();
            Control.SetCurrentFrame(frame);

            if (frame == targetFrame)
            {
                APIContainer.Joypad.Set("A", true);
            } else if (frame >= targetFrame)
            {
                if (state == GameState.Battle)
                {
                    var party = GameVersion.Memory.GetParty();
                    if (party.Count > 0)
                    {
                        var pokemon = party[0];
                        PokemonEncountered?.Invoke(pokemon);
                        Control.SetPID(pokemon.PID.ToString("X"));
                        if (pokemon.PID.ToString("X") == Control.GetPID())
                        {
                            Log.Info(Messages.Pokemon_Found);
                            PokemonFound?.Invoke(pokemon);
                        }

                        Stop();
                    }
                }
            }
        }

        private void Control_RetryClick()
        {
            _ = APIContainer.EmuClient.LoadOrStop(GetSaveStateName());
            Enabled = true;
            StateChanged?.Invoke(Enabled);

            if (APIContainer.Emulation.FrameCount() >= Control.GetTargetFrame())
            {
                throw new BotException(Messages.BotPokeFinder_InvalidFrame);
            }
        }

        public UserControl GetPanel()
        {
            return Control;
        }

        private string GetSaveStateName()
        {
            return $"{GameVersion.VersionInfo.Name}_pokefinder";
        }

        public bool UseDelay()
        {
            return false;
        }

        public void UpdateUI(GameState state)
        {
            var frame = APIContainer.Emulation.FrameCount();
            Control.SetCurrentFrame(frame);
        }
    }
}
