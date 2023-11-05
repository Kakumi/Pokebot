using BizHawk.Client.Common;
using Pokebot.Models;
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
    public class EmeraldVersion : GameVersion
    {
        public override IActionRunner ActionRunner { get; }

        public EmeraldVersion(ApiContainer apiContainer, VersionInfo versionInfo, HashData hashData, GenerationInfo generationInfo) 
            : base(apiContainer, versionInfo, hashData, generationInfo)
        {
            ActionRunner = new EmeraldRubySapphireActionRunner(APIContainer, this);
        }
    }
}
