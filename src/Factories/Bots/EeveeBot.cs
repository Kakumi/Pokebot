using BizHawk.Client.Common;
using Pokebot.Factories.Versions;
using Pokebot.Models.Player;
using Pokebot.Models.Pokemons;
using Pokebot.Models;
using Pokebot.Panels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pokebot.Utils;

namespace Pokebot.Factories.Bots
{
    public class EeveeBot : StaticBot
    {
        public EeveeBot(ApiContainer apiContainer, GameVersion gameVersion) : base(apiContainer, gameVersion)
        {
        }

        public override void Execute(PlayerData playerData, GameState state)
        {
            var task = GameVersion.Memory.GetTasks().FirstOrDefault(x => x.Name == "Task_YesNoMenu_HandleInput");
            if (task != null)
            {
                Pokemon pokemon = GameVersion.Memory.GetParty().FirstOrDefault(x => x.DexId == 133);
                Check(pokemon);
            } else
            {
                APIContainer.Joypad.Set("A", true);
            }
        }

        protected override string GetSaveStateName()
        {
            return $"{GameVersion.VersionInfo.Name}_eevee";
        }
    }
}
