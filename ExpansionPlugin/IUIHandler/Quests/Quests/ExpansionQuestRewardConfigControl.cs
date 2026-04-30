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
    public partial class ExpansionQuestRewardConfigControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestRewardConfig _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestRewardConfigControl()
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
            _data = data as ExpansionQuestRewardConfig ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            QuestItemClassnameTB.Text = _data.ClassName;
            QuestRewardsAmountNUD.Value = (int)_data.Amount;
            QuestRewardsHealthPercentNUD.Value = (int)_data.HealthPercent;
            QuestRewardsDamagePercentNUD.Value = (int)_data.DamagePercent;
            QuestRewardsQuestIDNUD.Value = (int)_data.QuestID;
            QuestRewardChanceNUD.Value = (decimal)_data.Chance;
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
                _nodes.Last().Text = _data.ClassName;
            }
        }

        #endregion

        private void QuestItemClassnameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ClassName = QuestItemClassnameTB.Text;
            UpdateTreeNodeText();
        }

        private void darkButton56_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes
            {
                UseOnlySingleItem = true
            };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                TreeNode FocusNode = new TreeNode();
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    QuestItemClassnameTB.Text = l;
                }
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }

        private void QuestRewardsAmountNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Amount = (int)QuestRewardsAmountNUD.Value;
        }

        private void QuestRewardsHealthPercentNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.HealthPercent = (int)QuestRewardsHealthPercentNUD.Value;
        }

        private void QuestRewardsDamagePercentNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.DamagePercent = (int)QuestRewardsDamagePercentNUD.Value;
        }

        private void QuestRewardsQuestIDNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.QuestID = (int)QuestRewardsQuestIDNUD.Value;
        }

        private void QuestRewardChanceNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Chance = (int)QuestRewardChanceNUD.Value;
        }
    }
}