using Newtonsoft.Json;

namespace Pokebot.Models.Pokemons
{
    public class PokemonLocation
    {
        public int Id { get; }
        public string Name { get; }

        [JsonConstructor]
        public PokemonLocation(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
