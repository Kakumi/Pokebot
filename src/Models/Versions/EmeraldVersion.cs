using BizHawk.Client.Common;
using Pokebot.Models.Config;
using Pokebot.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models.Versions
{
    public class EmeraldVersion : GameVersion
    {
        public EmeraldVersion(ApiContainer apiContainer, GenerationInfo generationInfo) : base(apiContainer, generationInfo, Resources.pokeemerald)
        {

        }
    }
}
