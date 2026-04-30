using CoreUI.Forms;
using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionQuestQuestAdvancedControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestQuest _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestQuestAdvancedControl()
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

            QuestCancelQuestOnPlayerDeathCB.Checked = _data.CancelQuestOnPlayerDeath == 1 ? true : false;
            QuestIsGroupQuestCB.Checked = _data.IsGroupQuest == 1 ? true : false;
            QuestObjectSetFileNameTB.Text = _data.ObjectSetFileName;
            QuestIsAchievementCB.Checked = _data.IsAchievement == 1 ? true : false;
            QuestSuppressQuestLogOnCompetionCB.Checked = _data.SuppressQuestLogOnCompetion == 1 ? true : false;
            Color selectedColor = Color.FromArgb((int)_data.QuestColor);
            QuestColourPB.BackColor = selectedColor;
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

        private void QuestCancelQuestOnPlayerDeathCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.CancelQuestOnPlayerDeath = QuestCancelQuestOnPlayerDeathCB.Checked == true ? 1 : 0;
        }

        private void QuestIsGroupQuestCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.IsGroupQuest = QuestIsGroupQuestCB.Checked == true ? 1 : 0;
        }

        private void QuestObjectSetFileNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ObjectSetFileName = QuestObjectSetFileNameTB.Text;
        }

        private void QuestIsAchievementCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.IsAchievement = QuestIsAchievementCB.Checked == true ? 1 : 0;
        }

        private void QuestColour_Click(object sender, EventArgs e)
        {
            Color startColor = QuestColourPB.BackColor;
            using (AdvancedColorPickerForm picker = new AdvancedColorPickerForm(startColor))
            {
                picker.StartPosition = FormStartPosition.CenterParent;
                if (picker.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColor = picker.SelectedColor;
                    QuestColourPB.BackColor = selectedColor;
                    _data.QuestColor = selectedColor.ToArgb();
                }
            }
        }

        private void QuestSuppressQuestLogOnCompetionCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SuppressQuestLogOnCompetion = QuestSuppressQuestLogOnCompetionCB.Checked == true ? 1 : 0;
        }

    }
}