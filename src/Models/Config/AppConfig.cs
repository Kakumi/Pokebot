using System.Collections.Generic;

namespace Pokebot.Models.Config
{
    public class AppConfig
    {
        public List<GenerationInfo> Generations { get; set; }
        public List<VersionInfo> Versions { get; set; }
        public List<BotType> BotTypes { get; set; }
    }
}
