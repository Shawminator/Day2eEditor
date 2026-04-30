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
    public partial class ExpansionQuestQuestRewardsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestQuest _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestQuestRewardsControl()
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
            _data = data as ExpansionQuestQuest ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;
            QuestRewardBehavorCB.DataSource = Enum.GetValues(typeof(ExpansionQuestRewardBehavior));
            QuestNeedToSelectRewardCB.Checked = _data.NeedToSelectReward == 1 ? true : false;
            QuestRewardsForGroupOwnerOnlyCB.Checked = _data.RewardsForGroupOwnerOnly == 1 ? true : false;
            QuestRandomRewardCB.Checked = _data.RandomReward == 1 ? true : false;
            QuestRandomRewardAmountNUD.Value = (int)_data.RandomRewardAmount;
            QuestRewardBehavorCB.SelectedItem = (ExpansionQuestRewardBehavior)_data.RewardBehavior;
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

        private void QuestNeedToSelectRewardCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.NeedToSelectReward = QuestNeedToSelectRewardCB.Checked == true ? 1 : 0;
        }

        private void QuestRewardsForGroupOwnerOnlyCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.RewardsForGroupOwnerOnly = QuestRewardsForGroupOwnerOnlyCB.Checked == true ? 1 : 0;
        }

        private void QuestRandomRewardCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.RandomReward = QuestRandomRewardCB.Checked == true ? 1 : 0;
        }

        private void QuestRandomRewardAmountNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.RandomRewardAmount = (int)QuestRandomRewardAmountNUD.Value;
        }

        private void QuestRewardBehavorCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.RewardBehavior = (ExpansionQuestRewardBehavior)QuestRewardBehavorCB.SelectedItem;
        }
    }
}