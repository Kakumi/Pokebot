using Pokebot.Models.Pokemons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Versions
{
    public interface IGameVersion
    {
        IReadOnlyList<Pokemon> GetParty();
        Pokemon GetOpponent();
        Pokemon ParsePokemon(byte[] bytesPokemon);
    }
}
