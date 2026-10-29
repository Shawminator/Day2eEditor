using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace CaparePlugin
{
    /// <summary>
    /// Lets the user pick which magazines and attachments from the Universal Attachment dump
    /// are copied onto a defined item
    /// </summary>
    public partial class CapareDefinedItemAttachmentPreview : Form
    {
        private FormController controller;
        private bool _updatingChecks = false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DumpItem DumpItem { get; set; }

        /// <summary>
        /// Checked magazines, filled when OK is pressed
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<string> SelectedMagazines { get; private set; } = new();

        /// <summary>
        /// One list per slot with at least one checked item, filled when OK is pressed
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<List<string>> SelectedSlots { get; private set; } = new();

        public CapareDefinedItemAttachmentPreview()
        {
            InitializeComponent();
            controller = new FormController(
                this,
                panel1,
                null,
                TitleLabel,
                null,
                CloseButton,
                null
            );
        }

        private void CapareDefinedItemAttachmentPreview_Load(object sender, EventArgs e)
        {
            TitleLabel.Text = $"Universal Attachments : {DumpItem.name}";

            if (DumpItem.magazines.Count > 0)
            {
                TreeNode magazinesTN = new TreeNode("Magazines") { Tag = "Magazines" };
                foreach (string mag in DumpItem.magazines)
                    magazinesTN.Nodes.Add(new TreeNode(mag) { Tag = mag });
                treeView1.Nodes.Add(magazinesTN);
            }

            // Slots with nothing compatible are left out, they would only add empty entries
            foreach (DumpSlot slot in DumpItem.attachmentSlots.Where(x => x.compatibleItems.Count > 0))
            {
                TreeNode slotTN = new TreeNode(slot.slotName) { Tag = slot };
                foreach (string item in slot.compatibleItems)
                    slotTN.Nodes.Add(new TreeNode(item) { Tag = item });
                treeView1.Nodes.Add(slotTN);
            }

            _updatingChecks = true;
            CheckAllNodes(treeView1.Nodes, true);
            _updatingChecks = false;
            treeView1.ExpandAll();
            if (treeView1.Nodes.Count > 0)
                treeView1.Nodes[0].EnsureVisible();
        }
        private void CheckAllNodes(TreeNodeCollection nodes, bool checkState)
        {
            foreach (TreeNode node in nodes)
            {
                node.Checked = checkState;
                CheckAllNodes(node.Nodes, checkState);
            }
        }
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (_updatingChecks)
                return;

            _updatingChecks = true;
            try
            {
                // Group nodes toggle all their items, items re-check their group
                foreach (TreeNode child in e.Node.Nodes)
                    child.Checked = e.Node.Checked;

                if (e.Node.Checked && e.Node.Parent != null)
                    e.Node.Parent.Checked = true;
            }
            finally
            {
                _updatingChecks = false;
            }
        }
        private void OKButton_Click(object sender, EventArgs e)
        {
            SelectedMagazines.Clear();
            SelectedSlots.Clear();

            foreach (TreeNode groupNode in treeView1.Nodes)
            {
                List<string> checkedItems = groupNode.Nodes
                    .Cast<TreeNode>()
                    .Where(n => n.Checked)
                    .Select(n => (string)n.Tag)
                    .ToList();

                if (groupNode.Tag is DumpSlot)
                {
                    if (checkedItems.Count > 0)
                        SelectedSlots.Add(checkedItems);
                }
                else
                {
                    SelectedMagazines.AddRange(checkedItems);
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
