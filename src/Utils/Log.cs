using Pokebot.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Utils
{
    public static class Log
    {
        public delegate void LogEventHandler(LogEventArgs e);

        public static event LogEventHandler? LogReceived;

        public static void Debug(string message)
        {
            Send(LogLevel.Debug, message);
        }

        public static void Info(string message)
        {
            Send(LogLevel.Info, message);
        }

        public static void Warn(string message)
        {
            Send(LogLevel.Warn, message);
        }

        public static void Error(string message)
        {
            Send(LogLevel.Error, message);
        }

        public static void Fatal(string message)
        {
            Send(LogLevel.Fatal, message);
        }

        public static void Send(LogLevel level, string message) {
            LogReceived?.Invoke(new LogEventArgs(level, message));
        }
    }

    public record LogEventArgs(LogLevel Level, string Message) { }
}
