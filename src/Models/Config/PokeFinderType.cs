using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Config
{
    public class PokeFinderType
    {
        public string Name { get; }
        public int FrameAdvance { get; }

        [JsonConstructor]
        public PokeFinderType(string name, int frameAdvance)
        {
            Name = name;
            FrameAdvance = frameAdvance;
        }
    }
}
