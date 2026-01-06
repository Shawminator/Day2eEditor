using Day2eEditor;
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
            _suppressEvents = true;
            DismantleFlagModeComboBox.DataSource = Enum.GetValues(typeof(ExpansionDismantleFlagMode));
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
            _originalData = _data.Clone();

            
            
            CanCraftVanillaBasebuildingCB.Checked = _data.CanCraftVanillaBasebuilding == 1 ? true : false;
            CanCraftExpansionBasebuildingCB.Checked = _data.CanCraftExpansionBasebuilding == 1 ? true : false;
            DestroyFlagOnDismantleCB.Checked = _data.DestroyFlagOnDismantle == 1 ? true : false;
            DismantleFlagModeComboBox.SelectedItem = (ExpansionDismantleFlagMode)_data.DismantleFlagMode;
            DismantleAnywhereCB.Checked = _data.DismantleAnywhere == 1 ? true : false;
            CanCraftTerritoryFlagKitCB.Checked = _data.CanCraftTerritoryFlagKit == 1 ? true : false;
            GetTerritoryFlagKitAfterBuildCB.Checked = _data.GetTerritoryFlagKitAfterBuild == 1 ? true : false;

           
        }

        /// <summary>
        /// Applies changes to the data and updates the original snapshot
        /// </summary>
        public void ApplyChanges()
        {
            _originalData = _data.Clone();
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