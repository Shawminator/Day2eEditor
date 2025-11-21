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
        private ExpansionHardlineSettings _originalData;
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
            _originalData = CloneData(_data); // Store original data for reset

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

        /// <summary>
        /// Applies changes to the data and updates the original snapshot
        /// </summary>
        public void ApplyChanges()
        {
            _originalData = CloneData(_data);
        }

        /// <summary>
        /// Resets control fields to the original data
        /// </summary>
        public void Reset()
        {
            // TODO: Reset control fields to _originalData
        }

        /// <summary>
        /// Checks if there are changes and updates the parent file's dirty state
        /// </summary>
        public void HasChanges()
        {
            var parentObj = _nodes.Last().FindParentOfType(_parentType);
            if (parentObj != null)
            {
                dynamic parent = parentObj;
                parent.isDirty = !_data.Equals(_originalData);
            }
        }

        #region Helper Methods

        /// <summary>
        /// Clones the data for reset purposes
        /// </summary>

        private ExpansionHardlineSettings CloneData(ExpansionHardlineSettings data)
        {
            if (data == null)
                return null;

            var clone = new ExpansionHardlineSettings
            {
                m_Version = data.m_Version,
                PoorItemRequirement = data.PoorItemRequirement,
                CommonItemRequirement = data.CommonItemRequirement,
                UncommonItemRequirement = data.UncommonItemRequirement,
                RareItemRequirement = data.RareItemRequirement,
                EpicItemRequirement = data.EpicItemRequirement,
                LegendaryItemRequirement = data.LegendaryItemRequirement,
                MythicItemRequirement = data.MythicItemRequirement,
                ExoticItemRequirement = data.ExoticItemRequirement,
                ShowHardlineHUD = data.ShowHardlineHUD,
                UseReputation = data.UseReputation,
                UseFactionReputation = data.UseFactionReputation,
                EnableFactionPersistence = data.EnableFactionPersistence,
                EnableItemRarity = data.EnableItemRarity,
                UseItemRarityOnInventoryIcons = data.UseItemRarityOnInventoryIcons,
                UseItemRarityForMarketPurchase = data.UseItemRarityForMarketPurchase,
                UseItemRarityForMarketSell = data.UseItemRarityForMarketSell,
                MaxReputation = data.MaxReputation,
                ReputationLossOnDeath = data.ReputationLossOnDeath,
                DefaultItemRarity = data.DefaultItemRarity,
                ItemRarityParentSearch = data.ItemRarityParentSearch,

                // Deep copy lists
                PoorItems = data.PoorItems != null ? new BindingList<string>(data.PoorItems.ToList()) : null,
                CommonItems = data.CommonItems != null ? new BindingList<string>(data.CommonItems.ToList()) : null,
                UncommonItems = data.UncommonItems != null ? new BindingList<string>(data.UncommonItems.ToList()) : null,
                RareItems = data.RareItems != null ? new BindingList<string>(data.RareItems.ToList()) : null,
                EpicItems = data.EpicItems != null ? new BindingList<string>(data.EpicItems.ToList()) : null,
                LegendaryItems = data.LegendaryItems != null ? new BindingList<string>(data.LegendaryItems.ToList()) : null,
                MythicItems = data.MythicItems != null ? new BindingList<string>(data.MythicItems.ToList()) : null,
                ExoticItems = data.ExoticItems != null ? new BindingList<string>(data.ExoticItems.ToList()) : null,
                QuestItems = data.QuestItems != null ? new BindingList<string>(data.QuestItems.ToList()) : null,
                CollectableItems = data.CollectableItems != null ? new BindingList<string>(data.CollectableItems.ToList()) : null,
                IngredientItems = data.IngredientItems != null ? new BindingList<string>(data.IngredientItems.ToList()) : null,
                entityreps = data.entityreps != null
                    ? new BindingList<EntityReputationlevels>(data.entityreps.Select(e => new EntityReputationlevels(e.Classname, e.Level)).ToList())
                    : null
            };

            return clone;
        }


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
            HasChanges();
        }
        private void UseReputationCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseReputation = UseReputationCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void EnableItemRarityCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableItemRarity = EnableItemRarityCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void UseItemRarityOnInventoryIconsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseItemRarityOnInventoryIcons = UseItemRarityOnInventoryIconsCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void UseItemRarityForMarketPurchaseNCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseItemRarityForMarketPurchase = UseItemRarityForMarketPurchaseNCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void UseItemRarityForMarketSellCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseItemRarityForMarketSell = UseItemRarityForMarketSellCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void UseFactionReputationCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseFactionReputation = UseFactionReputationCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void EnableFactionPersistenceCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableFactionPersistence = EnableFactionPersistenceCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void DefaultItemRarityCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DefaultItemRarity = (int)(ExpansionHardlineItemRarity)DefaultItemRarityCB.SelectedItem;
            HasChanges();
        }
        private void ItemRarityParentSearchCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ItemRarityParentSearch = ItemRarityParentSearchCB.Checked == true ? 1 : 0;
            HasChanges();
        }
    }
}