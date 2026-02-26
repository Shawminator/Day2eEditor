using Day2eEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ExpansionPlugin
{
    /// <summary>
    /// Template for a UI Control implementing IUIHandler
    /// TODO: Replace 'ClassType' with your actual data type
    /// </summary>
    public partial class ExpansionP2PMarketSettingsCatControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionP2PMarketMenuCategoryBase _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionP2PMarketSettingsCatControl()
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
            _data = data as ExpansionP2PMarketMenuCategoryBase ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            DisplayNameTB.Text = _data.DisplayName;
            IconPathTB.Text = _data.IconPath;

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
                _nodes.Last().Text = _data.DisplayName;
            }
        }
        #endregion

        private void DisplayNameTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SetDisplayName(DisplayNameTB.Text);
            UpdateTreeNodeText();
        }
        private void IconPathTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _data.SetIconPath(IconPathTB.Text);
        }
    }
}