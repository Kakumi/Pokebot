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
    internal class FishingBot : IBot
    {
        public bool Enabled { get; private set; }

        public event IBot.PokemonEncounterEventHandler? PokemonEncountered;
        public event IBot.PokemonFoundEventHandler? PokemonFound;
        public event IBot.StateChangedEventHandler? StateChanged;

        public ApiContainer APIContainer { get; }
        public GameVersion GameVersion { get; }
        public SpinControl Control { get; }

        public FishingBot(ApiContainer apiContainer, GameVersion gameVersion)
        {
            Enabled = false;
            APIContainer = apiContainer;
            GameVersion = gameVersion;

            Control = new SpinControl();
            Control.Dock = DockStyle.Fill;
            Control.SetFilterPanel(GameVersion.GenerationInfo);
        }

        public void Start()
        {
            Enabled = true;
            StateChanged?.Invoke(Enabled);

            var state = GameVersion.Memory.GetGameState();
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

        public void Execute(PlayerData playerData, GameState state)
        {
            if (GameVersion.Memory.GetTasks().FirstOrDefault(t => t.Name == "Task_ContinueTaskAfterMessagePrints") != null)
            {
                Log.Error(Messages.InvalidRegisteredItem);
                Stop();
                return;
            }

            var task = GameVersion.Memory.GetTasks().FirstOrDefault(t => t.Name == "Task_Fishing");
            if (task == null)
            {
                if (state == GameState.Battle)
                {
                    Pokemon pokemon = GameVersion.Memory.GetOpponent();
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
                        GameVersion.Runner.Escape();
                    }
                } else
                {
                    GameVersion.Runner.UseRegisteredItem();
                }
            }
            else if(task.Data[0] == (int)FishingResult.FISHING_STATE_REEL_WINDOW || task.Data[0] == (int)FishingResult.FISHING_STATE_START_ENCOUNTER || task.Data[0] == (int)FishingResult.FISHING_STATE_CLEANUP)
            {
                APIContainer.Joypad.SetWhenInactive("A");
            }
        }

        public UserControl GetPanel()
        {
            return Control;
        }

        public bool UseDelay()
        {
            return false;
        }

        public void UpdateUI(GameState state)
        {
        }
    }
}
