using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Pokemons
{
    public class PokemonMove
    {
        public int Id { get; }
        public string Name { get; }
        public string Type { get; }
        public string Category { get; }
        public int PP { get; }
        public int Power { get; }
        public int Accuracy { get; }

        [JsonConstructor]
        public PokemonMove(int id, string name, string type, string category, int pP, int power, int accuracy)
        {
            Id = id;
            Name = name;
            Type = type;
            Category = category;
            PP = pP;
            Power = power;
            Accuracy = accuracy;
        }
    }
}
