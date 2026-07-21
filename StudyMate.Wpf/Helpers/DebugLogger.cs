using System.Diagnostics;
using System.IO;

namespace StudyMate.Wpf.Helpers
{
    public static class DebugLogger
    {
        private static readonly string LogPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "StudyMate",
            "debug.log");

        private static readonly object LockObj = new();

        public static void Log(string message)
        {
            try
            {
                lock (LockObj)
                {
                    var timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
                    var logLine = $"[{timestamp}] {message}";

                    Debug.WriteLine(logLine);
                    File.AppendAllText(LogPath, logLine + Environment.NewLine);
                }
            }
            catch
            {
                // Ignore logging errors
            }
        }

        public static void Clear()
        {
            try
            {
                if (File.Exists(LogPath))
                {
                    File.Delete(LogPath);
                }
            }
            catch
            {
                // Ignore
            }
        }
    }
}