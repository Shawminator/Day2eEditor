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
    public partial class ExpansionQuestQuestFlowControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestQuest _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestQuestFlowControl()
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

            QuestRepeatableCB.Checked = _data.Repeatable == 1 ? true : false;
            QuestIsDailyQuestCB.Checked = _data.IsDailyQuest == 1 ? true : false;
            QuestIsWeeklyQuestCB.Checked = _data.IsWeeklyQuest == 1 ? true : false;
            questAutocompleteCB.Checked = _data.Autocomplete == 1 ? true : false;
            QuestSequentialObjectivesCB.Checked = _data.SequentialObjectives == 1 ? true : false;

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

        private void QuestRepeatableCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Repeatable = QuestRepeatableCB.Checked == true ? 1 : 0;
        }

        private void QuestIsDailyQuestCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.IsDailyQuest = QuestIsDailyQuestCB.Checked == true ? 1 : 0;
        }

        private void QuestIsWeeklyQuestCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.IsWeeklyQuest = QuestIsWeeklyQuestCB.Checked == true ? 1 : 0;
        }

        private void QuestSequentialObjectivesCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SequentialObjectives = QuestSequentialObjectivesCB.Checked == true ? 1 : 0;
        }

        private void questAutocompleteCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Autocomplete = questAutocompleteCB.Checked == true ? 1 : 0;
        }
    }
}