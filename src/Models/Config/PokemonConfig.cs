using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Config
{
    public class PokemonConfig
    {
        public int Id { get; }
        public int DexId { get; }
        public string Name { get; }
        public List<string> Abilities { get; }
        public List<string> Types { get; }
        public PokemonBaseStats BaseStats { get; }

        [JsonConstructor]
        public PokemonConfig(int id, int dexId, string name, List<string> abilities, List<string> types, PokemonBaseStats baseStats)
        {
            Id = id;
            DexId = dexId;
            Name = name;
            Abilities = abilities;
            Types = types;
            BaseStats = baseStats;
        }
    }
}
