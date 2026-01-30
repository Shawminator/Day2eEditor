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
    public partial class ExpansionPartySettingsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionPartySettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionPartySettingsControl()
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
            _data = data as ExpansionPartySettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            EnablePartiesCB.Checked = (int)_data.EnableParties == 1 ? true : false;
            MaxMembersInPartyNUD.Value = (int)_data.MaxMembersInParty;
            UseWholeMapForInviteListCB.Checked = (int)_data.UseWholeMapForInviteList == 1 ? true : false;
            ShowPartyMember3DMarkersCB.Checked = (int)_data.ShowPartyMember3DMarkers == 1 ? true : false;
            ShowDistanceUnderPartyMembersMarkersCB.Checked = (int)_data.ShowDistanceUnderPartyMembersMarkers == 1 ? true : false;
            ShowNameOnPartyMembersMarkersCB.Checked = (int)_data.ShowNameOnPartyMembersMarkers == 1 ? true : false;
            EnableQuickMarkerCB.Checked = (int)_data.EnableQuickMarker == 1 ? true : false;
            ShowDistanceUnderQuickMarkersCB.Checked = (int)_data.ShowDistanceUnderQuickMarkers == 1 ? true : false;
            ShowNameOnQuickMarkersCB.Checked = (int)_data.ShowNameOnQuickMarkers == 1 ? true : false;
            CanCreatePartyMarkersCB.Checked = (int)_data.CanCreatePartyMarkers == 1 ? true : false;
            ShowPartyMemberHUDCB.Checked = (int)_data.ShowPartyMemberHUD == 1 ? true : false;
            ShowHUDMemberBloodCB.Checked = (int)_data.ShowHUDMemberBlood == 1 ? true : false;
            ShowHUDMemberStatesCB.Checked = (int)_data.ShowHUDMemberStates == 1 ? true : false;
            ShowHUDMemberStanceCB.Checked = (int)_data.ShowHUDMemberStance == 1 ? true : false;
            ShowPartyMemberMapMarkersCB.Checked = (int)_data.ShowPartyMemberMapMarkers == 1 ? true : false;
            ShowHUDMemberDistanceCB.Checked = (int)_data.ShowHUDMemberDistance == 1 ? true : false;
            ForcePartyToHaveTagsCB.Checked = (int)_data.ForcePartyToHaveTags == 1 ? true : false;
            InviteCooldownNUD.Value = (int)_data.InviteCooldown;
            DisplayPartyTagCB.Checked = (int)_data.DisplayPartyTag == 1 ? true : false;

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

        private void PartySettingsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            CheckBox cb = sender as CheckBox;
            _data.SetIntValue(cb.Name.Substring(0, cb.Name.Length - 2), cb.Checked == true ? 1 : 0);
            
        }
        private void PartySettingsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            NumericUpDown nud = sender as NumericUpDown;
            _data.SetIntValue(nud.Name.Substring(0, nud.Name.Length - 3), (int)nud.Value);
            
        }
    }
}