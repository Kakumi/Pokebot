using System.Collections.Generic;

namespace Pokebot.Models.Config
{
    public class AppConfig
    {
        public List<GenerationInfo> Generations { get; set; }
        public List<VersionInfo> Versions { get; set; }
        public List<BotType> BotTypes { get; set; }
        public GithubConfig Github { get; set; }
        public string DiscordInvite { get; set; }
    }
}
