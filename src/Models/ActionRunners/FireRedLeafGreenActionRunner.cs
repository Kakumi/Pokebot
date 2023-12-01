using BizHawk.Client.Common;
using Pokebot.Models.Memory;
using System;

namespace Pokebot.Models.ActionRunners
{
    public class FireRedLeafGreenActionRunner : CommonActionRunner
    {
        public FireRedLeafGreenActionRunner(ApiContainer apiContainer, IGameMemory gameVersion) : base(apiContainer, gameVersion)
        {
        }

        public override bool ExecuteStarter(int indexStarter)
        {
            var state = GameVersion.GetGameState();
            if (state == GameState.Overworld)
            {
                var starterBotConfig = GameVersion.VersionInfo.BotsConfig.Starter;
                var player = GameVersion.GetPlayer();
                var playerIndex = (player.Position.X - starterBotConfig.Position.X) + 1; //0 = left, 1 = center, 2 = right
                if (playerIndex == indexStarter)
                {
                    if (player.FacingDirection != starterBotConfig.Facing)
                    {
                        PressUp();
                    } else
                    {
                        PressA();
                    }
                }
                else if (playerIndex > indexStarter)
                {
                    PressLeft();
                }
                else if (playerIndex < indexStarter)
                {
                    PressRight();
                }

                return false;
            }

            if (state == GameState.NamingScreen)
            {
                return true;
            }

            return false;
        }
    }
}
