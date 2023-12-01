using Newtonsoft.Json;

namespace Pokebot.Models.Config
{
    public class BotConfig
    {
        public BotStarterConfig Starter { get; }

        [JsonConstructor]
        public BotConfig(BotStarterConfig starter)
        {
            Starter = starter;
        }
    }
}
