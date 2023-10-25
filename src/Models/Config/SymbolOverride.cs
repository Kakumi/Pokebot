using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Config
{
    public class SymbolOverride
    {
        public int Address { get; }
        public string Name { get; }

        public SymbolOverride(int address, string name)
        {
            Address = address;
            Name = name;
        }
    }
}
