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
    public partial class ExpansionBookGeneralControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionBookSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionBookGeneralControl()
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
            _data = data as ExpansionBookSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            EnableStatusTabCB.Checked = _data.EnableStatusTab == 1 ? true : false;
            EnablePartyTabCB.Checked = _data.EnablePartyTab == 1 ? true : false;
            EnableServerInfoTabCB.Checked = _data.EnableServerInfoTab == 1 ? true : false;
            EnableServerRulesTabCB.Checked = _data.EnableServerRulesTab == 1 ? true : false;
            EnableTerritoryTabCB.Checked = _data.EnableTerritoryTab == 1 ? true : false;
            EnableBookMenuCB.Checked = _data.EnableBookMenu == 1 ? true : false;
            CreateBookmarksCB.Checked = _data.CreateBookmarks == 1 ? true : false;
            DisplayServerSettingsInServerInfoTabCB.Checked = _data.DisplayServerSettingsInServerInfoTab == 1 ? true : false;
            ShowHaBStatsCB.Checked = _data.ShowHaBStats == 1 ? true : false;
            ShowPlayerFactionCB.Checked = _data.ShowPlayerFaction == 1 ? true : false;
            EnableCraftingRecipesTabCB.Checked = _data.EnableCraftingRecipesTab == 1 ? true : false;

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

        private void EnableStatusTabCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableStatusTab = EnableStatusTabCB.Checked == true ? 1 : 0;
            
        }
        private void EnablePartyTabCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnablePartyTab = EnablePartyTabCB.Checked == true ? 1 : 0;
            
        }
        private void EnableServerInfoTabCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableServerInfoTab = EnableServerInfoTabCB.Checked == true ? 1 : 0;
            
        }
        private void EnableCraftingRecipesTabCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableCraftingRecipesTab = EnableCraftingRecipesTabCB.Checked == true ? 1 : 0;
            
        }
        private void EnableServerRulesTabCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableServerRulesTab = EnableServerRulesTabCB.Checked == true ? 1 : 0;
            
        }
        private void EnableTerritoryTabCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableTerritoryTab = EnableTerritoryTabCB.Checked == true ? 1 : 0;
            
        }
        private void EnableBookMenuCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableBookMenu = EnableBookMenuCB.Checked == true ? 1 : 0;
            
        }
        private void CreateBookmarksCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CreateBookmarks = CreateBookmarksCB.Checked == true ? 1 : 0;
            
        }
        private void ShowHaBStatsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShowHaBStats = ShowHaBStatsCB.Checked == true ? 1 : 0;
            
        }
        private void ShowPlayerFactionCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShowPlayerFaction = ShowPlayerFactionCB.Checked == true ? 1 : 0;
            
        }
        private void DisplayServerSettingsInServerInfoTabCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DisplayServerSettingsInServerInfoTab = DisplayServerSettingsInServerInfoTabCB.Checked == true ? 1 : 0;
            
        }
    }
}