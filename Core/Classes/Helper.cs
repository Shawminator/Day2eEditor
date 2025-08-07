using System;
using System.Diagnostics;
using System.IO;

namespace Day2eEditor
{
    public class EventGuard
    {
        private bool _isSuppressed = false;

        public bool IsUserInteraction => !_isSuppressed;

        public IDisposable Suppress()
        {
            _isSuppressed = true;
            return new Suppressor(() => _isSuppressed = false);
        }

        private class Suppressor : IDisposable
        {
            private readonly Action _onDispose;
            private bool _disposed;

            public Suppressor(Action onDispose)
            {
                _onDispose = onDispose;
            }

            public void Dispose()
            {
                if (!_disposed)
                {
                    _onDispose();
                    _disposed = true;
                }
            }
        }
    }
    public static class ShellHelper
    {
        public static void OpenFolderInExplorer(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
                throw new ArgumentException("Folder path is null or empty.", nameof(folderPath));

            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"The folder does not exist: {folderPath}");

            var psi = new ProcessStartInfo
            {
                FileName = folderPath,
                UseShellExecute = true
            };

            Process.Start(psi);
        }

        public static void ShowFileInExplorer(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path is null or empty.", nameof(filePath));

            if (!File.Exists(filePath))
                throw new FileNotFoundException("The file does not exist.", filePath);

            var psi = new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = $"/select,\"{filePath}\"",
                UseShellExecute = true
            };

            Process.Start(psi);
        }
    }
}