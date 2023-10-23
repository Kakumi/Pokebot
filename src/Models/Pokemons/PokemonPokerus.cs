using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Pokemons
{
    public class PokemonPokerus
    {
        public int Days { get; }
        public int Strain { get; }

        public PokemonPokerus(int days, int strain)
        {
            Days = days;
            Strain = strain;
        }
    }
}
