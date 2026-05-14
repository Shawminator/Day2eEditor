using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    public partial class ExpansionQuestSettingsGeneralControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestSettingsGeneralControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as ExpansionQuestSettings ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            EnableQuestsCB.Checked = _data.EnableQuests == 1 ? true : false;
            EnableQuestLogTabCB.Checked = _data.EnableQuestLogTab == 1 ? true : false;
            MaxActiveQuestsNUD.Value = (int)_data.MaxActiveQuests;
            GroupQuestModeCB.SelectedIndex = (int)_data.GroupQuestMode;
            _suppressEvents = false;
        }
        private void EnableQuestsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.EnableQuests = EnableQuestsCB.Checked ? 1 : 0;
        }
        private void EnableQuestLogTabCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.EnableQuestLogTab = EnableQuestLogTabCB.Checked ? 1 : 0;
        }
        private void MaxActiveQuestsNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.MaxActiveQuests = (int)MaxActiveQuestsNUD.Value;
        }
        private void GroupQuestModeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.GroupQuestMode = GroupQuestModeCB.SelectedIndex;
        }
    }
}