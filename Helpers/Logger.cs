using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Malshinon.Helpers
{

    public static class Logger
    {
        public enum LogLevel
        {
            Info,
            Warning,
            Error
        }

        // Set this to true to enable file logging
        public static bool EnableFileLogging { get; set; } = true;

        private static readonly string LogFilePath = "app.log";

        public static void Log(string message, LogLevel level = LogLevel.Info)
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";

            // Output to console
            Console.WriteLine(logMessage);

            // Optionally write to a file
            if (EnableFileLogging)
            {
                try
                {
                    File.AppendAllText(LogFilePath, logMessage + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to write to log file: {ex.Message}");
                }
            }
        }
    }
}
