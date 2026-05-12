using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    public partial class ExpansionMarketTraderNpcsControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionMarketTraderNpcs _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionMarketTraderNpcsControl()
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
            _data = data as ExpansionMarketTraderNpcs ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            textBox2.Text = Path.GetFileNameWithoutExtension(_data.FileName);

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = _data.FileName;
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            string dirName = Path.GetDirectoryName(_data._path);
            string newFilename = textBox2.Text + ".map";
            _data.SetPath(Path.Combine(dirName, newFilename));
            UpdateTreeNodeText();
        }
    }
}