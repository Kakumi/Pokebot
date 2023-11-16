using System.Collections.Generic;

namespace Pokebot.Models.Config
{
    public class VersionInfo
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public List<HashData> Hashs { get; set; }
        public int Generation { get; set; }
        public List<PokemonStarterConfig> Starters { get; set; }
        public BotConfig BotsConfig { get; set; }
    }
}
