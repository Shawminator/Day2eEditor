using FluentFTP;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Day2eEditor
{
    public sealed class RemoteItem
    {
        public string Name { get; set; } = string.Empty;
        public string FullPath { get; set; } = string.Empty;
        public bool IsDirectory { get; set; }
        public long Size { get; set; }
        public DateTime Modified { get; set; }
    }
    public sealed class FileTransferManager
    {
        // -----------------------------
        // PUBLIC API
        // -----------------------------

        public bool TestConnection(ProjectServerSettings settings)
        {
            return settings.Protocol switch
            {
                TransferProtocol.Sftp => TestSftp(settings),
                TransferProtocol.Ftp or TransferProtocol.Ftps => TestFtp(settings),
                _ => false
            };
        }
        public List<RemoteItem> ListDirectory(ProjectServerSettings settings, string path)
        {
            return settings.Protocol switch
            {
                TransferProtocol.Sftp => ListSftp(settings, path),
                TransferProtocol.Ftp or TransferProtocol.Ftps => ListFtp(settings, path),
                _ => new List<RemoteItem>()
            };
        }
        private List<RemoteItem> ListSftp(ProjectServerSettings s,string path)
        {
            using var client = CreateSftp(s);

            client.Connect();

            var items = client
                .ListDirectory(path)
                .Where(x => x.Name != "." && x.Name != "..")
                .Select(x => new RemoteItem
                {
                    Name = x.Name,
                    FullPath = x.FullName,
                    IsDirectory = x.IsDirectory,
                    Size = x.Attributes.Size,
                    Modified = x.LastWriteTime
                })
                .ToList();

            client.Disconnect();

            return items;
        }
        private List<RemoteItem> ListFtp(ProjectServerSettings s, string path)
        {
            using var client = CreateFtp(s);

            client.Connect();

            var items = client
                .GetListing(BuildRemotePath(s, path))
                .Select(x => new RemoteItem
                {
                    Name = x.Name,
                    FullPath = x.FullName,
                    IsDirectory = x.Type == FtpObjectType.Directory,
                    Size = x.Size,
                    Modified = x.Modified
                })
                .ToList();

            client.Disconnect();

            return items;
        }
        private string BuildRemotePath(ProjectServerSettings settings, string path)
        {
            path = path.Replace("\\", "/");

            if (string.IsNullOrWhiteSpace(settings.RootPath))
                return path;

            return $"{settings.RootPath.TrimEnd('/')}/{path.TrimStart('/')}";
        }
        public void Upload(ProjectServerSettings settings, string localPath, string remotePath)
        {
            switch (settings.Protocol)
            {
                case TransferProtocol.Sftp:
                    UploadSftp(settings, localPath, remotePath);
                    break;

                case TransferProtocol.Ftp:
                case TransferProtocol.Ftps:
                    UploadFtp(settings, localPath, remotePath);
                    break;
            }
        }

        public void Download(ProjectServerSettings settings, string remotePath, string localPath)
        {
            Console.WriteLine($"Downloading: {Path.GetFileName(remotePath)}");
            switch (settings.Protocol)
            {
                case TransferProtocol.Sftp:
                    DownloadSftp(settings, remotePath, localPath);
                    break;

                case TransferProtocol.Ftp:
                case TransferProtocol.Ftps:
                    DownloadFtp(settings, remotePath, localPath);
                    break;
            }
        }

        // -----------------------------
        // SFTP
        // -----------------------------

        private SftpClient CreateSftp(ProjectServerSettings s)
        {
            return new SftpClient(
                s.Host,
                s.Port,
                s.Username,
                Decrypt(s.EncryptedPassword)
            );
        }

        private bool TestSftp(ProjectServerSettings s)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using var client = CreateSftp(s);
                client.Connect();

                bool ok = client.IsConnected;

                client.Disconnect();
                Cursor.Current = Cursors.Default;
                return ok;
            }
            catch
            {
                Cursor.Current = Cursors.Default;
                return false;
            }
        }

        private void UploadSftp(ProjectServerSettings s, string local, string remote)
        {
            using var client = CreateSftp(s);

            client.Connect();

            string? remoteDir = Path.GetDirectoryName(remote)
                ?.Replace("\\", "/");

            if (!string.IsNullOrWhiteSpace(remoteDir))
            {
                EnsureSftpDirectoryExists(client, remoteDir);
            }

            using var fs = File.OpenRead(local);

            ulong totalBytes = (ulong)fs.Length;
            int lastReported = -1;

            Console.WriteLine($"Uploading: {Path.GetFileName(local)}");

            client.UploadFile(
                fs,
                remote,
                uploaded =>
                {
                    int percent = (int)(uploaded * 100 / totalBytes);

                    if (percent >= lastReported + 5)
                    {
                        lastReported = percent;
                        Console.WriteLine($"  {percent}%");
                    }
                });

            Console.WriteLine("  100%");
            Console.WriteLine("Upload complete.");

            client.Disconnect();
        }

        private void DownloadSftp(ProjectServerSettings s, string remote, string local)
        {
            using var client = CreateSftp(s);

            client.Connect();

            var attrs = client.GetAttributes(remote);
            ulong totalBytes = (ulong)attrs.Size;
            int lastReported = -1;

            Console.WriteLine($"Downloading: {Path.GetFileName(remote)}");

            using var fs = File.Create(local);

            client.DownloadFile(
                remote,
                fs,
                downloaded =>
                {
                    int percent = (int)(downloaded * 100 / totalBytes);

                    if (percent >= lastReported + 5)
                    {
                        lastReported = percent;
                        Console.WriteLine($"  {percent}%");
                    }
                });

            Console.WriteLine("  100%");
            Console.WriteLine("Download complete.");

            client.Disconnect();
        }

        // -----------------------------
        // FTP / FTPS
        // -----------------------------

        private FtpClient CreateFtp(ProjectServerSettings s)
        {
            var client = new FtpClient(s.Host, s.Port)
            {
                Credentials = new System.Net.NetworkCredential(
                    s.Username,
                    Decrypt(s.EncryptedPassword)
                )
            };

            if (s.Protocol == TransferProtocol.Ftps)
            {
                client.Config.EncryptionMode = FtpEncryptionMode.Explicit;
                client.Config.ValidateAnyCertificate = true;
            }

            client.Config.DataConnectionType = s.PassiveMode
                ? FtpDataConnectionType.PASV
                : FtpDataConnectionType.PORT;

            return client;
        }

        private bool TestFtp(ProjectServerSettings s)
        {
            try
            {
                using var client = CreateFtp(s);
                client.Connect();

                bool ok = client.IsConnected;

                client.Disconnect();
                return ok;
            }
            catch
            {
                return false;
            }
        }

        private void UploadFtp(ProjectServerSettings s, string local, string remote)
        {
            using var client = CreateFtp(s);

            client.Connect();

            // Ensure remote folder exists
            string? remoteDir = Path.GetDirectoryName(remote)
                ?.Replace("\\", "/");

            if (!string.IsNullOrWhiteSpace(remoteDir))
            {
                EnsureFtpDirectoryExists(client, remoteDir);
            }

            int lastReported = -1;

            Console.WriteLine($"Uploading: {Path.GetFileName(local)}");

            client.UploadFile(
                local,
                remote,
                FtpRemoteExists.Overwrite,
                false,
                FtpVerify.None,
                progress =>
                {
                    int percent = (int)progress.Progress;

                    if (percent >= lastReported + 5)
                    {
                        lastReported = percent;
                        Console.WriteLine($"  {percent}%");
                    }
                });

            Console.WriteLine("Upload complete.");

            client.Disconnect();
        }

        private void DownloadFtp(ProjectServerSettings s, string remote, string local)
        {
            using var client = CreateFtp(s);
            client.Connect();

            int lastReported = -1;

            Console.WriteLine($"Downloading: {Path.GetFileName(remote)}");

            client.DownloadFile(
                local,
                remote,
                FtpLocalExists.Overwrite,
                FtpVerify.None,
                progress =>
                {
                    int percent = (int)progress.Progress;

                    if (percent >= lastReported + 5)
                    {
                        lastReported = percent;
                        Console.WriteLine($"  {percent}%");
                    }
                });

            Console.WriteLine("Download complete.");

            client.Disconnect();
        }

        // -----------------------------
        // Decrypt password
        // -----------------------------

        private string Decrypt(string value)
        {
            return Helper.DecryptPassword(value);
        }
        public void DownloadDirectory(ProjectServerSettings settings, string remoteRoot, string localRoot)
        {
            string[] excludedFolders =
            {
                ".git",
                "@GameLabsStorage",
                "@Logging",
                "BattlEye",
                "CommunityOnlineTools",
                "DataCache",
                "EventManagerLog",
                "PermissionsFramework",
                "WebApiLog",
                "backup",

            };
            string[] excludedExtensions =
            {
                ".log",
                ".adm",
                ".rpt"
            };

            switch (settings.Protocol)
            {
                case TransferProtocol.Sftp:
                    DownloadDirectorySftp(settings, remoteRoot, localRoot, excludedExtensions, excludedFolders);
                    break;

                case TransferProtocol.Ftp:
                case TransferProtocol.Ftps:
                    DownloadDirectoryFtp(settings, remoteRoot, localRoot, excludedExtensions, excludedFolders);
                    break;
            }
        }
        private void DownloadDirectorySftp(ProjectServerSettings s, string remoteRoot, string localRoot, string[] excludedExtensions, string[] excludedFolders)
        {
            using var client = CreateSftp(s);

            client.Connect();

            DownloadSftpRecursive(client, remoteRoot, localRoot, excludedExtensions, excludedFolders);

            client.Disconnect();
        }

        private void DownloadSftpRecursive(SftpClient client, string remotePath, string localPath, string[] excludedExtensions, string[] excludedFolders)
        {
            Directory.CreateDirectory(localPath);

            var items = client.ListDirectory(remotePath);

            foreach (var item in items)
            {
                if (item.Name == "." || item.Name == "..")
                    continue;

                string localFilePath = Path.Combine(localPath, item.Name);
                string name = item.Name;
                if (item.IsDirectory)
                {
                    if (excludedFolders.Contains(
                        name,
                        StringComparer.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                    DownloadSftpRecursive(client, item.FullName, localFilePath, excludedExtensions, excludedFolders);
                }
                else
                {
                    string ext = Path.GetExtension(item.Name);

                    if (excludedExtensions.Any(x =>
                        x.Equals(ext, StringComparison.OrdinalIgnoreCase)))
                    {
                        continue;
                    }

                    Console.WriteLine($"Downloading: {item.FullName}");

                    using var fs = File.Create(localFilePath);

                    client.DownloadFile(item.FullName, fs);
                }
            }
        }
        private void DownloadDirectoryFtp(ProjectServerSettings s, string remoteRoot, string localRoot, string[] excludedExtensions, string[] excludedFolders)
        {
            using var client = CreateFtp(s);

            client.Connect();

            DownloadFtpRecursive(client, remoteRoot, localRoot, excludedExtensions, excludedFolders);

            client.Disconnect();
        }

        private void DownloadFtpRecursive(FtpClient client, string remotePath, string localPath, string[] excludedExtensions, string[] excludedFolders)
        {
            Directory.CreateDirectory(localPath);

            var listing = client.GetListing(remotePath);

            foreach (var item in listing)
            {
                string localFilePath = Path.Combine(localPath, item.Name);
                string name = item.Name;
                if (item.Type == FtpObjectType.Directory)
                {
                    if (excludedFolders.Contains(
                       name,
                       StringComparer.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                    DownloadFtpRecursive(client, item.FullName, localFilePath, excludedExtensions, excludedFolders);
                }
                else if (item.Type == FtpObjectType.File)
                {
                    string ext = Path.GetExtension(item.Name);

                    if (excludedExtensions.Any(x =>
                        x.Equals(ext, StringComparison.OrdinalIgnoreCase)))
                    {
                        continue;
                    }

                    Console.WriteLine($"Downloading: {item.FullName}");

                    client.DownloadFile(localFilePath, item.FullName);
                }
            }
        }
        public void Delete(ProjectServerSettings settings, string remotePath)
        {
            switch (settings.Protocol)
            {
                case TransferProtocol.Sftp:
                    DeleteSftp(settings, remotePath);
                    break;

                case TransferProtocol.Ftp:
                case TransferProtocol.Ftps:
                    DeleteFtp(settings, remotePath);
                    break;
            }
        }
        private void DeleteSftp(ProjectServerSettings s, string remote)
        {
            using var client = CreateSftp(s);

            client.Connect();

            if (client.Exists(remote))
                client.DeleteFile(remote);

            client.Disconnect();
        }
        private void DeleteFtp(ProjectServerSettings s, string remote)
        {
            using var client = CreateFtp(s);

            client.Connect();

            if (client.FileExists(remote))
                client.DeleteFile(remote);

            client.Disconnect();
        }
        private void EnsureFtpDirectoryExists(FtpClient client, string remotePath)
        {
            string? dir = Path.GetDirectoryName(remotePath);

            if (string.IsNullOrWhiteSpace(dir))
                return;

            dir = dir.Replace("\\", "/");

            client.CreateDirectory(dir, true);
        }
        private void EnsureSftpDirectoryExists(SftpClient client, string path)
        {
            path = path.Replace("\\", "/");

            string[] parts = path.Split(
                '/',
                StringSplitOptions.RemoveEmptyEntries);

            string current = "";

            foreach (string part in parts)
            {
                current += "/" + part;

                if (!client.Exists(current))
                {
                    client.CreateDirectory(current);
                }
            }
        }
    }
}
