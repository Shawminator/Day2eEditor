using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    public partial class ExpansionQuestSettingsIndicatorsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionQuestSettings _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionQuestSettingsIndicatorsControl()
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

            CreateQuestNPCMarkersCB.Checked = _data.CreateQuestNPCMarkers == 1 ? true : false;
            UseQuestNPCIndicatorsCB.Checked = _data.UseQuestNPCIndicators == 1 ? true : false;

            _suppressEvents = false;
        }
        private void CreateQuestNPCMarkersCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.CreateQuestNPCMarkers = CreateQuestNPCMarkersCB.Checked ? 1 : 0;
        }
        private void UseQuestNPCIndicatorsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.UseQuestNPCIndicators = UseQuestNPCIndicatorsCB.Checked ? 1 : 0;
        }
    }
}