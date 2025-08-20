using BizHawk.Client.Common;
using Pokebot.Models.Memory;
using Pokebot.Models.Player;
using System.Collections.Generic;

namespace Pokebot.Models.ActionRunners
{
    public abstract class CommonActionRunner : IActionRunner
    {
        public ApiContainer APIContainer { get; }
        public IGameMemory GameVersion { get; }
        protected Dictionary<PlayerFacingDirection, string> NextDirections { get; private set; }

        public CommonActionRunner(ApiContainer apiContainer, IGameMemory gameVersion)
        {
            APIContainer = apiContainer;
            GameVersion = gameVersion;
            NextDirections = new Dictionary<PlayerFacingDirection, string>()
            {
                { PlayerFacingDirection.Up, "Right" },
                { PlayerFacingDirection.Right, "Down" },
                { PlayerFacingDirection.Down, "Left" },
                { PlayerFacingDirection.Left, "Up" },
            };
        }

        public abstract bool ExecuteStarter(int indexStarter);

        public virtual bool Spin()
        {
            var player = GameVersion.GetPlayer();
            if (NextDirections.ContainsKey(player.FacingDirection))
            {
                APIContainer.Joypad.Set(NextDirections[player.FacingDirection], true);

                return true;
            }

            return false;
        }

        public virtual bool Escape()
        {
            var state = GameVersion.GetGameState();
            if (state == GameState.Battle)
            {
                var action = (BattleActionSelectionCursor)GameVersion.GetActionSelectionCursor();

                switch (action)
                {
                    case BattleActionSelectionCursor.Moves:
                        PressB();
                        PressRight();
                        return false;
                    case BattleActionSelectionCursor.Bag:
                        PressDown();
                        return false;
                    case BattleActionSelectionCursor.Team:
                        PressUp();
                        return false;
                    case BattleActionSelectionCursor.Escape:
                        PressA();
                        return false;
                }
            }

            return false;
        }

        public virtual bool UseRegisteredItem()
        {
            PressSelect();

            return true;
        }

        public void PressA() => APIContainer.Joypad.Set("A", true);
        public void PressB() => APIContainer.Joypad.Set("B", true);
        public void PressX() => APIContainer.Joypad.Set("X", true);
        public void PressY() => APIContainer.Joypad.Set("Y", true);
        public void PressLeft() => APIContainer.Joypad.Set("Left", true);
        public void PressRight() => APIContainer.Joypad.Set("Right", true);
        public void PressDown() => APIContainer.Joypad.Set("Down", true);
        public void PressUp() => APIContainer.Joypad.Set("Up", true);
        public void PressStart() => APIContainer.Joypad.Set("Start", true);
        public void PressSelect() => APIContainer.Joypad.Set("Select", true);
    }
}
