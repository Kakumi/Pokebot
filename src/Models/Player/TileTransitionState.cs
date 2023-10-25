using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Player
{
    public enum TileTransitionState
    {
        NotMoving = 0,
        BetweenTiles = 1,
        KeepMoving = 2
    }
}
