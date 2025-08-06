using Day2eEditor;
using EconomyPlugin;

namespace EconomyPlugin
{
    public partial class EconomyForm : Form
    {
        EconomyManager _economyManager;
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

        private void EconomyForm_Load(object sender, EventArgs e)
        {
            _economyManager = AppServices.GetRequired<EconomyManager>();
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
        public override string ToString() => pluginName;


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
