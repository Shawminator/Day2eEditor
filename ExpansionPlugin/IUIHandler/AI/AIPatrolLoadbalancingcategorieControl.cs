using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class AIPatrolLoadbalancingcategorieControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private Loadbalancingcategorie _data;
        private Loadbalancingcategorie _originalData;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public AIPatrolLoadbalancingcategorieControl()
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
            _data = data as Loadbalancingcategorie ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _originalData = _data.Clone();

            _suppressEvents = true;

            NameLBCTB.Text = _data.name;

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
                _nodes.Last().Text = $"Category Name : - {_data.name}";
            }
        }

        #endregion

        private void NameLBCTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.name = NameLBCTB.Text;
            UpdateTreeNodeText();
            HasChanges();
        }
    }
}