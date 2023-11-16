using System;

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
