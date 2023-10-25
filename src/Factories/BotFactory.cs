using BizHawk.Client.Common;
using Pokebot.Factories.Bots;
using Pokebot.Models.Config;
using Pokebot.Panels;
using Pokebot.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokebot.Factories
{
    public static class BotFactory
    {
        public static IBot Create(BotCode code, ApiContainer apiContainer, Versions.IGameVersion gameVersion)
        {
            switch (code)
            {
                case BotCode.None:
                    break;
                case BotCode.Starter:
                    return new StarterBot(apiContainer, gameVersion);
                case BotCode.Spin:
                    break;
                case BotCode.Egg:
                    break;
            }

            throw new NotSupportedException("Bot type is not supported");
        }
    }
}
