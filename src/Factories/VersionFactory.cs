using BizHawk.Client.Common;
using Pokebot.Exceptions;
using Pokebot.Factories.Versions;
using Pokebot.Models.ActionRunners;
using Pokebot.Models.Config;
using Pokebot.Models.Memory;
using Pokebot.Properties;
using System;
using System.Linq;
using System.Text;

namespace Pokebot.Factories
{
    public static class VersionFactory
    {
        public static GameVersion Create(ApiContainer apiContainer, string hash)
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
                        IGameMemory? memory = null;
                        IActionRunner? runner = null;

                        var versionType = (VersionCode)version.Code;
                        switch (versionType)
                        {
                            case VersionCode.Emerald:
                            case VersionCode.Sapphire:
                            case VersionCode.Ruby:
                                memory = new Gen3Memory(apiContainer, version, hashData, generation);
                                runner = new EmeraldRubySapphireActionRunner(apiContainer, memory);
                                break;
                            case VersionCode.LeafGreen:
                            case VersionCode.FireRed:
                                memory = new Gen3Memory(apiContainer, version, hashData, generation);
                                runner = new FireRedLeafGreenActionRunner(apiContainer, memory);
                                break;
                        }

                        if (memory == null || runner == null)
                        {
                            throw new NotSupportedException(Messages.Rom_NotSupported);
                        }

                        return new GameVersion(apiContainer, version, hashData, generation, memory, runner);
                    }
                }
            }

            throw new NotSupportedException(Messages.Rom_NotSupported);
        }
    }
}
