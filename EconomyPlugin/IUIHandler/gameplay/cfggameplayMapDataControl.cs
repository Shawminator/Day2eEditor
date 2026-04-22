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

            _suppressEvents = true;

            ignoreMapOwnershipCB.Checked = _data.ignoreMapOwnership;
            ignoreNavItemsOwnershipCB.Checked = _data.ignoreNavItemsOwnership;
            displayPlayerPositionCB.Checked = _data.displayPlayerPosition;
            displayNavInfoCB.Checked = _data.displayNavInfo;

            _suppressEvents = false;
        }
        private void ignoreMapOwnershipCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ignoreMapOwnership = ignoreMapOwnershipCB.Checked;
        }
        private void ignoreNavItemsOwnershipCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ignoreNavItemsOwnership = ignoreNavItemsOwnershipCB.Checked;
        }
        private void displayPlayerPositionCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.displayPlayerPosition = displayPlayerPositionCB.Checked;
        }
        private void displayNavInfoCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.displayNavInfo = displayNavInfoCB.Checked;
        }
    }
}