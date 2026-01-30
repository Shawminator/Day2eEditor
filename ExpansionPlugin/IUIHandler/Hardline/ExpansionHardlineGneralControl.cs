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
    public partial class ExpansionHardlineGneralControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionHardlineSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionHardlineGneralControl()
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
            _data = data as ExpansionHardlineSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            DefaultItemRarityCB.DataSource = Enum.GetValues(typeof(ExpansionHardlineItemRarity));
            ShowHardlineHUDCB.Checked = _data.ShowHardlineHUD == 1 ? true : false;
            UseReputationCB.Checked = _data.UseReputation == 1 ? true : false;
            UseFactionReputationCB.Checked = _data.UseFactionReputation == 1 ? true : false;
            EnableFactionPersistenceCB.Checked = _data.EnableFactionPersistence == 1 ? true : false;
            EnableItemRarityCB.Checked = _data.EnableItemRarity == 1 ? true : false;
            UseItemRarityOnInventoryIconsCB.Checked = _data.UseItemRarityOnInventoryIcons == 1 ? true : false;
            UseItemRarityForMarketPurchaseNCB.Checked = _data.UseItemRarityForMarketPurchase == 1 ? true : false;
            UseItemRarityForMarketSellCB.Checked = _data.UseItemRarityForMarketSell == 1 ? true : false;
            DefaultItemRarityCB.SelectedItem = (ExpansionHardlineItemRarity)_data.DefaultItemRarity;
            ItemRarityParentSearchCB.Checked = _data.ItemRarityParentSearch == 1 ? true : false;
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

        private void ShowHardlineHUDCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShowHardlineHUD = ShowHardlineHUDCB.Checked == true ? 1 : 0;
            
        }
        private void UseReputationCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseReputation = UseReputationCB.Checked == true ? 1 : 0;
            
        }
        private void EnableItemRarityCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableItemRarity = EnableItemRarityCB.Checked == true ? 1 : 0;
            
        }
        private void UseItemRarityOnInventoryIconsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseItemRarityOnInventoryIcons = UseItemRarityOnInventoryIconsCB.Checked == true ? 1 : 0;
            
        }
        private void UseItemRarityForMarketPurchaseNCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseItemRarityForMarketPurchase = UseItemRarityForMarketPurchaseNCB.Checked == true ? 1 : 0;
            
        }
        private void UseItemRarityForMarketSellCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseItemRarityForMarketSell = UseItemRarityForMarketSellCB.Checked == true ? 1 : 0;
            
        }
        private void UseFactionReputationCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseFactionReputation = UseFactionReputationCB.Checked == true ? 1 : 0;
            
        }
        private void EnableFactionPersistenceCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableFactionPersistence = EnableFactionPersistenceCB.Checked == true ? 1 : 0;
            
        }
        private void DefaultItemRarityCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DefaultItemRarity = (int)(ExpansionHardlineItemRarity)DefaultItemRarityCB.SelectedItem;
            
        }
        private void ItemRarityParentSearchCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ItemRarityParentSearch = ItemRarityParentSearchCB.Checked == true ? 1 : 0;
            
        }
    }
}