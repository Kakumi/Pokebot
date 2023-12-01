using BizHawk.Client.Common;
using Pokebot.Factories.Bots;
using Pokebot.Factories.Versions;
using Pokebot.Models.Config;
using System;

namespace Pokebot.Factories
{
    public static class BotFactory
    {
        public static IBot Create(BotCode code, ApiContainer apiContainer, GameVersion gameVersion)
        {
            switch (code)
            {
                case BotCode.Starter:
                    return new StarterBot(apiContainer, gameVersion);
                case BotCode.Spin:
                    return new SpinBot(apiContainer, gameVersion);
                case BotCode.Static:
                    return new LegendaryBot(apiContainer, gameVersion);
                case BotCode.PokeFinder:
                    return new PokeFinderBot(apiContainer, gameVersion);
            }

            throw new NotSupportedException(Messages.BotFactory_NotSupported);
        }
    }
}
