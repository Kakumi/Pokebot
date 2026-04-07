using Pokebot.Factories.Versions;
using Pokebot.Models.Tools;
using Pokebot.Models.Tools.Emerald;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Pokebot.Panels.Tools.Emerald
{
    public class GRngValueScannerPanel : ScannerPanel
    {
        private readonly TextBox _rngBox;
        private readonly TextBox _rng2Box;

        public override string ScannerName => "gRngValue";
        public override VersionCode[] SupportedVersions => new[]
        {
            VersionCode.Emerald,
            VersionCode.Ruby,
            VersionCode.Sapphire,
            VersionCode.FireRed,
            VersionCode.LeafGreen
        };

        public GRngValueScannerPanel()
        {
            Controls.Add(new Label { Text = "RNG:", AutoSize = true, Location = new Point(0, 6) });
            _rngBox = new TextBox { Location = new Point(36, 3), Size = new Size(80, 20), Text = "00000000", MaxLength = 8 };
            Controls.Add(_rngBox);

            Controls.Add(new Label { Text = "RNG2:", AutoSize = true, Location = new Point(124, 6) });
            _rng2Box = new TextBox { Location = new Point(164, 3), Size = new Size(80, 20), Text = "", MaxLength = 8 };
            Controls.Add(_rng2Box);

            Controls.Add(new Label
            {
                Text = "Pause BizHawk, read both u32 values as hex. RNG2 is optional but eliminates false positives.",
                AutoSize = true,
                Location = new Point(0, 30),
                ForeColor = System.Drawing.Color.Gray
            });
        }

        public override string Run(SymbolScanner scanner)
        {
            if (!uint.TryParse(_rngBox.Text.Trim(), NumberStyles.HexNumber, null, out uint rng))
            {
                return "Error: RNG value must be a valid 8-digit hex number (e.g. 1A2B3C4D).";
            }

            uint? rng2 = null;
            string rng2Text = _rng2Box.Text.Trim();
            if (!string.IsNullOrEmpty(rng2Text))
            {
                if (!uint.TryParse(rng2Text, NumberStyles.HexNumber, null, out uint parsed2))
                {
                    return "Error: RNG2 value must be a valid 8-digit hex number or leave it empty.";
                }
                rng2 = parsed2;
            }

            var results = GRngValueScanner.FindBase(scanner, rng, rng2);
            return FormatResults("gRngValue", results);
        }
    }
}
