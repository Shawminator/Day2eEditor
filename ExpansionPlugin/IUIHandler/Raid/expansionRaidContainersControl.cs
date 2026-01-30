using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class expansionRaidContainersControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionRaidSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public expansionRaidContainersControl()
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
            _data = data as ExpansionRaidSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            CanRaidLocksOnContainersCB.Checked = _data.CanRaidLocksOnContainers == 1 ? true : false;
            LockOnContainerRaidUseScheduleCB.Checked = _data.LockOnContainerRaidUseSchedule == 1 ? true : false;
            LockOnContainerRaidToolTimeSecondsNUD.Value = (decimal)_data.LockOnContainerRaidToolTimeSeconds;
            LockOnContainerRaidToolCyclesNUD.Value = (decimal)_data.LockOnContainerRaidToolCycles;
            LockOnContainerRaidToolDamagePercentNUD.Value = (decimal)_data.LockOnContainerRaidToolDamagePercent;

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
                // TODO: Update _nodes.Last().Text based on _data
            }
        }

        #endregion

        private void CanRaidLocksOnContainersCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.CanRaidLocksOnContainers = CanRaidLocksOnContainersCB.Checked == true ? 1 : 0;
            
        }

        private void LockOnContainerRaidUseScheduleCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.LockOnContainerRaidUseSchedule = LockOnContainerRaidUseScheduleCB.Checked == true ? 1 : 0;
            
        }

        private void LockOnContainerRaidToolTimeSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.LockOnContainerRaidToolTimeSeconds = (int)LockOnContainerRaidToolTimeSecondsNUD.Value;
            
        }

        private void LockOnContainerRaidToolCyclesNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.LockOnContainerRaidToolCycles = (int)LockOnContainerRaidToolCyclesNUD.Value;
            
        }

        private void LockOnContainerRaidToolDamagePercentNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.LockOnContainerRaidToolDamagePercent = (int)LockOnContainerRaidToolDamagePercentNUD.Value;
            
        }
    }
}