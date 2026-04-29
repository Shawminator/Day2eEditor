using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionQuestObjectiveCollectionConfigControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestObjectiveCollectionConfig _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestObjectiveCollectionConfigControl()
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
            _data = data as ExpansionQuestObjectiveCollectionConfig ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            ObjectivesCollectionShowDistanceCB.Checked = _data.ShowDistance == 1 ? true : false;
            checkBox3.Checked = _data.AddItemsToNearbyMarketZone == 1 ? true : false;
            checkBox5.Checked = _data.NeedAnyCollection == 1 ? true : false;

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


        private void ObjectivesCollectionShowDistanceCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShowDistance = ObjectivesCollectionShowDistanceCB.Checked == true ? 1 : 0;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AddItemsToNearbyMarketZone = checkBox3.Checked == true ? 1 : 0;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NeedAnyCollection = checkBox5.Checked == true ? 1 : 0;
        }
    }
}