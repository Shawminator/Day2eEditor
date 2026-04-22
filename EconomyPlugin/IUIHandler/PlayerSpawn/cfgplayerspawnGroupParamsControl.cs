using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class cfgplayerspawnGroupParamsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private playerspawnpointsGroup_params _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgplayerspawnGroupParamsControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as playerspawnpointsGroup_params ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            GroupParamsenablegroupsCB.Checked = _data.enablegroups;
            GroupParamgroups_as_regularCB.Checked = _data.groups_as_regular;
            GroupParamslifetimeNUD.Value = _data.lifetime;
            GroupParamscounterNUD.Value = _data.counter;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }
        private void GroupParamsenablegroupsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.enablegroups = GroupParamsenablegroupsCB.Checked;
        }
        private void GroupParamgroups_as_regularCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.groups_as_regular = GroupParamgroups_as_regularCB.Checked;
        }
        private void GroupParamslifetimeNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.lifetime = (int)GroupParamslifetimeNUD.Value;
        }
        private void GroupParamscounterNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.counter = (int)GroupParamscounterNUD.Value;
        }
    }
}