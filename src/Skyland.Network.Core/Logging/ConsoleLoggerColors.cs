#region using

using System;
using System.Collections.Generic;

#endregion

namespace Skyland.Network.Core.Logging
{
    class ConsoleLoggerColors
    {
        private readonly IDictionary<LogLevel, ConsoleColor> _backgroundColors, _foregroundColors;

        public ConsoleLoggerColors()
        {
            _backgroundColors = new Dictionary<LogLevel, ConsoleColor>();
            _foregroundColors =
                new Dictionary<LogLevel, ConsoleColor>
                {
                    {LogLevel.Trace, ConsoleColor.DarkGray},
                    {LogLevel.Debug, ConsoleColor.Gray},
                    {LogLevel.Info, ConsoleColor.Green},
                    {LogLevel.Warn, ConsoleColor.Yellow},
                    {LogLevel.Error, ConsoleColor.Red}
                };
        }

        public ConsoleColor? GetForegroundColor(LogLevel level)
        {
            ConsoleColor color;
            return
                _foregroundColors.TryGetValue(level, out color) 
                    ? color 
                    : default(ConsoleColor?);
        }

        public ConsoleColor? GetBackgroundColor(LogLevel level)
        {
            ConsoleColor color;
            return
                _backgroundColors.TryGetValue(level, out color)
                    ? color
                    : default(ConsoleColor?);
        }
    }
}
