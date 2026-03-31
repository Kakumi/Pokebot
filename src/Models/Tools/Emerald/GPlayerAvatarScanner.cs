using System.Collections.Generic;

namespace Pokebot.Models.Tools.Emerald
{
    public static class GPlayerAvatarScanner
    {
        public static List<SymbolScanResult> FindBase(SymbolScanner scanner, byte gender, byte? flags = null, byte? spriteId = null, bool requireOnFoot = true)
        {
            var conditions = new List<ScanCondition>
            {
                ScanCondition.U8(0x01, 0x00), // transitionFlags = 0 (standing still)
                ScanCondition.U8(0x02, 0x00), // runningState = NotMoving
                ScanCondition.U8(0x03, 0x00), // tileTransitionState = not transitioning
                ScanCondition.U8(0x05, 0x00), // objectEventId = 0 (player is always slot 0)
                ScanCondition.U8(0x06, 0x00), // preventStep = false
                ScanCondition.U8(0x07, gender),
                ScanCondition.U8(0x09, 0x00), // newDirBackup = 0 (not biking)
                ScanCondition.U8(0x0A, 0x00), // bikeFrameCounter = 0 (not biking)
                ScanCondition.U8(0x0B, 0x00), // bikeSpeed = 0 (not biking)
            };

            if (flags.HasValue)
            {
                conditions.Insert(0, ScanCondition.U8(0x00, flags.Value));
            }

            if (spriteId.HasValue)
            {
                conditions.Add(ScanCondition.U8(0x04, spriteId.Value));
            }

            if (requireOnFoot)
            {
                conditions.Add(ScanCondition.U8(0x08, 0x00)); // acroBikeState = normal
            }

            return scanner.ScanEwram(conditions, alignment: 4);
        }
    }
}
