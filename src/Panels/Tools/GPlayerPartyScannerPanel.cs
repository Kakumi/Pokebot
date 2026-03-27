using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Pokebot.Models.Tools;

namespace Pokebot.Panels.Tools
{
    public class GPlayerPartyScannerPanel : ScannerPanel
    {
        private readonly NumericUpDown _lvlUpDown;
        private readonly NumericUpDown _hpUpDown;
        private readonly NumericUpDown _maxHpUpDown;
        private readonly NumericUpDown _cntUpDown;

        public override string ScannerName => "gPlayerParty";
        public override int[] SupportedGenerations => new[] { 3 };
        public override bool SupportsRefine => true;

        public GPlayerPartyScannerPanel()
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
                    Text = "HP:",
                    AutoSize = true,
                    Location = new Point(82, 6),
                }
            );
            _hpUpDown = new NumericUpDown
            {
                Location = new Point(104, 3),
                Size = new Size(55, 20),
                Minimum = 0,
                Maximum = 65535,
            };
            Controls.Add(_hpUpDown);

            Controls.Add(
                new Label
                {
                    Text = "MaxHP:",
                    AutoSize = true,
                    Location = new Point(164, 6),
                }
            );
            _maxHpUpDown = new NumericUpDown
            {
                Location = new Point(208, 3),
                Size = new Size(55, 20),
                Minimum = 0,
                Maximum = 65535,
            };
            Controls.Add(_maxHpUpDown);

            Controls.Add(
                new Label
                {
                    Text = "Cnt:",
                    AutoSize = true,
                    Location = new Point(268, 6),
                }
            );
            _cntUpDown = new NumericUpDown
            {
                Location = new Point(292, 3),
                Size = new Size(40, 20),
                Minimum = 1,
                Maximum = 6,
                Value = 1,
            };
            Controls.Add(_cntUpDown);

            Controls.Add(
                new Label
                {
                    Text = "Open the party screen. HP and MaxHP are visible. Leave HP=0 to skip.",
                    AutoSize = true,
                    Location = new Point(0, 30),
                    ForeColor = System.Drawing.Color.Gray,
                }
            );
        }

        public override string Run(SymbolScanner scanner)
        {
            byte level = (byte)_lvlUpDown.Value;
            ushort? hp = _hpUpDown.Value > 0 ? (ushort?)_hpUpDown.Value : null;
            ushort? maxHp = _maxHpUpDown.Value > 0 ? (ushort?)_maxHpUpDown.Value : null;
            byte count = (byte)_cntUpDown.Value;
            var partyResults = scanner.FindPlayerPartyBase(level, hp, maxHp);

            var sb = new StringBuilder();
            sb.AppendLine($"=== gPlayerParty — {partyResults.Count} candidate(s) found ===");
            foreach (var r in partyResults)
            {
                sb.AppendLine($"{r.Hex}  →  {r.Address.ToString("X8")} g 00000258 gPlayerParty");
                var countResults = scanner.FindPartyCountNear(r.Address, count);
                if (countResults.Count > 0)
                    foreach (var cr in countResults)
                        sb.AppendLine($"  → gPlayerPartyCount candidate: {cr.Hex}");
                else
                    sb.AppendLine("  → gPlayerPartyCount: not found near this address");
            }
            if (partyResults.Count == 0)
                sb.AppendLine("No match. Enter the correct level and be in the party screen.");
            return sb.ToString();
        }
    }
}
