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
    public partial class ExpansionQuestObjectiveDeliveryConfigControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestObjectiveDeliveryConfig _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestObjectiveDeliveryConfigControl()
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
            _data = data as ExpansionQuestObjectiveDeliveryConfig ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            ObjectivesDeliveryMaxDistanceNUD.Value = (decimal)_data.MaxDistance;
            ObjectivesDeliveryMarkerNameTB.Text = _data.MarkerName;
            ObjectivesDeliveryShowDistanceCB.Checked = _data.ShowDistance == 1 ? true : false;
            checkBox6.Checked = _data.AddItemsToNearbyMarketZone == 1 ? true : false;

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
        private void ObjectivesDeliveryMaxDistanceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxDistance = (decimal)ObjectivesDeliveryMaxDistanceNUD.Value;
        }
        private void ObjectivesDeliveryMarkerNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MarkerName = ObjectivesDeliveryMarkerNameTB.Text;
        }

        private void ObjectivesDeliveryShowDistanceCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ShowDistance = ObjectivesDeliveryShowDistanceCB.Checked == true ? 1 : 0;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AddItemsToNearbyMarketZone = checkBox6.Checked == true ? 1 : 0;
        }
    }
}