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
    public class LegendaryBot : StaticBot
    {
        public LegendaryBot(ApiContainer apiContainer, GameVersion gameVersion) : base(apiContainer, gameVersion)
        {
        }

        public override void Execute(PlayerData playerData, GameState state)
        {
            if (state == GameState.Battle)
            {
                Pokemon pokemon = GameVersion.Memory.GetOpponent();
                Check(pokemon);
            } else
            {
                APIContainer.Joypad.Set("A", true);
            }
        }
    }
}
