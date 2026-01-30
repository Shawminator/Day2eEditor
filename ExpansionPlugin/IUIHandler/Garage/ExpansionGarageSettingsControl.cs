using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionGarageSettingsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionGarageSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionGarageSettingsControl()
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
            _data = data as ExpansionGarageSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;
            GarageModeCB.DataSource = Enum.GetValues(typeof(ExpansionGarageMode));
            GarageStoreModeCB.DataSource = Enum.GetValues(typeof(ExpansionGarageStoreMode));
            GarageRetrieveModeCB.DataSource = Enum.GetValues(typeof(ExpansionGarageRetrieveMode));
            GarageGroupStoreModeCB.DataSource = Enum.GetValues(typeof(ExpansionGarageGroupStoreMode));
            GarageEnabledCB.Checked = _data.Enabled == 1 ? true : false;
            AllowStoringDEVehiclesCB.Checked = _data.AllowStoringDEVehicles == 1 ? true : false;
            GarageModeCB.SelectedItem = (ExpansionGarageMode)_data.GarageMode;
            GarageStoreModeCB.SelectedItem = (ExpansionGarageStoreMode)_data.GarageStoreMode;
            GarageRetrieveModeCB.SelectedItem = (ExpansionGarageRetrieveMode)_data.GarageRetrieveMode;
            MaxStorableVehiclesNUD.Value = (int)_data.MaxStorableVehicles;
            GarageVehicleSearchRadiusNUD.Value = (decimal)_data.VehicleSearchRadius;
            GarageMaxDistanceFromStoredPositionNUD.Value = (decimal)_data.MaxDistanceFromStoredPosition;
            GarageCanStoreWithCargoCB.Checked = _data.CanStoreWithCargo == 1 ? true : false;
            GarageUseVirtualStorageForCargoCB.Checked = _data.UseVirtualStorageForCargo == 1 ? true : false;
            GarageNeedKeyToStoreCB.Checked = _data.NeedKeyToStore == 1 ? true : false;
            GarageEnableGroupFeaturesCB.Checked = _data.EnableGroupFeatures == 1 ? true : false;
            GarageGroupStoreModeCB.SelectedItem = (ExpansionGarageGroupStoreMode)_data.GroupStoreMode;
            GarageEnableMarketFeaturesCB.Checked = _data.EnableMarketFeatures == 1 ? true : false;
            GarageStorePricePercentNUD.Value = (decimal)_data.StorePricePercent;
            GarageStaticStorePriceNUD.Value = (int)_data.StaticStorePrice;
            GarageMaxStorableTier1NUD.Value = (int)_data.MaxStorableTier1;
            GarageMaxStorableTier2NUD.Value = (int)_data.MaxStorableTier2;
            GarageMaxStorableTier3NUD.Value = (int)_data.MaxStorableTier3;
            GarageMaxRangeTier1NUD.Value = (decimal)_data.MaxRangeTier1;
            GarageMaxRangeTier2NUD.Value = (decimal)_data.MaxRangeTier2;
            GarageMaxRangeTier3NUD.Value = (decimal)_data.MaxRangeTier3;
            GarageParkingMeterEnableFlavorCB.Checked = _data.ParkingMeterEnableFlavor == 1 ? true : false;

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

        private void GarageEnabledCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Enabled = GarageEnabledCB.Checked == true ? 1 : 0;
            
        }
        private void AllowStoringDEVehiclesCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AllowStoringDEVehicles = AllowStoringDEVehiclesCB.Checked == true ? 1 : 0;
            
        }
        private void GarageModeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            ExpansionGarageMode cacl = (ExpansionGarageMode)GarageModeCB.SelectedItem;
            _data.GarageMode = (int)cacl;
            
        }
        private void GarageStoreModeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            ExpansionGarageStoreMode cacl = (ExpansionGarageStoreMode)GarageStoreModeCB.SelectedItem;
            _data.GarageStoreMode = (int)cacl;
            
        }
        private void GarageRetrieveModeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            ExpansionGarageRetrieveMode cacl = (ExpansionGarageRetrieveMode)GarageRetrieveModeCB.SelectedItem;
            _data.GarageRetrieveMode = (int)cacl;
            
        }
        private void MaxStorableVehiclesNUD_ValueChanged(object sender, EventArgs e)
        {

            if (_suppressEvents) return;
            _data.MaxStorableVehicles = (int)MaxStorableVehiclesNUD.Value;
            
        }
        private void GarageVehicleSearchRadiusNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.VehicleSearchRadius = GarageVehicleSearchRadiusNUD.Value;
            
        }
        private void GarageMaxDistanceFromStoredPositionNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxDistanceFromStoredPosition = GarageMaxDistanceFromStoredPositionNUD.Value;
            
        }
        private void GarageCanStoreWithCargoCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CanStoreWithCargo = GarageCanStoreWithCargoCB.Checked == true ? 1 : 0;
            
        }
        private void GarageUseVirtualStorageForCargoCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseVirtualStorageForCargo = GarageUseVirtualStorageForCargoCB.Checked == true ? 1 : 0;
            
        }
        private void GarageNeedKeyToStoreCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NeedKeyToStore = GarageNeedKeyToStoreCB.Checked == true ? 1 : 0;
            
        }
        private void GarageEnableGroupFeaturesCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableGroupFeatures = GarageEnableGroupFeaturesCB.Checked == true ? 1 : 0;
            
        }
        private void GarageGroupStoreModeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            ExpansionGarageGroupStoreMode cacl = (ExpansionGarageGroupStoreMode)GarageGroupStoreModeCB.SelectedItem;
            _data.GroupStoreMode = (int)cacl;
            
        }
        private void GarageEnableMarketFeaturesCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableMarketFeatures = GarageEnableMarketFeaturesCB.Checked == true ? 1 : 0;
            
        }
        private void GarageStorePricePercentNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.StorePricePercent = GarageStorePricePercentNUD.Value;
            
        }
        private void GarageStaticStorePriceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.StaticStorePrice = (int)GarageStaticStorePriceNUD.Value;
            
        }
        private void GarageMaxStorableTier1NUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxStorableTier1 = (int)GarageMaxStorableTier1NUD.Value;
            
        }
        private void GarageMaxStorableTier2NUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxStorableTier2 = (int)GarageMaxStorableTier2NUD.Value;
            
        }
        private void GarageMaxStorableTier3NUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxStorableTier3 = (int)GarageMaxStorableTier3NUD.Value;
            
        }
        private void GarageMaxRangeTier1NUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxRangeTier1 = GarageMaxRangeTier1NUD.Value;
            
        }
        private void GarageMaxRangeTier2NUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxRangeTier2 = GarageMaxRangeTier2NUD.Value;
            
        }
        private void GarageMaxRangeTier3NUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxRangeTier3 = GarageMaxRangeTier3NUD.Value;
            
        }
        private void GarageParkingMeterEnableFlavorCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ParkingMeterEnableFlavor = GarageParkingMeterEnableFlavorCB.Checked == true ? 1 : 0;
            
        }
    }
}