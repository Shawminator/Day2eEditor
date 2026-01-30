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
    public partial class ExpansionGeneralGraveCrossControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionGeneralSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionGeneralGraveCrossControl()
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

            _suppressEvents = true;

            EnableGravecrossCB.Checked = _data.EnableGravecross == 1 ? true : false;
            EnableAIGravecrossCB.Checked = _data.EnableAIGravecross == 1 ? true : false;
            GravecrossDeleteBodyCB.Checked = _data.GravecrossDeleteBody == 1 ? true : false;
            GravecrossTimeThresholdNUD.Value = (decimal)_data.GravecrossTimeThreshold;
            GravecrossSpawnTimeDelayNUD.Value = (decimal)_data.GravecrossSpawnTimeDelay;

            _suppressEvents = false;
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

        private void EnableGravecrossCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableGravecross = EnableGravecrossCB.Checked == true ? 1 : 0;
            
        }

        private void EnableAIGravecrossCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableAIGravecross = EnableAIGravecrossCB.Checked == true?1:0;
            
        }

        private void GravecrossDeleteBodyCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.GravecrossDeleteBody = GravecrossDeleteBodyCB.Checked == true ? 1:0;
            
        }

        private void GravecrossTimeThresholdNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.GravecrossTimeThreshold = GravecrossTimeThresholdNUD.Value;
            
        }

        private void GravecrossSpawnTimeDelayNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.GravecrossSpawnTimeDelay = (decimal)GravecrossSpawnTimeDelayNUD.Value;
            
        }
    }
}