using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Day2eEditor
{
    // Services the host app registers in AppServices for plugins to use.
    // Plugins depend on these interfaces, never on the Day2eEditor project itself.

    public interface IFileTransferService
    {
        bool TestConnection(ProjectServerSettings settings);
        List<RemoteItem> ListDirectory(ProjectServerSettings settings, string path);
        void Upload(ProjectServerSettings settings, string localPath, string remotePath);
        void Download(ProjectServerSettings settings, string remotePath, string localPath);
        void DownloadDirectory(ProjectServerSettings settings, string remoteRoot, string localRoot, bool checknewer = false);
        void Delete(ProjectServerSettings settings, string remotePath);
        List<SyncItem> CheckForChanges(ProjectServerSettings settings, string remoteRoot, string localRoot);
    }

    public interface IPluginInstaller
    {
        Task DownloadNewPluginAsync(PluginInfo plugin);
        Task DownloadMapAddonAsync(MapAddonInfo addon);
    }

    public interface IHostShell
    {
        void SetStatusText(string text);
    }

    public class SyncItem
    {
        public string RelativePath { get; set; } = string.Empty;
        public string LocalPath { get; set; } = string.Empty;
        public string RemotePath { get; set; } = string.Empty;

        public DateTime LocalModified { get; set; }
        public DateTime RemoteModified { get; set; }

        public PendingServerAction Action { get; set; }
    }
    public sealed class RemoteItem
    {
        public string Name { get; set; } = string.Empty;
        public string FullPath { get; set; } = string.Empty;
        public bool IsDirectory { get; set; }
        public long Size { get; set; }
        public DateTime Modified { get; set; }
    }
    public class SingleFileInfo
    {
        public string FullPath { get; set; } = string.Empty;
        public long Size { get; set; }
        public DateTime Modified { get; set; }
    }
}
