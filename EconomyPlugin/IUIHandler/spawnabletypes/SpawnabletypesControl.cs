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
    public partial class SpawnabletypesControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private SpawnableType _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public SpawnabletypesControl()
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
            _data = data as SpawnableType ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            SpawnableTypeTB.Text = _data.name;

            _suppressEvents = false;
        }
        private void UpdateTreeNodeText()
        {
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = _data.name;
            }
        }
        private void darkButton30_Click(object sender, EventArgs e)
        {
            AddItemfromTypes form = new AddItemfromTypes
            {
                UseOnlySingleItem = true
            };
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                List<string> addedtypes = form.AddedTypes.ToList();
                foreach (string l in addedtypes)
                {
                    SpawnableTypeTB.Text = _data.name = l;
                    UpdateTreeNodeText();
                }
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }
    }
}