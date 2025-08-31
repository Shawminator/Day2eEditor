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
    public partial class SpawnGearQuickBarSlotControl : UserControl, IUIHandler
    {
        private IHasQuikBarSlot _data;
        private IHasQuikBarSlot _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public SpawnGearQuickBarSlotControl()
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
            _data = data as IHasQuikBarSlot ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            quickBarSlotNUD.Value = _data.QuickBarSlot;

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
                ef.isDirty = _data.QuickBarSlot != _originalData.QuickBarSlot;
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private IHasQuikBarSlot CloneData(IHasQuikBarSlot data)
        {
            // TODO: Implement actual cloning logic
            // TODO: Implement actual cloning logic
            return new SimpleQuickBArSlotSnapshot
            {
                QuickBarSlot = data.QuickBarSlot
            };
        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }

        #endregion

        private void quickBarSlotNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.QuickBarSlot = (int)quickBarSlotNUD.Value;
            HasChanges();
        }
    }
    internal class SimpleQuickBArSlotSnapshot : IHasQuikBarSlot
    {
        public int QuickBarSlot { get; set; }
    }
}