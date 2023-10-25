using BizHawk.Client.Common;
using BizHawk.Emulation.Common;
using Pokebot.Factories.Versions;
using Pokebot.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var version = config.Versions.FirstOrDefault(x => x.Hash.Equals(hash, StringComparison.InvariantCultureIgnoreCase));
                if (version != null)
                {
                    var generation = config.Generations.FirstOrDefault(x => x.Code == version.Code);
                    if (generation != null)
                    {
                        var versionType = (VersionCode)version.Code;
                        switch (versionType)
                        {
                            case VersionCode.Emerald:
                                return new EmeraldVersion(apiContainer, version, generation);
                        }
                    }
                }
            }

            throw new NotSupportedException("Game is not supported");
        }
    }
}
