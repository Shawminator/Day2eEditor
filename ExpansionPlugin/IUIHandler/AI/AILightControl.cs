using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    public partial class AILightControl : UserControl, IUIHandler
    {
        private Type _parentType;
        public Control GetControl() => this;
        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as AILightEntries ?? throw new InvalidCastException();
            _nodes = selectedNodes;
            _suppressEvents = true;

            variablesvarvalueNUD.Value = _data.Value;

            _suppressEvents = false;
        }
        private AILightEntries _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public AILightControl()
        {
            InitializeComponent();
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes[0] != null)
                _nodes[0].Text = $"Lighting Config {_data.Key} : Visibility {_data.Value}";
        }
        private void variablesvarvalueNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Value = variablesvarvalueNUD.Value;
            UpdateTreeNodeText();
        }
    }
}
