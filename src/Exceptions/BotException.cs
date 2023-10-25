using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Exceptions
{
    public class BotException : PokebotException
    {
        public BotException(string message) : base(message)
        {
        }
    }
}
