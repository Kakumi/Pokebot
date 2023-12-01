using System;

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
