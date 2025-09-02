using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class cfggameplayMapDataControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private CFGGameplayMapData _data;
        private CFGGameplayMapData _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfggameplayMapDataControl()
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
            _data = data as CFGGameplayMapData ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            ignoreMapOwnershipCB.Checked = _data.ignoreMapOwnership;
            ignoreNavItemsOwnershipCB.Checked = _data.ignoreNavItemsOwnership;
            displayPlayerPositionCB.Checked = _data.displayPlayerPosition;
            displayNavInfoCB.Checked = _data.displayNavInfo;

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
            var parentObj = _nodes.Last().FindParentOfType(_parentType);
            if (parentObj != null)
            {
                dynamic parent = parentObj;
                parent.isDirty = !_data.Equals(_originalData);
            }
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>
        private CFGGameplayMapData CloneData(CFGGameplayMapData data)
        {
            // TODO: Implement actual cloning logic
            return new CFGGameplayMapData
            {
                ignoreMapOwnership = data.ignoreMapOwnership,
                ignoreNavItemsOwnership = data.ignoreNavItemsOwnership,
                displayPlayerPosition = data.displayPlayerPosition,
                displayNavInfo = data.displayNavInfo,
            };
        }

        #endregion

        private void ignoreMapOwnershipCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ignoreMapOwnership = ignoreMapOwnershipCB.Checked;
            HasChanges();
        }

        private void ignoreNavItemsOwnershipCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ignoreNavItemsOwnership = ignoreNavItemsOwnershipCB.Checked;
            HasChanges();
        }

        private void displayPlayerPositionCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.displayPlayerPosition = displayPlayerPositionCB.Checked;
            HasChanges();
        }

        private void displayNavInfoCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.displayNavInfo = displayNavInfoCB.Checked;
            HasChanges();
        }
    }
}