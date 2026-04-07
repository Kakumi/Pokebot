using Pokebot.Factories.Versions;
using Pokebot.Models.Tools;
using Pokebot.Models.Tools.Emerald;
using System.Drawing;
using System.Windows.Forms;

namespace Pokebot.Panels.Tools.Emerald
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
        public override VersionCode[] SupportedVersions => new[]
        {
            VersionCode.Emerald,
            VersionCode.Ruby,
            VersionCode.Sapphire,
            VersionCode.FireRed,
            VersionCode.LeafGreen
        };

        // Row 1: Idx, HP, Atk, Def  — Row 2: Spd, SpA, SpD
        private const int Col0 = 0;
        private const int Col1 = 140;
        private const int Col2 = 280;
        private const int Col3 = 420;
        private const int Row1 = 5;
        private const int Row2 = 32;

        public GSpeciesInfoScannerPanel()
        {
            AddField("Idx:", Col0, Row1, ref _idxUpDown, 500, 1,  defaultValue: 1);
            AddField("HP:",  Col1, Row1, ref _hpUpDown,  255, 0,  defaultValue: 45);
            AddField("Atk:", Col2, Row1, ref _atkUpDown, 255, 0,  defaultValue: 49);
            AddField("Def:", Col3, Row1, ref _defUpDown, 255, 0,  defaultValue: 49);
            AddField("Spd:", Col0, Row2, ref _spdUpDown, 255, 0,  defaultValue: 45);
            AddField("SpA:", Col1, Row2, ref _spAUpDown, 255, 0,  defaultValue: 65);
            AddField("SpD:", Col2, Row2, ref _spDUpDown, 255, 0,  defaultValue: 65);

            Controls.Add(new Label
            {
                Text = "e.g. Bulbasaur: Idx=1 HP=45 Atk=49 Def=49 Spd=45 SpA=65 SpD=65",
                AutoSize = true,
                Location = new Point(0, 60),
                ForeColor = System.Drawing.Color.Gray
            });
        }

        private void AddField(string text, int x, int y, ref NumericUpDown upDown, int max, int min, int defaultValue)
        {
            Controls.Add(new Label { Text = text, AutoSize = true, Location = new Point(x, y + 3) });
            upDown = new NumericUpDown
            {
                Location = new Point(x + 34, y),
                Size = new Size(70, 20),
                Maximum = max,
                Minimum = min,
                Value = defaultValue
            };
            Controls.Add(upDown);
        }

        public override string Run(SymbolScanner scanner)
        {
            var results = GSpeciesInfoScanner.FindBase(
                scanner,
                (int)_idxUpDown.Value,
                (byte)_hpUpDown.Value,  (byte)_atkUpDown.Value, (byte)_defUpDown.Value,
                (byte)_spdUpDown.Value, (byte)_spAUpDown.Value, (byte)_spDUpDown.Value
            );
            return FormatResults("gSpeciesInfo", results);
        }
    }
}
