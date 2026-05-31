using Pokebot.Models.Config;

namespace Pokebot.Server.Models
{
    public sealed class RomWebSocketMessage
    {
        public string RomName { get; set; } = string.Empty;
        public GenerationInfo? GenerationInfo { get; set; }
        public VersionInfo? VersionInfo { get; set; }
    }
}
