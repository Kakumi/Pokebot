using Pokebot.Factories.Versions;
using Pokebot.Models.Tools;
using Pokebot.Models.Tools.Emerald;
using System.Drawing;
using System.Windows.Forms;

namespace Pokebot.Panels.Tools.Emerald
{
    public class GPlayerAvatarScannerPanel : ScannerPanel
    {
        private readonly CheckBox _femaleCB;
        private readonly CheckBox _flagsCB;
        private readonly NumericUpDown _flagsUpDown;

        public override string ScannerName => "gPlayerAvatar";
        public override VersionCode[] SupportedVersions => new[]
        {
            VersionCode.Emerald,
            VersionCode.Ruby,
            VersionCode.Sapphire,
            VersionCode.FireRed,
            VersionCode.LeafGreen
        };

        public GPlayerAvatarScannerPanel()
        {
            _femaleCB = new CheckBox { Text = "Female character", AutoSize = true, Location = new Point(0, 5) };
            Controls.Add(_femaleCB);

            _flagsCB = new CheckBox { Text = "Known flags (hex):", AutoSize = true, Location = new Point(0, 30) };
            Controls.Add(_flagsCB);

            _flagsUpDown = new NumericUpDown
            {
                Location = new Point(128, 27),
                Size = new Size(60, 20),
                Hexadecimal = true,
                Maximum = 255
            };
            Controls.Add(_flagsUpDown);

            Controls.Add(new Label
            {
                Text = "Typical: 0x21 (ON_FOOT | CONTROLLABLE) when idle in overworld",
                AutoSize = true,
                Location = new Point(0, 52),
                ForeColor = System.Drawing.Color.Gray
            });
        }

        public override string Run(SymbolScanner scanner)
        {
            byte gender = _femaleCB.Checked ? (byte)1 : (byte)0;
            byte? flags = _flagsCB.Checked ? (byte?)_flagsUpDown.Value : null;
            var results = GPlayerAvatarScanner.FindBase(scanner, gender, flags);
            return FormatResults("gPlayerAvatar", results);
        }
    }
}
