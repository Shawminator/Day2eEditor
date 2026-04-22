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
    public partial class cfgeffectAreaMainControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private Areas _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public cfgeffectAreaMainControl()
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
            this.DoubleBuffered = true;
            _parentType = parentType;
            _data = data as Areas ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            AreaNameTB.Text = _data.AreaName;
            TypeTB.Text = _data.Type;
            TriggerTypeTB.Text = _data.TriggerType;

            _suppressEvents = false;
        }

        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = _data.AreaName;
            }
        }


        private void AreaNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.AreaName = AreaNameTB.Text;
            UpdateTreeNodeText();
        }

        private void TypeTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Type = TypeTB.Text;
        }

        private void TriggerTypeTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.TriggerType = TriggerTypeTB.Text;
        }
    }
}