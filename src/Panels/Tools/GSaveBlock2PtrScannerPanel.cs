using Pokebot.Models.Tools;
using System.Drawing;
using System.Windows.Forms;

namespace Pokebot.Panels.Tools
{
    public class GSaveBlock2PtrScannerPanel : ScannerPanel
    {
        private readonly ComboBox _genderCB;
        private readonly NumericUpDown _trainerIdUpDown;
        private readonly CheckBox _useTrainerId;

        public override string ScannerName => "gSaveBlock2Ptr";
        public override int[] SupportedGenerations => new[] { 3 };

        public GSaveBlock2PtrScannerPanel()
        {
            Controls.Add(new Label { Text = "Gender:", AutoSize = true, Location = new Point(0, 6) });
            _genderCB = new ComboBox
            {
                Location = new Point(50, 3),
                Size = new Size(80, 21),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _genderCB.Items.AddRange(new object[] { "Male (0)", "Female (1)" });
            _genderCB.SelectedIndex = 0;
            Controls.Add(_genderCB);

            _useTrainerId = new CheckBox { Text = "TID:", AutoSize = true, Location = new Point(140, 5), Checked = true };
            Controls.Add(_useTrainerId);

            _trainerIdUpDown = new NumericUpDown
            {
                Location = new Point(184, 3),
                Size = new Size(72, 20),
                Minimum = 0,
                Maximum = 65535,
                Value = 0
            };
            Controls.Add(_trainerIdUpDown);

            _useTrainerId.CheckedChanged += (s, e) => _trainerIdUpDown.Enabled = _useTrainerId.Checked;

            Controls.Add(new Label
            {
                Text = "TID = 5-digit Trainer ID shown on the Trainer Card.\r\nResult is the pointer address, not the SaveBlock2 address itself.",
                AutoSize = true,
                Location = new Point(0, 30),
                ForeColor = System.Drawing.Color.Gray
            });
        }

        public override string Run(SymbolScanner scanner)
        {
            byte gender = (byte)_genderCB.SelectedIndex;
            ushort? trainerId = _useTrainerId.Checked ? (ushort?)_trainerIdUpDown.Value : null;

            var results = scanner.FindSaveBlock2Ptr(gender, trainerId);
            return FormatResults("gSaveBlock2Ptr", results);
        }
    }
}
