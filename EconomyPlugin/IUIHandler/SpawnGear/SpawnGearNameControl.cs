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
    public partial class SpawnGearNameControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private IHasSpawnName _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public SpawnGearNameControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as IHasSpawnName ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            SpawnGearNameTB.Text = _data.Name;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = $"Name: {_data.Name}";
            }
        }
        private void SpawnGearNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Name = SpawnGearNameTB.Text;
            UpdateTreeNodeText();
        }
    }
}