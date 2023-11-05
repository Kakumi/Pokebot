using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Config
{
    public class HashData
    {
        public string Hash { get; }
        public HashSymbols Symbols { get; }

        [JsonConstructor]
        public HashData(string hash, HashSymbols symbols)
        {
            Hash = hash;
            Symbols = symbols;
        }
    }
}
