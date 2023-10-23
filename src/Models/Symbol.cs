using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models
{
    public class Symbol
    {
        public long Address { get; }
        public char Letter { get; }
        public int Size { get; }
        public string Name { get; }

        public Symbol(long address, char letter, int size, string name)
        {
            Address = address;
            Letter = letter;
            Size = size;
            Name = name;
        }
    }
}
