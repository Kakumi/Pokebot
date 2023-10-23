using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Pokemons
{
    [Flags]
    public enum PokemonStatusType
    {
        None = 0,
        Sleep = 1 | 2 | 4,
        Poison = 8,
        Burn = 16,
        Freeze = 32,
        Paralysis = 64,
        BadPoison = 128
    }
}
