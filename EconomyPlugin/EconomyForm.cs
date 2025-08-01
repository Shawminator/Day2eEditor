using Day2eEditor;

namespace AddonPlugin
{
    public partial class EconomyForm : Form
    {
        private IPluginForm _plugin;
        public EconomyForm(IPluginForm plugin)
        {
            InitializeComponent();
            _plugin = plugin;
        }

        private void EconomyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_plugin is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }

    [PluginInfo("Economy Manager", "EconomyPlugin")]
    public class PluginEconomy : IPluginForm, IDisposable
    {
        private bool disposed = false;

        public string pluginIdentifier => "EconomyPlugin";
        public string pluginName => "Economy Manager";

        public Form GetForm()
        {
            return new EconomyForm(this);
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
