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
    public partial class SpawnGearSpawnWeightControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private IHasSpawnWeight _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public SpawnGearSpawnWeightControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as IHasSpawnWeight ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            spawnWeightNUD.Value = _data.SpawnWeight;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = $"Spawn Weight: {_data.SpawnWeight}";
            }
        }
        private void spawnWeightNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SpawnWeight = (int)spawnWeightNUD.Value;
            UpdateTreeNodeText();
        }

    }
}