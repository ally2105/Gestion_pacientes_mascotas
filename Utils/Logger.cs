using System;
using System.IO;
using System.Text;

namespace Gestion_pacientes_mascotas.Utils
{
    // Minimalist logger for demos: writes timestamped entries to logs/error.log
    // Does not replace a production logging framework (Serilog, NLog, Microsoft.Extensions.Logging).
    public static class Logger
    {
        private static readonly object _lock = new object();
        private static readonly string _logDir = Path.Combine(AppContext.BaseDirectory, "logs");
        private static readonly string _errorFile = Path.Combine(_logDir, "error.log");

    public static void LogError(Exception ex, string? context = null)
        {
            try
            {
                var sb = new StringBuilder();
                sb.AppendLine($"[{DateTime.UtcNow:O}] ERROR {(context ?? "")}");
                sb.AppendLine($"Type: {ex.GetType().FullName}");
                sb.AppendLine($"Message: {ex.Message}");
                sb.AppendLine("StackTrace:");
                sb.AppendLine(ex.StackTrace ?? "(no stack)");
                sb.AppendLine(new string('-', 80));

                Write(sb.ToString());
            }
            catch
            {
                // Do not throw from the logger; fallbacks in production should handle it.
            }
        }

    public static void LogWarning(string message, string? context = null)
        {
            try
            {
                var line = $"[{DateTime.UtcNow:O}] WARN {(context ?? "")} - {message}{Environment.NewLine}";
                Write(line);
            }
            catch { }
        }

    public static void LogInfo(string message, string? context = null)
        {
            try
            {
                var line = $"[{DateTime.UtcNow:O}] INFO {(context ?? "")} - {message}{Environment.NewLine}";
                Write(line);
            }
            catch { }
        }

        private static void Write(string content)
        {
            lock (_lock)
            {
                Directory.CreateDirectory(_logDir);
                File.AppendAllText(_errorFile, content);
            }
        }
    }
}
