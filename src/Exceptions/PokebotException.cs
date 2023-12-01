using System;

namespace Pokebot.Exceptions
{
    public class PokebotException : Exception
    {
        public PokebotException(string message) : base(message) { }
    }
}
