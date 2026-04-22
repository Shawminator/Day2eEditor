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
    public partial class cfgeffectAreaPlayerDataControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private PlayerData _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgeffectAreaPlayerDataControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Returns the UserControl instance
        /// </summary>
        public Control GetControl() => this;

        /// <summary>
        /// Loads data into the control and stores the selected tree nodes
        /// </summary>
        public void LoadFromData(Type parentType, object data, List<TreeNode> selectedNodes)
        {
            _parentType = parentType;
            _data = data as PlayerData ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            AroundPartNameTB.Text = _data.AroundPartName;
            TinyPartNameTB.Text = _data.AroundPartName;
            PPERequesterTypeTB.Text = _data.PPERequesterType;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                // TODO: Update _nodes.Last().Text based on _data
            }
        }
        private void AroundPartNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AroundPartName = AroundPartNameTB.Text;
        }
        private void TinyPartNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.TinyPartName = TinyPartNameTB.Text;
        }
        private void PPERequesterTypeTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.PPERequesterType = PPERequesterTypeTB.Text;
        }
    }
}