using System.Collections.Generic;
using System.Linq;

namespace Pokebot.Models.Tools.Emerald
{
    public static class GPlayerPartyScanner
    {
        public static List<SymbolScanResult> FindBase(SymbolScanner scanner, byte level, ushort? currentHp = null, ushort? maxHp = null, bool requireNoStatus = true)
        {
            var conditions = new List<ScanCondition>
            {
                ScanCondition.U8(0x13, 0x02), // hasSpecies=1, isBadEgg=0, isEgg=0
                ScanCondition.U8(0x54, level), // level
            };

            if (currentHp.HasValue)
            {
                conditions.Add(ScanCondition.U16(0x56, currentHp.Value));
            }

            if (maxHp.HasValue)
            {
                conditions.Add(ScanCondition.U16(0x58, maxHp.Value));
            }

            if (requireNoStatus)
            {
                conditions.Add(ScanCondition.U32(0x50, 0));
            }

            var ewram = scanner.ScanEwram(conditions, alignment: 4);
            var iwram = scanner.ScanIwram(conditions, alignment: 4);
            return ewram.Concat(iwram).ToList();
        }
    }
}
