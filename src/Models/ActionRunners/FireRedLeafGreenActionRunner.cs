using BizHawk.Client.Common;
using Pokebot.Models.Memory;
using System;

namespace Pokebot.Models.ActionRunners
{
    public class FireRedLeafGreenActionRunner : CommonActionRunner
    {
        private StepStarterMode _stepStarter;

        public FireRedLeafGreenActionRunner(ApiContainer apiContainer, IGameMemory gameVersion) : base(apiContainer, gameVersion)
        {
            _stepStarter = StepStarterMode.None;
        }

        public override bool ExecuteStarter(int indexStarter)
        {
            var state = GameVersion.GetGameState();
            if (state == GameState.Overworld)
            {
                PressA();

                return false;
            }

            if (state == GameState.Battle)
            {
                return true;
            }

            var tasks = GameVersion.GetTasks();

            throw new NotImplementedException();
        }
    }
}
