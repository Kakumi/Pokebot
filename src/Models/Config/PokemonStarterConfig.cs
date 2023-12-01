using Newtonsoft.Json;

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
