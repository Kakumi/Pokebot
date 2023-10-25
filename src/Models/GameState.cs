using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models
{
    public enum GameState
    {
        Unknow = 0,
        StartBattle = 1,
        Battle = 2,
        EndBattle = 3,
        ChooseStarter = 4,
        BagMenu = 5,
        TitleScreen = 6,
        MainMenu = 7,
        ChangeMap = 8,
        PartyMenu = 9,
        Overworld = 10,
    }
}
