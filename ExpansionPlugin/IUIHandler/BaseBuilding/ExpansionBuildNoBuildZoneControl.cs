using Day2eEditor;
using System.ComponentModel;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionBuildNoBuildZoneControl : UserControl, IUIHandler
    {
        public event Action<ExpansionBuildNoBuildZone> PositionChanged;
        public event Action<ExpansionBuildNoBuildZone> RadiusChanged;
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
            _originalData = _data.Clone();

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
                _nodes.Last().Text = _data.Name;
            }
        }

        #endregion

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Name = textBox3.Text;
            UpdateTreeNodeText();
            HasChanges();
        }

        private void numericUpDown14_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Center[0] = (float)numericUpDown14.Value;
            HasChanges();
            PositionChanged?.Invoke(_data);
        }

        private void numericUpDown15_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Center[1] = (float)numericUpDown15.Value;
            HasChanges();
        }

        private void numericUpDown16_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Center[2] = (float)numericUpDown16.Value;
            HasChanges();
            PositionChanged?.Invoke(_data);
        }

        private void numericUpDown13_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Radius = (float)numericUpDown13.Value;
            HasChanges();
            RadiusChanged?.Invoke(_data);
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.IsWhitelist = checkBox6.Checked == true ? 1 : 0;
            HasChanges();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CustomMessage = textBox4.Text;
            HasChanges();
        }
    }
}