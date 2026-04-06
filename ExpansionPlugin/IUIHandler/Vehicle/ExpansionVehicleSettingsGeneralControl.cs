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
    public partial class ExpansionVehicleSettingsGeneralControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionVehicleSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionVehicleSettingsGeneralControl()
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
            _data = data as ExpansionVehicleSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            VehicleSyncComboBox.DataSource = Enum.GetValues(typeof(ExpansionVehicleNetworkMode));
            PlacePlayerOnGroundOnReconnectInVehicleComboBox.DataSource = Enum.GetValues(typeof(ExpansionPPOGORIVMode));

            VehicleSyncComboBox.SelectedItem = (ExpansionVehicleNetworkMode)_data.VehicleSync;
            EnableWindAerodynamicsCB.Checked = _data.EnableWindAerodynamics == 1 ? true : false;
            EnableTailRotorDamageCB.Checked = _data.EnableTailRotorDamage == 1 ? true : false;
            EnableHelicopterExplosionsCB.Checked = _data.EnableHelicopterExplosions == 1 ? true : false;
            DisableVehicleDamageCB.Checked = _data.DisableVehicleDamage == 1 ? true : false;
            VehicleCrewDamageMultiplierNUD.Value = (decimal)_data.VehicleCrewDamageMultiplier;
            VehicleSpeedDamageMultiplierNUD.Value = (decimal)_data.VehicleSpeedDamageMultiplier;
            VehicleRoadKillDamageMultiplierNUD.Value = (decimal)_data.VehicleRoadKillDamageMultiplier;
            CollisionDamageIfEngineOffCB.Checked = _data.CollisionDamageIfEngineOff == 1 ? true : false;
            CollisionDamageMinSpeedKmhNUD.Value = (decimal)_data.CollisionDamageMinSpeedKmh;
            PlacePlayerOnGroundOnReconnectInVehicleComboBox.SelectedItem = (ExpansionPPOGORIVMode)_data.PlacePlayerOnGroundOnReconnectInVehicle;
            RevvingOverMaxRPMRuinsEngineInstantlyCB.Checked = _data.RevvingOverMaxRPMRuinsEngineInstantly == 1 ? true : false;
            VehicleDropsRuinedDoorsCB.Checked = _data.VehicleDropsRuinedDoors == 1 ? true : false;
            ExplodingVehicleDropsAttachmentsCB.Checked = _data.ExplodingVehicleDropsAttachments == 1 ? true : false;
            DesyncInvulnerabilityTimeoutSecondsNUD.Value = (decimal)_data.DesyncInvulnerabilityTimeoutSeconds;
            PilotlessAutoHoverEngineStopDelaySecondsNUD.Value = (decimal)_data.PilotlessAutoHoverEngineStopDelaySeconds;
            RoughLandingVerticalSpeedThresholdNUD.Value = (decimal)_data.RoughLandingVerticalSpeedThreshold;
            DamagedEngineStartupChancePercentNUD.Value = (decimal)_data.DamagedEngineStartupChancePercent;
            TowingCB.Checked = _data.Towing == 1 ? true : false;
            ShowVehicleOwnersCB.Checked = _data.ShowVehicleOwners == 1 ? true : false;
            FuelConsumptionPercentNUD.Value = (decimal)_data.FuelConsumptionPercent;

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
        private void VehicleSyncComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.VehicleSync = (ExpansionVehicleNetworkMode)VehicleSyncComboBox.SelectedItem;
        }
        private void EnableWindAerodynamicsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.EnableWindAerodynamics = EnableWindAerodynamicsCB.Checked ? 1 : 0;
        }
        private void EnableTailRotorDamageCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.EnableTailRotorDamage = EnableTailRotorDamageCB.Checked ? 1 : 0;
        }
        private void EnableHelicopterExplosionsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.EnableHelicopterExplosions = EnableHelicopterExplosionsCB.Checked ? 1 : 0;
        }
        private void DisableVehicleDamageCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.DisableVehicleDamage = DisableVehicleDamageCB.Checked ? 1 : 0;
        }
        private void VehicleCrewDamageMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.VehicleCrewDamageMultiplier = (decimal)VehicleCrewDamageMultiplierNUD.Value;
        }
        private void VehicleSpeedDamageMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.VehicleSpeedDamageMultiplier = (decimal)VehicleSpeedDamageMultiplierNUD.Value;
        }
        private void VehicleRoadKillDamageMultiplierNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.VehicleRoadKillDamageMultiplier = (decimal)VehicleRoadKillDamageMultiplierNUD.Value;
        }
        private void CollisionDamageIfEngineOffCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.CollisionDamageIfEngineOff = CollisionDamageIfEngineOffCB.Checked ? 1 : 0;
        }
        private void CollisionDamageMinSpeedKmhNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.CollisionDamageMinSpeedKmh = (decimal)CollisionDamageMinSpeedKmhNUD.Value;
        }
        private void PlacePlayerOnGroundOnReconnectInVehicleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.PlacePlayerOnGroundOnReconnectInVehicle = (ExpansionPPOGORIVMode)PlacePlayerOnGroundOnReconnectInVehicleComboBox.SelectedItem;
        }
        private void RevvingOverMaxRPMRuinsEngineInstantlyCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.RevvingOverMaxRPMRuinsEngineInstantly = RevvingOverMaxRPMRuinsEngineInstantlyCB.Checked == true ? 1 : 0;
        }
        private void VehicleDropsRuinedDoorsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.VehicleDropsRuinedDoors = VehicleDropsRuinedDoorsCB.Checked == true ? 1 : 0;
        }
        private void ExplodingVehicleDropsAttachmentsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.ExplodingVehicleDropsAttachments = ExplodingVehicleDropsAttachmentsCB.Checked == true ? 1 : 0;
        }
        private void DesyncInvulnerabilityTimeoutSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.DesyncInvulnerabilityTimeoutSeconds = (decimal)DesyncInvulnerabilityTimeoutSecondsNUD.Value;
        }
        private void PilotlessAutoHoverEngineStopDelaySecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.PilotlessAutoHoverEngineStopDelaySeconds = (decimal)PilotlessAutoHoverEngineStopDelaySecondsNUD.Value;
        }
        private void RoughLandingVerticalSpeedThresholdNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.RoughLandingVerticalSpeedThreshold = (decimal)RoughLandingVerticalSpeedThresholdNUD.Value;
        }
        private void DamagedEngineStartupChancePercentNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.DamagedEngineStartupChancePercent = (decimal)DamagedEngineStartupChancePercentNUD.Value;
        }
        private void TowingCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.Towing = TowingCB.Checked == true ? 1 : 0;
        }
        private void ShowVehicleOwnersCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.ShowVehicleOwners = ShowVehicleOwnersCB.Checked == true ? 1 : 0;
        }
        private void FuelConsumptionPercentNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.FuelConsumptionPercent = (decimal)FuelConsumptionPercentNUD.Value;
        }
    }
}