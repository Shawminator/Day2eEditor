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
    public partial class ExpansionCoreControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionCoreSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionCoreControl()
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
            _data = data as ExpansionCoreSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            ServerUpdateRateLimitNUD.Value = (int)_data.ServerUpdateRateLimit;
            ForceExactCEItemLifetimeCB.Checked = _data.ForceExactCEItemLifetime == 1 ? true : false;
            EnableInventoryCargoTidyCB.Checked = _data.EnableInventoryCargoTidy == 1 ? true : false;

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

        private void ServerUpdateRateLimitNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ServerUpdateRateLimit = (int)ServerUpdateRateLimitNUD.Value;
        }
        private void ForceExactCEItemLifetimeCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ForceExactCEItemLifetime = ForceExactCEItemLifetimeCB.Checked == true ? 1 : 0;
        }
        private void EnableInventoryCargoTidyCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.EnableInventoryCargoTidy = EnableInventoryCargoTidyCB.Checked == true ? 1 : 0;
        }
    }
}