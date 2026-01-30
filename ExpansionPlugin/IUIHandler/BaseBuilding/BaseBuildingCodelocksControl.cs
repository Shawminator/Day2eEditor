using Day2eEditor;

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

            _suppressEvents = true;

            CodelockAttachModeCB.SelectedItem = (ExpansionCodelockAttachMode)_data.CodelockAttachMode;
            CodelockActionsAnywhereCB.Checked = _data.CodelockActionsAnywhere == 1 ? true : false;
            CodeLockLengthNUD.Value = (decimal)_data.CodeLockLength;
            DoDamageWhenEnterWrongCodeLockCB.Checked = _data.DoDamageWhenEnterWrongCodeLock == 1 ? true : false;
            DamageWhenEnterWrongCodeLockNUD.Value = (decimal)_data.DamageWhenEnterWrongCodeLock;
            RememberCodeCB.Checked = _data.RememberCode == 1 ? true : false;

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

        private void CodelockAttachModeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            ExpansionCodelockAttachMode cacl = (ExpansionCodelockAttachMode)CodelockAttachModeCB.SelectedItem;
            _data.CodelockAttachMode = (int)cacl;
            
        }

        private void CodelockActionsAnywhereCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CodelockActionsAnywhere = CodelockActionsAnywhereCB.Checked == true ? 1 : 0;
            
        }

        private void CodeLockLengthNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CodeLockLength = (int)CodeLockLengthNUD.Value;
            
        }

        private void DoDamageWhenEnterWrongCodeLockCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DoDamageWhenEnterWrongCodeLock = DoDamageWhenEnterWrongCodeLockCB.Checked == true ? 1 : 0;
            
        }

        private void DamageWhenEnterWrongCodeLockNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DamageWhenEnterWrongCodeLock = (decimal)DamageWhenEnterWrongCodeLockNUD.Value;
            
        }

        private void RememberCodeCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.RememberCode = RememberCodeCB.Checked == true ? 1 : 0;
            
        }
    }
}