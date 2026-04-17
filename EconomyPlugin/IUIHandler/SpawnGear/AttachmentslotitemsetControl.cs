using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class AttachmentslotitemsetControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private Attachmentslotitemset _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public AttachmentslotitemsetControl()
        {
            InitializeComponent();
            
        }

        /// <summary>
        /// Returns the UserControl instance
        /// </summary>
        public Control GetControl() => this;

        /// <summary>
        /// Loads data into the control and stores the selected tree nodes
        /// </summary>
        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as Attachmentslotitemset ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;
            ItemAttachmentSlotNameCB.DataSource = File.ReadAllLines("Data\\VanillaSlotNames.txt").ToList();
            ItemAttachmentSlotNameCB.SelectedIndex = ItemAttachmentSlotNameCB.FindStringExact(_data.slotName);

            _suppressEvents = false;
        }


        #region Helper Methods


        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = _data.slotName;
            }
        }

        #endregion

        private void ItemAttachmentSlotNameCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            var SpawnGearPresetFiles = _nodes.Last().FindParentOfType<SpawnGearPresetFile>();
            if (_nodes.Last().Parent.Tag.ToString() == "SpawnGearAttachmentSlotItemSetsParent")
            {
                string Slotname = ItemAttachmentSlotNameCB.GetItemText(ItemAttachmentSlotNameCB.SelectedItem);
                if (!SpawnGearPresetFiles.Data.attachmentSlotItemSets.Any(x => x.slotName == Slotname))
                {
                    _data.slotName = Slotname;
                    UpdateTreeNodeText();
                }
                else
                {
                    MessageBox.Show("Slot Name allready in Use.....");
                }
            }
        }
    }
}