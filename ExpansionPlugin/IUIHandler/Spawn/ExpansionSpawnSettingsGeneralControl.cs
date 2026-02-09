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
    public partial class ExpansionSpawnSettingsGeneralControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionSpawnSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionSpawnSettingsGeneralControl()
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
            _data = data as ExpansionSpawnSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            EnableSpawnSelectionCB.Checked = _data.EnableSpawnSelection == 1 ? true : false;
            SpawnOnTerritoryCB.Checked = _data.SpawnOnTerritory == 1 ? true : false;
            SpawnHealthValueNUD.Value = (decimal)_data.SpawnHealthValue;
            SpawnEnergyValueNUD.Value = (decimal)_data.SpawnEnergyValue;
            SpawnWaterValueNUD.Value = (decimal)_data.SpawnWaterValue;
            EnableRespawnCooldownsCB.Checked = _data.EnableRespawnCooldowns == 1 ? true : false;
            RespawnCooldownNUD.Value = (int)_data.RespawnCooldown;
            TerritoryRespawnCooldownNUD.Value = (int)_data.TerritoryRespawnCooldown;
            PunishMultispawnCB.Checked = _data.PunishMultispawn == 1 ? true : false;
            PunishCooldownNUD.Value = (int)_data.PunishCooldown;
            PunishTimeframeNUD.Value = (int)_data.PunishTimeframe;
            SpawnCreateDeathMarkerCB.Checked = _data.CreateDeathMarker == 1 ? true : false;
            BackgroundImagePathTB.Text = _data.BackgroundImagePath;

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

        private void PunishMultispawnCB_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}