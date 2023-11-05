using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
