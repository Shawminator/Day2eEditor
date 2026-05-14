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
    public partial class ExpansionQuestSettingsGroupMessagesControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestSettingsGroupMessagesControl()
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

            QuestNotInGroupTitleTB.Text = _data.QuestNotInGroupTitle;
            QuestNotInGroupTextTB.Text = _data.QuestNotInGroupText;

            QuestNotGroupOwnerTitleTB.Text = _data.QuestNotGroupOwnerTitle;
            QuestNotGroupOwnerTextTB.Text = _data.QuestNotGroupOwnerText;

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

        private void QuestNotInGroupTitleTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.QuestNotInGroupTitle = QuestNotInGroupTitleTB.Text;
        }
        private void QuestNotInGroupTextTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.QuestNotInGroupText = QuestNotInGroupTextTB.Text;
        }
        private void QuestNotGroupOwnerTitleTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.QuestNotGroupOwnerTitle = QuestNotGroupOwnerTitleTB.Text;
        }
        private void QuestNotGroupOwnerTextTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.QuestNotGroupOwnerText = QuestNotGroupOwnerTextTB.Text;
        }
    }
}