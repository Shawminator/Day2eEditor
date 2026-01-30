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
    public partial class ExpansionPlayerListSettingsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionPlayerListSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionPlayerListSettingsControl()
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
            _data = data as ExpansionPlayerListSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            EnablePlayerListCB.Checked = (int)_data.EnablePlayerList == 1 ? true : false;
            EnableTooltipCB.Checked = (int)_data.EnableTooltip == 1 ? true : false;

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

        private void EnablePlayerListCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnablePlayerList = EnablePlayerListCB.Checked == true ? 1 : 0;
            
        }

        private void EnableTooltipCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            CheckBox cb = sender as CheckBox;
            _data.EnableTooltip = EnableTooltipCB.Checked == true ? 1 : 0;
            
        }
    }
}