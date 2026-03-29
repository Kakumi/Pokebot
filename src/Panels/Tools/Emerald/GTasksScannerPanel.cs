using Pokebot.Factories.Versions;
using Pokebot.Models.Tools;
using Pokebot.Models.Tools.Emerald;
using System.Drawing;
using System.Windows.Forms;

namespace Pokebot.Panels.Tools.Emerald
{
    public class GTasksScannerPanel : ScannerPanel
    {
        public override string ScannerName => "gTasks";
        public override VersionCode[] SupportedVersions => new[]
        {
            VersionCode.Emerald,
            VersionCode.Ruby,
            VersionCode.Sapphire,
            VersionCode.FireRed,
            VersionCode.LeafGreen
        };

        public GTasksScannerPanel()
        {
            Controls.Add(new Label
            {
                Text = "No parameters required. Be in the overworld with the bot idle.",
                AutoSize = true,
                Location = new Point(0, 8)
            });
        }

        public override string Run(SymbolScanner scanner)
            => FormatResults("gTasks", GTasksScanner.FindBase(scanner));
    }
}
