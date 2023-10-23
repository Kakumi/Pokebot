using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Config
{
    public class AppConfig
    {
        public List<GenerationInfo> Generations { get; set; }
        public List<VersionInfo> Versions { get; set; }
        public List<BotType> BotTypes { get; set; }
    }
}
