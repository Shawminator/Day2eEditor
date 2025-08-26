using System.ComponentModel;

namespace Day2eEditor
{
    public partial class AddItemfromTypes : Form
    {
        private readonly FormController controller;
        private readonly EconomyManager _economyManager;

        public TypeEntry CurrentLootPart { get; private set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UseMultipleOfSameItem { get; set; } = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool UseOnlySingleItem { get; set; } = false;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool LowerCase { get; set; } = false;
        public bool HideUsed { get; private set; } = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Dictionary<string, bool> UsedTypes { get; set; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BindingList<string> AddedTypes { get; private set; }

        private int searchIndex;
        private List<TreeNode> searchResults;

        public AddItemfromTypes()
        {
            InitializeComponent();

            controller = new FormController(
                this,
                panel1,
                null,
                TitleLabel,
                TitleLabel,
                CloseButton,
                null
            );

            _economyManager = AppServices.GetRequired<EconomyManager>();
            AddedTypes = new BindingList<string>();
        }

        private void AddItemfromTypes_Load(object sender, EventArgs e)
        {
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "Value";
            listBox1.DataSource = AddedTypes;

            if (UsedTypes == null)
                darkButton5.Visible = false;

            if (UseOnlySingleItem)
                treeViewMS1.SetMultiselect = false;

            PopulateTreeView();
        }

        #region ListBox Drawing
        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            ListBox lb = sender as ListBox;
            if (lb.SelectedItem == null) return;
            e.DrawBackground();
            Brush myBrush = Brushes.Black;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            }
            else
            {
                myBrush = Brushes.White;
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, 63, 65)), e.Bounds);
            }
            e.Graphics.DrawString(lb.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds);
            e.DrawFocusRectangle();
        }
        #endregion

        #region Tree Building
        private void PopulateTreeView()
        {
            treeViewMS1.Nodes.Clear();

            foreach (TypesFile tf in _economyManager.TypesConfig.AllData)
            {
                var rootNode = new TreeNode(tf.FileName) { Tag = tf };
                var categoryNodes = new Dictionary<string, TreeNode>();

                foreach (TypeEntry type in tf.Data.TypeList)
                {
                    string catName = type.Category?.Name ?? "other";

                    if (!categoryNodes.TryGetValue(catName, out var catNode))
                    {
                        catNode = new TreeNode(catName)
                        {
                            Name = catName,
                            Tag = type.Category ?? new Category { Name = "other" }
                        };
                        rootNode.Nodes.Add(catNode);
                        categoryNodes[catName] = catNode;
                    }

                    catNode.Nodes.Add(new TreeNode(type.Name) { Tag = type });
                }

                treeViewMS1.Nodes.Add(rootNode);
            }
        }
        #endregion

        #region Item Adding / Removing
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is TypeEntry typeEntry)
                CurrentLootPart = typeEntry;
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (CurrentLootPart != null)
                AddItemByName(CurrentLootPart.Name);
        }

        private void darkButton4_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == -1) return;

            if (tabControl1.SelectedIndex == 0)
            {
                foreach (TreeNode tn in treeViewMS1.SelectedNodes)
                    AddItemFromNode(tn);
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                foreach (string line in richTextBox1.Lines)
                    AddItemByName(line);
            }
        }

        private void AddItemFromNode(TreeNode tn)
        {
            if (tn.Tag is string)
            {
                foreach (TreeNode child in tn.Nodes)
                    AddItemFromNode(child);
            }
            else if (tn.Tag is TypeEntry type)
            {
                AddItemByName(type.Name);
            }
        }

        public void AddItemByName(string item)
        {
            string value = LowerCase ? item.ToLower() : item;

            if (UseOnlySingleItem)
                AddedTypes.Clear();

            if (UseMultipleOfSameItem || !AddedTypes.Contains(value))
                AddedTypes.Add(value);
            else
                MessageBox.Show($"{value} already in the list");
        }

        private void RemoveItemsButton_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null) return;

            var itemsToRemove = listBox1.SelectedItems.Cast<object>()
                .Select(x => x.ToString())
                .ToList();

            foreach (string item in itemsToRemove)
                AddedTypes.Remove(item);

            if (listBox1.Items.Count == 0)
                listBox1.SelectedIndex = -1;
        }
        #endregion

        #region TreeView Search & Selection
        private void darkButton6_Click(object sender, EventArgs e)
        {
            if (treeViewMS1.Nodes.Count == 0) return;

            string searchTerm = darkTextBox1.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm)) return;

            var foundNodes = treeViewMS1.Nodes.Cast<TreeNode>()
                                 .SelectMany(n => FindMatchingNodes(n, searchTerm))
                                 .ToList();

            if (foundNodes.Count == 0) return;

            searchIndex = 0;
            searchResults = foundNodes;
            SelectSearchResult();
            darkButton7.Visible = searchResults.Count > 1;
        }

        /// <summary>
        /// Recursively find nodes whose text contains the search term.
        /// </summary>
        private IEnumerable<TreeNode> FindMatchingNodes(TreeNode node, string searchTerm)
        {
            if (node.Text.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                yield return node;

            foreach (TreeNode child in node.Nodes)
            {
                foreach (var match in FindMatchingNodes(child, searchTerm))
                    yield return match;
            }
        }

        private void SelectSearchResult()
        {
            if (searchIndex >= searchResults.Count)
            {
                MessageBox.Show("No more items found");
                darkButton7.Visible = false;
                return;
            }

            var node = searchResults[searchIndex];
            treeViewMS1.SelectedNode = node;
            treeViewMS1.Focus();

            if (node.Tag is TypeEntry typeEntry)
                CurrentLootPart = typeEntry;
        }

        private void darkButton7_Click(object sender, EventArgs e)
        {
            searchIndex++;
            SelectSearchResult();
        }
        #endregion

        #region Misc
        private void darkButton5_Click(object sender, EventArgs e)
        {
            if (darkButton5.Tag?.ToString() == "HideUsed")
            {
                darkButton5.Text = "Show Used Types";
                darkButton5.Tag = "ShowUsed";
                HideUsed = true;
            }
            else
            {
                darkButton5.Text = "Hide Used Types";
                darkButton5.Tag = "HideUsed";
                HideUsed = false;
            }

            PopulateTreeView();
        }

        private void darkButton8_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
        #endregion
    }
}
