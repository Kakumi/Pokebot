using Pokebot.Factories.Versions;
using Pokebot.Models.Tools;
using Pokebot.Models.Tools.Emerald;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Pokebot.Panels.Tools.Emerald
{
    public class GActionSelectionCursorScannerPanel : ScannerPanel
    {
        private readonly ComboBox _cursorCB;

        private List<SymbolScanResult> _lastResults;
        private int _passNumber;

        public override string ScannerName => "gActionSelectionCursor";
        public override VersionCode[] SupportedVersions => new[]
        {
            VersionCode.Emerald,
            VersionCode.Ruby,
            VersionCode.Sapphire,
            VersionCode.FireRed,
            VersionCode.LeafGreen
        };
        public override bool SupportsRefine => true;

        public GActionSelectionCursorScannerPanel()
        {
            Controls.Add(new Label { Text = "Cursor:", AutoSize = true, Location = new Point(0, 6) });
            _cursorCB = new ComboBox
            {
                Location = new Point(50, 3),
                Size = new Size(110, 21),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _cursorCB.Items.AddRange(new object[] { "FIGHT (0)", "BAG (1)", "POKEMON (2)", "RUN (3)" });
            _cursorCB.SelectedIndex = 0;
            Controls.Add(_cursorCB);

            Controls.Add(new Label
            {
                Text = "Be on the action selection screen in battle.\r\nChange cursor, update dropdown, click Next to narrow candidates.",
                AutoSize = true,
                Location = new Point(0, 30),
                ForeColor = System.Drawing.Color.Gray
            });
        }

        public override string Run(SymbolScanner scanner)
        {
            byte cursor = (byte)_cursorCB.SelectedIndex;
            _lastResults = GActionSelectionCursorScanner.FindBase(scanner, cursor);
            _passNumber = 1;
            return FormatResults("gActionSelectionCursor", _lastResults);
        }

        public override string Refine(SymbolScanner scanner)
        {
            if (_lastResults == null || _lastResults.Count == 0)
            {
                return "No previous scan results. Click Start first.";
            }

            byte cursor = (byte)_cursorCB.SelectedIndex;
            var conditions = new List<ScanCondition>
            {
                ScanCondition.U8(0x00, cursor),
            };

            _passNumber++;
            _lastResults = scanner.Refine(_lastResults, conditions);
            return FormatRefinedResults("gActionSelectionCursor", _lastResults, _passNumber);
        }
    }
}
