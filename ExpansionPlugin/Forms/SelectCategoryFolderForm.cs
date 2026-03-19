using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ExpansionPlugin
{
    public partial class SelectCategoryFolderForm : Form
    {
        public enum SelectionMode
        {
            Folder,
            ExpansionMarketCategoryFile
        }

        public string SelectedCategoryTag { get; private set; }
        public ExpansionMarketCategory SelectedExpansionMarketCategory { get; private set; }

        private readonly SelectionMode _selectionMode;

        public SelectCategoryFolderForm(TreeNode sourceRootNode, SelectionMode selectionMode)
        {
            InitializeComponent();
            _selectionMode = selectionMode;
            CloneTree(sourceRootNode);
        }

        private void CloneTree(TreeNode sourceRootNode)
        {
            treeViewFolders.Nodes.Clear();

            TreeNode clonedRoot = CloneNode(sourceRootNode);
            if (clonedRoot != null)
            {
                treeViewFolders.Nodes.Add(clonedRoot);
                //treeViewFolders.ExpandAll();
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

            // 🚀 STOP here if this is a category file node
            if (original.Tag is ExpansionMarketCategory)
                return cloned;

            foreach (TreeNode child in original.Nodes)
            {
                TreeNode clonedChild = CloneNode(child);
                if (clonedChild != null)
                {
                    if (cloned == null)
                    {
                        // Create parent container if needed so matching descendants stay visible
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
                    // Exclude ExpansionMarketCategory file nodes
                    return !(node.Tag is ExpansionMarketCategory);

                case SelectionMode.ExpansionMarketCategoryFile:
                    // Keep folder/path nodes so user can navigate,
                    // and include ExpansionMarketCategory file nodes
                    return node.Tag is string ||
                           node.Tag is ExpansionMarketCategory;

                default:
                    return false;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeViewFolders.SelectedNode;
            if (selectedNode == null)
            {
                MessageBox.Show("Please select a valid item.");
                return;
            }

            switch (_selectionMode)
            {
                case SelectionMode.Folder:
                    if (selectedNode.Parent == null)
                    {
                        SelectedCategoryTag = "MarketCategoryRelativePath:";
                        DialogResult = DialogResult.OK;
                        return;
                    }

                    if (selectedNode.Tag is string tag &&
                        tag.StartsWith("MarketCategoryRelativePath:"))
                    {
                        SelectedCategoryTag = tag;
                        DialogResult = DialogResult.OK;
                        return;
                    }

                    MessageBox.Show("Please select a valid folder.");
                    break;

                case SelectionMode.ExpansionMarketCategoryFile:
                    if (selectedNode.Tag is ExpansionMarketCategory expansionMarketCategory)
                    {
                        SelectedExpansionMarketCategory = expansionMarketCategory;
                        DialogResult = DialogResult.OK;
                        return;
                    }

                    MessageBox.Show("Please select an ExpansionMarketCategory file.");
                    break;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private void treeViewFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            buttonOK.Enabled =
                (_selectionMode == SelectionMode.Folder &&
                 (
                     e.Node.Parent == null ||
                     (e.Node.Tag is string tag &&
                      tag.StartsWith("MarketCategoryRelativePath:"))
                 ))
                ||
                (_selectionMode == SelectionMode.ExpansionMarketCategoryFile &&
                 e.Node.Tag is ExpansionMarketCategory);
        }
    }
}
