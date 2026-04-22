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
    public partial class SpawnGearsimpleChildrenUseDefaultAttributesControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private IHassimpleChildrenUseDefaultAttributes _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public SpawnGearsimpleChildrenUseDefaultAttributesControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as IHassimpleChildrenUseDefaultAttributes ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            simpleChildrenUseDefaultAttributesCB.Checked = _data.SimpleChildrenUseDefaultAttributes;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() != true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }
        private void simpleChildrenUseDefaultAttributesCB_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SimpleChildrenUseDefaultAttributes = simpleChildrenUseDefaultAttributesCB.Checked;    
        }
    }
}