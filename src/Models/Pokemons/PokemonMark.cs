using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Pokemons
{
    [Flags]
    public enum PokemonMark
    {
        None = 0,
        Circle = 1,
        Square = 2,
        Triangle = 4,
        Heart = 8
    }
}
