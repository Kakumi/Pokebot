using BizHawk.Client.Common;
using BizHawk.Common.IOExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Utils
{
    public static class MemoryAddressFinder
    {
        public static List<string> Search(ApiContainer apicontainer, int address, int iteration, string value, int size)
        {
            var addresses = new List<string>();
            try
            {
                //size before and size after
                int checkAddress = address - iteration;
                var bytesObjects = SymbolUtil.Read(apicontainer, checkAddress, 0, iteration * 2).ToArray();

                for (int i = 0; i < bytesObjects.Length; i++)
                {
                    string valueAtAddress;
                    if (size == 1)
                    {
                        valueAtAddress = bytesObjects[i].ToString();
                    } else if (size == 2)
                    {
                        valueAtAddress = bytesObjects.Skip(i).Take(size).ToUInt16().ToString();
                    } else if (size == 4)
                    {
                        valueAtAddress = bytesObjects.Skip(i).Take(size).ToUInt32().ToString();
                    } else
                    {
                        valueAtAddress = Encoding.UTF8.GetString(bytesObjects.Skip(i).Take(size).ToArray());
                    }

                    if (valueAtAddress == value)
                    {
                        addresses.Add((checkAddress + i).ToString("X"));
                    }
                }
            }
            catch { }

            return addresses;
        }
    }
}
