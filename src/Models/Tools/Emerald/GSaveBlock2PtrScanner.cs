using System.Collections.Generic;

namespace Pokebot.Models.Tools.Emerald
{
    public static class GSaveBlock2PtrScanner
    {
        public static List<SymbolScanResult> FindBase(SymbolScanner scanner, byte gender, ushort? trainerId = null)
        {
            var ewram = scanner.ReadEwram();
            var results = new List<SymbolScanResult>();

            bool TargetMatches(uint ptrValue)
            {
                if (ptrValue < SymbolScanner.EwramStart || ptrValue >= SymbolScanner.EwramStart + SymbolScanner.EwramSize)
                {
                    return false;
                }

                int t = (int)(ptrValue - SymbolScanner.EwramStart);

                if (t + 0x0C > SymbolScanner.EwramSize)
                {
                    return false;
                }

                if (ewram[t + 0x08] != gender)
                {
                    return false;
                }

                if (trainerId.HasValue)
                {
                    ushort storedTid = (ushort)(ewram[t + 0x0A] | (ewram[t + 0x0B] << 8));
                    if (storedTid != trainerId.Value)
                    {
                        return false;
                    }
                }

                return true;
            }

            for (int i = 0; i <= SymbolScanner.EwramSize - 4; i += 4)
            {
                uint ptrValue = (uint)(ewram[i] | (ewram[i + 1] << 8) | (ewram[i + 2] << 16) | (ewram[i + 3] << 24));
                if (TargetMatches(ptrValue))
                {
                    results.Add(new SymbolScanResult(SymbolScanner.EwramStart + i));
                }
            }

            var iwram = scanner.ReadIwram();
            for (int i = 0; i <= SymbolScanner.IwramSize - 4; i += 4)
            {
                uint ptrValue = (uint)(iwram[i] | (iwram[i + 1] << 8) | (iwram[i + 2] << 16) | (iwram[i + 3] << 24));
                if (TargetMatches(ptrValue))
                {
                    results.Add(new SymbolScanResult(SymbolScanner.IwramStart + i));
                }
            }

            return results;
        }
    }
}
