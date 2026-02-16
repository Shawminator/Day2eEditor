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
    }
}