﻿using BizHawk.Client.Common;
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
                case BotCode.Egg:
                    break;
            }

            throw new NotSupportedException(Messages.BotFactory_NotSupported);
        }
    }
}
