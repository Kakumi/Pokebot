using Pokebot.Models.Pokemons;
using System.Collections.Generic;

namespace Pokebot.Models.Config
{
    public class VersionInfo
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public List<HashData> Hashs { get; set; }
        public int Generation { get; set; }
        public List<string> Starters { get; set; }
        public BotConfig BotConfig { get; set; }
    }
}
