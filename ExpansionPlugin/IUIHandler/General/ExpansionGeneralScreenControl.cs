using Day2eEditor;
using ExpansionPlugin;
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
    public partial class ExpansionGeneralScreenControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionGeneralSettings _data;
        private ExpansionGeneralSettings _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionGeneralScreenControl()
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
            _data = data as ExpansionGeneralSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            UseDeathScreenCB.Checked = _data.UseDeathScreen == 1 ? true : false;
            UseDeathScreenStatisticsCB.Checked = _data.UseDeathScreenStatistics == 1 ? true : false;
            UseExpansionMainMenuLogoCB.Checked = _data.UseExpansionMainMenuLogo == 1 ? true : false;
            UseExpansionMainMenuIconsCB.Checked = _data.UseExpansionMainMenuIcons == 1 ? true : false;
            UseExpansionMainMenuIntroSceneCB.Checked = _data.UseExpansionMainMenuIntroScene == 1 ? true : false;
            UseNewsFeedInGameMenuCB.Checked = _data.UseNewsFeedInGameMenu == 1 ? true : false;
            InGameMenuLogoPathTB.Text = _data.InGameMenuLogoPath;

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
        private ExpansionGeneralSettings CloneData(ExpansionGeneralSettings data)
        {
            if (data == null)
                return null;

            return new ExpansionGeneralSettings
            {
                m_Version = data.m_Version,
                DisableShootToUnlock = data.DisableShootToUnlock,
                EnableGravecross = data.EnableGravecross,
                EnableAIGravecross = data.EnableAIGravecross,
                GravecrossDeleteBody = data.GravecrossDeleteBody,
                GravecrossTimeThreshold = data.GravecrossTimeThreshold,
                GravecrossSpawnTimeDelay = data.GravecrossSpawnTimeDelay,
                EnableLamps = data.EnableLamps,
                LampAmount_OneInX = data.LampAmount_OneInX,
                LampSelectionMode = data.LampSelectionMode,
                EnableGenerators = data.EnableGenerators,
                EnableLighthouses = data.EnableLighthouses,
                EnableHUDNightvisionOverlay = data.EnableHUDNightvisionOverlay,
                DisableMagicCrosshair = data.DisableMagicCrosshair,
                EnableAutoRun = data.EnableAutoRun,
                UseDeathScreen = data.UseDeathScreen,
                UseDeathScreenStatistics = data.UseDeathScreenStatistics,
                UseExpansionMainMenuLogo = data.UseExpansionMainMenuLogo,
                UseExpansionMainMenuIcons = data.UseExpansionMainMenuIcons,
                UseExpansionMainMenuIntroScene = data.UseExpansionMainMenuIntroScene,
                UseNewsFeedInGameMenu = data.UseNewsFeedInGameMenu,
                UseHUDColors = data.UseHUDColors,
                EnableEarPlugs = data.EnableEarPlugs,
                InGameMenuLogoPath = data.InGameMenuLogoPath,
                Mapping = data.Mapping != null ? new ExpansionMapping
                {
                    UseCustomMappingModule = data.Mapping.UseCustomMappingModule,
                    BuildingInteriors = data.Mapping.BuildingInteriors,
                    BuildingIvys = data.Mapping.BuildingIvys,
                    Mapping = new BindingList<string>(data.Mapping.Mapping.ToList()),
                    Interiors = new BindingList<string>(data.Mapping.Interiors.ToList())
                } : null,
                HUDColors = data.HUDColors != null ? new ExpansionHudIndicatorColors
                {
                    StaminaBarColor = data.HUDColors.StaminaBarColor,
                    StaminaBarColorHalf = data.HUDColors.StaminaBarColorHalf,
                    StaminaBarColorLow = data.HUDColors.StaminaBarColorLow,
                    NotifierDividerColor = data.HUDColors.NotifierDividerColor,
                    TemperatureBurningColor = data.HUDColors.TemperatureBurningColor,
                    TemperatureHotColor = data.HUDColors.TemperatureHotColor,
                    TemperatureIdealColor = data.HUDColors.TemperatureIdealColor,
                    TemperatureColdColor = data.HUDColors.TemperatureColdColor,
                    TemperatureFreezingColor = data.HUDColors.TemperatureFreezingColor,
                    NotifiersIdealColor = data.HUDColors.NotifiersIdealColor,
                    NotifiersHalfColor = data.HUDColors.NotifiersHalfColor,
                    NotifiersLowColor = data.HUDColors.NotifiersLowColor,
                    ReputationBaseColor = data.HUDColors.ReputationBaseColor,
                    ReputationMedColor = data.HUDColors.ReputationMedColor,
                    ReputationHighColor = data.HUDColors.ReputationHighColor
                } : null
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

        private void UseDeathScreenCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseDeathScreen = UseDeathScreenCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void UseDeathScreenStatisticsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseDeathScreenStatistics = UseDeathScreenStatisticsCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void UseExpansionMainMenuLogoCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseExpansionMainMenuLogo = UseExpansionMainMenuLogoCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void UseExpansionMainMenuIconsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseExpansionMainMenuIcons = UseExpansionMainMenuIconsCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void UseExpansionMainMenuIntroSceneCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseExpansionMainMenuIntroScene = UseExpansionMainMenuIntroSceneCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void UseNewsFeedInGameMenuCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseNewsFeedInGameMenu = UseNewsFeedInGameMenuCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void InGameMenuLogoPathTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.InGameMenuLogoPath = InGameMenuLogoPathTB.Text;
            HasChanges();
        }
    }
}