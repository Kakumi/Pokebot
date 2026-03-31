using System.Collections.Generic;
using System.Linq;

namespace Pokebot.Models.Tools.Emerald
{
    public static class GRngValueScanner
    {
        public static List<SymbolScanResult> FindBase(SymbolScanner scanner, uint rngValue, uint? rng2Value = null)
        {
            var conditions = new List<ScanCondition> { ScanCondition.U32(0x00, rngValue) };

            if (rng2Value.HasValue)
            {
                conditions.Add(ScanCondition.U32(0x04, rng2Value.Value));
            }

            var ewram = scanner.ScanEwram(conditions, alignment: 4);
            var iwram = scanner.ScanIwram(conditions, alignment: 4);
            return ewram.Concat(iwram).ToList();
        }
    }
}
