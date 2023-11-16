using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pokebot.Models.Config
{
    public class HashSymbols
    {
        public string Main { get; }
        public List<string> Patches { get; }

        [JsonConstructor]
        public HashSymbols(string main, List<string> patches)
        {
            Main = main;
            Patches = patches;
        }
    }
}
