using Pokebot.Models.Tools;
using System.Drawing;
using System.Windows.Forms;

namespace Pokebot.Panels.Tools
{
    public class GMainScannerPanel : ScannerPanel
    {
        public override string ScannerName => "gMain";
        public override int[] SupportedGenerations => new[] { 3 };

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
            => FormatResults("gMain", scanner.FindMainBase());
    }
}
