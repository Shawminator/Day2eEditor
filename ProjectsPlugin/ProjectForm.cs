using Day2eEditor;
using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ProjectsPlugin
{

    public partial class ProjectForm : Form
    {
        private IPluginForm _plugin;
        private Manifest _manifest;
        private ProjectManager _ProjectManager;
        private ListViewItem _clickedItem;
        private string appDirectory;
        public ProjectForm(IPluginForm plugin)
        {
            InitializeComponent();
            _plugin = plugin;
            _manifest = AppServices.GetRequired<Manifest>();
            _ProjectManager = AppServices.GetRequired<ProjectManager>();
            AutoScaleMode = AutoScaleMode.Dpi;
        }

        private void ProjectForm_Load(object sender, EventArgs e)
        {
            appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (_manifest.Plugins != null)
            {
                foreach (var plugin in _manifest.Plugins)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = plugin.Name;
                    item.Tag = plugin;
                    if (AppServices.GetRequired<List<PluginEntry>>().FirstOrDefault(x => x.Identifier == plugin.Name) != null)
                        item.SubItems.Add("Installed");
                    PluginLB.Items.Add(item);
                }
            }
            if (_manifest.MapAddons != null)
            {
                foreach (var mapaddon in _manifest.MapAddons)
                {
                    string imagePath = Path.Combine(appDirectory, "MapAddons", mapaddon.MapInfo.MapPng);
                    string XYZPath = Path.Combine(appDirectory, "MapAddons", mapaddon.MapInfo.MapXYZ);
                    ListViewItem item = new ListViewItem();
                    item.Text = mapaddon.Name;
                    item.Tag = mapaddon;
                    if (File.Exists(imagePath) && File.Exists(XYZPath))
                        item.SubItems.Add("Installed");
                    MapAddonsLB.Items.Add(item);
                }
            }

            ProtocolCB.DataSource = new[]
            {
                new EnumItem<TransferProtocol> { Value = TransferProtocol.Ftp,  Text = "FTP" },
                new EnumItem<TransferProtocol> { Value = TransferProtocol.Ftps, Text = "FTPS (FTP over TLS)" },
                new EnumItem<TransferProtocol> { Value = TransferProtocol.Sftp, Text = "SFTP (SSH)" }
            };

            ProtocolCB.DisplayMember = "Text";
            ProtocolCB.ValueMember = "Value";

            ProjectTypeComboBox.SelectedIndex = 0;

            listBoxProjects.DataSource = _ProjectManager.Projects;
            listBoxProjects.Invalidate();
            listBoxProjects.SelectedItem = _ProjectManager.CurrentProject;
        }

        private void ProjectForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_plugin is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        private async void downloadAndInstallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = _clickedItem;
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();

            // async void: anything thrown here would crash the editor, so report failures instead
            try
            {
                if (item?.Tag is PluginInfo plugin)
                {
                    await AppServices.GetRequired<IPluginInstaller>().DownloadNewPluginAsync(plugin);
                    this.Cursor = Cursors.Default;
                    MessageBox.Show($"{plugin.Name} Downloaded.", "Plugin Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MarkInstalled(item);
                }
                else if (item?.Tag is MapAddonInfo mapaddoninfo)
                {
                    await AppServices.GetRequired<IPluginInstaller>().DownloadMapAddonAsync(mapaddoninfo);
                    this.Cursor = Cursors.Default;
                    MessageBox.Show($"{mapaddoninfo.Name} Downloaded.", "MapAddon Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MarkInstalled(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Download failed:\n\n{ex.Message}", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        private static void MarkInstalled(ListViewItem item)
        {
            if (item.SubItems.Count < 2)
                item.SubItems.Add("Installed");
        }
        private static void MarkNotInstalled(ListViewItem item)
        {
            if (item.SubItems.Count > 1)
                item.SubItems.RemoveAt(1);
        }
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_clickedItem?.Tag is PluginInfo plugin)
            {
                var result = MessageBox.Show($"Are you sure you want to remove {plugin.Name}?\nIt will be deleted on next application start.",
                                             "Confirm Removal",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    MarkNotInstalled(_clickedItem);

                    string pluginPath = Path.Combine("Plugins", $"{plugin.Name}.dll");
                    string deleteMarkerPath = pluginPath + ".delete";

                    try
                    {
                        File.Move(pluginPath, deleteMarkerPath);
                        MessageBox.Show($"{plugin.Name} marked for deletion. It will be removed on next start.", "Marked", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to mark plugin for deletion: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    _clickedItem = null;
                }
            }
            else if (_clickedItem?.Tag is MapAddonInfo mapaddoninfo)
            {
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string imagePath = Path.Combine(appDirectory, "MapAddons", mapaddoninfo.MapInfo.MapPng);
                string xyzPath = Path.Combine(appDirectory, "MapAddons", mapaddoninfo.MapInfo.MapXYZ);

                bool mapisused = false;
                foreach (Project p in _ProjectManager.Projects)
                {
                    if (p.MapPath == mapaddoninfo.MapInfo.MapPng) { mapisused = true; }
                }

                if (mapisused)
                {
                    var result = MessageBox.Show($"{mapaddoninfo.Name} is used by one of your projects.\nAre you sure you want to remove it?",
                                                 "Confirm Removal",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }
                File.Delete(imagePath);
                File.Delete(xyzPath);
                MarkNotInstalled(_clickedItem);
            }
        }
        private void PluginLB_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            if (PluginLB.Visible)
            {
                var item = PluginLB.GetItemAt(e.X, e.Y);

                if (item == null)
                    return;

                _clickedItem = item;

                if (_clickedItem.Text == "ProjectsPlugin" ||
                    _clickedItem.Text == "EconomyPlugin" ||
                    _clickedItem.Text == "DayZFileManagerPlugin")
                {
                    return;
                }

                PluginCM.Show(PluginLB, e.Location);
            }
            else if (MapAddonsLB.Visible)
            {
                var item = MapAddonsLB.GetItemAt(e.X, e.Y);

                if (item == null)
                    return;

                _clickedItem = item;

                PluginCM.Show(MapAddonsLB, e.Location);
            }
        }
        private void ProjectTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string projecttype = ProjectTypeComboBox.GetItemText(ProjectTypeComboBox.SelectedItem);         
            if (projecttype == "Create Blank")
            {
                ProjectNameLabel.Visible = true;
                ProjectNameTB.Visible = true;

                SelectProjectFolderlabel.Visible = true;
                SelectProjectFolderlabel.Text = "Select Project Folder";
                ProjectFolderTB.Visible = true;
                ProjectFolderTB.ReadOnly = true;
                ProjectFolderTB.Text = "";
                SelectProjectFolderbutton.Visible = true;

                ProfileFolderNamelabel.Visible = true;
                ProfileFolderNamelabel.Text = "Profile Folder Name";
                ProjectProfileTB.Visible = true;
                ProjectProfileTB.Text = "Profiles";
                ProjectProfileTB.ReadOnly = false;
                selectProfilefolderNamebutton.Visible = false;

                MissionFoldertoUselabel.Visible = true;
                MissionFoldertoUselabel.Text = "Mission Folder to use";
                ProjectMissionFolderTB.Visible = true;
                ProjectMissionFolderTB.Text = "dayzOffline.chernarusplus";
                ProjectMissionFolderTB.ReadOnly = false;
                MissionFoldertoUsebutton.Visible = false;
            }
            else if (projecttype == "Create Project to Existing Project Files")
            {
                ProjectNameLabel.Visible = true;
                ProjectNameTB.Visible = true;

                SelectProjectFolderlabel.Visible = true;
                SelectProjectFolderlabel.Text = "Project Folder";
                ProjectFolderTB.Visible = true;
                ProjectFolderTB.ReadOnly = true;
                ProjectFolderTB.Text = "Will Be Auto populated....";
                SelectProjectFolderbutton.Visible = false;

                ProfileFolderNamelabel.Visible = true;
                ProfileFolderNamelabel.Text = "Profile Path";
                ProjectProfileTB.Visible = true;
                ProjectProfileTB.Text = " <Full Path to Profiles Required> Select using the button to my right";
                ProjectProfileTB.ReadOnly = true;
                selectProfilefolderNamebutton.Visible = true;

                MissionFoldertoUselabel.Visible = true;
                MissionFoldertoUselabel.Text = "Mission Path";
                ProjectMissionFolderTB.Visible = true;
                ProjectMissionFolderTB.Text = " <Full Path to Missions folder Required> Select using the button to my right";
                ProjectMissionFolderTB.ReadOnly = true;
                MissionFoldertoUsebutton.Visible = true;

            }
        }
        private void CreateProjectbutton_Click(object sender, EventArgs e)
        {
            string projecttype = ProjectTypeComboBox.GetItemText(ProjectTypeComboBox.SelectedItem);
            if (projecttype == "Create Blank")
            {
                string ProjectFolder = ProjectFolderTB.Text;
                string ProjectName = ProjectNameTB.Text;
                if (ProjectFolder == "" || ProjectName == "")
                {
                    MessageBox.Show("Please select both a Project name and a directory where to save it.");
                    return;
                }

                if (ProjectProfileTB.Text == "" || ProjectMissionFolderTB.Text == "")
                {
                    MessageBox.Show("Please select both a Profile name and an exact mpmission name.");
                    return;
                }
                Directory.CreateDirectory(Path.Combine(ProjectFolder, ProjectName));

                string missionsfolder = ProjectMissionFolderTB.Text;
                string profilefolder = ProjectProfileTB.Text;
                string mpmissionpath = Path.GetFileName(missionsfolder);
                string PmissionFolder = Path.Combine(ProjectFolder, ProjectName, "mpmissions", Path.GetFileName(missionsfolder));
                string Pprofilefolder = Path.Combine(ProjectFolder, ProjectName, profilefolder);

                Directory.CreateDirectory(PmissionFolder);
                Directory.CreateDirectory(Pprofilefolder);

                Project project = new Project();
                project.AddNames(ProjectName);
                project.MapSize = Helper.Getmapsizefrommissionpath(mpmissionpath);
                project.MpMissionPath = mpmissionpath;
                project.MapPath = Helper.GetMapNameFromMissionPath(mpmissionpath).ToLower() + "_Map.png";
                project.ProfileName = profilefolder;
                project.ProjectRoot = Path.Combine(ProjectFolder, ProjectName);
                _ProjectManager.AddProject(project);
                CheckMapanddownload(project);
                //MessageBox.Show("Project created, Please Close the editor and populate the missions files before trying to load this project");
                //Helper.OpenFolderInExplorer(Path.Combine(project.ProjectRoot, project.ProjectName));
                listBoxProjects.SelectedItem = _ProjectManager.CurrentProject;

            }
            else if (projecttype == "Create From SFTP")
            {
            }
            else if (projecttype == "Create Project to Existing Project Files")
            {
                string ProjectPath = ProjectFolderTB.Text;
                string ProjectName = ProjectNameTB.Text;
                if (ProjectName == "")
                {
                    MessageBox.Show("Please select a Project name.");
                    return;
                }
                string missionsfolder = ProjectMissionFolderTB.Text;

                // The text boxes start out holding placeholder text, so make sure real folders were picked
                if (!Directory.Exists(ProjectProfileTB.Text) || !Directory.Exists(missionsfolder))
                {
                    MessageBox.Show("Please select both the profile folder and the mission folder using the buttons.");
                    return;
                }
                if (!Directory.Exists(ProjectPath))
                {
                    MessageBox.Show("The project folder could not be worked out. Check that the profile and mission folders are in the same server folder.");
                    return;
                }

                string profilefolder = Path.GetFileName(ProjectProfileTB.Text);
                string mpmissionpath = Path.GetFileName(missionsfolder);

                Project project = new Project();
                project.AddNames(ProjectName);
                project.MapSize = Helper.Getmapsizefrommissionpath(mpmissionpath);
                project.MpMissionPath = mpmissionpath;
                project.MapPath = Helper.GetMapNameFromMissionPath(mpmissionpath).ToLower() + "_Map.png";
                project.ProfileName = profilefolder;
                project.ProjectRoot = ProjectPath;
                _ProjectManager.AddProject(project);
                CheckMapanddownload(project);
                MessageBox.Show("Project created, select the project from the list and load....");
            }
        }
        private async void CheckMapanddownload(Project project)
        {
            MapAddonInfo? mapaddon = _manifest.MapAddons?.FirstOrDefault(x => x.MapInfo.MapPng == project.MapPath);
            if (mapaddon == null)
                return;

            string imagePath = Path.Combine(appDirectory, "MapAddons", mapaddon.MapInfo.MapPng);
            string XYZPath = Path.Combine(appDirectory, "MapAddons", mapaddon.MapInfo.MapXYZ);
            if (File.Exists(imagePath) && File.Exists(XYZPath))
                return;

            // async void: a failed download must be reported, not allowed to crash the editor
            try
            {
                await AppServices.GetRequired<IPluginInstaller>().DownloadMapAddonAsync(mapaddon);

                var addonItem = MapAddonsLB.Items.Cast<ListViewItem>().FirstOrDefault(x => x.Tag == mapaddon);
                if (addonItem != null)
                    MarkInstalled(addonItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"The map for this project could not be downloaded:\n\n{ex.Message}\n\nYou can install it later from the Map Addons list.",
                    "MapAddon Download", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SelectProjectFolderbutton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == DialogResult.OK)
            {
                ProjectFolderTB.Text = fb.SelectedPath;
            }
            else
                ProjectFolderTB.Text = "";
        }
        private void selectProfilefolderNamebutton_Click(object sender, EventArgs e)
        {
            Form owner = Form.ActiveForm ?? Application.OpenForms[0];
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select the profile folder";
                dialog.UseDescriptionForTitle = true; // Optional, makes dialog title better
                DialogResult result = dialog.ShowDialog(owner);

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    ProjectProfileTB.Text = dialog.SelectedPath;
                    // Use this path
                }
            }
        }
        private void MissionFoldertoUsebutton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == DialogResult.OK)
            {
                ProjectMissionFolderTB.Text = fb.SelectedPath;

                string path1 = ProjectMissionFolderTB.Text;
                string path2 = ProjectProfileTB.Text;

                // Get two levels up from each path
                string basePath1 = Directory.GetParent(Directory.GetParent(path1).FullName).FullName;
                string basePath2 = Directory.GetParent(path2).FullName;

                // Print results
                Console.WriteLine("Base Path 1: " + basePath1);
                Console.WriteLine("Base Path 2: " + basePath2);

                // Compare
                if (string.Equals(basePath1, basePath2, StringComparison.OrdinalIgnoreCase))
                {
                    ProjectFolderTB.Text = basePath1;
                }
                else
                    MessageBox.Show("Root Directory seems to be different, check yo uhave selected the correct paths.....");
            }
        }
        private void listBoxProjects_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = listBoxProjects.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    listBoxProjects.SelectedIndex = index; // Select the item
                    var project = listBoxProjects.Items[index] as Project;

                    // Now project is your clicked item
                    ProjectsCM.Show(Cursor.Position); // Or e.Location relative to form
                }
            }
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var selected = listBoxProjects.SelectedItem as Project;
            if (selected == null)
                return;

            if (_ProjectManager.CurrentProject?.ProjectName == selected.ProjectName)
            {
                // Already active, do nothing
                MessageBox.Show($"{selected.ProjectName} is already the active project.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _ProjectManager.SetCurrentProject(selected);
            MessageBox.Show($"{selected.ProjectName} is now the active project.");

            AppServices.GetRequired<IHostShell>().SetStatusText($"Active Project:{selected.ProjectName}");
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

            var selected = listBoxProjects.SelectedItem as Project;
            if (selected != null)
            {
                string missionDir = Path.Combine(selected.ProjectRoot, "mpmissions", selected.MpMissionPath);
                string profileDir = Path.Combine(selected.ProjectRoot, selected.ProfileName);

                DialogResult result = MessageBox.Show($"Yes will remove the project and delete its mission and profile folders:\n{missionDir}\n{profileDir}\n\nNo will only remove the project from the editor\nCancel will return with no changes", "Delete All Files.....", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {

                    if (MessageBox.Show("Double checking you want to remove all files\nselecting no will still remove the project from the editor but keep all files in the project folder\nAre you sure you want to do this?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        // Only delete the folders the project owns. For a project made from existing files,
                        // ProjectRoot is the user's real server folder and must never be wiped.
                        try
                        {
                            if (Directory.Exists(missionDir))
                                Directory.Delete(missionDir, true);
                            if (Directory.Exists(profileDir))
                                Directory.Delete(profileDir, true);

                            string mpmissionsDir = Path.Combine(selected.ProjectRoot, "mpmissions");
                            if (Directory.Exists(mpmissionsDir) && !Directory.EnumerateFileSystemEntries(mpmissionsDir).Any())
                                Directory.Delete(mpmissionsDir);
                            if (Directory.Exists(selected.ProjectRoot) && !Directory.EnumerateFileSystemEntries(selected.ProjectRoot).Any())
                                Directory.Delete(selected.ProjectRoot);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Some project files could not be deleted:\n\n{ex.Message}", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    RemoveProject(selected);
                    MessageBox.Show("Project and files Removed....", "Done", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (result == DialogResult.No)
                {
                    RemoveProject(selected);
                    MessageBox.Show("Project Removed....", "Done", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
        }
        private void RemoveProject(Project project)
        {
            bool wasActive = _ProjectManager.CurrentProject == project;

            AppServices.GetRequired<UploadTrackerService>().RemoveProject(project.ProjectName);
            _ProjectManager.RemoveProject(project);
            groupBox2.Visible = false;

            // Only the active project changes the status bar; removing another project leaves it alone
            if (wasActive)
                AppServices.GetRequired<IHostShell>().SetStatusText($"Active Project:None Selected");
        }
        private void listBoxProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxProjects.SelectedItems.Count < 1) return;
            Project p = listBoxProjects.SelectedItem as Project;
            bool isActive = p == _ProjectManager.CurrentProject;
            groupBox2.Visible = true;
            if (isActive)
            {
                EditProjectRootTB.Enabled = false;
                EditProfilePathTB.Enabled = false;
                EditMissionPathTB.Enabled = false;
            }
            else
            {
                EditProjectRootTB.Enabled = true;
                EditProfilePathTB.Enabled = true;
                EditMissionPathTB.Enabled = true;
            }
            EditProjectNameTB.Text = p.ProjectName;
            EditProjectRootTB.Text = p.ProjectRoot;
            EditProfilePathTB.Text = p.ProfileName;
            EditMissionPathTB.Text = p.MpMissionPath;
            EditMapPathTB.Text = p.MapPath;
            EditMapSizeNUD.Value = p.MapSize;
            ProtocolCB.SelectedValue = p.ServerSettings.Protocol;
            HostTB.Text = p.ServerSettings.Host;
            PortNUD.Value = p.ServerSettings.Port;
            UsernameTB.Text = p.ServerSettings.Username;
            PasswordTB.Text = Helper.DecryptPassword(p.ServerSettings.EncryptedPassword);
            ServerRootTN.Text = p.ServerSettings.RootPath;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBoxProjects.SelectedItem is not Project p)
                return;

            // Renaming goes through ProjectManager so the active project and pending uploads follow the new name
            if (!_ProjectManager.RenameProject(p, EditProjectNameTB.Text.Trim()))
            {
                MessageBox.Show($"A project called {EditProjectNameTB.Text.Trim()} already exists.", "Rename", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_ProjectManager.CurrentProject == p)
                AppServices.GetRequired<IHostShell>().SetStatusText($"Active Project:{p.ProjectName}");

            p.ProjectRoot = EditProjectRootTB.Text;
            p.ProfileName = EditProfilePathTB.Text;
            p.MpMissionPath = EditMissionPathTB.Text;
            p.MapPath = EditMapPathTB.Text;
            p.MapSize = (int)EditMapSizeNUD.Value;
            // Update the existing settings rather than replacing them, so fields this form
            // doesn't edit (such as PassiveMode) are kept
            p.ServerSettings ??= new ProjectServerSettings();
            p.ServerSettings.Protocol = ProtocolCB.SelectedValue is TransferProtocol protocol ? protocol : TransferProtocol.Ftp;
            p.ServerSettings.Port = (int)PortNUD.Value;
            p.ServerSettings.Host = HostTB.Text;
            p.ServerSettings.Username = UsernameTB.Text;
            p.ServerSettings.EncryptedPassword = Helper.EncryptPassword(PasswordTB.Text);
            p.ServerSettings.RootPath = ServerRootTN.Text;
            _ProjectManager.Save();
            listBoxProjects.Invalidate();
            MessageBox.Show("Projects Json has now been saved.");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MapAddonsLB.Visible = false;
            PluginLB.Visible = true;
            button1.Enabled = false;
            button3.Enabled = true;
            button4.Visible = false;
            button5.Visible = false;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            MapAddonsLB.Visible = true;
            PluginLB.Visible = false;
            button1.Enabled = true;
            button3.Enabled = false;
            button4.Visible = true;
            button5.Visible = true;
        }
        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sourcefile = Path.Combine(
                    _ProjectManager.CurrentProject.ProjectRoot,
                    _ProjectManager.CurrentProject.ProfileName,
                    "map_output.txt"
                    );

            if(!File.Exists(sourcefile))
            {

                Console.WriteLine("[INFO] Map_output.txt doesn not exist for current project");
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            MapData.CreateNewData(
                Path.Combine(
                    _ProjectManager.CurrentProject.ProjectRoot,
                    _ProjectManager.CurrentProject.ProfileName,
                    "map_output.txt"
                    )
                , Path.Combine(
                    appDirectory,
                    "MapAddons",
                    _ProjectManager.CurrentProject.MapPath + ".xyz"
                    )
                );
            MessageBox.Show(
                    "XYZ Converted successfully.",
                    "Import Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            Cursor.Current = Cursors.Default;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFile = new OpenFileDialog();

            openFile.Title = "Please select the map you wish to import";
            openFile.Filter = "PNG Images (*.png)|*.png";
            openFile.DefaultExt = "png";
            openFile.CheckFileExists = true;
            openFile.Multiselect = false;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string mapFile = Path.Combine(
                    "MapAddons",
                    _ProjectManager.CurrentProject.MapPath);

                // Ensure destination directory exists
                string? directory = Path.GetDirectoryName(mapFile);
                if (!string.IsNullOrEmpty(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Copy the selected PNG to the destination
                File.Copy(openFile.FileName, mapFile, overwrite: true);

                MessageBox.Show(
                    "Map imported successfully.",
                    "Import Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }


        public bool isPasswordVisible = false;
        private void button6_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;
            button6.Text = isPasswordVisible ? "👁": "🙈" ;
            PasswordTB.UseSystemPasswordChar = !isPasswordVisible;
        }
    }
    public sealed class EnumItem<T>
    {
        public T Value { get; init; }
        public string Text { get; init; } = string.Empty;
    }


    [PluginInfo("Project Manager", "ProjectsPlugin", "ProjectsPlugin.Projects.png")]
    public class PluginProject : IPluginForm, IDisposable
    {
        private bool disposed = false;

        public string pluginIdentifier => "ProjectsPlugin";
        public string pluginName => "Project Manager";

        public Form GetForm()
        {
            return new ProjectForm(this);
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
