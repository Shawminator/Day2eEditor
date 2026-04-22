using Day2eEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class SpawnGearQuickBarSlotControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private IHasQuikBarSlot _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public SpawnGearQuickBarSlotControl()
        {
            InitializeComponent();
        }

        public Control GetControl() => this;

        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as IHasQuikBarSlot ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            quickBarSlotNUD.Value = _data.QuickBarSlot;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }
        private void quickBarSlotNUD_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.QuickBarSlot = (int)quickBarSlotNUD.Value;
        }
    }
}