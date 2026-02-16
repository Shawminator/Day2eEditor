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
    public partial class ExpansionVehicleSettingsCoversControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionVehicleSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionVehicleSettingsCoversControl()
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

            EnableVehicleCoversCB.Checked = _data.EnableVehicleCovers == 1 ? true : false;
            AllowCoveringDEVehiclesCB.Checked = _data.AllowCoveringDEVehicles == 1 ? true : false;
            CanCoverWithCargoCB.Checked = _data.CanCoverWithCargo == 1 ? true : false;
            UseVirtualStorageForCoverCargoCB.Checked = _data.UseVirtualStorageForCoverCargo == 1 ? true : false;
            VehicleAutoCoverTimeSecondsNUD.Value = (int)_data.VehicleAutoCoverTimeSeconds;
            VehicleAutoCoverRequireCamonetCB.Checked = _data.VehicleAutoCoverRequireCamonet == 1 ? true : false;
            EnableAutoCoveringDEVehiclesCB.Checked = _data.EnableAutoCoveringDEVehicles == 1 ? true : false;

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

        private void EnableVehicleCoversCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.EnableVehicleCovers = EnableVehicleCoversCB.Checked == true ? 1 : 0;
        }
        private void AllowCoveringDEVehiclesCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.AllowCoveringDEVehicles = AllowCoveringDEVehiclesCB.Checked == true ? 1 : 0;
        }
        private void CanCoverWithCargoCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.CanCoverWithCargo = CanCoverWithCargoCB.Checked == true ? 1 : 0;
        }
        private void UseVirtualStorageForCoverCargoCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.UseVirtualStorageForCoverCargo = UseVirtualStorageForCoverCargoCB.Checked == true ? 1 : 0;
        }
        private void VehicleAutoCoverTimeSecondsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.VehicleAutoCoverTimeSeconds = VehicleAutoCoverTimeSecondsNUD.Value;
        }
        private void VehicleAutoCoverRequireCamonetCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.VehicleAutoCoverRequireCamonet = VehicleAutoCoverRequireCamonetCB.Checked == true ? 1 : 0;
        }
        private void EnableAutoCoveringDEVehiclesCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.EnableAutoCoveringDEVehicles = EnableAutoCoveringDEVehiclesCB.Checked == true ? 1 : 0;
        }
    }
}