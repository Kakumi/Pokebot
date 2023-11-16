using Newtonsoft.Json;

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
