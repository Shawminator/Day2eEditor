using Day2eEditor;

namespace ProjectsPlugin
{
    public class PluginProject : IPluginForm
    {
        public string pluginIdentifier => "ProjectForm";
        public string pluginName => "ProjectForm Manager";
        public decimal pluginVersion => 0.1m;

        public Form GetForm()
        {
            return new ProjectForm();
        }
    }
    public partial class ProjectForm : Form
    {
        public ProjectForm()
        {
            InitializeComponent();
        }
    }

}
