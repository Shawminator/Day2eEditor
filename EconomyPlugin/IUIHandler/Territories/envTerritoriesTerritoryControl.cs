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
    public partial class envTerritoriesTerritoryControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private envTerritoriesTerritory _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public envTerritoriesTerritoryControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as envTerritoriesTerritory ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            nameTB.Text = _data.name;
            TypeTB.Text = _data.type;
            BehaviorTB.Text = _data.behavior;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = _data.name;
            }
        }
        private void nameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.name = nameTB.Text;
            UpdateTreeNodeText();
        }
        private void TypeTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.type = TypeTB.Text;
        }
        private void BehaviorTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.behavior = BehaviorTB.Text;
        }
    }
}