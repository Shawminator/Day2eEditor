using Day2eEditor;
using System.ComponentModel;

namespace ProjectsPlugin
{

    public partial class ProjectForm : Form
    {
        private IPluginForm _plugin;
        private Manifest _manifest;
        private List<PluginEntry> currentlyinstalledplugins;
        public ProjectForm(IPluginForm plugin)
        {
            InitializeComponent();
            _plugin = plugin;
            _manifest = AppServices.Get<Manifest>();
            currentlyinstalledplugins = AppServices.Get<List<PluginEntry>>();
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
                    if(currentlyinstalledplugins.FirstOrDefault(x => x.Identifier == plugin.Name) != null)
                        item.SubItems.Add("Installed");
                    listView1.Items.Add(item);
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
