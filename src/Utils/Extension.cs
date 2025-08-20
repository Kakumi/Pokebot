using BizHawk.Client.Common;
using Pokebot.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

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

        public static bool HasSaveState(this IEmuClientApi emuClient, string name)
        {
            var directory = Environment.CurrentDirectory;
            var fullDirectory = Path.Combine(directory, "GBA", "State", $"{name}.State");
            return File.Exists(fullDirectory);
        }

        public static bool LoadOrStop(this IEmuClientApi emuClient, string name)
        {
            bool loaded = false;
            try
            {
                loaded = emuClient.LoadState(name);
            }
            catch (FileNotFoundException) //If the save state doesn't exists
            {

            }
            finally
            {
                if (!loaded)
                {
                    throw new BotException(Messages.BotPokeFinder_InvalidSaveState);
                }
            }

            return loaded;
        }

        public static void SetWhenInactive(this IJoypadApi api, string button)
        {
            var t = api.Get();
            var buttonState = api.Get().FirstOrDefault(x => x.Key == button);
            if (!(bool) buttonState.Value)
            {
                api.Set(button, true);
            }
        }
    }
}
