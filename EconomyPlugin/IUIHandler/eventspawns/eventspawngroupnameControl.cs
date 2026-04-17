using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EconomyPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class eventspawngroupnameControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private eventgroupdefGroup _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public eventspawngroupnameControl()
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
            _data = data as eventgroupdefGroup ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            textBox1.Text = _data.name;

            _suppressEvents = false;
        }


        #region Helper Methods


        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = $"Group Name : {_data.name}";
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            _data.name = textBox1.Text;

            eventposdefEventPos spawnpos = _nodes.Last().Parent.Tag as eventposdefEventPos;
            spawnpos.group = _data.name;
            UpdateTreeNodeText();
        }
    }
}