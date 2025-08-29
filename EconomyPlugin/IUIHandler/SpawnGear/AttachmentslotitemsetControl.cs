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
        private Attachmentslotitemset _data;
        private Attachmentslotitemset _originalData;
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
        public void LoadFromData(object data, List<TreeNode> selectedNodes)
        {
            // TODO: Replace ClassType with your actual type
            _data = data as Attachmentslotitemset ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;
            ItemAttachmentSlotNameCB.DataSource = File.ReadAllLines("Data\\SlotNames.txt").ToList();
            ItemAttachmentSlotNameCB.SelectedIndex = ItemAttachmentSlotNameCB.FindStringExact(_data.slotName);

            _suppressEvents = false;
        }

        /// <summary>
        /// Applies changes to the data and updates the original snapshot
        /// </summary>
        public void ApplyChanges()
        {
            _originalData = CloneData(_data);
        }

        /// <summary>
        /// Resets control fields to the original data
        /// </summary>
        public void Reset()
        {
            // TODO: Reset control fields to _originalData
        }

        /// <summary>
        /// Checks if there are changes and updates the parent file's dirty state
        /// </summary>
        public void HasChanges()
        {
            if (_nodes?.Any() != true) return;

            // TODO: Replace Parentfile with your actual parent type if different
            var ef = _nodes.Last().FindParentOfType<SpawnGearPresetFiles>();
            if (ef != null)
                ef.isDirty = !_data.Equals(_originalData);
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private Attachmentslotitemset CloneData(Attachmentslotitemset data)
        {
            return new Attachmentslotitemset
            {
                slotName = data.slotName
            };

        }

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
            var SpawnGearPresetFiles = _nodes.Last().FindParentOfType<SpawnGearPresetFiles>();
            if (_nodes.Last().Parent.Tag.ToString() == "SpawnGearAttachmentSlotItemSetsParent")
            {
                string Slotname = ItemAttachmentSlotNameCB.GetItemText(ItemAttachmentSlotNameCB.SelectedItem);
                if (!SpawnGearPresetFiles.attachmentSlotItemSets.Any(x => x.slotName == Slotname))
                {
                    _data.slotName = Slotname;
                    HasChanges();
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