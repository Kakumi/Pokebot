using Pokebot.Models.Player;
using Pokebot.Models.Tools;
using System.Drawing;
using System.Windows.Forms;

namespace Pokebot.Panels.Tools
{
    public class GObjectEventsScannerPanel : ScannerPanel
    {
        private readonly NumericUpDown _xUpDown;
        private readonly NumericUpDown _yUpDown;
        private readonly ComboBox _facingCB;

        public override string ScannerName => "gObjectEvents";
        public override int[] SupportedGenerations => new[] { 3 };

        public GObjectEventsScannerPanel()
        {
            Controls.Add(new Label { Text = "Player X:", AutoSize = true, Location = new Point(0, 6) });
            _xUpDown = new NumericUpDown { Location = new Point(60, 3), Size = new Size(70, 20), Maximum = 9999 };
            Controls.Add(_xUpDown);

            Controls.Add(new Label { Text = "Player Y:", AutoSize = true, Location = new Point(136, 6) });
            _yUpDown = new NumericUpDown { Location = new Point(196, 3), Size = new Size(70, 20), Maximum = 9999 };
            Controls.Add(_yUpDown);

            Controls.Add(new Label { Text = "Facing:", AutoSize = true, Location = new Point(272, 6) });
            _facingCB = new ComboBox
            {
                Location = new Point(316, 3),
                Size = new Size(80, 21),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _facingCB.Items.AddRange(new object[] { "Any", "Down", "Up", "Left", "Right" });
            _facingCB.SelectedIndex = 0;
            Controls.Add(_facingCB);
        }

        public override string Run(SymbolScanner scanner)
        {
            PlayerFacingDirection? facing = _facingCB.SelectedIndex switch
            {
                1 => PlayerFacingDirection.Down,
                2 => PlayerFacingDirection.Up,
                3 => PlayerFacingDirection.Left,
                4 => PlayerFacingDirection.Right,
                _ => null
            };
            var results = scanner.FindObjectEventsBase((ushort)_xUpDown.Value, (ushort)_yUpDown.Value, facing);
            return FormatResults("gObjectEvents", results);
        }
    }
}
