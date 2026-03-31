using Pokebot.Factories.Versions;
using Pokebot.Models.Tools;
using Pokebot.Models.Tools.RubySapphire;
using System.Drawing;
using System.Windows.Forms;

namespace Pokebot.Panels.Tools.RubySapphire
{
    public class GMainScannerPanel : ScannerPanel
    {
        public override string ScannerName => "gMain";
        public override VersionCode[] SupportedVersions => new[] { VersionCode.Ruby, VersionCode.Sapphire };

        public GMainScannerPanel()
        {
            Controls.Add(new Label
            {
                Text = "Stand in overworld. No input needed.",
                AutoSize = true,
                Location = new Point(0, 8)
            });
        }

        public override string Run(SymbolScanner scanner)
            => FormatResults("gMain", GMainScanner.FindBase(scanner));
    }
}
