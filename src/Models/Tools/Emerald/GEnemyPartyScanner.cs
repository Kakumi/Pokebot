using System;
using System.Collections.Generic;
using System.Linq;

namespace Pokebot.Models.Tools.Emerald
{
    public static class GEnemyPartyScanner
    {
        public static List<SymbolScanResult> FindBase(SymbolScanner scanner, byte level, ushort? maxHp = null, bool requireNoStatus = true)
        {
            var conditions = new List<ScanCondition>
            {
                ScanCondition.U8(0x13, 0x02), // hasSpecies=1, isBadEgg=0, isEgg=0
                ScanCondition.U8(0x54, level), // level visible in battle HUD
            };

            if (maxHp.HasValue)
            {
                conditions.Add(ScanCondition.U16(0x58, maxHp.Value));
            }

            if (requireNoStatus)
            {
                conditions.Add(ScanCondition.U32(0x50, 0)); // no status condition
            }

            var ewram = scanner.ScanEwram(conditions, alignment: 4);
            var iwram = scanner.ScanIwram(conditions, alignment: 4);
            return ewram.Concat(iwram).ToList();
        }

        public static List<SymbolScanResult> FindPartyCountNear(SymbolScanner scanner, long partyBase, byte count)
        {
            const int searchRange = 0x400;

            bool isIwram = partyBase >= SymbolScanner.IwramStart && partyBase < SymbolScanner.IwramStart + SymbolScanner.IwramSize;
            long regionStart = isIwram ? SymbolScanner.IwramStart : SymbolScanner.EwramStart;
            int regionSize = isIwram ? SymbolScanner.IwramSize : SymbolScanner.EwramSize;

            long scanStart = Math.Max(partyBase - searchRange, regionStart);
            long scanEnd = Math.Min(partyBase + searchRange, regionStart + regionSize);
            int scanSize = (int)(scanEnd - scanStart);

            var conditions = new List<ScanCondition> { ScanCondition.U8(0x00, count) };

            return scanner.Scan(conditions, scanStart, scanSize, alignment: 1);
        }
    }
}
