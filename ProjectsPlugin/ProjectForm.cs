using Day2eEditor;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProjectsPlugin
{

    public partial class ProjectForm : Form
    {
        private IPluginForm _plugin;
        private Manifest _manifest;
        private ListViewItem _clickedItem;
        public ProjectForm(IPluginForm plugin)
        {
            InitializeComponent();
            _plugin = plugin;
            _manifest = AppServices.GetRequired<Manifest>();
        }

        private void ProjectForm_Load(object sender, EventArgs e)
        {
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
            else
            {
                MessageBox.Show("No valid plugin selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
