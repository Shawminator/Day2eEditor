using Day2eEditor;
using System.ComponentModel;

namespace ProjectsPlugin
{
    public partial class ProjectForm : Form
    {
        private List<IPluginForm> CurrentInstalledPluginList;
        public ProjectForm()
        {
            InitializeComponent();
        }

        public void SetData(List<object> datalist)
        {
            foreach (object d in datalist)
            {
                if(d is List<IPluginForm> pluginForms)
                {
                    CurrentInstalledPluginList = pluginForms;
                }
            }
        }

        private void ProjectForm_Load(object sender, EventArgs e)
        {
            
        }
    }
    public class PluginProject : IPluginForm
    {
        private List<object> _datalist;
        public string pluginIdentifier => "ProjectForm";
        public string pluginName => "Project Manager";


        public void SetData(List<object> datalist)
        {
            _datalist = datalist;
        }

        public Form GetForm()
        {
            var form = new ProjectForm();
            form.SetData(_datalist);
            return form;

        }
        public override string ToString()
        {
            return pluginName;
        }
    }
}
