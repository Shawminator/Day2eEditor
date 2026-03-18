using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ExpansionPlugin
{
    public partial class SelectCategoryFolderForm : Form
    {
        public string SelectedCategoryTag { get; private set; }

        public SelectCategoryFolderForm(TreeNode sourceRootNode)
        {
            InitializeComponent();
            CloneTree(sourceRootNode);
        }

        private void CloneTree(TreeNode sourceRootNode)
        {
            treeViewFolders.Nodes.Clear();

            TreeNode clonedRoot = CloneNode(sourceRootNode);
            if (clonedRoot != null)
            {
                treeViewFolders.Nodes.Add(clonedRoot);
                treeViewFolders.ExpandAll();
            }
        }

        private TreeNode CloneNode(TreeNode original)
        {
            if (original.Tag is ExpansionMarketCategory)
                return null;

            TreeNode cloned = new TreeNode(original.Text)
            {
                Tag = original.Tag
            };

            foreach (TreeNode child in original.Nodes)
            {
                TreeNode clonedChild = CloneNode(child);
                if (clonedChild != null)
                {
                    cloned.Nodes.Add(clonedChild);
                }
            }

            return cloned;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (treeViewFolders.SelectedNode?.Tag is string tag &&
                tag.StartsWith("MarketCategoryRelativePath:"))
            {
                SelectedCategoryTag = tag;
                DialogResult = DialogResult.OK;
                return;
            }

            MessageBox.Show("Please select a valid folder.");
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
