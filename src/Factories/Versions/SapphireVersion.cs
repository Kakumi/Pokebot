using BizHawk.Client.Common;
using Pokebot.Models.ActionRunners;
using Pokebot.Models.Config;
using Pokebot.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Factories.Versions
{
    public class SapphireVersion : GameVersion
    {
        public override IActionRunner ActionRunner { get; }

        public SapphireVersion(ApiContainer apiContainer, VersionInfo versionInfo, HashData hashData, GenerationInfo generationInfo)
            : base(apiContainer, versionInfo, hashData, generationInfo)
        {
            ActionRunner = new EmeraldRubySapphireActionRunner(APIContainer, this);
        }
    }
}
