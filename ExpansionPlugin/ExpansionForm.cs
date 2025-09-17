using Day2eEditor;

namespace ExpansionPlugin
{
    public partial class ExpansionForm : Form
    {
        private IUIHandler? _currentHandler;
        public MapViewerControl MapControl => _mapControl;
        private IPluginForm _plugin;
        private TreeNode? currentTreeNode;
        private ExpansionManager ExpansionManager { get; set; }

        #region Ditionarys
        private Dictionary<Type, Action<TreeNode, List<TreeNode>>> _typeHandlers;
        private Dictionary<string, Action<TreeNode, List<TreeNode>>> _stringHandlers;
        private Dictionary<Type, Action<TreeNode>> _typeContextMenus;
        private Dictionary<string, Action<TreeNode>> _stringContextMenus;
        #endregion Dictionarys

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        public ExpansionForm(IPluginForm plugin)
        {
            InitializeComponent();
            _plugin = plugin;
            ExpansionManager = new ExpansionManager();
            ExpansionManager.SetExpansionStuff();
        }

        private void ExpansionForm_Load(object sender, EventArgs e)
        {
            bool flowControl = SetupMap();
            if (!flowControl)
            {
                return;
            }
            BuildTreeview();
        }

        private bool SetupMap()
        {
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string imagePath = Path.Combine(appDirectory, "MapAddons", AppServices.GetRequired<ProjectManager>().CurrentProject.MapPath);
            if (!File.Exists(imagePath))
            {
                MessageBox.Show($"Map File does not exist for {AppServices.GetRequired<ProjectManager>().CurrentProject.ProjectName}\nPlease download it from the Projects Manager");
                this.BeginInvoke(new Action(Close)); // defer until safe
                return false;
            }
            Image mapImage = Image.FromFile(imagePath);
            _mapControl.LoadMap(mapImage, AppServices.GetRequired<ProjectManager>().CurrentProject.MapSize);
            return true;
        }
        private void AddFileToTree<TFile>(TreeNode parentNode, string relativePath, string rootPath, TFile file, Func<TFile, TreeNode> createFileNode, bool expand = false)
        {
            string[] parts = relativePath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            TreeNode currentNode = parentNode;

            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];

                if (i == parts.Length - 1)
                {
                    TreeNode fileNode = createFileNode(file);
                    currentNode.Nodes.Add(fileNode);
                    if (expand)
                    {
                        // Expand all parent folders
                        fileNode.Expand();
                        ExpansionTV.SelectedNode = fileNode;
                    }
                }
                else
                {
                    TreeNode folderNode = currentNode.Nodes
                        .Cast<TreeNode>()
                        .FirstOrDefault(n => n.Text.Equals(part, StringComparison.OrdinalIgnoreCase));

                    if (folderNode == null)
                    {
                        folderNode = new TreeNode(part)
                        {
                            Tag = Path.Combine(
                                rootPath,
                                string.Join(Path.DirectorySeparatorChar.ToString(), parts.Take(i + 1))
                            )
                        };
                        currentNode.Nodes.Add(folderNode);
                    }

                    currentNode = folderNode;

                    if (expand)
                    {
                        // Expand all parent folders
                        currentNode.Expand();
                    }
                }
            }
        }
        private void BuildTreeview()
        {
            ExpansionTV.Nodes.Clear();
            TreeNode rootNode = new TreeNode("Expansion Mod")
            {
                Tag = "RootNode",
            };
            TreeNode missionNode = new TreeNode(Path.GetFileName(AppServices.GetRequired<EconomyManager>().basePath))
            {
                Tag = "missionNode",
            };

            string _relativePath = Path.GetRelativePath(ExpansionManager.basePath, ExpansionManager.ExpansionBaseBuildingConfig.FilePath);
            AddFileToTree(missionNode, _relativePath, ExpansionManager.basePath, ExpansionManager.ExpansionBaseBuildingConfig, CreateExpansionBaseBuildingConfigNodes);

            TreeNode profileNode = new TreeNode(AppServices.GetRequired<ProjectManager>().CurrentProject.ProfileName)
            {
                Tag = "profileNode",
            };

            _relativePath = Path.GetRelativePath(ExpansionManager.profilePath, ExpansionManager.ExpansionAirdropConfig.FilePath);
            AddFileToTree(profileNode, _relativePath, ExpansionManager.profilePath, ExpansionManager.ExpansionAirdropConfig, CreateExpansionAirdropConfigNodes);

            _relativePath = Path.GetRelativePath(ExpansionManager.profilePath, ExpansionManager.ExpansionBookConfig.FilePath);
            AddFileToTree(profileNode, _relativePath, ExpansionManager.profilePath, ExpansionManager.ExpansionBookConfig, CreateExpansionBookConfigConfigNodes);


            rootNode.Nodes.Add(profileNode);
            rootNode.Nodes.Add(missionNode);
            ExpansionTV.Nodes.Add(rootNode);
        }

        private TreeNode CreateExpansionBaseBuildingConfigNodes(ExpansionBaseBuildingConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };

            return EconomyRootNode;
        }
        private TreeNode CreateExpansionAirdropConfigNodes(ExpansionAirdropConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };

            return EconomyRootNode;
        }
        private TreeNode CreateExpansionBookConfigConfigNodes(ExpansionBookConfig ef)
        {
            TreeNode EconomyRootNode = new TreeNode(ef.FileName)
            {
                Tag = ef
            };

            return EconomyRootNode;
        }

    }

    [PluginInfo("Exspansion Manager", "ExspansionPlugin")]
    public class PluginExspansion : IPluginForm, IDisposable
    {
        private bool disposed = false;

        public string pluginIdentifier => "ExspansionPlugin";
        public string pluginName => "Exspansion Manager";

        public Form GetForm()
        {
            return new ExpansionForm(this);
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
