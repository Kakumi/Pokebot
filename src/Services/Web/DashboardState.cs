using System.Collections.Generic;

namespace Pokebot.Services.Web
{
    public class DashboardState
    {
        public string AppVersion { get; set; } = string.Empty;
        public string DashboardUrl { get; set; } = string.Empty;
        public bool IsRomLoaded { get; set; }
        public bool IsReady { get; set; }
        public bool IsBotRunning { get; set; }
        public string Status { get; set; } = string.Empty;
        public string TestedStatus { get; set; } = string.Empty;
        public string CurrentBot { get; set; } = string.Empty;
        public string BotStatus { get; set; } = string.Empty;
        public string CurrentRom { get; set; } = string.Empty;
        public List<string> RecentLogs { get; set; } = new List<string>();
    }
}
