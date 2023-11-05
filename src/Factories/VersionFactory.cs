using BizHawk.Client.Common;
using BizHawk.Common;
using BizHawk.Emulation.Common;
using Pokebot.Exceptions;
using Pokebot.Factories.Versions;
using Pokebot.Models.Config;
using Pokebot.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokebot.Factories
{
    public static class VersionFactory
    {
        public static IGameVersion Create(ApiContainer apiContainer, string hash)
        {
            var configText = Encoding.UTF8.GetString(Resources.appconfig);
            var config = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Config.AppConfig>(configText);
            if (config != null)
            {
                var tupleVersion = config.Versions.Select(x =>
                {
                    var hashFound = x.Hashs.FirstOrDefault(y => y.Hash.Equals(hash, StringComparison.InvariantCultureIgnoreCase));
                    if (hashFound != null)
                    {
                        return new Tuple<Models.Config.VersionInfo, HashData>(x, hashFound);
                    }

                    return null;
                }).FirstOrDefault(x => x != null);

                if (tupleVersion != null && tupleVersion.Item1 != null && tupleVersion.Item2 != null)
                {
                    var version = tupleVersion.Item1;
                    var hashData = tupleVersion.Item2;

                    if (string.IsNullOrEmpty(hashData.Symbols?.Main))
                    {
                        throw new PokebotException(Messages.Symbols_Empty);
                    }

                    var generation = config.Generations.FirstOrDefault(x => x.Code == version.Generation);
                    if (generation != null)
                    {
                        var versionType = (VersionCode)version.Code;
                        switch (versionType)
                        {
                            case VersionCode.Emerald:
                                return new EmeraldVersion(apiContainer, version, hashData, generation);
                            case VersionCode.Sapphire:
                                return new SapphireVersion(apiContainer, version, hashData, generation);
                            case VersionCode.Ruby:
                                return new RubyVersion(apiContainer, version, hashData, generation);
                            case VersionCode.LeafGreen:
                                break;
                            case VersionCode.FireRed:
                                break;
                        }
                    }
                }
            }

            throw new NotSupportedException(Messages.Rom_NotSupported);
        }
    }
}
