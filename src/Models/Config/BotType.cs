using System.Collections.Generic;

namespace Pokebot.Models.Config
{
    public class BotType
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public List<int> Supported { get; set; } = new List<int>();
    }
}
