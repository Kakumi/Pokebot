using Newtonsoft.Json;

namespace Pokebot.Models.Pokemons
{
    public class PokemonItem
    {
        public int Id { get; }
        public string Name { get; }

        [JsonConstructor]
        public PokemonItem(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
