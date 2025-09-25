using Day2eEditor;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionBuildNoBuildZoneControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionBuildNoBuildZone _data;
        private ExpansionBuildNoBuildZone _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionBuildNoBuildZoneControl()
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
            _data = data as ExpansionBuildNoBuildZone ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            textBox3.Text = _data.Name;

            numericUpDown13.Value = (decimal)_data.Radius;
            numericUpDown14.Value = (decimal)_data.Center[0];
            numericUpDown15.Value = (decimal)_data.Center[1];
            numericUpDown16.Value = (decimal)_data.Center[2];
            checkBox6.Checked = _data.IsWhitelist == 1 ? true : false;
            textBox4.Text = _data.CustomMessage;

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
        private ExpansionBuildNoBuildZone CloneData(ExpansionBuildNoBuildZone data)
        {
            // TODO: Implement actual cloning logic
            return new ExpansionBuildNoBuildZone
            {
                // Copy properties here
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
    }
}