using Day2eEditor;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionBuildNoBuildZonesControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionBaseBuildingSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionBuildNoBuildZonesControl()
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
            _data = data as ExpansionBaseBuildingSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            textBox2.Text = _data.BuildZoneRequiredCustomMessage;
            ZonesAreNoBuildZonesCB.Checked = _data.ZonesAreNoBuildZones == 1 ? true : false;
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.BuildZoneRequiredCustomMessage = textBox2.Text;
        }

        private void ZonesAreNoBuildZonesCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ZonesAreNoBuildZones = ZonesAreNoBuildZonesCB.Checked == true ? 1 : 0;
        }
    }
}