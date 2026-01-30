using Day2eEditor;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionBaseBuildingTerritoryControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionBaseBuildingSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionBaseBuildingTerritoryControl()
        {
            InitializeComponent();
            _suppressEvents = true;
            FlagMenuModeComboBox.DataSource = Enum.GetValues(typeof(ExpansionFlagMenuMode));
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

            _suppressEvents = true;

            CanBuildAnywhereCB.Checked = _data.CanBuildAnywhere == 1 ? true : false;
            AllowBuildingWithoutATerritoryCB.Checked = _data.AllowBuildingWithoutATerritory == 1 ? true : false;
            DismantleOutsideTerritoryCB.Checked = _data.DismantleOutsideTerritory == 1 ? true : false;
            DismantleInsideTerritoryCB.Checked = _data.DismantleInsideTerritory == 1 ? true : false;
            SimpleTerritoryCB.Checked = _data.SimpleTerritory == 1 ? true : false;
            AutomaticFlagOnCreationCB.Checked = _data.AutomaticFlagOnCreation == 1 ? true : false;
            FlagMenuModeComboBox.SelectedItem = (ExpansionFlagMenuMode)_data.FlagMenuMode;
            PreventItemAccessThroughObstructingItemsCB.Checked = _data.PreventItemAccessThroughObstructingItems == 1 ? true : false;

            _suppressEvents = false;
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

        private void SimpleTerritoryCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SimpleTerritory = SimpleTerritoryCB.Checked == true ? 1 : 0;
            
        }

        private void AllowBuildingWithoutATerritoryCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AllowBuildingWithoutATerritory = AllowBuildingWithoutATerritoryCB.Checked == true ? 1 : 0;
            
        }

        private void CanBuildAnywhereCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CanBuildAnywhere = CanBuildAnywhereCB.Checked == true ? 1 : 0;
            
        }

        private void FlagMenuModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            ExpansionFlagMenuMode cacl = (ExpansionFlagMenuMode)FlagMenuModeComboBox.SelectedItem;
            _data.FlagMenuMode = (int)cacl;
            
        }

        private void AutomaticFlagOnCreationCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AutomaticFlagOnCreation = AutomaticFlagOnCreationCB.Checked == true ? 1 : 0;
            
        }

        private void DismantleOutsideTerritoryCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DismantleOutsideTerritory = DismantleOutsideTerritoryCB.Checked == true ? 1 : 0;
            
        }

        private void DismantleInsideTerritoryCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DismantleInsideTerritory = DismantleInsideTerritoryCB.Checked == true ? 1 : 0;
            
        }

        private void PreventItemAccessThroughObstructingItemsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.PreventItemAccessThroughObstructingItems = PreventItemAccessThroughObstructingItemsCB.Checked == true ? 1 : 0;
            
        }
    }
}