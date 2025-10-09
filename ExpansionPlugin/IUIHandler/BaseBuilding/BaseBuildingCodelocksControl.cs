using Day2eEditor;
using System.ComponentModel;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class BaseBuildingCodelocksControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionBaseBuildingSettings _data;
        private ExpansionBaseBuildingSettings _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public BaseBuildingCodelocksControl()
        {
            InitializeComponent();
            _suppressEvents = true;
            CodelockAttachModeCB.DataSource = Enum.GetValues(typeof(ExpansionCodelockAttachMode));
            _suppressEvents = false;
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

            CodelockAttachModeCB.SelectedItem = (ExpansionCodelockAttachMode)_data.CodelockAttachMode;
            CodelockActionsAnywhereCB.Checked = _data.CodelockActionsAnywhere == 1 ? true : false;
            CodeLockLengthNUD.Value = (decimal)_data.CodeLockLength;
            DoDamageWhenEnterWrongCodeLockCB.Checked = _data.DoDamageWhenEnterWrongCodeLock == 1 ? true : false;
            DamageWhenEnterWrongCodeLockNUD.Value = (decimal)_data.DamageWhenEnterWrongCodeLock;
            RememberCodeCB.Checked = _data.RememberCode == 1 ? true : false;

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

        private void CodelockAttachModeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            ExpansionCodelockAttachMode cacl = (ExpansionCodelockAttachMode)CodelockAttachModeCB.SelectedItem;
            _data.CodelockAttachMode = (int)cacl;
            HasChanges();
        }

        private void CodelockActionsAnywhereCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CodelockActionsAnywhere = CodelockActionsAnywhereCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void CodeLockLengthNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CodeLockLength = (int)CodeLockLengthNUD.Value;
            HasChanges();
        }

        private void DoDamageWhenEnterWrongCodeLockCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DoDamageWhenEnterWrongCodeLock = DoDamageWhenEnterWrongCodeLockCB.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void DamageWhenEnterWrongCodeLockNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DamageWhenEnterWrongCodeLock = (decimal)DamageWhenEnterWrongCodeLockNUD.Value;
            HasChanges();
        }

        private void RememberCodeCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.RememberCode = RememberCodeCB.Checked == true ? 1 : 0;
            HasChanges();
        }
    }
}