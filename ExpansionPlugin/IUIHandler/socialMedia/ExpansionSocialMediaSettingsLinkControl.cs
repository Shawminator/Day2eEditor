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
    public partial class ExpansionSocialMediaSettingsLinkControl : UserControl, IUIHandler
    {
        private Type _parentType;
        private ExpansionNewsFeedLinkSetting _data;
        private List<TreeNode> _nodes;
        private bool _suppressEvents;

        public ExpansionSocialMediaSettingsLinkControl()
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
            _data = data as ExpansionNewsFeedLinkSetting ?? throw new InvalidCastException();
            _nodes = selectedNodes;

            _suppressEvents = true;

            ExpansionNewsFeedLinkSettingLabelTB.Text = _data.m_Label;
            ExpansionNewsFeedLinkSettingIconTB.Text = _data.m_Icon;
            ExpansionNewsFeedLinkSettingURLTB.Text = _data.m_URL;

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
                // TODO: Update _nodes.Last().Text based on _data
            }
        }

        #endregion

        private void ExpansionNewsFeedLinkSettingLabelTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.m_Label = ExpansionNewsFeedLinkSettingLabelTB.Text;
        }

        private void ExpansionNewsFeedLinkSettingIconTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.m_Icon = ExpansionNewsFeedLinkSettingIconTB.Text;
        }

        private void ExpansionNewsFeedLinkSettingURLTB_TextChanged(object sender, EventArgs e)
        {
            if (_suppressEvents) { return; }
            _data.m_URL = ExpansionNewsFeedLinkSettingURLTB.Text;
        }
    }
}