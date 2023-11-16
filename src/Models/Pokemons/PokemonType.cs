using Newtonsoft.Json;

namespace Pokebot.Models.Pokemons
{
    public class PokemonType
    {
        public int Id { get; }
        public string Name { get; }

        [JsonConstructor]
        public PokemonType(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
