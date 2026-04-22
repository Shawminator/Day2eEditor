using Day2eEditor;

namespace DayZFileManagerPlugin
{
    public partial class DayZFileManagerForm : Form
    {
        private IPluginForm _plugin;
        private readonly ProjectManager _projectManager;
        private readonly UploadTrackerService _uploadTrackerService;
        public DayZFileManagerForm(IPluginForm plugin)
        {
            InitializeComponent();
            _plugin = plugin;
            _projectManager = AppServices.GetRequired<ProjectManager>();
            _uploadTrackerService = AppServices.GetRequired<UploadTrackerService>();

        }
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
