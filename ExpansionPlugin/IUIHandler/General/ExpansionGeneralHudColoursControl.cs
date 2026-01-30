using CoreUI.Forms;
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
    public partial class ExpansionGeneralHudColoursControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionGeneralSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionGeneralHudColoursControl()
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

            UseHUDColorsCB.Checked = _data.UseHUDColors == 1 ? true : false;

            SetHudColor(_data.HUDColors.StaminaBarColor, StaminaBarColorPB);
            SetHudColor(_data.HUDColors.StaminaBarColorHalf, StaminaBarColorHalfPB);
            SetHudColor(_data.HUDColors.StaminaBarColorLow, StaminaBarColorLowPB);
            SetHudColor(_data.HUDColors.NotifierDividerColor, NotifierDividerColorPB);
            SetHudColor(_data.HUDColors.TemperatureBurningColor, TemperatureBurningColorPB);
            SetHudColor(_data.HUDColors.TemperatureHotColor, TemperatureHotColorPB);
            SetHudColor(_data.HUDColors.TemperatureIdealColor, TemperatureIdealColorPB);
            SetHudColor(_data.HUDColors.TemperatureColdColor, TemperatureColdColorPB);
            SetHudColor(_data.HUDColors.TemperatureFreezingColor, TemperatureFreezingColorPB);
            SetHudColor(_data.HUDColors.NotifiersIdealColor, NotifiersIdealColorPB);
            SetHudColor(_data.HUDColors.NotifiersHalfColor, NotifiersHalfColorPB);
            SetHudColor(_data.HUDColors.NotifiersLowColor, NotifiersLowColorPB);
            SetHudColor(_data.HUDColors.ReputationBaseColor, ReputationBaseColorPB);
            SetHudColor(_data.HUDColors.ReputationMedColor, ReputationMedColorPB);
            SetHudColor(_data.HUDColors.ReputationHighColor, ReputationHighColorPB);



            _suppressEvents = false;
        }
        private void SetHudColor(string hexColor, PictureBox targetPB)
        {
            string formattedColor = "#" + hexColor.Substring(6) + hexColor.Remove(6, 2);
            Color selectedColor = ColorTranslator.FromHtml(formattedColor);
            targetPB.BackColor = selectedColor;
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

        private void UseHUDColorsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.UseHUDColors = UseHUDColorsCB.Checked == true ? 1 : 0;
            
        }
        private void HudColourPB_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox pb)
            {
                HandleColorChange(pb);
            }

        }
        private void HandleColorChange(PictureBox pb)
        {
            Color startColor = pb.BackColor;
            using (AdvancedColorPickerForm picker = new AdvancedColorPickerForm(startColor))
            {
                picker.StartPosition = FormStartPosition.CenterParent;
                if (picker.ShowDialog() == DialogResult.OK)
                {
                    string colorHex = picker.SelectedColorHex;
                    Color selectedColor = ColorTranslator.FromHtml(colorHex);
                    pb.BackColor = selectedColor;

                    // Map PictureBox name to _data property dynamically
                    string propertyName = pb.Name.Replace("PB", ""); // e.g., SystemChatColorPB → SystemChatColor
                    var prop = typeof(ExpansionHudIndicatorColors).GetProperty(propertyName);
                    if (prop != null)
                    {
                        prop.SetValue(_data.HUDColors, colorHex.Substring(4, 6) + colorHex.Substring(2, 2));
                    }

                    
                }
            }
        }
    }
}