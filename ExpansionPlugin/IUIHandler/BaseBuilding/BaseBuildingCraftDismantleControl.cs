﻿using Day2eEditor;
using System.ComponentModel;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class BaseBuildingCraftDismantleControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionBaseBuildingSettings _data;
        private ExpansionBaseBuildingSettings _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public BaseBuildingCraftDismantleControl()
        {
            InitializeComponent();
            DismantleFlagModeComboBox.DataSource = Enum.GetValues(typeof(ExpansionDismantleFlagMode));
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
            _data = data as ExpansionBaseBuildingSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            CanCraftVanillaBasebuildingCB.Checked = _data.CanCraftVanillaBasebuilding == 1 ? true : false;
            CanCraftExpansionBasebuildingCB.Checked = _data.CanCraftExpansionBasebuilding == 1 ? true : false;
            DestroyFlagOnDismantleCB.Checked = _data.DestroyFlagOnDismantle == 1 ? true : false;
            DismantleFlagModeComboBox.SelectedItem = (ExpansionDismantleFlagMode)_data.DismantleFlagMode;
            DismantleAnywhereCB.Checked = _data.DismantleAnywhere == 1 ? true : false;
            CanCraftTerritoryFlagKitCB.Checked = _data.CanCraftTerritoryFlagKit == 1 ? true : false;
            GetTerritoryFlagKitAfterBuildCB.Checked = _data.GetTerritoryFlagKitAfterBuild == 1 ? true : false;

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
        private ExpansionBaseBuildingSettings CloneData(ExpansionBaseBuildingSettings data)
        {
            return new ExpansionBaseBuildingSettings
            {
                m_Version = data.m_Version,
                CanBuildAnywhere = data.CanBuildAnywhere,
                AllowBuildingWithoutATerritory = data.AllowBuildingWithoutATerritory,
                DeployableOutsideATerritory = new BindingList<string>(data.DeployableOutsideATerritory.ToList()),
                DeployableInsideAEnemyTerritory = new BindingList<string>(data.DeployableInsideAEnemyTerritory.ToList()),
                CanCraftVanillaBasebuilding = data.CanCraftVanillaBasebuilding,
                CanCraftExpansionBasebuilding = data.CanCraftExpansionBasebuilding,
                DestroyFlagOnDismantle = data.DestroyFlagOnDismantle,
                DismantleOutsideTerritory = data.DismantleOutsideTerritory,
                DismantleInsideTerritory = data.DismantleInsideTerritory,
                DismantleAnywhere = data.DismantleAnywhere,
                CodelockActionsAnywhere = data.CodelockActionsAnywhere,
                CodeLockLength = data.CodeLockLength,
                DoDamageWhenEnterWrongCodeLock = data.DoDamageWhenEnterWrongCodeLock,
                DamageWhenEnterWrongCodeLock = data.DamageWhenEnterWrongCodeLock,
                RememberCode = data.RememberCode,
                CanCraftTerritoryFlagKit = data.CanCraftTerritoryFlagKit,
                SimpleTerritory = data.SimpleTerritory,
                AutomaticFlagOnCreation = data.AutomaticFlagOnCreation,
                GetTerritoryFlagKitAfterBuild = data.GetTerritoryFlagKitAfterBuild,
                BuildZoneRequiredCustomMessage = data.BuildZoneRequiredCustomMessage,
                ZonesAreNoBuildZones = data.ZonesAreNoBuildZones,
                CodelockAttachMode = data.CodelockAttachMode,
                DismantleFlagMode = data.DismantleFlagMode,
                FlagMenuMode = data.FlagMenuMode,
                PreventItemAccessThroughObstructingItems = data.PreventItemAccessThroughObstructingItems,
                EnableVirtualStorage = data.EnableVirtualStorage,
                VirtualStorageExcludedContainers = new BindingList<string>(data.VirtualStorageExcludedContainers.ToList()),
                Zones = new BindingList<ExpansionBuildNoBuildZone>(
                    data.Zones.Select(zone => new ExpansionBuildNoBuildZone
                    {
                        Name = zone.Name,
                        Center = (float[])zone.Center.Clone(),
                        Radius = zone.Radius,
                        Items = new BindingList<string>(zone.Items.ToList()),
                        IsWhitelist = zone.IsWhitelist,
                        CustomMessage = zone.CustomMessage
                    }).ToList()
                )
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

        private void CanCraftVanillaBasebuildingCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CanCraftVanillaBasebuilding = CanCraftVanillaBasebuildingCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void CanCraftExpansionBasebuildingCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CanCraftExpansionBasebuilding = CanCraftExpansionBasebuildingCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void CanCraftTerritoryFlagKitCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CanCraftTerritoryFlagKit = CanCraftTerritoryFlagKitCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void DestroyFlagOnDismantleCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DestroyFlagOnDismantle = DestroyFlagOnDismantleCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void DismantleFlagModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            ExpansionDismantleFlagMode cacl = (ExpansionDismantleFlagMode)DismantleFlagModeComboBox.SelectedItem;
            _data.DismantleFlagMode = (int)cacl;
            HasChanges();
        }

        private void DismantleAnywhereCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DismantleAnywhere = DismantleAnywhereCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void GetTerritoryFlagKitAfterBuildCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.GetTerritoryFlagKitAfterBuild = GetTerritoryFlagKitAfterBuildCB.Checked == true ? 1 : 0;
            HasChanges();
        }
    }
}