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
    public partial class ExpansionQuestSettingsAchievementsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestSettingsAchievementsControl()
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

            AchievementCompletedTitleTB.Text = _data.AchievementCompletedTitle;
            AchievementCompletedTextTB.Text = _data.AchievementCompletedText;

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

        private void AchievementCompletedTitleTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.AchievementCompletedTitle = AchievementCompletedTitleTB.Text;
        }

        private void AchievementCompletedTextTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.AchievementCompletedText = AchievementCompletedTextTB.Text;
        }
    }
}