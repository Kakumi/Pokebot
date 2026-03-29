using System.Collections.Generic;

namespace Pokebot.Models.Tools.Emerald
{
    public static class GTasksScanner
    {
        public static List<SymbolScanResult> FindBase(SymbolScanner scanner)
        {
            const int taskSize = 40;
            const int taskCount = 16;
            const int arraySize = taskSize * taskCount; // 640 = 0x280

            var memory = scanner.ReadIwram();
            var results = new List<SymbolScanResult>();

            for (int i = 0; i <= SymbolScanner.IwramSize - arraySize; i += 4)
            {
                if (memory[i + 4] != 0x01 || memory[i + 3] != 0x08)
                {
                    continue;
                }

                if (i >= taskSize && memory[i - taskSize + 4] == 0x01 && memory[i - taskSize + 3] == 0x08)
                {
                    continue;
                }

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
                    results.Add(new SymbolScanResult(SymbolScanner.IwramStart + i));
                }
            }

            return results;
        }
    }
}
