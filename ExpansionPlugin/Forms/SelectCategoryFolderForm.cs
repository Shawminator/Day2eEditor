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

        public SelectCategoryFolderForm(TreeView sourceTree)
        {
            InitializeComponent();
            CloneTree(sourceTree);
        }

        private void CloneTree(TreeView source)
        {
            treeViewFolders.Nodes.Clear();

            foreach (TreeNode node in source.Nodes)
            {
                treeViewFolders.Nodes.Add(CloneNode(node));
            }

            treeViewFolders.ExpandAll();
        }

        private TreeNode CloneNode(TreeNode original)
        {
            TreeNode cloned = new TreeNode(original.Text)
            {
                Tag = original.Tag
            };

            foreach (TreeNode child in original.Nodes)
            {
                cloned.Nodes.Add(CloneNode(child));
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
