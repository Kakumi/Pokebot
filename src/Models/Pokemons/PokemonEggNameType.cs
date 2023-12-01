using System;

namespace Pokebot.Models.Pokemons
{
    [Flags]
    public enum PokemonEggNameType
    {
        None = 0,
        BadEgg = 1,
        HasSpecies = 2,
        EggName = 4,
        //Bits 3-7: These bits are unused, and are just padding for the three flags. They should be set to 0.
    }
}
