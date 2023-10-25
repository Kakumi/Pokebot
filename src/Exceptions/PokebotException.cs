using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Exceptions
{
    public class PokebotException : Exception
    {
        public PokebotException(string message) : base(message) { }
    }
}
