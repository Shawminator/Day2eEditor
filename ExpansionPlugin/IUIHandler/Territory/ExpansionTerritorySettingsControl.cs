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
    public partial class ExpansionTerritorySettingsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionTerritorySettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionTerritorySettingsControl()
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
            _data = data as ExpansionTerritorySettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            EnableTerritoriesTCB.Checked = _data.EnableTerritories == 1 ? true : false;
            UseWholeMapForInviteListTCB.Checked = _data.UseWholeMapForInviteList == 1 ? true : false;
            TerritorySizeTNUD.Value = (decimal)_data.TerritorySize;
            TerritoryPerimeterSizeTNUD.Value = (decimal)_data.TerritoryPerimeterSize;
            MaxMembersInTerritoryTNUD.Value = (int)_data.MaxMembersInTerritory;
            MaxTerritoryPerPlayerTNUD.Value = (int)_data.MaxTerritoryPerPlayer;
            TerritoryInviteAcceptRadiusTNUD.Value = (decimal)_data.TerritoryInviteAcceptRadius;
            AuthenticateCodeLockIfTerritoryMemberTCB.Checked = _data.AuthenticateCodeLockIfTerritoryMember == 1 ? true : false;
            TerritoryInviteCooldownNUD.Value = (int)_data.InviteCooldown;
            OnlyInviteGroupMemberTCB.Checked = _data.OnlyInviteGroupMember == 1 ? true : false;
            MaxCodeLocksOnBBPerTerritoryTNUD.Value = (int)_data.MaxCodeLocksOnBBPerTerritory;
            MaxCodeLocksOnItemsPerTerritoryTNUD.Value = (int)_data.MaxCodeLocksOnItemsPerTerritory;

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

        private void TerritoriesTCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            CheckBox cb = sender as CheckBox;
            _data.SetIntValue(cb.Name.Substring(0, cb.Name.Length - 3), cb.Checked == true ? 1 : 0);
        }
        private void TerritoryTNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            NumericUpDown nud = sender as NumericUpDown;
            if (nud.DecimalPlaces > 0)
                _data.SetdeciamlValue(nud.Name.Substring(0, nud.Name.Length - 4), nud.Value);
            else
                _data.SetIntValue(nud.Name.Substring(0, nud.Name.Length - 4), (int)nud.Value);
        }
    }
}