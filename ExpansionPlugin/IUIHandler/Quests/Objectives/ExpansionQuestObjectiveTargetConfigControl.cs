using Day2eEditor;
using ExpansionPlugin;
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
    public partial class ExpansionQuestObjectiveTargetConfigControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestObjectiveTargetConfig _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestObjectiveTargetConfigControl()
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
            _data = data as ExpansionQuestObjectiveTargetConfig ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            ObjectivesTargetMaxDistanceNUD.Value = (decimal)_data.MaxDistance;
            ObjectivesTargetMinDistanceNUD.Value = (decimal)_data.MinDistance;
            ObjectivesTargetAmountNUD.Value = (int)_data.Amount;
            ObjectivesTargetCountSelfKillCB.Checked = _data.CountSelfKill == 1 ? true : false;
            checkBox7.Checked = _data.CountAIPlayers == 1 ? true : false;

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


        private void ObjectivesTargetAmountNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Amount = (int)ObjectivesTargetAmountNUD.Value;
        }

        private void ObjectivesTargetMaxDistanceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MaxDistance = ObjectivesTargetMaxDistanceNUD.Value;
        }

        private void ObjectivesTargetMinDistanceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.MinDistance = ObjectivesTargetMinDistanceNUD.Value;
        }

        private void ObjectivesTargetCountSelfKillCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CountSelfKill = ObjectivesTargetCountSelfKillCB.Checked == true ? 1 : 0;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CountAIPlayers = checkBox7.Checked == true ? 1 : 0;
        }
    }
}