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
    public partial class ExpansionRaidSettingsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionRaidSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionRaidSettingsControl()
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
            _data = data as ExpansionRaidSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;



            _suppressEvents = true;
            BaseBuildingRaidModeComboBox.DataSource = Enum.GetValues(typeof(ExpansionBaseBuildingRaidMode));
            BaseBuildingRaidModeComboBox.SelectedItem = (ExpansionBaseBuildingRaidMode)_data.BaseBuildingRaidMode;

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

        private void BaseBuildingRaidModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.BaseBuildingRaidMode = (ExpansionBaseBuildingRaidMode)BaseBuildingRaidModeComboBox.SelectedItem;
            
        }
    }
}