using BizHawk.Client.Common;
using Pokebot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokebot.Utils
{
    public static class SymbolUtil
    {
        public static IReadOnlyList<byte> Read(ApiContainer api, long address, int offset = 0x0, int size = 0x0)
        {
            return api.Memory.ReadByteRange(address + offset, size);
        }

        public static IReadOnlyList<byte> Read(ApiContainer api, Symbol symbol)
        {
            return Read(api, symbol.Address, 0, symbol.Size);
        }

        public static IReadOnlyList<byte> Read(ApiContainer api, Symbol symbol, int offset = 0x0, int size = 0x0)
        {
            return Read(api, symbol.Address, offset, size);
        }

        public static void Write(ApiContainer api, Symbol symbol, byte[] bytes)
        {
            api.Memory.WriteByteRange(symbol.Address, bytes);
        }

        public static IReadOnlyList<Symbol> Load(byte[] resource)
        {
            List<Symbol> symbols = new List<Symbol>();
            var text = Encoding.UTF8.GetString(resource);
            var lines = text.Split('\n');
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line) || line.StartsWith("#")) continue;

                var arguments = line.Split(' ');
                if (arguments.Count() != 4)
                {
                    throw new Exception("File invalid, missing arguments for line " + line);
                }

                try
                {
                    int address = Convert.ToInt32(arguments[0], 16);
                    char letter = Convert.ToChar(arguments[1]);
                    int size = Convert.ToInt32(arguments[2], 16);
                    string name = arguments[3].Replace("\r", ""); ;

                    symbols.Add(new Symbol(address, letter, size, name));
                }
                catch (Exception)
                {
                    throw new Exception("File invalid, error while parsing line " + line);
                }
            }

            return symbols;
        }
    }
}
