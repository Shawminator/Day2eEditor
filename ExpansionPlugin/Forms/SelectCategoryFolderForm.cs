using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpansionPlugin
{
    public partial class SelectCategoryFolderForm : Form
    {
        public enum SelectionMode
        {
            Folder,
            ExpansionMarketCategoryFile,
            ExpansionMarketItem
        }

        public string SelectedCategoryTag { get; private set; }
        public List<ExpansionMarketCategory> SelectedExpansionMarketCategories { get; private set; } = new();
        public List<ExpansionMarketItem> SelectedExpansionMarketItems { get; private set; } = new();

        public ExpansionMarketCategory SelectedExpansionMarketCategory { get; private set; } = new();

        private readonly SelectionMode _selectionMode;
        private readonly bool _singleSelect;
        private bool _suppressAfterCheck;

        public SelectCategoryFolderForm(TreeNode sourceRootNode, SelectionMode selectionMode,bool singleSelect = false)
        {
            InitializeComponent();
            _selectionMode = selectionMode;
            _singleSelect = singleSelect;
            bool checkBoxMode =
                _selectionMode == SelectionMode.ExpansionMarketItem ||
                _selectionMode == SelectionMode.ExpansionMarketCategoryFile;

            if (checkBoxMode)
            {
                treeViewFolders.HideSelection = true;
                treeViewFolders.FullRowSelect = false;
            }

            treeViewFolders.CheckBoxes = checkBoxMode;

            CloneTree(sourceRootNode);
        }

        private void CloneTree(TreeNode sourceRootNode)
        {
            treeViewFolders.Nodes.Clear();

            TreeNode clonedRoot = CloneNode(sourceRootNode);
            if (clonedRoot != null)
            {
                treeViewFolders.Nodes.Add(clonedRoot);
                // treeViewFolders.ExpandAll();
            }
        }

        private TreeNode CloneNode(TreeNode original)
        {
            bool includeNode = ShouldIncludeNode(original);

            TreeNode cloned = null;
            if (includeNode)
            {
                cloned = new TreeNode(original.Text)
                {
                    Tag = original.Tag
                };
            }

            // If original is a category node and we are in item-selection mode,
            // append its market items as child nodes.
            if (_selectionMode == SelectionMode.ExpansionMarketItem &&
                original.Tag is ExpansionMarketCategory category)
            {
                if (cloned == null)
                {
                    cloned = new TreeNode(original.Text)
                    {
                        Tag = original.Tag
                    };
                }

                if (category.Items != null)
                {
                    foreach (var item in category.Items)
                    {
                        if (item == null)
                            continue;

                        string text = !string.IsNullOrWhiteSpace(item.ClassName)
                            ? item.ClassName
                            : "(unnamed item)";

                        TreeNode itemNode = new TreeNode(text)
                        {
                            Tag = item
                        };

                        cloned.Nodes.Add(itemNode);
                    }
                }

                return cloned;
            }

            // In category-file mode, stop at category files
            if (_selectionMode == SelectionMode.ExpansionMarketCategoryFile &&
                original.Tag is ExpansionMarketCategory)
            {
                return cloned;
            }


            foreach (TreeNode child in original.Nodes)
            {
                TreeNode clonedChild = CloneNode(child);
                if (clonedChild != null)
                {
                    if (cloned == null)
                    {
                        cloned = new TreeNode(original.Text)
                        {
                            Tag = original.Tag
                        };
                    }

                    cloned.Nodes.Add(clonedChild);
                }
            }

            return cloned;
        }

        private bool ShouldIncludeNode(TreeNode node)
        {
            switch (_selectionMode)
            {
                case SelectionMode.Folder:
                    // Exclude category files and market items
                    return !(node.Tag is ExpansionMarketCategory) &&
                           !(node.Tag is ExpansionMarketItem);

                case SelectionMode.ExpansionMarketCategoryFile:
                    // Include folder/path nodes and category files
                    return node.Tag is string ||
                           node.Tag is ExpansionMarketCategory;

                case SelectionMode.ExpansionMarketItem:
                    // Include folders and category files for navigation.
                    // Market item nodes are added manually from the category.
                    return node.Tag is string ||
                           node.Tag is ExpansionMarketCategory;

                default:
                    return false;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewFolders.SelectedNode;

            switch (_selectionMode)
            {
                case SelectionMode.Folder:
                    if (selectedNode == null)
                    {
                        MessageBox.Show("Please select a valid folder.");
                        return;
                    }

                    if (selectedNode.Parent == null)
                    {
                        SelectedCategoryTag = "MarketCategoryRelativePath:";
                        DialogResult = DialogResult.OK;
                        return;
                    }

                    if (selectedNode.Tag is string folderTag &&
                        folderTag.StartsWith("MarketCategoryRelativePath:"))
                    {
                        SelectedCategoryTag = folderTag;
                        DialogResult = DialogResult.OK;
                        return;
                    }

                    MessageBox.Show("Please select a valid folder.");
                    return;

                case SelectionMode.ExpansionMarketCategoryFile:

                    var checkedCategories = GetCheckedCategories();

                    if (checkedCategories.Count > 0)
                    {
                        SelectedExpansionMarketCategories = checkedCategories;
                        SelectedExpansionMarketCategory = checkedCategories.First();

                        DialogResult = DialogResult.OK;
                        return;
                    }

                    MessageBox.Show("Please select one or more ExpansionMarketCategory files.");
                    return;

                case SelectionMode.ExpansionMarketItem:
                    var checkedItems = GetCheckedMarketItems();

                    if (checkedItems.Count > 0)
                    {
                        SelectedExpansionMarketItems = checkedItems;
                        DialogResult = DialogResult.OK;
                        return;
                    }

                    // fallback: allow single selected item even if not checked
                    if (selectedNode?.Tag is ExpansionMarketItem singleItem)
                    {
                        SelectedExpansionMarketItems = new List<ExpansionMarketItem> { singleItem };
                        DialogResult = DialogResult.OK;
                        return;
                    }

                    MessageBox.Show("Please select one or more ExpansionMarketItem entries.");
                    return;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void treeViewFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (_selectionMode)
            {
                case SelectionMode.Folder:
                    buttonOK.Enabled =
                        e.Node.Parent == null ||
                        (e.Node.Tag is string folderTag &&
                         folderTag.StartsWith("MarketCategoryRelativePath:"));
                    break;

                case SelectionMode.ExpansionMarketCategoryFile:
                case SelectionMode.ExpansionMarketItem:
                    treeViewFolders.SelectedNode = null;
                    break;
            }
        }

        private void treeViewFolders_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (_selectionMode != SelectionMode.ExpansionMarketItem &&
                _selectionMode != SelectionMode.ExpansionMarketCategoryFile)
                return;

            if (_suppressAfterCheck)
                return;

            try
            {
                _suppressAfterCheck = true;

                switch (_selectionMode)
                {
                    case SelectionMode.ExpansionMarketCategoryFile:

                        if (!(e.Node.Tag is ExpansionMarketCategory))
                        {
                            e.Node.Checked = false;
                            return;
                        }

                        break;

                    case SelectionMode.ExpansionMarketItem:

                        if (!(e.Node.Tag is ExpansionMarketItem))
                        {
                            e.Node.Checked = false;
                            return;
                        }

                        break;
                }

                // Single-select mode
                if (_singleSelect && e.Node.Checked)
                {
                    UncheckAllExcept(treeViewFolders.Nodes, e.Node);
                }
            }
            finally
            {
                _suppressAfterCheck = false;
            }

            UpdateOkButton();
        }
        private void UncheckAllExcept(TreeNodeCollection nodes, TreeNode keepNode)
        {
            foreach (TreeNode node in nodes)
            {
                if (!ReferenceEquals(node, keepNode))
                    node.Checked = false;

                UncheckAllExcept(node.Nodes, keepNode);
            }
        }

        private void UpdateOkButton()
        {
            switch (_selectionMode)
            {
                case SelectionMode.ExpansionMarketCategoryFile:
                    buttonOK.Enabled = GetCheckedCategories().Count > 0;
                    break;

                case SelectionMode.ExpansionMarketItem:
                    buttonOK.Enabled = GetCheckedMarketItems().Count > 0;
                    break;
            }
        }
        private List<ExpansionMarketCategory> GetCheckedCategories()
        {
            var results = new List<ExpansionMarketCategory>();

            foreach (TreeNode rootNode in treeViewFolders.Nodes)
            {
                CollectCheckedCategories(rootNode, results);
            }

            return results;
        }

        private void CollectCheckedCategories(
            TreeNode node,
            List<ExpansionMarketCategory> results)
        {
            if (node.Checked && node.Tag is ExpansionMarketCategory category)
            {
                results.Add(category);
            }

            foreach (TreeNode child in node.Nodes)
            {
                CollectCheckedCategories(child, results);
            }
        }
        private List<ExpansionMarketItem> GetCheckedMarketItems()
        {
            var results = new List<ExpansionMarketItem>();

            foreach (TreeNode rootNode in treeViewFolders.Nodes)
            {
                CollectCheckedMarketItems(rootNode, results);
            }

            return results;
        }

        private void CollectCheckedMarketItems(TreeNode node, List<ExpansionMarketItem> results)
        {
            if (node.Checked && node.Tag is ExpansionMarketItem item)
            {
                results.Add(item);
            }

            foreach (TreeNode child in node.Nodes)
            {
                CollectCheckedMarketItems(child, results);
            }
        }

        private void treeViewFolders_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeViewHitTestInfo hit = treeViewFolders.HitTest(e.Location);

            if (hit.Location != TreeViewHitTestLocations.StateImage)
            {
                e.Node.Checked = !e.Node.Checked;
            }
        }

        private void treeViewFolders_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            bool checkBoxMode =
               _selectionMode == SelectionMode.ExpansionMarketItem ||
               _selectionMode == SelectionMode.ExpansionMarketCategoryFile;
            if(checkBoxMode) e.Cancel = true;
        }
    }
}
