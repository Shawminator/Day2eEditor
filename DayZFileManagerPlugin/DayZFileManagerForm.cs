using Day2eEditor;
using System.Windows.Forms;

namespace DayZFileManagerPlugin
{
    public partial class DayZFileManagerForm : Form
    {
        private IPluginForm _plugin;
        private readonly ProjectManager _projectManager;
        private readonly UploadTrackerService _uploadTrackerService;


        private static readonly Color UploadRowColor =
            Color.FromArgb(225, 245, 234);   // Soft green

        private static readonly Color RemoveRowColor =
            Color.FromArgb(252, 228, 228);   // Soft red/pink


        public DayZFileManagerForm(IPluginForm plugin)
        {
            InitializeComponent();
            _plugin = plugin;
            _projectManager = AppServices.GetRequired<ProjectManager>();
            _uploadTrackerService = AppServices.GetRequired<UploadTrackerService>();
            ConfigurePendingListView();
        }

        private void ConfigurePendingListView()
        {
            pendingListView.View = View.Details;
            pendingListView.CheckBoxes = true;
            pendingListView.FullRowSelect = true;
            pendingListView.HideSelection = false;
            pendingListView.MultiSelect = true;
            pendingListView.GridLines = false;

            pendingListView.Columns.Clear();
            pendingListView.Columns.Add("Action", 80);
            pendingListView.Columns.Add("File", 180);
            pendingListView.Columns.Add("Relative Path", 350);
            pendingListView.Columns.Add("Last Saved", 150);
        }

        private void DayZFileManagerForm_Load(object sender, EventArgs e)
        {
            PopulatePendingListView();
        }

        public void PopulatePendingListView()
        {
            pendingListView.BeginUpdate();
            pendingListView.Items.Clear();

            var project = _projectManager.CurrentProject;
            var pendingFiles = _uploadTrackerService
                .GetFilesForProject(project.ProjectName);

            if (pendingFiles == null || pendingFiles.Count == 0)
            {
                pendingListView.EndUpdate();
                return;
            }

            foreach (var item in pendingFiles)
            {
                string relativePath;

                try
                {
                    relativePath = Path.GetRelativePath(
                        project.ProjectRoot,
                        item.FullPath);
                }
                catch
                {
                    relativePath = item.FullPath;
                }
                var listItem = new ListViewItem(item.Action.ToString())
                {
                    Tag = item,
                    Checked = false
                };

                listItem.SubItems.Add(item.FileName);
                listItem.SubItems.Add(relativePath);
                listItem.SubItems.Add(item.LastSavedAt.ToString("yyyy-MM-dd HH:mm"));
                listItem.ForeColor = item.Action == PendingServerAction.Upload ? UploadRowColor : RemoveRowColor;

                pendingListView.Items.Add(listItem);

            }

            pendingListView.EndUpdate();
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
