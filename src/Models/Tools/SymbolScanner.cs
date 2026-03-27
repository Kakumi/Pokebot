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

        /// <summary>ROM base address on the GBA system bus.</summary>
        public const long RomStart = 0x08000000;

        /// <summary>ROM size in bytes (16 MB — covers all Gen 3 cartridges).</summary>
        public const int RomSize = 0x1000000;

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
        /// <param name="alignment">Step between candidates: 1 = every byte, 4 = 4-byte aligned only.</param>
        public List<SymbolScanResult> Scan(IReadOnlyList<ScanCondition> conditions, long rangeStart, int rangeSize, int alignment = 1)
        {
            if (conditions == null || conditions.Count == 0)
            {
                throw new ArgumentException("At least one condition is required.");
            }

            int minStructSize = conditions.Max(c => c.Offset + c.Pattern.Length);
            var results = new List<SymbolScanResult>();
            var memory = _api.Memory.ReadByteRange(rangeStart, rangeSize).ToArray();

            for (int i = 0; i <= rangeSize - minStructSize; i += alignment)
            {
                if (MatchesAll(memory, i, conditions))
                {
                    results.Add(new SymbolScanResult(rangeStart + i));
                }
            }

            return results;
        }

        /// <summary>Scans the full EWRAM region (256 KB).</summary>
        public List<SymbolScanResult> ScanEwram(IReadOnlyList<ScanCondition> conditions, int alignment = 1)
        {
            return Scan(conditions, EwramStart, EwramSize, alignment);
        }

        /// <summary>Scans the full IWRAM region (32 KB).</summary>
        public List<SymbolScanResult> ScanIwram(IReadOnlyList<ScanCondition> conditions, int alignment = 1)
        {
            return Scan(conditions, IwramStart, IwramSize, alignment);
        }

        /// <summary>Scans the full ROM region (16 MB).</summary>
        public List<SymbolScanResult> ScanRom(IReadOnlyList<ScanCondition> conditions, int alignment = 1)
        {
            return Scan(conditions, RomStart, RomSize, alignment);
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
        ///   • Stand completely still in the overworld on foot (not surfing, not cycling).
        ///   • Know your character's gender.
        ///
        /// Gen 3 gPlayerAvatar struct offsets:
        ///   +0x00  flags               (u8) — active state flags; typical values:
        ///                                       0x01 = ON_FOOT only
        ///                                       0x21 = ON_FOOT | CONTROLLABLE (most common when idle)
        ///   +0x01  transitionFlags     (u8) — transition state flags
        ///   +0x02  runningState        (u8) = 0  — NotMoving (00 = not moving)
        ///   +0x03  tileTransitionState (u8) = 0  — not transitioning
        ///   +0x04  spriteId            (u8) — player object sprite ID (visible in BizHawk memory viewer)
        ///   +0x05  objectEventId       (u8) = 0  — player is always object event 0
        ///   +0x06  preventStep         (u8)
        ///   +0x07  gender              (u8) = 0 male / 1 female
        ///   +0x08  acroBikeState       (u8) = 0  — normal / not on acro bike
        /// </summary>
        /// <param name="gender">Character gender: 0 = male, 1 = female.</param>
        /// <param name="flags">
        ///   Optional: value of the flags byte at +0x00. Greatly narrows results.
        ///   Typical value when standing still on foot: 0x21 (ON_FOOT | CONTROLLABLE).
        ///   Pass null to skip this condition.
        /// </param>
        /// <param name="spriteId">
        ///   Optional: player sprite ID at +0x04, visible in BizHawk's memory viewer.
        ///   Pass null to skip this condition.
        /// </param>
        /// <param name="requireOnFoot">
        ///   true (default): add +0x08==0x00 (acroBikeState=normal, only reliable on foot).
        ///   false: omit it if you must scan while surfing or cycling.
        /// </param>
        public List<SymbolScanResult> FindPlayerAvatarBase(byte gender, byte? flags = null, byte? spriteId = null, bool requireOnFoot = true)
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

            // Struct contains u32 fields so it is always 4-byte aligned.
            return ScanEwram(conditions, alignment: 4);
        }

        // -------------------------------------------------------------------------
        // Gen 3 – gMain
        // -------------------------------------------------------------------------

        /// <summary>
        /// Finds the base address of gMain by matching the ROM function-pointer signature
        /// at the head of the struct and optional game-state bytes near the tail.
        ///
        /// How to use:
        ///   • Be in the overworld with the game fully loaded (not in a battle or transition).
        ///   • No special position is required.
        ///   • gMain lives in IWRAM (0x03000000–0x03008000), not EWRAM.
        ///
        /// Gen 3 gMain struct offsets (abbreviated):
        ///   +0x000  callback1      (u32 LE) — ROM ptr; high byte 0x08
        ///   +0x004  callback2      (u32 LE) — ROM ptr; high byte 0x08
        ///   +0x008  savedCallback  (u32 LE) — ROM ptr or 0
        ///   +0x00C  vblankCallback (u32 LE) — ROM ptr; high byte 0x08 (always set)
        ///   +0x010  hblankCallback (u32 LE) — ROM ptr or 0
        ///   +0x014  vcountCallback (u32 LE) — ROM ptr or 0
        ///   +0x018  serialCallback (u32 LE) — ROM ptr or 0
        ///   +0x038  oamBuffer[128] — 0x400 bytes of OAM entries
        ///   +0x438  state          (u8)  — main loop state index
        ///   +0x439  flags          (u8)  — packed: oamLoadDisabled | inBattle | anyLinkBattlerHasFrontierPass
        ///
        /// The scan relies on three guaranteed ROM pointers (high byte == 0x08) at fixed
        /// offsets: callback1 (+0x03), callback2 (+0x07), vblankCallback (+0x0F).
        /// This three-byte signature at non-trivial positions is highly unique in EWRAM.
        /// </summary>
        /// <param name="state">
        ///   Optional: main loop state byte at +0x438. Narrows results when known.
        ///   Pass null to skip.
        /// </param>
        /// <param name="requireOverworld">
        ///   true (default): require the flags byte at +0x439 to be 0x00
        ///   (oamLoadDisabled=0, inBattle=0, anyLinkBattlerHasFrontierPass=0).
        ///   false: omit the check — use when scanning during a battle or transition.
        /// </param>
        public List<SymbolScanResult> FindMainBase(byte? state = null, bool requireOverworld = true)
        {
            var conditions = new List<ScanCondition>
            {
                // Three guaranteed ROM function pointers at the head of the struct.
                // High byte of each little-endian u32 must be 0x08 (GBA ROM space).
                ScanCondition.U8(0x03, 0x08), // callback1 high byte
                ScanCondition.U8(0x07, 0x08), // callback2 high byte
                ScanCondition.U8(0x0F, 0x08), // vblankCallback high byte (always set)
            };

            if (state.HasValue)
            {
                conditions.Add(ScanCondition.U8(0x438, state.Value));
            }

            if (requireOverworld)
            {
                // oamLoadDisabled=0, inBattle=0, anyLinkBattlerHasFrontierPass=0
                conditions.Add(ScanCondition.U8(0x439, 0x00));
            }

            // All u32 fields → struct is 4-byte aligned.
            // gMain lives in IWRAM (0x03000000), not EWRAM.
            return ScanIwram(conditions, alignment: 4);
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
        // Gen 3 – gEnemyParty / gEnemyPartyCount
        // -------------------------------------------------------------------------

        /// <summary>
        /// Finds the base address of gEnemyParty by matching the first slot of the
        /// enemy party (gEnemyParty[0]) using known battle values.
        ///
        /// How to use:
        ///   • Be in an active wild or trainer battle.
        ///   • Read the enemy Pokemon's level and Max HP from the battle HUD.
        ///   • Status should be clear (no poison/burn/etc.) for requireNoStatus=true.
        ///
        /// gEnemyParty lives in EWRAM for FR/LG/Emerald and IWRAM for Ruby/Sapphire.
        /// Both regions are scanned and results are combined.
        ///
        /// Pokemon struct offsets (gEnemyParty[0]):
        ///   +0x00  BoxPokemon (0x50 bytes)
        ///     +0x13  flags byte: isBadEgg|hasSpecies|isEgg|... = 0x02 for a normal loaded mon
        ///   +0x50  status    (u32) = 0 when no status condition
        ///   +0x54  level     (u8)
        ///   +0x56  hp        (u16) — current, changes during battle
        ///   +0x58  maxHP     (u16) — stable for the whole battle
        /// </summary>
        /// <param name="level">Enemy Pokemon's level shown in the battle HUD.</param>
        /// <param name="maxHp">
        ///   Optional: enemy Pokemon's max HP at +0x58. Gen 3 does not show the enemy's
        ///   actual HP number — only the bar ratio — so leave this null unless you can
        ///   derive it externally (e.g. from a reference or a prior memory read).
        ///   Providing it greatly narrows results; omitting it still works in most cases.
        /// </param>
        /// <param name="requireNoStatus">
        ///   true (default): add +0x50==0 (status=none). Works if the enemy has no status condition.
        ///   false: omit the check.
        /// </param>
        public List<SymbolScanResult> FindEnemyPartyBase(byte level, ushort? maxHp = null, bool requireNoStatus = true)
        {
            var conditions = new List<ScanCondition>
            {
                ScanCondition.U8(0x13, 0x02),  // hasSpecies=1, isBadEgg=0, isEgg=0
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

            // Pokemon struct is 0x64 bytes, always 4-byte aligned.
            // Cover both memory regions since the location differs by game.
            var ewram = ScanEwram(conditions, alignment: 4);
            var iwram = ScanIwram(conditions, alignment: 4);
            return ewram.Concat(iwram).ToList();
        }

        /// <summary>
        /// Finds gEnemyPartyCount by searching a ±0x400 byte window around a known
        /// gEnemyParty address. The count byte is always near the party array but
        /// the exact offset varies by game (2 bytes for FR/LG, 8 for R/S, ~600 for Emerald).
        ///
        /// How to use:
        ///   • Run FindEnemyPartyBase first and confirm the correct address.
        ///   • Pass that address and the known enemy party size (1-6).
        ///   • The padding byte immediately after gEnemyPartyCount is always 0x00,
        ///     which helps rule out random bytes that happen to equal the count.
        /// </summary>
        /// <param name="partyBase">Confirmed gEnemyParty address from FindEnemyPartyBase.</param>
        /// <param name="count">Known number of enemy Pokemon (1-6).</param>
        public List<SymbolScanResult> FindEnemyPartyCountNear(long partyBase, byte count)
        {
            const int searchRange = 0x400;

            bool isIwram = partyBase >= IwramStart && partyBase < IwramStart + IwramSize;
            long regionStart = isIwram ? IwramStart : EwramStart;
            int regionSize   = isIwram ? IwramSize  : EwramSize;

            long scanStart = Math.Max(partyBase - searchRange, regionStart);
            long scanEnd   = Math.Min(partyBase + searchRange, regionStart + regionSize);
            int  scanSize  = (int)(scanEnd - scanStart);

            // Match only the count byte. The byte immediately after is NOT always padding:
            // FR/LG and R/S have it, but Emerald has live game data there.
            // The narrow ±0x400 window keeps the result list manageable.
            var conditions = new List<ScanCondition>
            {
                ScanCondition.U8(0x00, count),
            };

            return Scan(conditions, scanStart, scanSize, alignment: 1);
        }

        // -------------------------------------------------------------------------
        // Gen 3 – gSpeciesInfo
        // -------------------------------------------------------------------------

        /// <summary>
        /// Finds the base address of gSpeciesInfo by matching the base stats of a known
        /// species entry in ROM. gSpeciesInfo is a const array stored in ROM.
        ///
        /// How to use:
        ///   • Look up the base stats of any Pokemon whose species index you know
        ///     (e.g. Bulbasaur = index 1: HP 45, Atk 49, Def 49, Spd 45, SpA 65, SpD 65).
        ///   • Run while the game is loaded — ROM is always readable.
        ///   • The returned address is the base of the full gSpeciesInfo array (index 0).
        ///
        /// Gen 3 SpeciesInfo struct offsets (size 0x1C with padding):
        ///   +0x00  baseHP
        ///   +0x01  baseAttack
        ///   +0x02  baseDefense
        ///   +0x03  baseSpeed
        ///   +0x04  baseSpAttack
        ///   +0x05  baseSpDefense
        ///   +0x06  type1
        ///   +0x07  type2
        ///   ...
        /// </summary>
        /// <param name="speciesIndex">Index of the Pokemon whose stats are provided (e.g. 1 for Bulbasaur).</param>
        /// <param name="baseHp">Base HP of the reference species.</param>
        /// <param name="baseAttack">Base Attack.</param>
        /// <param name="baseDefense">Base Defense.</param>
        /// <param name="baseSpeed">Base Speed.</param>
        /// <param name="baseSpAttack">Base Sp. Attack.</param>
        /// <param name="baseSpDefense">Base Sp. Defense.</param>
        /// <param name="type1">Optional: type1 byte for extra narrowing.</param>
        /// <param name="type2">Optional: type2 byte for extra narrowing.</param>
        public List<SymbolScanResult> FindSpeciesInfoBase(
            int speciesIndex,
            byte baseHp, byte baseAttack, byte baseDefense,
            byte baseSpeed, byte baseSpAttack, byte baseSpDefense,
            byte? type1 = null, byte? type2 = null)
        {
            const int structSize = 0x1C;

            var conditions = new List<ScanCondition>
            {
                ScanCondition.U8(0x00, baseHp),
                ScanCondition.U8(0x01, baseAttack),
                ScanCondition.U8(0x02, baseDefense),
                ScanCondition.U8(0x03, baseSpeed),
                ScanCondition.U8(0x04, baseSpAttack),
                ScanCondition.U8(0x05, baseSpDefense),
            };

            if (type1.HasValue)
            {
                conditions.Add(ScanCondition.U8(0x06, type1.Value));
            }

            if (type2.HasValue)
            {
                conditions.Add(ScanCondition.U8(0x07, type2.Value));
            }

            // gSpeciesInfo entries are 4-byte aligned in ROM.
            var entries = ScanRom(conditions, alignment: 4);

            // Each hit is gSpeciesInfo[speciesIndex]. Subtract back to get the array base.
            return entries
                .Select(r => new SymbolScanResult(r.Address - (long)speciesIndex * structSize))
                .ToList();
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
