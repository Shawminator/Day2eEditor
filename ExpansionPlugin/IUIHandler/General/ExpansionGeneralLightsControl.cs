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
    public partial class ExpansionGeneralLightsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionGeneralSettings _data;
        private ExpansionGeneralSettings _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionGeneralLightsControl()
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

            EnableLampsComboBox.DataSource = Enum.GetValues(typeof(LampModeEnum));
            LampSelectionModeCB.DataSource = Enum.GetValues(typeof(ExpansionLampSelectionMode));

            EnableLampsComboBox.SelectedItem = (LampModeEnum)_data.EnableLamps;
            numericUpDown34.Value = (int)_data.LampAmount_OneInX;
            EnableGeneratorsCB.Checked = _data.EnableGenerators == 1 ? true : false;
            EnableLighthousesCB.Checked = _data.EnableLighthouses == 1 ? true : false;
            LampSelectionModeCB.SelectedIndex = LampSelectionModeCB.FindStringExact(_data.LampSelectionMode);

            _suppressEvents = false;
        }

        private string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attr?.Description ?? value.ToString();
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



        private void EnableLampsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableLamps = (int)(LampModeEnum)EnableLampsComboBox.SelectedItem;
            HasChanges();
        }
        private void EnableGeneratorsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableGenerators = EnableGeneratorsCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void EnableLighthousesCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableLighthouses = EnableLighthousesCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void numericUpDown34_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.LampAmount_OneInX = (int)numericUpDown34.Value;
            HasChanges();
        }

        private void LampSelectionModeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.LampSelectionMode = LampSelectionModeCB.GetItemText(LampSelectionModeCB.SelectedIndex);
            HasChanges();
        }
    }
}