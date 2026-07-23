using Day2eEditor;

namespace AddonPlugin
{
    public partial class RenameMeForm : Form
    {
        private IPluginForm _plugin;
        public RenameMeForm(IPluginForm plugin)
        {
            InitializeComponent();
            _plugin = plugin;
        }
    }
    
    [PluginInfo("RenameMe Manager", "RenameMePlugin")]
    public class PluginRenameMe : IPluginForm, IDisposable
    {
        private bool disposed = false;

        public string pluginIdentifier => "RenameMePlugin";
        public string pluginName => "RenameMe Manager";

        public Form GetForm()
        {
            return new RenameMeForm(this);
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
