using Day2eEditor;
using ExpansionPlugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionBookGeneralControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionBookSettings _data;
        private ExpansionBookSettings _originalData;
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
            _originalData = CloneData(_data); // Store original data for reset

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
        private ExpansionBookSettings CloneData(ExpansionBookSettings data)
        {
            if (data == null)
                return null;

            return new ExpansionBookSettings
            {
                m_Version = data.m_Version,
                EnableStatusTab = data.EnableStatusTab,
                EnablePartyTab = data.EnablePartyTab,
                EnableServerInfoTab = data.EnableServerInfoTab,
                EnableServerRulesTab = data.EnableServerRulesTab,
                EnableTerritoryTab = data.EnableTerritoryTab,
                EnableBookMenu = data.EnableBookMenu,
                CreateBookmarks = data.CreateBookmarks,
                ShowHaBStats = data.ShowHaBStats,
                ShowPlayerFaction = data.ShowPlayerFaction,
                DisplayServerSettingsInServerInfoTab = data.DisplayServerSettingsInServerInfoTab,
                EnableCraftingRecipesTab = data.EnableCraftingRecipesTab,

                RuleCategories = new BindingList<ExpansionBookRuleCategory>(
                    data.RuleCategories?.Select(rc => new ExpansionBookRuleCategory
                    {
                        CategoryName = rc.CategoryName,
                        Rules = new BindingList<ExpansionBookRule>(
                            rc.Rules?.Select(r => new ExpansionBookRule
                            {
                                RuleParagraph = r.RuleParagraph,
                                RuleText = r.RuleText
                            }).ToList() ?? new List<ExpansionBookRule>())
                    }).ToList() ?? new List<ExpansionBookRuleCategory>()),

                SettingCategories = new BindingList<ExpansionBookSettingCategory>(
                    data.SettingCategories?.Select(sc => new ExpansionBookSettingCategory
                    {
                        CategoryName = sc.CategoryName,
                        Settings = new BindingList<ExpansionBookSetting>(
                            sc.Settings?.Select(s => new ExpansionBookSetting
                            {
                                SettingTitle = s.SettingTitle,
                                SettingText = s.SettingText,
                                SettingValue = s.SettingValue
                            }).ToList() ?? new List<ExpansionBookSetting>())
                    }).ToList() ?? new List<ExpansionBookSettingCategory>()),

                Links = new BindingList<ExpansionBookLink>(
                    data.Links?.Select(l => new ExpansionBookLink
                    {
                        Name = l.Name,
                        URL = l.URL,
                        IconName = l.IconName,
                        IconColor = l.IconColor
                    }).ToList() ?? new List<ExpansionBookLink>()),

                Descriptions = new BindingList<ExpansionBookDescriptionCategory>(
                    data.Descriptions?.Select(dc => new ExpansionBookDescriptionCategory
                    {
                        CategoryName = dc.CategoryName,
                        Descriptions = new BindingList<ExpansionBookDescription>(
                            dc.Descriptions?.Select(d => new ExpansionBookDescription
                            {
                                DescriptionText = d.DescriptionText,
                                DTName = d.DTName
                            }).ToList() ?? new List<ExpansionBookDescription>())
                    }).ToList() ?? new List<ExpansionBookDescriptionCategory>()),

                CraftingCategories = new BindingList<ExpansionBookCraftingCategory>(
                    data.CraftingCategories?.Select(cc => new ExpansionBookCraftingCategory
                    {
                        CategoryName = cc.CategoryName,
                        Results = new BindingList<string>(
                            cc.Results?.ToList() ?? new List<string>())
                    }).ToList() ?? new List<ExpansionBookCraftingCategory>())
            };
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

        private void EnableStatusTabCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableStatusTab = EnableStatusTabCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void EnablePartyTabCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnablePartyTab = EnablePartyTabCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void EnableServerInfoTabCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableServerInfoTab = EnableServerInfoTabCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void EnableCraftingRecipesTabCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableCraftingRecipesTab = EnableCraftingRecipesTabCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void EnableServerRulesTabCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableServerRulesTab = EnableServerRulesTabCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void EnableTerritoryTabCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableTerritoryTab = EnableTerritoryTabCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void EnableBookMenuCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableBookMenu = EnableBookMenuCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void CreateBookmarksCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CreateBookmarks = CreateBookmarksCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void ShowHaBStatsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShowHaBStats = ShowHaBStatsCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void ShowPlayerFactionCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShowPlayerFaction = ShowPlayerFactionCB.Checked == true ? 1 : 0;
            HasChanges();
        }
        private void DisplayServerSettingsInServerInfoTabCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DisplayServerSettingsInServerInfoTab = DisplayServerSettingsInServerInfoTabCB.Checked == true ? 1 : 0;
            HasChanges();
        }
    }
}