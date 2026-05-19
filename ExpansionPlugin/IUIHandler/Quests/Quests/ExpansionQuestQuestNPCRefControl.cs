using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    public partial class ExpansionQuestQuestNPCRefControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private QuestNPCReferenceNode _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestQuestNPCRefControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as QuestNPCReferenceNode ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            List<ExpansionQuestNPCData> questlist = AppServices.GetRequired<ExpansionManager>().ExpansionQuestNPCDataConfig.MutableItems.ToList();
            List<QuestNPCReferenceNode> questReferenceNodes = questlist
                .Where(q => q.ID.HasValue)
                .OrderBy(q => q.ID.Value)
                .Select(q => new QuestNPCReferenceNode
                {
                    NPCID = q.ID.Value
                })
                .ToList();

            QuestsCB.DataSource = questReferenceNodes;
            QuestsCB.DisplayMember = nameof(QuestNPCReferenceNode.DisplayText);
            QuestsCB.ValueMember = nameof(QuestNPCReferenceNode.NPCID);

            QuestsCB.SelectedValue = _data.NPCID;

            _suppressEvents = false;
        }

        #region Helper Methods
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
            _data.NPCID = (int)QuestsCB.SelectedValue;
            UpdateTreeNodeText();
        }
    }
}