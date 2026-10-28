using Day2eEditor;
using System.Windows.Forms;

namespace DayZFileManagerPlugin
{
    public partial class DayZFileManagerForm : Form
    {
        private IPluginForm _plugin;
        private readonly ProjectManager _projectManager;
        private readonly UploadTrackerService _uploadTrackerService;


        private static readonly Color UploadRowColor =
            Color.FromArgb(225, 245, 234);   // Soft green

        private static readonly Color RemoveRowColor =
            Color.FromArgb(252, 228, 228);   // Soft red/pink


        public DayZFileManagerForm(IPluginForm plugin)
        {
            InitializeComponent();
            _plugin = plugin;
            _projectManager = AppServices.GetRequired<ProjectManager>();
            _uploadTrackerService = AppServices.GetRequired<UploadTrackerService>();
            menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            menuStrip1.Renderer =
                new ToolStripProfessionalRenderer(new DarkColorTable());

            menuStrip1.ForeColor = SystemColors.Control;
            ConfigurePendingListView();
        }
        private void ConfigurePendingListView()
        {
            pendingListView.View = View.Details;
            pendingListView.CheckBoxes = true;
            pendingListView.FullRowSelect = true;
            pendingListView.HideSelection = false;
            pendingListView.MultiSelect = true;
            pendingListView.GridLines = false;

            pendingListView.Columns.Clear();
            pendingListView.Columns.Add("Action", 80);
            pendingListView.Columns.Add("File", 180);
            pendingListView.Columns.Add("Relative Path", 350);
            pendingListView.Columns.Add("Last Saved", 150);
        }
        private void DayZFileManagerForm_Load(object sender, EventArgs e)
        {
            PopulatePendingListView();
        }
        public void PopulatePendingListView(List<SyncItem> syncItems = null)
        {
            pendingListView.BeginUpdate();
            pendingListView.Items.Clear();

            var project = _projectManager.CurrentProject;
            var pendingFiles = _uploadTrackerService.GetFilesForProject(project.ProjectName);

            

            if (syncItems != null)
            {
                foreach (var item in syncItems)
                {
                    string relativePath = string.IsNullOrEmpty(item.LocalPath)
                        ? item.RelativePath
                        : Path.GetRelativePath(project.ProjectRoot, item.LocalPath);

                    var listItem = new ListViewItem(item.Action.ToString())
                    {
                        Tag = item,
                        Checked = false
                    };

                    // Server-only files have no local timestamp, so show the server's
                    var modified = item.Action == PendingServerAction.Download ? item.RemoteModified : item.LocalModified;

                    listItem.SubItems.Add(Path.GetFileName(item.RelativePath));
                    listItem.SubItems.Add(relativePath);
                    listItem.SubItems.Add(modified.ToLocalTime().ToString("yyyy-MM-dd HH:mm"));
                    listItem.ForeColor = item.Action == PendingServerAction.Upload ? UploadRowColor : RemoveRowColor;
                    pendingListView.Items.Add(listItem);
                }
            }
            else
            {
                if (pendingFiles == null || pendingFiles.Count == 0)
                {
                    pendingListView.EndUpdate();
                    return;
                }
                foreach (var item in pendingFiles)
                {
                    string relativePath;

                    try
                    {
                        relativePath = Path.GetRelativePath(
                            project.ProjectRoot,
                            item.FullPath);
                    }
                    catch
                    {
                        relativePath = item.FullPath;
                    }
                    var listItem = new ListViewItem(item.Action.ToString())
                    {
                        Tag = item,
                        Checked = false
                    };

                    listItem.SubItems.Add(item.FileName);
                    listItem.SubItems.Add(relativePath);
                    listItem.SubItems.Add(item.LastSavedAt.ToString("yyyy-MM-dd HH:mm"));
                    listItem.ForeColor = item.Action == PendingServerAction.Upload ? UploadRowColor : RemoveRowColor;

                    pendingListView.Items.Add(listItem);

                }
            }

            pendingListView.EndUpdate();
        }
        private void uploadAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessRows(pendingListView.Items.Cast<ListViewItem>().ToList());
        }
        private void uploadCheckedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pendingListView.CheckedItems.Count == 0)
            {
                MessageBox.Show(
                    "No files selected.",
                    "Upload",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            ProcessRows(pendingListView.CheckedItems.Cast<ListViewItem>().ToList());
        }
        // Upload all and Upload checked share this, so both handle every action the same way
        private void ProcessRows(List<ListViewItem> rows)
        {
            var ftp = AppServices.GetRequired<IFileTransferService>();
            var project = _projectManager.CurrentProject;

            foreach (ListViewItem row in rows)
            {
                string localPath;
                string remotePath;
                PendingServerAction action;
                string displayName;

                if (row.Tag is PendingUploadFile pending)
                {
                    localPath = pending.FullPath;
                    remotePath = GetRemotePathFor(project, localPath);
                    action = pending.Action;
                    displayName = pending.FileName;
                }
                else if (row.Tag is SyncItem syncitem)
                {
                    localPath = syncitem.LocalPath;
                    remotePath = !string.IsNullOrEmpty(syncitem.RemotePath)
                        ? syncitem.RemotePath
                        : GetRemotePathFor(project, localPath);
                    action = syncitem.Action;
                    displayName = Path.GetFileName(syncitem.RelativePath);
                }
                else
                {
                    continue;
                }

                try
                {
                    switch (action)
                    {
                        case PendingServerAction.Upload:
                            ftp.Upload(project.ServerSettings, localPath, remotePath);
                            Console.WriteLine($"UPLOAD OK: {displayName}");
                            break;

                        case PendingServerAction.Remove:
                            ftp.Delete(project.ServerSettings, remotePath);
                            Console.WriteLine($"DELETE OK: {displayName}");
                            break;

                        case PendingServerAction.Download:
                            ftp.Download(project.ServerSettings, remotePath, localPath);
                            Console.WriteLine($"DOWNLOADED OK: {displayName}");
                            break;
                    }

                    // Remove from tracker after success
                    _uploadTrackerService.MarkUploaded(project.ProjectName, localPath);

                    row.Remove();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[FAIL] {action}: {displayName} - {ex.Message}");
                    MessageBox.Show(
                        $"Failed:\n{displayName}\n\n{ex.Message}",
                        "Transfer Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
        private static string GetRemotePathFor(Project project, string localPath)
        {
            string relativePath = Path.GetRelativePath(project.ProjectRoot, localPath);
            return CombineRemote(project.ServerSettings.RootPath, relativePath);
        }
        // Remote servers (Linux especially) need '/' separators, which Path.Combine doesn't give on Windows
        private static string CombineRemote(string root, params string[] parts)
        {
            string path = (root ?? string.Empty).Replace("\\", "/").TrimEnd('/');
            foreach (string part in parts)
            {
                string cleaned = part.Replace("\\", "/").Trim('/');
                if (cleaned.Length > 0)
                    path += "/" + cleaned;
            }
            return path;
        }
        private void ShowTransferError(string action, Exception ex)
        {
            Console.WriteLine($"[FAIL] {action}: {ex.Message}");
            MessageBox.Show(
                $"{action} failed:\n\n{ex.Message}",
                "File Transfer",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        private void TestConnectionButton_Click(object sender, EventArgs e)
        {

            var ftp = AppServices.GetRequired<IFileTransferService>();

            bool ok = ftp.TestConnection(_projectManager.CurrentProject.ServerSettings);
            if (ok)
            {
                MessageBox.Show(
                    "Connection successful.",
                    "File Transfer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Connection failed.",
                    "File Transfer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void SelectRootDieButton_Click(object sender, EventArgs e)
        {
            NewProjectFTP ftpproject = new NewProjectFTP();
            DialogResult result = ftpproject.ShowDialog();
            if (result == DialogResult.OK)
            {
                string serverroot = ftpproject.MpMissionDirectory;
                _projectManager.CurrentProject.ServerSettings.RootPath = serverroot;
                _projectManager.Save();
            }
        }
        private void DownloadAllButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "This will remove ALL local files in the selected profile and mission folders.\n\nDo you want to continue?",
                "Confirm Download",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            var ftp = AppServices.GetRequired<IFileTransferService>();
            string profileDir = _projectManager.CurrentProject.ProfileName;
            string mpMissionDirectory = _projectManager.CurrentProject.MpMissionPath;

            string mpMissionPath = Path.GetFileName(mpMissionDirectory);
            string profile = Path.GetFileName(profileDir);

            string localProfilePath = Path.Combine(_projectManager.CurrentProject.ProjectRoot, profile);
            string localMissionPath = Path.Combine(_projectManager.CurrentProject.ProjectRoot, "mpmissions", mpMissionPath);

            string remoteProfilePath = CombineRemote(_projectManager.CurrentProject.ServerSettings.RootPath, profile);
            string remoteMissionPath = CombineRemote(_projectManager.CurrentProject.ServerSettings.RootPath, "mpmissions", mpMissionPath);

            // Download into temporary folders first and only replace the local files once both
            // downloads succeeded, so a failed transfer can't leave the project empty
            string tempProfilePath = localProfilePath + ".download";
            string tempMissionPath = localMissionPath + ".download";

            Cursor.Current = Cursors.WaitCursor;
            try
            {
                DeleteIfExists(tempProfilePath);
                DeleteIfExists(tempMissionPath);

                ftp.DownloadDirectory(_projectManager.CurrentProject.ServerSettings, remoteProfilePath, tempProfilePath);
                ftp.DownloadDirectory(_projectManager.CurrentProject.ServerSettings, remoteMissionPath, tempMissionPath);

                Helper.ClearDirectory(localProfilePath, "DumpAttatch.json", "map_output.txt");
                Helper.ClearDirectory(localMissionPath);

                MoveDirectoryContents(tempProfilePath, localProfilePath);
                MoveDirectoryContents(tempMissionPath, localMissionPath);
            }
            catch (Exception ex)
            {
                ShowTransferError("Download all", ex);
                return;
            }
            finally
            {
                DeleteIfExists(tempProfilePath);
                DeleteIfExists(tempMissionPath);
                Cursor.Current = Cursors.Default;
            }

            AppServices.GetRequired<EconomyManager>().SetProject(_projectManager.CurrentProject);
        }
        private static void DeleteIfExists(string directory)
        {
            if (Directory.Exists(directory))
                Directory.Delete(directory, true);
        }
        private static void MoveDirectoryContents(string source, string destination)
        {
            Directory.CreateDirectory(destination);

            foreach (string file in Directory.GetFiles(source))
                File.Move(file, Path.Combine(destination, Path.GetFileName(file)), overwrite: true);

            foreach (string dir in Directory.GetDirectories(source))
                MoveDirectoryContents(dir, Path.Combine(destination, Path.GetFileName(dir)));
        }
        private void pendingListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            bool anyChecked = pendingListView.CheckedItems.Count > 0;

            uploadAllToolStripMenuItem.Enabled = !anyChecked;
        }
        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool noitems = pendingListView.Items.Count == 0;
            if (noitems)
            {
                uploadAllToolStripMenuItem.Enabled = !noitems;
                uploadCheckedToolStripMenuItem.Enabled = !noitems;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "This will Download and Overwrite any Map_out.txt thats it currently in the profile dir.\n\nDo you want to continue?",
                "Confirm Download",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            var ftp = AppServices.GetRequired<IFileTransferService>();
            string profileDir = _projectManager.CurrentProject.ProfileName;
            string profile = Path.GetFileName(profileDir);
            string localProfilePath = Path.Combine(_projectManager.CurrentProject.ProjectRoot, profile);
            string localmapoutputpath = Path.Combine(localProfilePath, "map_output.txt");
            string remotemapoutputpath = CombineRemote(_projectManager.CurrentProject.ServerSettings.RootPath, profile, "map_output.txt");

            try
            {
                ftp.Download(_projectManager.CurrentProject.ServerSettings, remotemapoutputpath, localmapoutputpath);
            }
            catch (Exception ex)
            {
                ShowTransferError("Downloading map_output.txt", ex);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "This will overwrite any DumpAttch.json thats currently in the profile dir.\n\nDo you want to continue?",
                "Confirm Download",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            var ftp = AppServices.GetRequired<IFileTransferService>();
            string profileDir = _projectManager.CurrentProject.ProfileName;
            string profile = Path.GetFileName(profileDir);
            string localProfilePath = Path.Combine(_projectManager.CurrentProject.ProjectRoot, profile);
            string localmapoutputpath = Path.Combine(localProfilePath, "DumpAttatch.json");
            string remotemapoutputpath = CombineRemote(_projectManager.CurrentProject.ServerSettings.RootPath, profile, "DumpAttatch.json");

            try
            {
                ftp.Download(_projectManager.CurrentProject.ServerSettings, remotemapoutputpath, localmapoutputpath);
            }
            catch (Exception ex)
            {
                ShowTransferError("Downloading DumpAttatch.json", ex);
            }
        }
        private void pendingListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            var project = _projectManager.CurrentProject;
            foreach (ListViewItem row in pendingListView.Items)
            {
                if (row.Tag is not PendingUploadFile pending)
                    continue;

                // Remove from tracker after success
                _uploadTrackerService.MarkUploaded(
                     project.ProjectName,
                     pending.FullPath);

                row.Remove();
            }
        }
        private void connectionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void checkForChangesOnServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ftp = AppServices.GetRequired<IFileTransferService>();
            string profileDir = _projectManager.CurrentProject.ProfileName;
            string mpMissionDirectory = _projectManager.CurrentProject.MpMissionPath;

            string mpMissionPath = Path.GetFileName(mpMissionDirectory);
            string profile = Path.GetFileName(profileDir);

            string localProfilePath = Path.Combine(_projectManager.CurrentProject.ProjectRoot, profile);
            string localMissionPath = Path.Combine(_projectManager.CurrentProject.ProjectRoot, "mpmissions", mpMissionPath);

            string remoteProfilePath = CombineRemote(_projectManager.CurrentProject.ServerSettings.RootPath, profile);
            string remoteMissionPath = CombineRemote(_projectManager.CurrentProject.ServerSettings.RootPath, "mpmissions", mpMissionPath);

            List<SyncItem> filechanges;
            try
            {
                filechanges = ftp.CheckForChanges(_projectManager.CurrentProject.ServerSettings, remoteProfilePath, localProfilePath);
                filechanges.AddRange(ftp.CheckForChanges(_projectManager.CurrentProject.ServerSettings, remoteMissionPath, localMissionPath));
            }
            catch (Exception ex)
            {
                ShowTransferError("Checking for changes", ex);
                return;
            }

            PopulatePendingListView(filechanges);

        }
    }
    public class DarkColorTable : ProfessionalColorTable
    {
        public override Color MenuStripGradientBegin => Color.FromArgb(30, 30, 30);
        public override Color MenuStripGradientEnd => Color.FromArgb(30, 30, 30);
        public override Color MenuItemPressedGradientBegin => Color.RoyalBlue;
        public override Color MenuItemPressedGradientEnd => Color.RoyalBlue;
        public override Color MenuItemSelected => Color.FromArgb(60, 63, 65);
        public override Color MenuItemBorder => Color.FromArgb(60, 63, 65);

        public override Color ToolStripDropDownBackground => Color.FromArgb(40, 40, 40);
    }

    [PluginInfo("DayZ File Manager", "DayZFileManagerPlugin", "DayZFileManagerPlugin.DayZFileManager.png")]
    public class PluginDayZFileManager : IPluginForm, IDisposable
    {
        private bool disposed = false;

        public string pluginIdentifier => "DayZFileManagerPlugin";
        public string pluginName => "DayZ File Manager";

        public Form GetForm()
        {
            return new DayZFileManagerForm(this);
        }
        public override string ToString()
        {
            return pluginName;
        }

        public void Dispose()
        {
            if (!disposed)
            {
                // Dispose any resources (e.g., file handles, etc.)
                disposed = true;
            }
        }
    }
}
