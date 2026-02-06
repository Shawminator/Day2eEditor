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
    public partial class ExpansionSafeZoneSettingsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionSafeZoneSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionSafeZoneSettingsControl()
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
            _data = data as ExpansionSafeZoneSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            EnabledCB.Checked = _data.Enabled == 1 ? true : false;
            FrameRateCheckSafeZoneInMsNUD.Value = (decimal)_data.FrameRateCheckSafeZoneInMs;
            DisablePlayerCollisionCB.Checked = _data.DisablePlayerCollision == 1 ? true : false;
            DisableVehicleDamageInSafeZoneCB.Checked = _data.DisableVehicleDamageInSafeZone == 1 ? true : false;
            EnableForceSZCleanupCB.Checked = _data.EnableForceSZCleanup == 1 ? true : false;
            ItemLifetimeInSafeZoneNUD.Value = (decimal)_data.ItemLifetimeInSafeZone;
            ActorsPerTickNUD.Value = (int)_data.ActorsPerTick;
            EnableForceSZCleanupVehiclesCB.Checked = _data.EnableForceSZCleanupVehicles == 1 ? true : false;
            VehicleLifetimeInSafeZoneNUD.Value = (decimal)_data.VehicleLifetimeInSafeZone;

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
        private void EnabledCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.Enabled = EnabledCB.Checked == true ? 1 : 0;
        }
        private void FrameRateCheckSafeZoneInMsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.FrameRateCheckSafeZoneInMs = (int)FrameRateCheckSafeZoneInMsNUD.Value;
        }
        private void ActorsPerTickNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.ActorsPerTick = (int)ActorsPerTickNUD.Value;
        }
        private void DisablePlayerCollisionCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.DisablePlayerCollision = DisablePlayerCollisionCB.Checked == true ? 1 : 0;
        }
        private void DisableVehicleDamageInSafeZoneCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
             _data.DisableVehicleDamageInSafeZone = DisableVehicleDamageInSafeZoneCB.Checked == true ? 1 : 0;
        }
        private void EnableForceSZCleanupCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.EnableForceSZCleanup = EnableForceSZCleanupCB.Checked == true ? 1 : 0;
        }
        private void ItemLifetimeInSafeZoneNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.ItemLifetimeInSafeZone = ItemLifetimeInSafeZoneNUD.Value;
        }
        private void EnableForceSZCleanupVehiclesCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.EnableForceSZCleanupVehicles = EnableForceSZCleanupVehiclesCB.Checked == true ? 1 : 0;
        }
        private void VehicleLifetimeInSafeZoneNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.VehicleLifetimeInSafeZone = VehicleLifetimeInSafeZoneNUD.Value;
        }
    }
}