using System;
using System.Collections.Generic;
using System.Linq;
using BizHawk.Client.Common;

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

        /// <summary>
        /// Optional extra annotation shown alongside the result (e.g. a dereferenced ROM pointer).
        /// Not part of the symbol file output — used for human verification only.
        /// </summary>
        public string Tag { get; set; }

        public SymbolScanResult(long address)
        {
            Address = address;
        }

        public override string ToString() => Hex;
    }

    /// <summary>
    /// Scans GBA EWRAM/IWRAM/ROM to locate symbol addresses missing from non-English ROM patch files.
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

        /// <summary>Reads the full EWRAM region as a byte array.</summary>
        public byte[] ReadEwram() => _api.Memory.ReadByteRange(EwramStart, EwramSize).ToArray();

        /// <summary>Reads the full IWRAM region as a byte array.</summary>
        public byte[] ReadIwram() => _api.Memory.ReadByteRange(IwramStart, IwramSize).ToArray();

        // -------------------------------------------------------------------------
        // Multi-pass refinement
        // -------------------------------------------------------------------------

        /// <summary>
        /// Re-evaluates a previous set of scan results against new conditions, returning only
        /// addresses where all new conditions are satisfied (intersection / narrowing).
        /// </summary>
        public List<SymbolScanResult> Refine(IReadOnlyList<SymbolScanResult> previous, IReadOnlyList<ScanCondition> conditions)
        {
            if (previous == null || previous.Count == 0)
            {
                return new List<SymbolScanResult>();
            }

            if (conditions == null || conditions.Count == 0)
            {
                throw new ArgumentException("At least one condition is required.");
            }

            int minSize = conditions.Max(c => c.Offset + c.Pattern.Length);

            bool needEwram = previous.Any(r => r.Address >= EwramStart && r.Address < EwramStart + EwramSize);
            bool needIwram = previous.Any(r => r.Address >= IwramStart && r.Address < IwramStart + IwramSize);
            bool needRom = previous.Any(r => r.Address >= RomStart && r.Address < RomStart + RomSize);

            byte[] ewram = needEwram ? _api.Memory.ReadByteRange(EwramStart, EwramSize).ToArray() : null;
            byte[] iwram = needIwram ? _api.Memory.ReadByteRange(IwramStart, IwramSize).ToArray() : null;
            byte[] rom = needRom ? _api.Memory.ReadByteRange(RomStart, RomSize).ToArray() : null;

            var results = new List<SymbolScanResult>();

            foreach (var prev in previous)
            {
                byte[] region;
                long regionStart;
                int regionSize;

                if (prev.Address >= EwramStart && prev.Address < EwramStart + EwramSize)
                {
                    region = ewram;
                    regionStart = EwramStart;
                    regionSize = EwramSize;
                }
                else if (prev.Address >= IwramStart && prev.Address < IwramStart + IwramSize)
                {
                    region = iwram;
                    regionStart = IwramStart;
                    regionSize = IwramSize;
                }
                else if (prev.Address >= RomStart && prev.Address < RomStart + RomSize)
                {
                    region = rom;
                    regionStart = RomStart;
                    regionSize = RomSize;
                }
                else
                {
                    continue;
                }

                int offset = (int)(prev.Address - regionStart);
                if (offset + minSize > regionSize)
                {
                    continue;
                }

                if (MatchesAll(region, offset, conditions))
                {
                    results.Add(prev);
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
