using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Utils
{
    public static class Extension
    {
        public static uint ToUInt32(this IEnumerable<byte> bytes)
        {
            return ToUInt32(bytes.ToArray());
        }

        public static int ToInt16(this IEnumerable<byte> bytes)
        {
            return ToInt16(bytes.ToArray());
        }

        public static int ToUInt16(this IEnumerable<byte> bytes)
        {
            return ToUInt16(bytes.ToArray());
        }

        public static uint ToUInt32(this byte[] bytes)
        {
            return BitConverter.ToUInt32(bytes, 0);
        }

        public static int ToInt16(this byte[] bytes)
        {
            return BitConverter.ToInt16(bytes, 0);
        }

        public static int ToUInt16(this byte[] bytes)
        {
            return BitConverter.ToUInt16(bytes, 0) & 0xFFFF;
        }
    }
}
