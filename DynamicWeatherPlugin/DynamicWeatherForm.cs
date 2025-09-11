using Day2eEditor;
using System.ComponentModel;
using System.Text.Json;
using static FileService;

namespace DynamicWeatherPlugin
{
    public partial class DynamicWeatherForm : Form
    {
        private IPluginForm _plugin;
        private EconomyManager _economyManager;
        private readonly ProjectManager _projectManager;
        public WeatherDynamic CurrentDynamicWeather { get; private set; }
        public string DynamicWeatherPluginPath { get; private set; }

        public DynamicWeatherPluginConfig DynamicWeatherPlugin;
        public DynamicWeatherForm(IPluginForm plugin)
        {
            InitializeComponent();
            _plugin = plugin;
            _economyManager = AppServices.GetRequired<EconomyManager>();
            _projectManager = AppServices.GetRequired<ProjectManager>();
        }

        private void DynamicWeatherForm_Load(object sender, EventArgs e)
        {
            DynamicWeatherPluginPath = Path.Combine(_economyManager.basePath, "weather.json");
            DynamicWeatherPlugin = new DynamicWeatherPluginConfig();

            DynamicWeatherPlugin.m_Dynamics = new BindingList<WeatherDynamic>(JsonSerializer.Deserialize<WeatherDynamic[]>(File.ReadAllText(DynamicWeatherPluginPath), new JsonSerializerOptions { Converters = { new BoolConverter() } }).ToList());
            DynamicWeatherPlugin.isDirty = false;
            DynamicWeatherPlugin.Filename = DynamicWeatherPluginPath;

            TreeNode Root = new TreeNode("Dynamic Weather")
            { 
                Tag = DynamicWeatherPlugin 
            };
            foreach (WeatherDynamic wd in DynamicWeatherPlugin.m_Dynamics)
            {
                TreeNode wdnode = new TreeNode(wd.name)
                {
                    Tag = wd
                };
                Root.Nodes.Add(wdnode);
            }
            DynamicWeatherTV.Nodes.Add( Root );
        }
    }

    [PluginInfo("Dynamic Weather Manager", "DynamicWeatherPlugin")]
    public class PluginRenameMe : IPluginForm, IDisposable
    {
        private bool disposed = false;

        public string pluginIdentifier => "DynamicWeatherPlugin";
        public string pluginName => "Dynamic Weather  Manager";

        public Form GetForm()
        {
            return new DynamicWeatherForm(this);
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
