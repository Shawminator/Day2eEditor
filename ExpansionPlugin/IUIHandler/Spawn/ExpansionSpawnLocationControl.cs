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
    public partial class ExpansionSpawnLocationControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionSpawnLocation _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionSpawnLocationControl()
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
            _data = data as ExpansionSpawnLocation ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            NameTB.Text = _data.Name;
            UseCooldownCB.Checked = _data.UseCooldown == 1 ? true : false;

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
                _nodes.Last().Text = _data.Name;
            }
        }

        #endregion

        private void NameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.Name = NameTB.Text;
            UpdateTreeNodeText();
        }

        private void UseCooldownCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.UseCooldown = UseCooldownCB.Checked == true ? 1 : 0;
        }
    }
}