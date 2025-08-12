using Day2eEditor;
using System.Diagnostics;
using System.Windows.Forms;

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

            if (_clickedItem?.Tag is PluginInfo plugin)
            {
                await AppServices.GetRequired<UpdateManager>().DownloadNewPluginAsync(plugin);
                MessageBox.Show($"{plugin.Name} Downloaded.", "Plugin Download", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _clickedItem.SubItems.Add("Installed");
            }
            else if(_clickedItem?.Tag is MapAddonInfo mapaddoninfo)
            {
                await AppServices.GetRequired<UpdateManager>().DownloadMapAddonAsync(mapaddoninfo);
                MessageBox.Show($"{mapaddoninfo.Name} Downloaded.", "Plugin Download", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _clickedItem.SubItems.Add("Installed");
            }

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
                    _clickedItem.SubItems.RemoveAt(1);

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
        }
        private void PluginLB_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (PluginLB.Visible)
                {
                    var item = PluginLB.GetItemAt(e.X, e.Y);
                    if (item != null)
                    {
                        _clickedItem = item;
                        if (_clickedItem.Text == "ProjectsPlugin" || _clickedItem.Text == "EconomyPlugin")
                            return;
                        PluginLB.ContextMenuStrip = PluginCM;
                        PluginCM.Show(PluginLB, e.Location);
                    }
                    else
                    {
                        _clickedItem = null;
                        PluginLB.ContextMenuStrip = null;
                    }
                }
                else if (MapAddonsLB.Visible)
                {
                    var item = MapAddonsLB.GetItemAt(e.X, e.Y);
                    if (item != null)
                    {
                        _clickedItem = item;
                        if (_clickedItem.Text == "ProjectsPlugin" || _clickedItem.Text == "EconomyPlugin")
                            return;
                        MapAddonsLB.ContextMenuStrip = PluginCM;
                        PluginCM.Show(MapAddonsLB, e.Location);
                    }
                    else
                    {
                        _clickedItem = null;
                        MapAddonsLB.ContextMenuStrip = null;
                    }
                }
            }

        }

        private void ProjectTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string projecttype = ProjectTypeComboBox.GetItemText(ProjectTypeComboBox.SelectedItem);
            if (projecttype == "Create Local from FTP/SFTP" || projecttype == "Connect Direct to FTP/SFTP")
            {
                ProjectNameLabel.Visible = true;
                ProjectNameTB.Visible = true;

                SelectProjectFolderlabel.Visible = true;
                SelectProjectFolderlabel.Text = "Select Project Folder";
                ProjectFolderTB.Visible = true;
                ProjectFolderTB.ReadOnly = true;
                ProjectFolderTB.Text = "";
                ProjectFolderTB.Size = new Size(424, 23);
                SelectProjectFolderbutton.Visible = true;

                ProfileFolderNamelabel.Visible = false;
                ProjectProfileTB.Visible = false;
                selectProfilefolderNamebutton.Visible = false;

                MissionFoldertoUselabel.Visible = false;
                ProjectMissionFolderTB.Visible = false;
                ProjectMissionFolderTB.Text = "";
                ProjectMissionFolderTB.ReadOnly = false;
                MissionFoldertoUsebutton.Visible = false;

                CreateProjectbutton.Location = new Point(615, 119);
            }
            else if (projecttype == "Create Blank")
            {
                ProjectNameLabel.Visible = true;
                ProjectNameTB.Visible = true;

                SelectProjectFolderlabel.Visible = true;
                SelectProjectFolderlabel.Text = "Select Project Folder";
                ProjectFolderTB.Visible = true;
                ProjectFolderTB.ReadOnly = true;
                ProjectFolderTB.Size = new Size(424, 23);
                ProjectFolderTB.Text = "";
                SelectProjectFolderbutton.Visible = true;

                ProfileFolderNamelabel.Visible = true;
                ProfileFolderNamelabel.Text = "Profile Folder Name";
                ProjectProfileTB.Visible = true;
                ProjectProfileTB.Size = new Size(452, 23);
                ProjectProfileTB.Text = "Profiles";
                ProjectProfileTB.ReadOnly = false;
                selectProfilefolderNamebutton.Visible = false;

                MissionFoldertoUselabel.Visible = true;
                MissionFoldertoUselabel.Text = "Mission Folder to use";
                ProjectMissionFolderTB.Visible = true;
                ProjectMissionFolderTB.Size = new Size(452, 23);
                ProjectMissionFolderTB.Text = "dayzOffline.chernarusplus";
                ProjectMissionFolderTB.ReadOnly = false;
                MissionFoldertoUsebutton.Visible = false;

                CreateProjectbutton.Location = new Point(615, 167);
            }
            else if (projecttype == "Connect to Exisiting Server")
            {
                ProjectNameLabel.Visible = true;
                ProjectNameTB.Visible = true;

                SelectProjectFolderlabel.Visible = true;
                SelectProjectFolderlabel.Text = "Project Folder";
                ProjectFolderTB.Visible = true;
                ProjectFolderTB.ReadOnly = true;
                ProjectFolderTB.Text = "Will Be Auto populated....";
                ProjectFolderTB.Size = new Size(452, 23);
                SelectProjectFolderbutton.Visible = false;

                ProfileFolderNamelabel.Visible = true;
                ProfileFolderNamelabel.Text = "Profile Path";
                ProjectProfileTB.Visible = true;
                ProjectProfileTB.Size = new Size(424, 23);
                ProjectProfileTB.Text = "";
                ProjectProfileTB.ReadOnly = true;
                selectProfilefolderNamebutton.Visible = true;

                MissionFoldertoUselabel.Visible = true;
                MissionFoldertoUselabel.Text = "Mission Path";
                ProjectMissionFolderTB.Visible = true;
                ProjectMissionFolderTB.Size = new Size(424, 23);
                ProjectMissionFolderTB.ReadOnly = true;
                MissionFoldertoUsebutton.Visible = true;

                CreateProjectbutton.Location = new Point(615, 167);
            }
        }
        private int Getmapsizefrommissionpath(string mpmissionpath)
        {
            string[] MapSizeList = File.ReadAllLines("Maps/MapSizes.txt");
            Dictionary<string, int> maplist = new Dictionary<string, int>();
            foreach (string line in MapSizeList)
            {
                maplist.Add(line.Split(':')[0], Convert.ToInt32(line.Split(':')[1]));
            }
            string currentmap = mpmissionpath.ToLower().Split('.')[1];
            int size;
            if (maplist.TryGetValue(currentmap, out size))
            {
                return size;
            }
            return 0;
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
                string missionsfolder = ProjectMissionFolderTB.Text;
                string profilefolder = ProjectProfileTB.Text;
                string mpmissionpath = Path.GetFileName(missionsfolder);
                string PmissionFolder = ProjectFolder + "\\mpmissions\\" + Path.GetFileName(missionsfolder);
                string Pprofilefolder = ProjectFolder + "\\" + profilefolder;

                Directory.CreateDirectory(PmissionFolder);
                Directory.CreateDirectory(Pprofilefolder);

                Project project = new Project();
                project.AddNames(ProjectName);
                project.MapSize = Getmapsizefrommissionpath(mpmissionpath);
                project.MpMissionPath = mpmissionpath;
                project.MapPath = "Maps\\" + mpmissionpath.ToLower().Split('.')[1] + "_Map.png";
                project.ProfileName = profilefolder;
                project.ProjectRoot = ProjectFolder;
                _ProjectManager.AddProject(project);
                MessageBox.Show("Project created, Please Close the editor and populate the missions files before trying to load this project");
                ShellHelper.OpenFolderInExplorer(project.ProjectRoot);
                listBoxProjects.SelectedItem = _ProjectManager.CurrentProject;
            }
            else if (projecttype == "Create Local from FTP/SFTP")
            {
            }
            else if (projecttype == "Connect to Exisiting Server")
            {
                string ProjectPath = ProjectFolderTB.Text;
                string ProjectName = ProjectNameTB.Text;
                if (ProjectName == "")
                {
                    MessageBox.Show("Please select a Project name.");
                    return;
                }
                string missionsfolder = ProjectMissionFolderTB.Text;
                string profilefolder = Path.GetFileName(ProjectProfileTB.Text);
                string mpmissionpath = Path.GetFileName(missionsfolder);

                Project project = new Project();
                project.AddNames(ProjectName);
                project.MapSize = Getmapsizefrommissionpath(mpmissionpath);
                project.MpMissionPath = mpmissionpath;
                project.MapPath = "Maps\\" + mpmissionpath.ToLower().Split('.')[1] + "_Map.png";
                project.ProfileName = profilefolder;
                project.ProjectRoot = ProjectPath;
                _ProjectManager.AddProject(project);
                MessageBox.Show("Project created, select the project from the list and load....");
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

            if (this.MdiParent is Form1 mainForm)
            {
                mainForm.toolStripStatusLabel1.Text = $"Active Project : {selected.ProjectName}";
            }
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

            var selected = listBoxProjects.SelectedItem as Project;
            if (selected != null)
            {
                DialogResult result = MessageBox.Show("Yes will remove project and all files in the project folder\nNo will only remove the project from the editor\nCancel will return with no changes", "Delete All Files.....", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {

                    if (MessageBox.Show("Double checking you want to remove all files\nselecting no will still remove the project from the editor but keep all files in the project folder\nAre you sure you want to do this?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        Directory.Delete(selected.ProjectRoot, true);
                    }
                    _ProjectManager.RemoveProject(selected);
                    MessageBox.Show("Project and files Removed....", "Done", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (result == DialogResult.No)
                {
                    _ProjectManager.RemoveProject(selected);
                    MessageBox.Show("Project Removed....", "Done", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
        }

        private void listBoxProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxProjects.SelectedItems.Count < 1) return;
            Project p = listBoxProjects.SelectedItem as Project;
            bool isActive = p == _ProjectManager.CurrentProject;
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
            EditCreateBackupsCB.Checked = p.CreateBackups;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Project p = listBoxProjects.SelectedItem as Project;
            p.ProjectName = EditProjectNameTB.Text;
            p.ProjectRoot = EditProjectRootTB.Text;
            p.ProfileName = EditProfilePathTB.Text;
            p.MpMissionPath = EditMissionPathTB.Text;
            p.MapPath = EditMapPathTB.Text;
            p.MapSize = (int)EditMapSizeNUD.Value;
            p.CreateBackups = EditCreateBackupsCB.Checked;
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MapAddonsLB.Visible = true;
            PluginLB.Visible = false;
            button1.Enabled = true;
            button3.Enabled = false;
        }
    }
    [PluginInfo("Project Manager", "ProjectsPlugin")]
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
