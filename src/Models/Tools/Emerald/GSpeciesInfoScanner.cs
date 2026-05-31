using System.Collections.Generic;
using System.Linq;

namespace Pokebot.Models.Tools.Emerald
{
    public static class GSpeciesInfoScanner
    {
        public static List<SymbolScanResult> FindBase(
            SymbolScanner scanner,
            int speciesIndex,
            byte baseHp,
            byte baseAttack,
            byte baseDefense,
            byte baseSpeed,
            byte baseSpAttack,
            byte baseSpDefense,
            byte? type1 = null,
            byte? type2 = null
        )
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

            var entries = scanner.ScanRom(conditions, alignment: 4);

            return entries.Select(r => new SymbolScanResult(r.Address - (long)speciesIndex * structSize)).ToList();
        }
    }
}
