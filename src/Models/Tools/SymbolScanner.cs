using BizHawk.Client.Common;
using Pokebot.Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pokebot.Models.Tools
{
    /// <summary>
    /// A condition for a byte pattern at a specific offset within a candidate structure.
    /// Used by SymbolScanner to locate unknown symbol addresses.
    /// </summary>
    public class ScanCondition
    {
        public int Offset { get; }
        public byte[] Pattern { get; }

        public ScanCondition(int offset, byte[] pattern)
        {
            Offset = offset;
            Pattern = pattern;
        }

        /// <summary>Single byte at a given offset.</summary>
        public static ScanCondition U8(int offset, byte value)
        {
            return new ScanCondition(offset, new[] { value });
        }

        /// <summary>Little-endian u16 at a given offset.</summary>
        public static ScanCondition U16(int offset, ushort value)
        {
            return new ScanCondition(offset, BitConverter.GetBytes(value));
        }

        /// <summary>Little-endian u32 at a given offset.</summary>
        public static ScanCondition U32(int offset, uint value)
        {
            return new ScanCondition(offset, BitConverter.GetBytes(value));
        }
    }

    /// <summary>
    /// Result from a SymbolScanner scan — carries the candidate address.
    /// </summary>
    public class SymbolScanResult
    {
        public long Address { get; }

        /// <summary>Address formatted as an 8-digit hex string, ready to paste into a patch symbol file.</summary>
        public string Hex => Address.ToString("X8");

        public SymbolScanResult(long address)
        {
            Address = address;
        }

        public override string ToString() => Hex;
    }

    /// <summary>
    /// Scans GBA EWRAM to locate symbol addresses missing from non-English ROM patch files.
    ///
    /// Usage workflow:
    ///   1. Load the non-English ROM in BizHawk.
    ///   2. Stand still on a known tile before scanning.
    ///   3. Call the relevant Find* method with known game-state values.
    ///   4. Use the returned hex address(es) in your .sym patch file.
    ///
    /// GBA memory layout (Gen 3):
    ///   EWRAM  0x02000000 – 0x02040000  (256 KB) — most game variables live here
    ///   IWRAM  0x03000000 – 0x03008000  (32 KB)  — tasks and short-lived state live here
    /// </summary>
    public class SymbolScanner
    {
        private readonly ApiContainer _api;

        /// <summary>EWRAM base address on the GBA system bus.</summary>
        public const long EwramStart = 0x02000000;

        /// <summary>EWRAM size in bytes (256 KB).</summary>
        public const int EwramSize = 0x40000;

        /// <summary>IWRAM base address on the GBA system bus.</summary>
        public const long IwramStart = 0x03000000;

        /// <summary>IWRAM size in bytes (32 KB).</summary>
        public const int IwramSize = 0x8000;

        public SymbolScanner(ApiContainer api)
        {
            _api = api;
        }

        // -------------------------------------------------------------------------
        // General-purpose scanner
        // -------------------------------------------------------------------------

        /// <summary>
        /// Scans a memory range and returns all base addresses where every condition
        /// is satisfied simultaneously.
        /// </summary>
        /// <param name="conditions">Byte patterns with their offsets relative to the candidate base.</param>
        /// <param name="rangeStart">Absolute start address to search.</param>
        /// <param name="rangeSize">Number of bytes to read.</param>
        public List<SymbolScanResult> Scan(IReadOnlyList<ScanCondition> conditions, long rangeStart, int rangeSize)
        {
            if (conditions == null || conditions.Count == 0)
            {
                throw new ArgumentException("At least one condition is required.");
            }

            int minStructSize = conditions.Max(c => c.Offset + c.Pattern.Length);
            var results = new List<SymbolScanResult>();
            var memory = _api.Memory.ReadByteRange(rangeStart, rangeSize).ToArray();

            for (int i = 0; i <= rangeSize - minStructSize; i++)
            {
                if (MatchesAll(memory, i, conditions))
                {
                    results.Add(new SymbolScanResult(rangeStart + i));
                }
            }

            return results;
        }

        /// <summary>Scans the full EWRAM region (256 KB).</summary>
        public List<SymbolScanResult> ScanEwram(IReadOnlyList<ScanCondition> conditions)
        {
            return Scan(conditions, EwramStart, EwramSize);
        }

        /// <summary>Scans the full IWRAM region (32 KB).</summary>
        public List<SymbolScanResult> ScanIwram(IReadOnlyList<ScanCondition> conditions)
        {
            return Scan(conditions, IwramStart, IwramSize);
        }

        // -------------------------------------------------------------------------
        // Gen 3 – gObjectEvents
        // -------------------------------------------------------------------------

        /// <summary>
        /// Finds the base address of gObjectEvents by matching the player's current tile position
        /// inside the first object-event slot (index 0 = player).
        ///
        /// How to use:
        ///   • Stand completely still on any tile.
        ///   • Note your X and Y coordinates (visible in BizHawk's debug view or in-game debug).
        ///   • Optionally note your facing direction for fewer false positives.
        ///
        /// Gen 3 object-event struct offsets (relative to gObjectEvents[0]):
        ///   +0x10  currentX  (u16 LE)
        ///   +0x12  currentY  (u16 LE)
        ///   +0x14  previousX (u16 LE) — equal to currentX while standing still
        ///   +0x16  previousY (u16 LE) — equal to currentY while standing still
        ///   +0x18  facingDirection (u8): Down=0x11, Up=0x22, Left=0x33, Right=0x44
        /// </summary>
        /// <param name="playerX">Player's current tile X coordinate.</param>
        /// <param name="playerY">Player's current tile Y coordinate.</param>
        /// <param name="facing">Optional: player's facing direction for a tighter match.</param>
        public List<SymbolScanResult> FindObjectEventsBase(ushort playerX, ushort playerY, PlayerFacingDirection? facing = null)
        {
            var conditions = new List<ScanCondition>
            {
                ScanCondition.U16(0x10, playerX),
                ScanCondition.U16(0x12, playerY),
                ScanCondition.U16(0x14, playerX), // prev == current when not moving
                ScanCondition.U16(0x16, playerY),
            };

            if (facing.HasValue)
            {
                conditions.Add(ScanCondition.U8(0x18, (byte)facing.Value));
            }

            return ScanEwram(conditions);
        }

        // -------------------------------------------------------------------------
        // Gen 3 – gPlayerAvatar
        // -------------------------------------------------------------------------

        /// <summary>
        /// Finds the base address of gPlayerAvatar by matching known state bytes.
        ///
        /// How to use:
        ///   • Stand completely still in the overworld (not surfing, not cycling, not in grass animation).
        ///   • Know your character's gender.
        ///
        /// Gen 3 gPlayerAvatar struct offsets:
        ///   +0x00  objectEventId      (u8) = 0  (player is always object event 0)
        ///   +0x01  fieldEffectSpriteId(u8) = 0xFF when no field effect (not surfing/cycling/etc.)
        ///   +0x02  runningState       (u8) = 0  when standing still (NotMoving)
        ///   +0x03  tileTransitionState(u8) = 0  when not transitioning between tiles
        ///   +0x07  gender             (u8) = 0 for male, 1 for female
        ///
        /// Note: fieldEffectSpriteId=0xFF is a very distinctive marker. If you are surfing or
        /// cycling, omit it with <paramref name="requireNoFieldEffect"/> = false.
        /// </summary>
        /// <param name="gender">Character gender: 0 = male, 1 = female.</param>
        /// <param name="requireNoFieldEffect">
        ///   true (default): add +0x01==0xFF condition (only works when not surfing/cycling).
        ///   false: skip that condition if you cannot stand on a plain tile.
        /// </param>
        public List<SymbolScanResult> FindPlayerAvatarBase(byte gender, bool requireNoFieldEffect = true)
        {
            var conditions = new List<ScanCondition>
            {
                ScanCondition.U8(0x00, 0x00), // objectEventId = 0
                ScanCondition.U8(0x02, 0x00), // runningState = NotMoving
                ScanCondition.U8(0x03, 0x00), // tileTransitionState = not transitioning
                ScanCondition.U8(0x07, gender),
            };

            if (requireNoFieldEffect)
            {
                conditions.Insert(1, ScanCondition.U8(0x01, 0xFF));
            }

            return ScanEwram(conditions);
        }

        // -------------------------------------------------------------------------
        // Gen 3 – gTasks
        // -------------------------------------------------------------------------

        /// <summary>
        /// Finds the base address of gTasks by validating the full 640-byte task array structure in IWRAM.
        ///
        /// How to use:
        ///   • Be in the overworld with the bot idle (at least one task must be running).
        ///   • gTasks lives in IWRAM — the scan targets 0x03000000–0x03008000.
        ///
        /// Gen 3 task struct offsets (relative to each gTasks[i]):
        ///   +0x00  func      (u32 LE) — ROM function pointer; high byte is 0x08
        ///   +0x04  isActive  (u8) — 1 when running, 0 when empty
        ///   +0x05  prev, +0x06 next, +0x07 priority  (u8 each)
        ///   +0x08..+0x27  data (32 bytes)
        ///
        /// The scan checks ALL 16 isActive bytes in the array: every value must be exactly
        /// 0x00 or 0x01, and every active slot must have a ROM function pointer (high byte 0x08).
        /// This works regardless of how many tasks are currently running.
        /// </summary>
        public List<SymbolScanResult> FindTasksBase()
        {
            const int taskSize = 40;
            const int taskCount = 16;
            const int arraySize = taskSize * taskCount; // 640 = 0x280

            var memory = _api.Memory.ReadByteRange(IwramStart, IwramSize).ToArray();
            var results = new List<SymbolScanResult>();

            // Step in 4-byte increments — task arrays are always 4-byte aligned in ROM/RAM,
            // which immediately eliminates unaligned false positives.
            for (int i = 0; i <= IwramSize - arraySize; i += 4)
            {
                // Condition 1: slot 0 itself must be an active task with a ROM function pointer.
                // This rejects windows that start in memory before the real array, where
                // the first "virtual slot" is just unrelated data (typically isActive == 0).
                if (memory[i + 4] != 0x01 || memory[i + 3] != 0x08)
                {
                    continue;
                }

                // Condition 2: the 40 bytes immediately before this position must NOT also
                // be an active task with a ROM pointer. If they are, this position is somewhere
                // inside the array (e.g. gTasks[1], gTasks[2] …), not at gTasks[0].
                if (i >= taskSize && memory[i - taskSize + 4] == 0x01 && memory[i - taskSize + 3] == 0x08)
                {
                    continue;
                }

                // Condition 3: all 16 slots must have isActive ∈ {0, 1},
                // and every active slot must carry a ROM function pointer.
                bool validArray = true;
                for (int slot = 0; slot < taskCount; slot++)
                {
                    int slotOffset = i + slot * taskSize;
                    byte isActive = memory[slotOffset + 4];

                    if (isActive != 0x00 && isActive != 0x01)
                    {
                        validArray = false;
                        break;
                    }

                    if (isActive == 0x01 && memory[slotOffset + 3] != 0x08)
                    {
                        validArray = false;
                        break;
                    }
                }

                if (validArray)
                {
                    results.Add(new SymbolScanResult(IwramStart + i));
                }
            }

            return results;
        }

        // -------------------------------------------------------------------------
        // Private helpers
        // -------------------------------------------------------------------------

        private static bool MatchesAll(byte[] memory, int baseIndex, IReadOnlyList<ScanCondition> conditions)
        {
            foreach (var cond in conditions)
            {
                int pos = baseIndex + cond.Offset;
                for (int j = 0; j < cond.Pattern.Length; j++)
                {
                    if (memory[pos + j] != cond.Pattern[j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
