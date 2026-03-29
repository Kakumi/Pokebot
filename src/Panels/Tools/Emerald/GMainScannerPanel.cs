using Pokebot.Factories.Versions;
using Pokebot.Models.Tools;
using Pokebot.Models.Tools.Emerald;
using System.Drawing;
using System.Windows.Forms;

namespace Pokebot.Panels.Tools.Emerald
{
    public class GMainScannerPanel : ScannerPanel
    {
        public override string ScannerName => "gMain";
        public override VersionCode[] SupportedVersions => new[] { VersionCode.Emerald };

        public GMainScannerPanel()
        {
            Controls.Add(new Label
            {
                Text = "No parameters required. Make sure the game is loaded.",
                AutoSize = true,
                Location = new Point(0, 8)
            });
        }

        public override string Run(SymbolScanner scanner)
            => FormatResults("gMain", GMainScanner.FindBase(scanner));
    }
}
