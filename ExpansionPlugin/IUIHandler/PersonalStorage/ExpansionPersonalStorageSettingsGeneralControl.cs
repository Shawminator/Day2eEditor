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
    public partial class ExpansionPersonalStorageSettingsGeneralControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionPersonalStorageSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionPersonalStorageSettingsGeneralControl()
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
            _data = data as ExpansionPersonalStorageSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            EnabledCB.Checked = _data.Enabled == 1 ? true : false;
            UsePersonalStorageCaseCB.Checked = _data.UsePersonalStorageCase == 1 ? true : false;
            MaxItemsPerStorageNUD.Value = (int)_data.MaxItemsPerStorage;

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

        private void EnabledCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.Enabled = EnabledCB.Checked == true ? 1 : 0;
        }

        private void UsePersonalStorageCaseCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.UsePersonalStorageCase = UsePersonalStorageCaseCB.Checked == true ? 1 : 0;
        }

        private void MaxItemsPerStorageNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.MaxItemsPerStorage = (int)MaxItemsPerStorageNUD.Value;
        }
    }
}