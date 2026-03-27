using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Pokebot.Models.Tools;

namespace Pokebot.Panels.Tools
{
    public class GEnemyPartyScannerPanel : ScannerPanel
    {
        private readonly NumericUpDown _lvlUpDown;
        private readonly NumericUpDown _cntUpDown;

        public override string ScannerName => "gEnemyParty";
        public override int[] SupportedGenerations => new[] { 3 };
        public override bool SupportsRefine => true;

        public GEnemyPartyScannerPanel()
        {
            Controls.Add(
                new Label
                {
                    Text = "Lvl:",
                    AutoSize = true,
                    Location = new Point(0, 6),
                }
            );
            _lvlUpDown = new NumericUpDown
            {
                Location = new Point(28, 3),
                Size = new Size(48, 20),
                Minimum = 1,
                Maximum = 100,
                Value = 1,
            };
            Controls.Add(_lvlUpDown);

            Controls.Add(
                new Label
                {
                    Text = "Cnt:",
                    AutoSize = true,
                    Location = new Point(82, 6),
                }
            );
            _cntUpDown = new NumericUpDown
            {
                Location = new Point(108, 3),
                Size = new Size(40, 20),
                Minimum = 1,
                Maximum = 6,
                Value = 1,
            };
            Controls.Add(_cntUpDown);

            Controls.Add(
                new Label
                {
                    Text = "Be in an active battle. Cnt = number of enemy Pokémon.",
                    AutoSize = true,
                    Location = new Point(0, 30),
                    ForeColor = System.Drawing.Color.Gray,
                }
            );
        }

        public override string Run(SymbolScanner scanner)
        {
            byte level = (byte)_lvlUpDown.Value;
            byte count = (byte)_cntUpDown.Value;
            var partyResults = scanner.FindEnemyPartyBase(level);

            var sb = new StringBuilder();
            sb.AppendLine($"=== gEnemyParty — {partyResults.Count} candidate(s) found ===");
            foreach (var r in partyResults)
            {
                sb.AppendLine($"{r.Hex}  →  {r.Address.ToString("X8")} g 00000258 gEnemyParty");
                var countResults = scanner.FindPartyCountNear(r.Address, count);
                if (countResults.Count > 0)
                    foreach (var cr in countResults)
                        sb.AppendLine($"  → gEnemyPartyCount candidate: {cr.Hex}");
                else
                    sb.AppendLine("  → gEnemyPartyCount: not found near this address");
            }
            if (partyResults.Count == 0)
                sb.AppendLine("No match. Enter the correct level and be in an active battle.");
            return sb.ToString();
        }
    }
}
