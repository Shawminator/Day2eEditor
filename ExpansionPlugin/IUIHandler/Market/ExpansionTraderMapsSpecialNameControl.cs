using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionTraderMapsSpecialNameControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private TraderNPCSpecialProperties _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionTraderMapsSpecialNameControl()
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
            _data = data as TraderNPCSpecialProperties ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            FilenameTB.Text = _data?.Name ?? string.Empty;

            _suppressEvents = false;
        }

        #region Helper Methods
        /// <summary>
        /// Updates the TreeNode text based on current data
        /// </summary>
        private void UpdateTreeNodeText()
        {
            string name = _data.Name;
            if (_nodes?.Any() == true)
            {
                _nodes.Last().Text = string.IsNullOrEmpty(_data.Name) ? "Name:" : $"Name: {name}";
            }
        }

        #endregion

        private void FilenameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.Name = FilenameTB.Text;
            if (_data.Name == "")
                _data.Name = null;
            UpdateTreeNodeText();
        }
    }
}