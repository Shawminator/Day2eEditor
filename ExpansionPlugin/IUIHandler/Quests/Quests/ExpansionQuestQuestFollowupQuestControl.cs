using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionQuestQuestFollowupQuestControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private QuestReferenceNode _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestQuestFollowupQuestControl()
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
            _data = data as QuestReferenceNode ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            List<ExpansionQuestQuest> questlist = AppServices.GetRequired<ExpansionManager>().ExpansionQuestQuestConfig.MutableItems.ToList();
            List<QuestReferenceNode> questReferenceNodes = questlist
                .Where(q => q.ID.HasValue)
                .OrderBy(q => q.ID.Value)
                .Select(q => new QuestReferenceNode
                {
                    QuestID = q.ID.Value
                })
                .ToList();

            QuestsCB.DataSource = questReferenceNodes;
            QuestsCB.DisplayMember = nameof(QuestReferenceNode.DisplayText);
            QuestsCB.ValueMember = nameof(QuestReferenceNode.QuestID);

            QuestsCB.SelectedValue = _data.QuestID;

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
                _nodes.Last().Text = _data.DisplayText;
            }
        }

        #endregion

        private void QuestsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.QuestID = (int)QuestsCB.SelectedValue;
            UpdateTreeNodeText();
        }
    }
}