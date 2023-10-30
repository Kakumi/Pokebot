using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
