using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.Context.Logger
{
    public static class DBLogger
    {
        public static readonly LoggerFactory DbCommandConsoleLoggerFactory
            = new LoggerFactory(new[] {
              new ConsoleLoggerProvider ((category, level) =>
                category == DbLoggerCategory.Database.Command.Name &&
                level == LogLevel.Information, true)
            });
    }
}
