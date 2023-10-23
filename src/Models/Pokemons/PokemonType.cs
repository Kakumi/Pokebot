using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
