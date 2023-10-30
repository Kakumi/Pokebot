using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Config
{
    public class PokemonStarterConfig
    {
        public int PositionIndex { get; }
        public int PokemonId { get; }

        [JsonConstructor]
        public PokemonStarterConfig(int positionIndex, int pokemonId)
        {
            PositionIndex = positionIndex;
            PokemonId = pokemonId;
        }
    }
}
