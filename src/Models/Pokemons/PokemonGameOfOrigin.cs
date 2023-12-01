using Newtonsoft.Json;

namespace Pokebot.Models.Pokemons
{
    public class PokemonGameOfOrigin
    {
        public int Id { get; }
        public string Name { get; }

        [JsonConstructor]
        public PokemonGameOfOrigin(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
