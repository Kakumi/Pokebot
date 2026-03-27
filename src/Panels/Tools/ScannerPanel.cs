using Pokebot.Models.Tools;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Pokebot.Panels.Tools
{
    public abstract class ScannerPanel : UserControl
    {
        public abstract string ScannerName { get; }
        public abstract int[] SupportedGenerations { get; }
        public abstract string Run(SymbolScanner scanner);

        protected string FormatResults(string symbolName, List<SymbolScanResult> results)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"=== {symbolName} — {results.Count} candidate(s) found ===");
            foreach (var r in results)
                sb.AppendLine($"{r.Hex}  →  {r.Address.ToString("X8")} g 00000000 {symbolName}");
            if (results.Count == 0)
                sb.AppendLine("No match.");
            return sb.ToString();
        }
    }
}
