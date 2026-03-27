using Pokebot.Models.Tools;
using System.Drawing;
using System.Windows.Forms;

namespace Pokebot.Panels.Tools
{
    public class GSpeciesInfoScannerPanel : ScannerPanel
    {
        private readonly NumericUpDown _idxUpDown;
        private readonly NumericUpDown _hpUpDown;
        private readonly NumericUpDown _atkUpDown;
        private readonly NumericUpDown _defUpDown;
        private readonly NumericUpDown _spdUpDown;
        private readonly NumericUpDown _spAUpDown;
        private readonly NumericUpDown _spDUpDown;

        public override string ScannerName => "gSpeciesInfo";
        public override int[] SupportedGenerations => new[] { 3 };

        public GSpeciesInfoScannerPanel()
        {
            AddLabelAndUpDown("Idx:",  0,   ref _idxUpDown, 500, 1);
            AddLabelAndUpDown("HP:",   85,  ref _hpUpDown,  255, 0);
            AddLabelAndUpDown("Atk:",  155, ref _atkUpDown, 255, 0);
            AddLabelAndUpDown("Def:",  225, ref _defUpDown, 255, 0);
            AddLabelAndUpDown("Spd:",  295, ref _spdUpDown, 255, 0);
            AddLabelAndUpDown("SpA:",  365, ref _spAUpDown, 255, 0);
            AddLabelAndUpDown("SpD:",  435, ref _spDUpDown, 255, 0);

            Controls.Add(new Label
            {
                Text = "e.g. Bulbasaur: Idx=1 HP=45 Atk=49 Def=49 Spd=45 SpA=65 SpD=65",
                AutoSize = true,
                Location = new Point(0, 30),
                ForeColor = System.Drawing.Color.Gray
            });
        }

        private void AddLabelAndUpDown(string text, int x, ref NumericUpDown upDown, int max, int min)
        {
            Controls.Add(new Label { Text = text, AutoSize = true, Location = new Point(x, 6) });
            upDown = new NumericUpDown
            {
                Location = new Point(x + (text.Length > 3 ? 26 : 22), 3),
                Size = new Size(42, 20),
                Maximum = max,
                Minimum = min
            };
            Controls.Add(upDown);
        }

        public override string Run(SymbolScanner scanner)
        {
            var results = scanner.FindSpeciesInfoBase(
                (int)_idxUpDown.Value,
                (byte)_hpUpDown.Value,  (byte)_atkUpDown.Value, (byte)_defUpDown.Value,
                (byte)_spdUpDown.Value, (byte)_spAUpDown.Value, (byte)_spDUpDown.Value
            );
            return FormatResults("gSpeciesInfo", results);
        }
    }
}
