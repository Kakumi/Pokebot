using System;
using System.Collections.Generic;

namespace Pokebot.Models.Tools.RubySapphire
{
    public static class GMainScanner
    {
        public static List<SymbolScanResult> FindBase(SymbolScanner scanner, byte? state = null, bool requireOverworld = true)
        {
            var conditions = new List<ScanCondition>
            {
                ScanCondition.U8(0x03, 0x08), // callback1 high byte
                ScanCondition.U8(0x07, 0x08), // callback2 high byte
            };

            if (state.HasValue)
            {
                conditions.Add(ScanCondition.U8(0x43C, state.Value));
            }

            if (requireOverworld)
            {
                conditions.Add(ScanCondition.U8(0x43D, 0x00));
            }

            var candidates = scanner.ScanIwram(conditions, alignment: 4);

            if (candidates.Count > 0)
            {
                var iwram = scanner.ReadIwram();
                foreach (var result in candidates)
                {
                    int off = (int)(result.Address - SymbolScanner.IwramStart);
                    uint cb2 = BitConverter.ToUInt32(iwram, off + 4);
                    result.Tag = $"cb2 = 0x{cb2:X8}";
                }
            }

            return candidates;
        }
    }
}
