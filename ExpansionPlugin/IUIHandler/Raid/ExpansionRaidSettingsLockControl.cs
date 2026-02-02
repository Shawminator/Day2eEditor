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
    public partial class ExpansionRaidSettingsLockControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionRaidSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionRaidSettingsLockControl()
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
            CanRaidLocksOnWallsCB.DataSource = Enum.GetValues(typeof(RaidLocksOnWallsEnum));
            CanRaidLocksOnWallsCB.SelectedItem = (RaidLocksOnWallsEnum)_data.CanRaidLocksOnWalls;
            CanRaidLocksOnFencesCB.Checked = (decimal)_data.CanRaidLocksOnFences == 1 ? true : false;
            CanRaidLocksOnTentsCB.Checked = (decimal)_data.CanRaidLocksOnTents == 1 ? true : false;
            LockOnWallRaidToolTimeSecondsNUD.Value = (decimal)_data.LockOnWallRaidToolTimeSeconds;
            LockOnFenceRaidToolTimeSecondsNUD.Value = (decimal)_data.LockOnFenceRaidToolTimeSeconds;
            LockOnTentRaidToolTimeSecondsNUD.Value = (decimal)_data.LockOnTentRaidToolTimeSeconds;
            LockRaidToolCyclesNUD.Value = (decimal)_data.LockRaidToolCycles;
            LockRaidToolDamagePercentNUD.Value = (decimal)_data.LockRaidToolDamagePercent;

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

        private void CanRaidLocksOnWallsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.CanRaidLocksOnWalls = (RaidLocksOnWallsEnum)CanRaidLocksOnWallsCB.SelectedItem;
        }

        private void CanRaidLocksOnTentsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.CanRaidLocksOnTents = CanRaidLocksOnTentsCB.Checked == true ? 1 : 0;
        }
        private void CanRaidLocksOnFencesCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.CanRaidLocksOnFences = CanRaidLocksOnFencesCB.Checked == true ? 1 : 0;
        }

        private void LockOnWallRaidToolTimeSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.LockOnWallRaidToolTimeSeconds = (int)LockOnWallRaidToolTimeSecondsNUD.Value;
        }

        private void LockOnFenceRaidToolTimeSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.LockOnFenceRaidToolTimeSeconds = (int)LockOnFenceRaidToolTimeSecondsNUD.Value;
        }

        private void LockOnTentRaidToolTimeSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.LockOnTentRaidToolTimeSeconds = (int)LockOnTentRaidToolTimeSecondsNUD.Value;
        }

        private void LockRaidToolCyclesNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.LockRaidToolCycles = (int)LockRaidToolCyclesNUD.Value;
        }

        private void LockRaidToolDamagePercentNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.LockRaidToolDamagePercent = LockRaidToolDamagePercentNUD.Value;
        }
    }
}