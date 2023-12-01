using BizHawk.Client.Common;
using Pokebot.Models.Memory;
using System.Linq;

namespace Pokebot.Models.ActionRunners
{
    public class EmeraldRubySapphireActionRunner : CommonActionRunner
    {
        private StepStarterMode _stepStarter;

        public EmeraldRubySapphireActionRunner(ApiContainer apiContainer, IGameMemory gameVersion) : base(apiContainer, gameVersion)
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
            var taskChoose = tasks.FirstOrDefault(x => x.Name == "Task_HandleStarterChooseInput" || x.Name == "Task_StarterChoose2");
            if (taskChoose != null)
            {
                if (taskChoose.Data[0] == indexStarter)
                {
                    PressA();
                    _stepStarter = StepStarterMode.Confirm;
                }
                else if (taskChoose.Data[0] > indexStarter)
                {
                    PressLeft();
                }
                else if (taskChoose.Data[0] < indexStarter)
                {
                    PressRight();
                }
            }

            if (_stepStarter == StepStarterMode.Confirm)
            {
                var taskConfirmChoose = tasks.FirstOrDefault(x => x.Name == "Task_HandleConfirmStarterInput" || x.Name == "Task_StarterChoose5");
                if (taskConfirmChoose != null)
                {
                    PressA();
                }
            }

            return false;
        }
    }
}
