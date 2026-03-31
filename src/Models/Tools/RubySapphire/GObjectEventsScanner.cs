using System.Collections.Generic;
using Pokebot.Models.Player;

namespace Pokebot.Models.Tools.RubySapphire
{
    public static class GObjectEventsScanner
    {
        public static List<SymbolScanResult> FindBase(SymbolScanner scanner, ushort playerX, ushort playerY, PlayerFacingDirection? facing = null)
        {
            var conditions = new List<ScanCondition>
            {
                ScanCondition.U8(0x01, 0x80),
                ScanCondition.U16(0x10, playerX),
                ScanCondition.U16(0x12, playerY),
            };
            if (facing.HasValue)
            {
                conditions.Add(ScanCondition.U8(0x18, (byte)facing.Value));
            }

            return scanner.ScanIwram(conditions);
        }
    }
}
