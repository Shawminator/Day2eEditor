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
    public partial class cfgweatherresetControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private weather _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgweatherresetControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as weather ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            checkBox1.Checked = _data.reset == 1 ? true:false;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = $"Reset: {(_data.reset != 0)}";
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.reset = checkBox1.Checked == true ? 1 : 0;
            UpdateTreeNodeText();
        }
    }
}