using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Pokemons
{
    public class PokemonCondition
    {
        public int Coolness { get; }
        public int Beauty { get; }
        public int Cuteness { get; }
        public int Smartness { get; }
        public int Toughness { get; }
        public int Feel { get; }

        public PokemonCondition(int coolness, int beauty, int cuteness, int smartness, int toughness, int feel)
        {
            Coolness = coolness;
            Beauty = beauty;
            Cuteness = cuteness;
            Smartness = smartness;
            Toughness = toughness;
            Feel = feel;
        }
    }
}
