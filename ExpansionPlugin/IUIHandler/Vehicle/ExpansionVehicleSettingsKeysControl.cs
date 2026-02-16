using Day2eEditor;
using ExpansionPlugin;
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
    public partial class ExpansionVehicleSettingsKeysControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionVehicleSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionVehicleSettingsKeysControl()
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

            VehicleRequireKeyToStartComboBox.DataSource = Enum.GetValues(typeof(ExpansionVehicleKeyStartMode));
            MasterKeyPairingModeComboBox.DataSource = Enum.GetValues(typeof(ExpansionMasterKeyPairingMode));


            VehicleRequireKeyToStartComboBox.SelectedItem = (ExpansionVehicleKeyStartMode)_data.VehicleRequireKeyToStart;
            VehicleRequireAllDoorsCB.Checked = _data.VehicleRequireAllDoors == 1 ? true : false;
            VehicleLockedAllowInventoryAccessCB.Checked = _data.VehicleLockedAllowInventoryAccess == 1 ? true : false;
            VehicleLockedAllowInventoryAccessWithoutDoorsCB.Checked = _data.VehicleLockedAllowInventoryAccessWithoutDoors == 1 ? true : false;
            MasterKeyPairingModeComboBox.SelectedItem = (ExpansionMasterKeyPairingMode)_data.MasterKeyPairingMode;
            MasterKeyUsesNUD.Value = (int)_data.MasterKeyUses;

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

        private void VehicleRequireKeyToStartComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.VehicleRequireKeyToStart = (ExpansionVehicleKeyStartMode)VehicleRequireKeyToStartComboBox.SelectedItem;
        }
        private void VehicleRequireAllDoorsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
        }
        private void VehicleLockedAllowInventoryAccessCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
        }
        private void VehicleLockedAllowInventoryAccessWithoutDoorsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
        }
        private void MasterKeyPairingModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.MasterKeyPairingMode = (ExpansionMasterKeyPairingMode)MasterKeyPairingModeComboBox.SelectedItem;
        }
        private void MasterKeyUsesNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
        }
    }
}