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
    public partial class ExpansionHardlineReputationControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionHardlineSettings _data;
        private ExpansionHardlineSettings _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public EntityReputationlevels? CurrentEntityrep { get; private set; }

        public ExpansionHardlineReputationControl()
        {
            InitializeComponent();
        }
        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            ListBox lb = sender as ListBox;
            e.DrawBackground();
            Brush myBrush = Brushes.Black;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            }
            else
            {
                myBrush = Brushes.White;
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(60, 63, 65)), e.Bounds);
            }
            e.Graphics.DrawString(lb.Items[e.Index].ToString(), e.Font, myBrush, e.Bounds);
            e.DrawFocusRectangle();
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

            ReputationMaxReputationNUD.Value = (int)_data.MaxReputation;
            ReputationLossOnDeathNUD.Value = (int)_data.ReputationLossOnDeath;

            EntityReputationLB.DisplayMember = "DisplayName";
            EntityReputationLB.ValueMember = "Value";
            EntityReputationLB.DataSource = _data.entityreps;

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

        private void EntityReputationLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentEntityrep = EntityReputationLB.SelectedItem as EntityReputationlevels;
            if (CurrentEntityrep == null) { return; }
            _suppressEvents = true;
            EntityReputationNUD.Value = CurrentEntityrep.Level;
            _suppressEvents = false;
        }
        private void ReputationLossOnDeathNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ReputationLossOnDeath = (int)ReputationLossOnDeathNUD.Value;
            HasChanges();
        }
        private void ReputationMaxReputationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxReputation = (int)ReputationMaxReputationNUD.Value;
            HasChanges();
        }
        private void EntityReputationNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            CurrentEntityrep.Level = (int)EntityReputationNUD.Value;
            HasChanges();
        }
        private void darkButton111_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes();
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    if (!_data.entityreps.Any(x => x.Classname == l))
                    {
                        _data.entityreps.Add(new EntityReputationlevels(l, 0));
                    }
                    HasChanges();
                }
            }
        }
        private void darkButton110_Click(object sender, EventArgs e)
        {
            EntityReputationlevels erl = _data.entityreps.First(x => x.Classname == EntityReputationLB.GetItemText(EntityReputationLB.SelectedItem));
            if (erl != null)
            {
                _data.entityreps.Remove(erl);
                HasChanges();
            }
        }
    }
}