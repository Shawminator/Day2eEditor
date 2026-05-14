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
    public partial class ExpansionQuestSettingsNotificationsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestSettingsNotificationsControl()
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
            _data = data as ExpansionQuestSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            QuestAcceptedTitleTB.Text = _data.QuestAcceptedTitle;
            QuestAcceptedTextTB.Text = _data.QuestAcceptedText;

            QuestCompletedTitleTB.Text = _data.QuestCompletedTitle;
            QuestCompletedTextTB.Text = _data.QuestCompletedText;

            QuestFailedTitleTB.Text = _data.QuestFailedTitle;
            QuestFailedTextTB.Text = _data.QuestFailedText;

            QuestCanceledTitleTB.Text = _data.QuestCanceledTitle;
            QuestCanceledTextTB.Text = _data.QuestCanceledText;

            QuestTurnInTitleTB.Text = _data.QuestTurnInTitle;
            QuestTurnInTextTB.Text = _data.QuestTurnInText;

            QuestObjectiveCompletedTitleTB.Text = _data.QuestObjectiveCompletedTitle;
            QuestObjectiveCompletedTextTB.Text = _data.QuestObjectiveCompletedText;

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

        private void QuestAcceptedTitleTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.QuestAcceptedTitle = QuestAcceptedTitleTB.Text;
        }
        private void QuestAcceptedTextTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.QuestAcceptedText = QuestAcceptedTextTB.Text;
        }
        private void QuestCompletedTitleTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.QuestCompletedTitle = QuestCompletedTitleTB.Text;
        }
        private void QuestCompletedTextTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.QuestCompletedText = QuestCompletedTextTB.Text;
        }
        private void QuestFailedTitleTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.QuestFailedTitle = QuestFailedTitleTB.Text;
        }
        private void QuestFailedTextTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.QuestFailedText = QuestFailedTextTB.Text;
        }
        private void QuestCanceledTitleTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.QuestCanceledTitle = QuestCanceledTitleTB.Text;
        }
        private void QuestCanceledTextTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.QuestCanceledText = QuestCanceledTextTB.Text;
        }
        private void QuestTurnInTitleTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.QuestTurnInTitle = QuestTurnInTitleTB.Text;
        }
        private void QuestTurnInTextTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.QuestTurnInText = QuestTurnInTextTB.Text;
        }
        private void QuestObjectiveCompletedTitleTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.QuestObjectiveCompletedTitle = QuestObjectiveCompletedTitleTB.Text;
        }
        private void QuestObjectiveCompletedTextTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.QuestObjectiveCompletedText = QuestObjectiveCompletedTextTB.Text;
        }
    }
}