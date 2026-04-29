using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    public partial class ExpansionQuestObjectiveActionConfigControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestObjectiveActionConfig _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestObjectiveActionConfigControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as ExpansionQuestObjectiveActionConfig ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            ExecutionAmountNUD.Value = (int)_data.ExecutionAmount;

            _suppressEvents = false;
        }

        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }

        private void ExecutionAmountNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.ExecutionAmount = (int)ExecutionAmountNUD.Value;
        }
    }
}