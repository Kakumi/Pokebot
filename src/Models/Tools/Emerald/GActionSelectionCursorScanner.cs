using System;
using System.Collections.Generic;

namespace Pokebot.Models.Tools.Emerald
{
    public static class GActionSelectionCursorScanner
    {
        public static List<SymbolScanResult> FindBase(SymbolScanner scanner, byte cursorPosition)
        {
            if (cursorPosition > 3)
            {
                throw new ArgumentOutOfRangeException(nameof(cursorPosition), "Cursor position must be 0–3 (FIGHT/BAG/POKEMON/RUN).");
            }

            var conditions = new List<ScanCondition> { ScanCondition.U8(0x00, cursorPosition) };

            return scanner.ScanEwram(conditions, alignment: 1);
        }
    }
}
