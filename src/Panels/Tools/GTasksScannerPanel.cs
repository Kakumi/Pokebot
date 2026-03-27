using Pokebot.Models.Tools;
using System.Drawing;
using System.Windows.Forms;

namespace Pokebot.Panels.Tools
{
    public class GTasksScannerPanel : ScannerPanel
    {
        public override string ScannerName => "gTasks";
        public override int[] SupportedGenerations => new[] { 3 };

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
            => FormatResults("gTasks", scanner.FindTasksBase());
    }
}
