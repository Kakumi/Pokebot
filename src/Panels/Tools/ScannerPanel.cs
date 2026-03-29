using Pokebot.Factories.Versions;
using Pokebot.Models.Tools;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Pokebot.Panels.Tools
{
    public abstract class ScannerPanel : UserControl
    {
        public abstract string ScannerName { get; }
        public abstract VersionCode[] SupportedVersions { get; }
        public abstract string Run(SymbolScanner scanner);

        /// <summary>
        /// True if this scanner supports multi-pass refinement via the "Next" button.
        /// Override and return true to opt in; also override <see cref="Refine"/>.
        /// </summary>
        public virtual bool SupportsRefine => false;

        /// <summary>
        /// Narrows a previous scan's results by re-checking candidates against new
        /// conditions (intersection). Only called when <see cref="SupportsRefine"/> is true
        /// and <see cref="Run"/> has been called at least once.
        /// </summary>
        public virtual string Refine(SymbolScanner scanner) => null;

        protected string FormatResults(string symbolName, List<SymbolScanResult> results)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"=== {symbolName} — {results.Count} candidate(s) found ===");
            foreach (var r in results)
            {
                sb.Append($"{r.Hex}  →  {r.Address.ToString("X8")} g 00000000 {symbolName}");
                if (r.Tag != null)
                {
                    sb.Append($"   [{r.Tag}]");
                }
                sb.AppendLine();
            }
            if (results.Count == 0)
            {
                sb.AppendLine("No match.");
            }
            return sb.ToString();
        }

        protected string FormatRefinedResults(string symbolName, List<SymbolScanResult> results, int passNumber)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"=== {symbolName} — Pass {passNumber}: {results.Count} candidate(s) remaining ===");
            foreach (var r in results)
                sb.AppendLine($"{r.Hex}  →  {r.Address.ToString("X8")} g 00000000 {symbolName}");
            if (results.Count == 0)
                sb.AppendLine("No candidates remain. Reset and try a different approach.");
            return sb.ToString();
        }
    }
}
