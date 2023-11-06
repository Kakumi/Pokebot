using BizHawk.Client.Common;
using Pokebot.Exceptions;
using Pokebot.Factories.Versions;
using Pokebot.Models.Player;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pokebot.Models.ActionRunners
{
    public class EmeraldRubySapphireActionRunner : IActionRunner
    {
        public ApiContainer APIContainer { get; }
        public IGameVersion GameVersion { get; }
        private StepsStarter _stepStarter;
        private Dictionary<PlayerFacingDirection, string> _nextDirections;

        public EmeraldRubySapphireActionRunner(ApiContainer apiContainer, IGameVersion gameVersion)
        {
            APIContainer = apiContainer;
            GameVersion = gameVersion;
            _stepStarter = StepsStarter.None;
            _nextDirections = new Dictionary<PlayerFacingDirection, string>()
            {
                { PlayerFacingDirection.Up, "Right" },
                { PlayerFacingDirection.Right, "Down" },
                { PlayerFacingDirection.Down, "Left" },
                { PlayerFacingDirection.Left, "Up" },
            };
        }

        public bool ExecuteStarter(int indexStarter)
        {
            var state = GameVersion.GetGameState();
            if (state == GameState.Overworld)
            {
                APIContainer.Joypad.Set("A", true);

                return false;
            }

            if (state == GameState.Battle)
            {
                return true;
            }

            var tasks = GameVersion.GetTasks();
            var taskChoose = tasks.FirstOrDefault(x => x.Name == "Task_HandleStarterChooseInput" || x.Name == "Task_StarterChoose2");
            if (taskChoose != null)
            {
                if (taskChoose.Data[0] == indexStarter)
                {
                    APIContainer.Joypad.Set("A", true);
                    _stepStarter = StepsStarter.Confirm;
                }
                else if (taskChoose.Data[0] > indexStarter)
                {
                    APIContainer.Joypad.Set("Left", true);
                }
                else if (taskChoose.Data[0] < indexStarter)
                {
                    APIContainer.Joypad.Set("Right", true);
                }
            }

            if (_stepStarter == StepsStarter.Confirm)
            {
                var taskConfirmChoose = tasks.FirstOrDefault(x => x.Name == "Task_HandleConfirmStarterInput" || x.Name == "Task_StarterChoose5");
                if (taskConfirmChoose != null)
                {
                    APIContainer.Joypad.Set("A", true);
                }
            }

            return false;
        }

        public bool Spin()
        {
            var player = GameVersion.GetPlayer();
            if (_nextDirections.ContainsKey(player.FacingDirection))
            {
                APIContainer.Joypad.Set(_nextDirections[player.FacingDirection], true);

                return true;
            }

            return false;
        }

        public bool Escape()
        {
            var state = GameVersion.GetGameState();
            if (state == GameState.Battle)
            {
                var action = (BattleActionSelectionCursor) GameVersion.GetActionSelectionCursor();

                switch(action)
                {
                    case BattleActionSelectionCursor.Moves:
                        APIContainer.Joypad.Set("B", true);
                        APIContainer.Joypad.Set("Right", true);
                        return false;
                    case BattleActionSelectionCursor.Bag:
                        APIContainer.Joypad.Set("Down", true);
                        return false;
                    case BattleActionSelectionCursor.Team:
                        APIContainer.Joypad.Set("Up", true);
                        return false;
                    case BattleActionSelectionCursor.Escape:
                        APIContainer.Joypad.Set("A", true);
                        return false;
                }
            }

            return false;
        }

        private enum StepsStarter
        {
            None,
            Confirm
        }
    }
}
