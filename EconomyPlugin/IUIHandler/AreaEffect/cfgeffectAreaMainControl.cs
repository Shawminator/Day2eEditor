using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class cfgeffectAreaMainControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private Areas _data;
        private Areas _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgeffectAreaMainControl()
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
            this.DoubleBuffered = true;
            _parentType = parentType;
            _data = data as Areas ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = CloneData(_data); // Store original data for reset

            _suppressEvents = true;

            AreaNameTB.Text = _data.AreaName;
            TypeTB.Text = _data.Type;
            TriggerTypeTB.Text = _data.TriggerType;

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
        private Areas CloneData(Areas data)
        {
            // TODO: Implement actual cloning logic
            return new Areas
            {
                AreaName = data.AreaName,
                Type = data.Type,
                TriggerType = data.TriggerType
            };
        }

        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = _data.AreaName;
            }
        }

        #endregion

        private void AreaNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AreaName = AreaNameTB.Text;
            UpdateTreeNodeText();
            HasChanges();
        }

        private void TypeTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Type = TypeTB.Text;
            HasChanges();

        }

        private void TriggerTypeTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.TriggerType = TriggerTypeTB.Text;
            HasChanges();
        }
    }
}