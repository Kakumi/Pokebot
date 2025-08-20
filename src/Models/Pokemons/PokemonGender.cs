using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Pokemons
{
    public enum PokemonGender
    {
        Unknow = -1,
        Male = 0x00,
        Female = 0xFE,
        Genderless = 0xFF
    }
}
