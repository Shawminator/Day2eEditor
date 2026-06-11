using Day2eEditor;
using Renci.SshNet;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

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
                    string relativePath = Path.GetRelativePath(
                            project.ProjectRoot,
                            item.LocalPath);

                    var listItem = new ListViewItem(item.Action.ToString())
                    {
                        Tag = item,
                        Checked = false
                    };

                    listItem.SubItems.Add(Path.GetFileName(item.LocalPath));
                    listItem.SubItems.Add(relativePath);
                    listItem.SubItems.Add(item.LocalModified.ToString("yyyy-MM-dd HH:mm"));
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
            var ftp = AppServices.GetRequired<FileTransferManager>();
            var project = _projectManager.CurrentProject;


            foreach (ListViewItem row in pendingListView.Items)
            {
                if (row.Tag is PendingUploadFile pending)
                {
                    try
                    {
                        string relativePath = Path.GetRelativePath(
                            project.ProjectRoot,
                            pending.FullPath);

                        // Convert windows path to remote unix path
                        relativePath = relativePath.Replace("\\", "/");

                        string remotePath =
                            $"{project.ServerSettings.RootPath.TrimEnd('/')}/{relativePath}";

                        switch (pending.Action)
                        {
                            case PendingServerAction.Upload:

                                ftp.Upload(
                                    project.ServerSettings,
                                    pending.FullPath,
                                    remotePath);
                                Console.WriteLine($"UPLOAD OK: {pending.FileName}");
                                break;

                            case PendingServerAction.Remove:

                                ftp.Delete(
                                    project.ServerSettings,
                                    remotePath);
                                Console.WriteLine($"DELETE OK: {pending.FileName}");
                                break;
                        }

                        // Remove from tracker after success
                        _uploadTrackerService.MarkUploaded(
                            project.ProjectName,
                            pending.FullPath);

                        row.Remove();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[FAIL] {pending.Action}: {pending.FileName} - {ex.Message}");
                        MessageBox.Show(
                            $"Failed:\n{pending.FileName}\n\n{ex.Message}",
                            "Transfer Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else if (row.Tag is SyncItem syncitem)
                {
                    try
                    {
                        string relativePath = Path.GetRelativePath(
                            project.ProjectRoot,
                            syncitem.LocalPath);

                        // Convert windows path to remote unix path
                        relativePath = relativePath.Replace("\\", "/");

                        string remotePath =
                            $"{project.ServerSettings.RootPath.TrimEnd('/')}/{relativePath}";

                        switch (syncitem.Action)
                        {
                            case PendingServerAction.Upload:

                                ftp.Upload(
                                    project.ServerSettings,
                                    syncitem.LocalPath,
                                    remotePath);
                                Console.WriteLine($"UPLOAD OK: {Path.GetFileName(syncitem.LocalPath)}");
                                break;

                            case PendingServerAction.Remove:

                                ftp.Delete(
                                    project.ServerSettings,
                                    remotePath);
                                Console.WriteLine($"DELETE OK: {Path.GetFileName(syncitem.LocalPath)}");
                                break;
                            case PendingServerAction.Download:
                                ftp.Download(
                                    project.ServerSettings,
                                    syncitem.RemotePath,
                                    syncitem.LocalPath);
                                Console.WriteLine($"DOWNLOADED OK: {Path.GetFileName(syncitem.LocalPath)}");
                                break;
                        }

                        // Remove from tracker after success
                        _uploadTrackerService.MarkUploaded(
                            project.ProjectName,
                            syncitem.LocalPath);

                        row.Remove();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[FAIL] {syncitem.Action}: {Path.GetFileName(syncitem.LocalPath)} - {ex.Message}");
                        MessageBox.Show(
                            $"Failed:\n{Path.GetFileName(syncitem.LocalPath)}\n\n{ex.Message}",
                            "Transfer Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void uploadCheckedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ftp = AppServices.GetRequired<FileTransferManager>();
            var project = _projectManager.CurrentProject;

            if (pendingListView.CheckedItems.Count == 0)
            {
                MessageBox.Show(
                    "No files selected.",
                    "Upload",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            foreach (ListViewItem row in pendingListView.CheckedItems)
            {
                if (row.Tag is PendingUploadFile pending)
                {
                    try
                    {
                        string relativePath = Path.GetRelativePath(
                            project.ProjectRoot,
                            pending.FullPath);

                        // Convert windows path to remote unix path
                        relativePath = relativePath.Replace("\\", "/");

                        string remotePath =
                            $"{project.ServerSettings.RootPath.TrimEnd('/')}/{relativePath}";

                        switch (pending.Action)
                        {
                            case PendingServerAction.Upload:

                                ftp.Upload(
                                    project.ServerSettings,
                                    pending.FullPath,
                                    remotePath);
                                Console.WriteLine($"UPLOAD OK: {pending.FileName}");
                                break;

                            case PendingServerAction.Remove:

                                ftp.Delete(
                                    project.ServerSettings,
                                    remotePath);
                                Console.WriteLine($"DELETE OK: {pending.FileName}");
                                break;
                        }

                        // Remove from tracker after success
                        _uploadTrackerService.MarkUploaded(
                            project.ProjectName,
                            pending.FullPath);

                        row.Remove();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[FAIL] {pending.Action}: {pending.FileName} - {ex.Message}");
                        MessageBox.Show(
                            $"Failed:\n{pending.FileName}\n\n{ex.Message}",
                            "Transfer Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                else if (row.Tag is SyncItem syncitem)
                {
                    try
                    {
                        string relativePath = syncitem.RelativePath;

                        // Convert windows path to remote unix path
                        relativePath = relativePath.Replace("\\", "/");

                        string remotePath = syncitem.RemotePath;

                        switch (syncitem.Action)
                        {
                            case PendingServerAction.Upload:

                                ftp.Upload(
                                    project.ServerSettings,
                                    syncitem.LocalPath,
                                    remotePath);
                                Console.WriteLine($"UPLOAD OK: {Path.GetFileName(syncitem.LocalPath)}");
                                break;

                            case PendingServerAction.Remove:

                                ftp.Delete(
                                    project.ServerSettings,
                                    remotePath);
                                Console.WriteLine($"DELETE OK: {Path.GetFileName(syncitem.LocalPath)}");
                                break;
                        }

                        // Remove from tracker after success
                        _uploadTrackerService.MarkUploaded(
                            project.ProjectName,
                            syncitem.LocalPath);

                        row.Remove();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[FAIL] {syncitem.Action}: {Path.GetFileName(syncitem.LocalPath)} - {ex.Message}");
                        MessageBox.Show(
                            $"Failed:\n{Path.GetFileName(syncitem.LocalPath)}\n\n{ex.Message}",
                            "Transfer Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void TestConnectionButton_Click(object sender, EventArgs e)
        {

            var ftp = AppServices.GetRequired<FileTransferManager>();

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

            var ftp = AppServices.GetRequired<FileTransferManager>();
            string profileDir = _projectManager.CurrentProject.ProfileName;
            string mpMissionDirectory = _projectManager.CurrentProject.MpMissionPath;

            string mpMissionPath = Path.GetFileName(mpMissionDirectory);
            string profile = Path.GetFileName(profileDir);

            string localProfilePath = Path.Combine(_projectManager.CurrentProject.ProjectRoot, profile);
            string localMissionPath = Path.Combine(_projectManager.CurrentProject.ProjectRoot, "mpmissions", mpMissionPath);

            Helper.ClearDirectory(localProfilePath, "DumpAttatch.json", "map_output.txt");
            Helper.ClearDirectory(localMissionPath);

            string remoteProfilePath = Path.Combine(_projectManager.CurrentProject.ServerSettings.RootPath, profile);
            string remoteMissionPath = Path.Combine(_projectManager.CurrentProject.ServerSettings.RootPath, "mpmissions", mpMissionPath);

            ftp.DownloadDirectory(_projectManager.CurrentProject.ServerSettings, remoteProfilePath, localProfilePath);

            ftp.DownloadDirectory(_projectManager.CurrentProject.ServerSettings, remoteMissionPath, localMissionPath);
            AppServices.GetRequired<EconomyManager>().SetProject(_projectManager.CurrentProject);
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

            var ftp = AppServices.GetRequired<FileTransferManager>();
            string profileDir = _projectManager.CurrentProject.ProfileName;
            string profile = Path.GetFileName(profileDir);
            string localProfilePath = Path.Combine(_projectManager.CurrentProject.ProjectRoot, profile);
            string remoteProfilePath = Path.Combine(_projectManager.CurrentProject.ServerSettings.RootPath, profile);
            string localmapoutputpath = Path.Combine(localProfilePath, "map_output.txt");
            string remotemapoutputpath = Path.Combine(remoteProfilePath, "map_output.txt");

            ftp.Download(_projectManager.CurrentProject.ServerSettings, remotemapoutputpath, localmapoutputpath);
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

            var ftp = AppServices.GetRequired<FileTransferManager>();
            string profileDir = _projectManager.CurrentProject.ProfileName;
            string profile = Path.GetFileName(profileDir);
            string localProfilePath = Path.Combine(_projectManager.CurrentProject.ProjectRoot, profile);
            string remoteProfilePath = Path.Combine(_projectManager.CurrentProject.ServerSettings.RootPath, profile);
            string localmapoutputpath = Path.Combine(localProfilePath, "DumpAttatch.json");
            string remotemapoutputpath = Path.Combine(remoteProfilePath, "DumpAttatch.json");

            ftp.Download(_projectManager.CurrentProject.ServerSettings, remotemapoutputpath, localmapoutputpath);
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
            var ftp = AppServices.GetRequired<FileTransferManager>();
            string profileDir = _projectManager.CurrentProject.ProfileName;
            string mpMissionDirectory = _projectManager.CurrentProject.MpMissionPath;

            string mpMissionPath = Path.GetFileName(mpMissionDirectory);
            string profile = Path.GetFileName(profileDir);

            string localProfilePath = Path.Combine(_projectManager.CurrentProject.ProjectRoot, profile);
            string localMissionPath = Path.Combine(_projectManager.CurrentProject.ProjectRoot, "mpmissions", mpMissionPath);

            string remoteProfilePath = Path.Combine(_projectManager.CurrentProject.ServerSettings.RootPath, profile);
            string remoteMissionPath = Path.Combine(_projectManager.CurrentProject.ServerSettings.RootPath, "mpmissions", mpMissionPath);

            List<SyncItem> filechanges = ftp.CheckForChanges(_projectManager.CurrentProject.ServerSettings, remoteProfilePath, localProfilePath);
            filechanges.AddRange(ftp.CheckForChanges(_projectManager.CurrentProject.ServerSettings, remoteMissionPath, localMissionPath));

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
