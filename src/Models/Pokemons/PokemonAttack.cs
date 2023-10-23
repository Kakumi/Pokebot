using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Pokemons
{
    public class PokemonAttack
    {
        public PokemonMove MoveInfo { get; }
        public int PP { get; }

        public PokemonAttack(PokemonMove moveInfo, int pP)
        {
            MoveInfo = moveInfo;
            PP = pP;
        }
    }
}
