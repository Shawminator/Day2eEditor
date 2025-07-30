using Day2eEditor;

namespace ProjectsPlugin
{
    public partial class ProjectForm : Form
    {
        public ProjectForm()
        {
            InitializeComponent();
        }
    }
    public class PluginProject : IPluginForm
    {
        public string pluginIdentifier => "ProjectForm";
        public string pluginName => "ProjectForm Manager";
        public string pluginVersion => "0.0.1";

        public Form GetForm()
        {
            return new ProjectForm();
        }
    }
}
