using Newtonsoft.Json;

namespace Pokebot.Models.Pokemons
{
    public class PokemonNature
    {
        public int Id { get; }
        public string Name { get; }

        [JsonConstructor]
        public PokemonNature(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
